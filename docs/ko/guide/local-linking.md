# 로컬 연결

## 패밀리 레이아웃 규칙

```
lh-ui/                        # 패밀리 상위 디렉터리(저장소 아님)
├── lh-ui-core/               # 토큰 원본 + codegen
├── lh-ui-android/            # 형제 저장소
├── lh-ui-ios/
├── lh-ui-harmony/
└── lh-ui-windows/            # 이 저장소
```

각 저장소가 동일한 상위 디렉터리에 있으면 토큰을 **로컬 직접 연결**합니다. 형제 저장소가 없는 경우(CI 단일 저장소 clone) **자동으로 npm 패키지로 대체**합니다.
이 구조는 Web 혈통의 vite alias 가드 메커니즘과 동일합니다(마스터 플랜 D18).

## 3단계 가드

`scripts/link-tokens.mjs`가 우선순위에 따라 토큰 산출물을 찾습니다:

| 우선순위 | 출처 | 설명 |
| --- | --- | --- |
| 1 | `../lh-ui-core/dist/windows` | 로컬 직접 연결: 토큰을 변경하면 즉시 반영 |
| 2 | `node_modules/@lh-ui/core/dist/windows` | npm 대체: `@lh-ui/core`의 `files:["dist"]`에 각 엔드 산출물 포함 |
| 3 | — | 오류로 종료, 토큰이 없으면 빌드 불가 |

토큰 저장 위치는 `src/Tokens/`입니다(codegen의 `relativePath`가 `Tokens/*.cs`). 컴포넌트 측에서는
`using Top.Lhgb.Ui.Tokens;`가 이곳을 가리킵니다. 이 디렉터리는 생성물(`.gitignore`)이므로 수동 수정을 금지합니다.

## 일상 흐름

```bash
# 1. 토큰 수정(core 저장소에서)
cd ../lh-ui-core
vim tokens/color.json           # 또는 spacing / typography / ...

# 2. 재생성 + 검증
pnpm codegen                    # dist/의 각 엔드 산출물 생성
pnpm verify                     # schema + 참조 + WCAG 대비 + 계약 일관성

# 3. 이 저장소로 돌아와 동기화
cd ../lh-ui-windows
pnpm link:tokens                # src/Tokens/ 덮어쓰기

# 4. 회귀(dotnet)
dotnet test                     # xUnit 골든 케이스
dotnet build                    # 컴파일이 곧 타입 검사
```

## 문서 사이트 토큰

문서 사이트의 시뮬레이터 CSS 변수도 토큰에서 생성됩니다:

```bash
pnpm gen:sim                    # docs/.vitepress/theme/sim-tokens.css 생성
pnpm docs:dev                   # 내부적으로 gen:sim을 먼저 실행, 포트 5186
pnpm sim:check                  # sim CSS에 토큰 리터럴이 없는지 검증(금지 사항)
```

## CI 게이트

```bash
pnpm link:tokens:check          # 토큰 동기화 여부 감지(파일 수 비교, 미동기화 시 종료 코드 1)
```

## 금지 사항

- ❌ `src/Tokens/` 수동 수정(생성물, gitignore)
- ❌ 생성된 토큰 파일을 저장소에 커밋
- ❌ 컴포넌트에 색상 / 간격 리터럴 작성(예: `Color.FromArgb(0xFF,0x2B,0x4C,0x7C)`, `new GridLength(40)`, 폰트 크기 숫자)
- ❌ `link:tokens`를 우회해 산출물을 수동으로 복사

## 문제 해결

| 증상 | 원인 | 해결 |
| --- | --- | --- |
| 컴파일 오류 `The type or namespace 'LhTokens' does not exist` | 토큰 미동기화 | `pnpm link:tokens` |
| 토큰 값이 예상과 다름 | core 저장소에서 codegen 재실행 안 함 | core 저장소에서 `pnpm codegen` 후 다시 link |
| `link:tokens`가 「토큰 산출물을 찾을 수 없음」 보고 | 형제 저장소가 없고 npm 패키지도 설치 안 됨 | 패밀리 레이아웃 확인 또는 `pnpm i -D @lh-ui/core` |
| 다크 색상이 이상함 | 라이트 메인 컬러만 바꾸고 다크 사전 계산은 업데이트 안 함 | `color-dark.json` 업데이트 후 재실행(R-n6) |
| 컴포넌트 색상이 모두 라이트 | 진입점에서 `LhTheme.Apply` 미호출 | App / 메인 윈도우 시작 시 한 번 호출 |
| `dotnet build`가 `MC1000` 등 XAML 오류 보고 | 컴포넌트는 모두 코드로 시각 트리를 구축하며 저장소 내에 XAML 없음 | 실수로 `.xaml` 파일을 추가했는지 확인 |
