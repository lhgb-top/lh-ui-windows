/**
 * 把 package.json 的 version 单向同步到 C# 工程的版本真源，幂等。
 *
 * 家族约定：changeset version 只 bump package.json，平台版本真源由本脚本
 * 跟随同步。windows 端的真源是 src/LhUi.csproj 的 <Version>（NuGet 打包
 * 与 git tag 都以它为准）。
 *
 * 由 `pnpm changeset:version` 自动调用（见 package.json），也可单独执行：
 *   node scripts/sync-version.mjs          同步
 *   node scripts/sync-version.mjs --check  只校验一致性，不同步（CI 门禁用）
 *
 * 注意：令牌目录 src/Tokens/ 是 codegen 生成物，本脚本不触碰它。
 */
import fs from 'node:fs'
import path from 'node:path'
import { fileURLToPath } from 'node:url'

const __dirname = path.dirname(fileURLToPath(import.meta.url))
const root = path.resolve(__dirname, '..')
const pkgPath = path.resolve(root, 'package.json')
const csprojPath = path.resolve(root, 'src', 'LhUi.csproj')

const checkOnly = process.argv.includes('--check')

const pkg = JSON.parse(fs.readFileSync(pkgPath, 'utf8'))
const { version } = pkg

if (!version) {
  console.error('package.json 缺少 version 字段')
  process.exit(1)
}

if (!fs.existsSync(csprojPath)) {
  console.error(`未找到 ${csprojPath}`)
  process.exit(1)
}

const content = fs.readFileSync(csprojPath, 'utf8')

// 匹配 PropertyGroup 内的 <Version>x.y.z</Version>（忽略大小写与自闭合写法）
const versionTagRe = /<Version>([^<]*)<\/Version>/i

const match = content.match(versionTagRe)
if (!match) {
  console.error(`${csprojPath} 中未找到 <Version> 标签，请先手工补上初值`)
  process.exit(1)
}

const current = match[1].trim()

if (current === version) {
  console.log(`✅ 版本一致：${version}（src/LhUi.csproj）`)
  process.exit(0)
}

if (checkOnly) {
  console.error(`❌ 版本不一致：package.json=${version}，src/LhUi.csproj=${current}`)
  process.exit(1)
}

const updated = content.replace(versionTagRe, `<Version>${version}</Version>`)
fs.writeFileSync(csprojPath, updated, 'utf8')

console.log(`✅ 已同步版本 ${current} → ${version}（src/LhUi.csproj）`)
