# 설치

## 환경 요구 사항

- .NET 8 SDK(`net8.0-windows10.0.19041.0` 타깃 프레임워크)
- Windows App SDK 1.6+(Windows 10 1809+ 또는 Windows 11)
- 빌드 시간 도구 체인: Node.js >= 20 + pnpm(토큰 연결과 문서 사이트에만 사용, **런타임에 포함되지 않음**)

## NuGet 도입(nM6 패키지 배포 체인 개통 후)

```bash
dotnet add package LhUi.Windows
```

```xml
<ItemGroup>
  <PackageReference Include="LhUi.Windows" Version="0.0.1" />
</ItemGroup>
```

## 소스 코드 통합(현재 단계)

패밀리 레이아웃에서 이 저장소의 클래스 라이브러리를 직접 참조합니다(솔루션은 `src/LhUi.csproj` 클래스 라이브러리 + `tests/` 테스트 + `demo/` 데모 앱으로 구성):

```
lh-ui-windows/
├── src/                  # LhUi 클래스 라이브러리(Components/ + Common/ + Tokens/)
├── tests/                # xUnit 골든 케이스
├── demo/                 # 데모 앱(전체 형태 표시 + 라이트/다크 전환)
└── docs/                 # VitePress 문서 사이트
```

## 토큰 동기화

토큰은 생성물이며 `lh-ui-core`에서 동기화해야 합니다(유일한 원본, 수동 수정 금지):

```bash
cd ../lh-ui-core && pnpm codegen   # dist/windows 토큰 산출물 생성
cd ../lh-ui-windows
pnpm install                        # 빌드 시간 도구 체인 설치
pnpm link:tokens                    # src/Tokens/로 토큰 동기화
```

`pnpm link:tokens`는 3단계 존재성 가드를 갖추고 있습니다: 로컬 `../lh-ui-core/dist/windows` → npm `@lh-ui/core` 산출물 → 오류.

## 설치 확인

```bash
dotnet build     # LhUi / LhUi.Tests / LhUi.Demo 컴파일(C# 타입 검사 포함)
dotnet test      # xUnit 골든 케이스(15건, Resolve 순수 함수의 외관 해석을 어서션)
```
