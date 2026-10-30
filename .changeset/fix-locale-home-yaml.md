---
'@lh-ui/windows': patch
---

修复文档站构建失败：`docs/es/index.md`、`docs/fr/index.md` 首页 frontmatter 的 `details` 明文标量包含「冒号+空格」（如 `espaciado: todos`、`composants : tout`），YAML 解析报 `incomplete explicit mapping pair`；给这两处值加上双引号（与 ios / android 语言的同类修复一致）。
