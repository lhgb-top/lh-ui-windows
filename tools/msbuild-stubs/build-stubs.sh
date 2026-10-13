#!/usr/bin/env bash
# 编译两个 MSBuild 任务占位程序集到 tools/msbuild-stubs/lib/。
# 官方任务程序集缺席时的回退（见各仓 csproj 的存在性守卫），随仓库走、可重建。
set -e
ROOT="$(cd "$(dirname "$0")" && pwd)"
dotnet build "$ROOT/src/AppxPackageStub/AppxPackageStub.csproj" -c Release -v q --nologo -o "$ROOT/lib"
dotnet build "$ROOT/src/PriTasksStub/PriTasksStub.csproj" -c Release -v q --nologo -o "$ROOT/lib"
echo "✅ 占位任务程序集已输出到 $ROOT/lib/"
