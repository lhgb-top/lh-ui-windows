// gen-doc-pages.mjs —— md → 原生文档页（C#）
//
// 数据驱动：新增/改文档只重跑本脚本（pnpm gen:docs）。生成的 pages/*.cs 与
// DocRegistry.cs 禁手改（漂移由脚本兜底）；真交互预览手写在 src/Docs/previews/，
// 本脚本按 section 标题经 SCENE_MAP 映射引用，命名固定为 {Comp}{Scene}(scope)。
//
// 输入：docs/components/{slug}.md + docs/guide/{slug}.md + docs/.vitepress/config.ts（分组顺序真源）
// 输出：src/Docs/pages/{Pascal}Doc.cs（组件）/ Guide{Pascal}Doc.cs（指南）+ src/Docs/DocRegistry.cs
//
// md 解析约定（与 lh-ui-android 的 gen-doc-pages.py 对齐）：
//   首行 `# ` 为 title；其后散文为 lede；连续 `> ` 合并为 contract
//   `## ` 分节，`### ` 为 API 子标题（仅记录 Props 表标题用）
//   `|` 表格逐行解析，含 `:-:` 的分割行跳过
//   ```bash 代码块进 commands，其余代码块进当前 section 的 code
//   `<div>` / `<p>` / `<details>` 整块跳过（网页模拟器 HTML 不进原生）

import { readFileSync, writeFileSync, readdirSync, existsSync, mkdirSync } from 'node:fs'
import { resolve, dirname } from 'node:path'
import { fileURLToPath } from 'node:url'

const ROOT = resolve(dirname(fileURLToPath(import.meta.url)), '..')
const DOCS = resolve(ROOT, 'docs')
const OUT_PAGES = resolve(ROOT, 'src/Docs/pages')
const OUT_REGISTRY = resolve(ROOT, 'src/Docs/DocRegistry.cs')

// section 标题 → 预览方法后缀（手写预览须命名 {Comp}{Suffix}，签名为 (DocPreviewScope) => UIElement）
const SCENE_MAP = {
  类型: 'Types',
  尺寸: 'Sizes',
  状态: 'States',
  图标与花纹底: 'IconAndPattern',
  明暗主题: 'Theme',
  色调: 'Tone',
  图标名: 'IconNames',
  描边与圆角: 'BorderAndRadius',
  尺寸与关闭: 'SizesAndClose',
  基础用法: 'Basic',
  图标与必填: 'IconAndRequired',
  分隔线与点击: 'DividerAndClick',
  级别: 'Levels',
  字重与色调: 'WeightAndTone',
  对齐: 'Alignment',
  类型与状态: 'TypesAndStates',
  清除与字数: 'ClearAndCount',
  字数统计: 'Count',
  索引钳制: 'IndexClamp',
  语义类型: 'SemanticTypes',
  定位: 'Position',
  图标: 'Icons',
  分隔线: 'Divider',
}

const TERMINAL_SECTIONS = ['API', '令牌对照', '黄金用例对照']

const GROUP_TO_ENUM = {
  基础组件: 'Basic',
  表单组件: 'Form',
  反馈组件: 'Feedback',
  导航组件: 'Navigation',
}

function pascal(slug) {
  return slug.split(/[-_]/).map((s) => s.charAt(0).toUpperCase() + s.slice(1)).join('')
}

// 转义进 C# **常规**字符串字面量（非 @verbatim）：反斜杠转义反斜杠，
// 双引号转义为 \"。注意 "" 双写只在 verbatim 字符串（@"..."）里才是转义，
// 在常规字面量里 "" 会被切成两个相邻字面量，数组初始化器里直接 CS1003。
function esc(s) {
  return s.replace(/\\/g, '\\\\').replace(/"/g, '\\"')
}

function quoteLines(lines) {
  if (lines.length === 0) return 'string.Empty'
  return lines.map((l) => `                "${esc(l)}"`).join(',\n')
}

// ---------- config.ts 分组顺序解析（侧边栏顺序即首页顺序）----------
// 按括号深度切出整个数组字面量（config.ts 里的字符串不含方括号，深度计数安全）
function sliceArrayLiteral(src, startKeyword) {
  const kw = src.indexOf(startKeyword)
  const br = src.indexOf('[', kw)
  let depth = 0
  for (let i = br; i < src.length; i++) {
    if (src[i] === '[') depth++
    else if (src[i] === ']') {
      depth--
      if (depth === 0) return src.slice(br, i + 1)
    }
  }
  return src.slice(br)
}

function parseConfig() {
  const src = readFileSync(resolve(DOCS, '.vitepress/config.ts'), 'utf-8')
  const out = { guides: [], components: [] }

  const guideSeg = sliceArrayLiteral(src, 'const guideGroups =')
  const compSeg = sliceArrayLiteral(src, 'const componentGroups =')

  const pairRe = /text:\s*'([^']+)'[^}]*?slug:\s*'([^']+)'/g
  let m
  while ((m = pairRe.exec(guideSeg))) out.guides.push({ text: m[1], slug: m[2] })

  const groupRe = /text:\s*'([^']+)'\s*,\s*items:\s*\[([\s\S]*?)\]/g
  while ((m = groupRe.exec(compSeg))) {
    const items = []
    const itemRe = /text:\s*'([^']+)'[^}]*?slug:\s*'([^']+)'/g
    let im
    while ((im = itemRe.exec(m[2]))) items.push({ text: im[1], slug: im[2] })
    out.components.push({ group: m[1], items })
  }
  return out
}

// ---------- md 解析 ----------
function parseTable(lines) {
  const rows = lines
    .filter((l) => l.trim().startsWith('|'))
    .filter((l) => !/^\|[\s:|-]+\|$/.test(l.trim()))
    .map((l) =>
      l
        .trim()
        .replace(/^\|/, '')
        .replace(/\|$/, '')
        .split('|')
        .map((c) => c.trim()),
    )
  if (rows.length === 0) return null
  return { headers: rows[0], rows: rows.slice(1) }
}

function parseMd(path, isGuide) {
  const raw = readFileSync(path, 'utf-8').replace(/^\uFEFF/, '')
  const lines = raw.split(/\r?\n/)
  const entry = { title: '', lede: [], contract: [], sections: [] }

  let i = 0
  while (i < lines.length && !lines[i].startsWith('# ')) i++
  entry.title = lines[i].slice(2).trim()
  i++

  // lede 与 contract（到首个 ## 之前）
  while (i < lines.length && !lines[i].startsWith('## ')) {
    const l = lines[i].trim()
    if (l.startsWith('>')) {
      entry.contract.push(l.replace(/^>\s?/, '').trim())
    } else if (l !== '') {
      entry.lede.push(l)
    }
    i++
  }

  let section = null
  let inDiv = 0
  let inFence = false
  let fenceLang = ''
  let fenceLines = []
  let tableLines = []
  let apiSubTitle = null

  const flushTable = () => {
    if (tableLines.length === 0) return
    const t = parseTable(tableLines)
    tableLines = []
    if (t && section) {
      // API 节内：### 属性 的表为 Props，其余 ### 的表为枚举表（标题沿用子标题名）
      const title = section.terminal === 'API' && apiSubTitle && apiSubTitle !== '属性' ? apiSubTitle : ''
      section.tables.push({ title, ...t })
    }
  }

  while (i < lines.length) {
    const line = lines[i]

    if (inFence) {
      if (line.trim() === '```') {
        inFence = false
        if (fenceLang === 'bash' && section) {
          // 命令块：以代码片段形式挂在当前（黄金用例对照）节
          section.code.push(...fenceLines)
        } else if (section) {
          section.code.push(...fenceLines)
        }
        fenceLines = []
      } else {
        fenceLines.push(line)
      }
      i++
      continue
    }

    if (inDiv > 0) {
      inDiv += (line.match(/<div/g) || []).length - (line.match(/<\/div>/g) || []).length
      i++
      continue
    }

    const trimmed = line.trim()

    if (trimmed.startsWith('```')) {
      inFence = true
      fenceLang = trimmed.slice(3).trim()
      fenceLines = []
      i++
      continue
    }
    if (trimmed.startsWith('<div')) {
      inDiv = (line.match(/<div/g) || []).length - (line.match(/<\/div>/g) || []).length
      i++
      continue
    }
    if (trimmed.startsWith('<p') || trimmed.startsWith('<details')) {
      i++
      continue
    }
    // vitepress 容器（::: tip / ::: warning …）：只剥掉标记行，正文当散文保留
    if (trimmed.startsWith(':::')) {
      i++
      continue
    }

    if (line.startsWith('## ')) {
      flushTable()
      const title = line.slice(3).trim()
      section = {
        title,
        prose: [],
        code: [],
        tables: [],
        preview: null,
        terminal: TERMINAL_SECTIONS.includes(title) ? title : null,
      }
      apiSubTitle = null
      entry.sections.push(section)
      i++
      continue
    }

    if (line.startsWith('### ')) {
      flushTable()
      apiSubTitle = line.slice(4).trim()
      i++
      continue
    }

    if (trimmed.startsWith('|')) {
      tableLines.push(line)
      i++
      continue
    }
    flushTable()

    if (trimmed === '') {
      i++
      continue
    }
    if (section) section.prose.push(trimmed)
    i++
  }
  flushTable()

  // 预览映射：组件页按 SCENE_MAP；指南页不要预览
  if (!isGuide) {
    for (const s of entry.sections) {
      if (s.terminal) continue
      const suffix = SCENE_MAP[s.title]
      if (!suffix) {
        console.warn(`⚠ 未登记场景标题：${path} 的「${s.title}」（该节不生成预览）`)
        continue
      }
      s.preview = suffix
    }
  }
  return entry
}

// ---------- C# 代码生成 ----------
function emitTable(t) {
  const cells = (arr) => arr.map((c) => `"${esc(c)}"`).join(', ')
  const rows = t.rows
    .map((r) => `                        new DocRow(new[] { ${cells(r.length ? r : [''])} }),`)
    .join('\n')
  return `                    new DocTable("${esc(t.title)}", new[] { ${cells(t.headers)} }, new[]
                    {
${rows}
                    })`
}

function emitEntry(entry, className, group, previewClass) {
  const sections = entry.sections.map((s) => {
    const preview = s.preview ? `${previewClass}.${s.preview}` : 'null'
    const proseArg = s.prose.length
      ? `new[]\n                {\n${s.prose.map((l) => `                    "${esc(l)}"`).join(',\n')}\n                }`
      : 'Array.Empty<string>()'
    const code =
      s.code.length > 0
        ? `new[]\n                {\n${s.code.map((l) => `                    "${esc(l)}"`).join(',\n')}\n                }`
        : 'Array.Empty<string>()'
    const tables =
      s.tables.length > 0
        ? `new[]\n                {\n${s.tables.map((t) => emitTable(t)).join(',\n')}\n                }`
        : 'Array.Empty<DocTable>()'
    return `            new DocSection(
                "${esc(s.title)}",
                ${proseArg},
                ${preview},
                ${code},
                ${tables}),`
  })

  return `// 本文件由 scripts/gen-doc-pages.mjs 从 ${entry.source} 生成 —— 禁手改。
// 改文档请改 md 再跑 pnpm gen:docs；预览手写在 src/Docs/previews/${previewClass}.cs。

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Previews;

namespace Top.Lhgb.Ui.Docs.Pages;

internal sealed class ${className}
{
    public static readonly DocEntry Entry = new DocEntry(
        "${esc(entry.id)}",
        "${esc(entry.title)}",
        ${entry.lede.length ? `new[]\n        {\n${quoteLines(entry.lede)}\n        }` : 'Array.Empty<string>()'},
        ${entry.contract.length ? `new[]\n        {\n${quoteLines(entry.contract)}\n        }` : 'Array.Empty<string>()'},
        DocGroup.${group},
        new[]
        {
${sections.join('\n')}
        });
}
`
}

function emitRegistry(registrations) {
  const list = registrations.map((r) => `        ${r.className}.Entry,`).join('\n')
  return `// 本文件由 scripts/gen-doc-pages.mjs 生成 —— 禁手改（分组顺序对齐 docs 侧边栏）。

using System.Collections.Generic;
using Top.Lhgb.Ui.Docs.Model;
using Top.Lhgb.Ui.Docs.Pages;

namespace Top.Lhgb.Ui.Docs;

/// <summary>
/// 文档注册表：数据驱动的页面清单。顺序与 <c>docs/.vitepress/config.ts</c> 的侧边栏一致。
/// </summary>
public static class DocRegistry
{
    public static readonly IReadOnlyList<DocEntry> All = new[]
    {
${list}
    };

    /// <summary>按 id 取条目；首页与详情页的导航都走它</summary>
    public static DocEntry? ById(string id)
    {
        foreach (var entry in All)
        {
            if (entry.Id == id) return entry;
        }
        return null;
    }
}
`
}

// ---------- 主流程 ----------
function main() {
  const cfg = parseConfig()
  if (!existsSync(OUT_PAGES)) mkdirSync(OUT_PAGES, { recursive: true })

  const registrations = []
  let warnings = 0

  // 指南页
  for (const g of cfg.guides) {
    const mdPath = resolve(DOCS, 'guide', `${g.slug}.md`)
    if (!existsSync(mdPath)) {
      console.warn(`⚠ 缺少指南 md：${mdPath}`)
      warnings++
      continue
    }
    const entry = parseMd(mdPath, true)
    entry.id = g.slug
    entry.source = `docs/guide/${g.slug}.md`
    const className = `Guide${pascal(g.slug)}Doc`
    writeFileSync(resolve(OUT_PAGES, `${className}.cs`), emitEntry(entry, className, 'Guide', 'Previews'))
    registrations.push({ className, group: 'Guide' })
  }

  // 组件页
  for (const grp of cfg.components) {
    const group = GROUP_TO_ENUM[grp.group]
    if (!group) {
      console.warn(`⚠ 未知分组「${grp.group}」，请在 GROUP_TO_ENUM 登记`)
      warnings++
    }
    for (const item of grp.items) {
      const mdPath = resolve(DOCS, 'components', `${item.slug}.md`)
      if (!existsSync(mdPath)) {
        console.warn(`⚠ 缺少组件 md：${mdPath}`)
        warnings++
        continue
      }
      const entry = parseMd(mdPath, false)
      entry.id = item.slug
      entry.source = `docs/components/${item.slug}.md`
      const className = `${pascal(item.slug)}Doc`
      const previewClass = `${pascal(item.slug)}Previews`
      writeFileSync(resolve(OUT_PAGES, `${className}.cs`), emitEntry(entry, className, group, previewClass))
      registrations.push({ className, group })
    }
  }

  writeFileSync(OUT_REGISTRY, emitRegistry(registrations))
  console.log(`✅ 生成 ${registrations.length} 个条目 → ${OUT_REGISTRY}`)
  console.log(`   页面目录：${OUT_PAGES}`)
  if (warnings > 0) console.log(`⚠ ${warnings} 处告警，请检查上方输出`)
}

main()
