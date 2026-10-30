# Icon 아이콘

아이콘 컴포넌트입니다. 로컬 그래픽 체계에 따라 아이콘 이름을 글리프로 해석해 렌더링하며, 크기와 시맨틱 색조를 지원합니다.

> 계약 출처: `lh-ui-core/specs/icon.json` ·
> 골든 케이스: `lh-ui-core/cases/icon.json` (5건, xUnit 테스트로 번역됨)

::: tip 플랫폼 메모
Windows 엔드는 현재 unicode 글리프 임시 처리(`LhIconTokens.GlyphFor`, TextBlock 렌더링)를 사용하며, 프로덕션 환경에서는
`FontIcon`(Segoe Fluent Icons 벡터 에셋)으로 교체해야 합니다. 이는 button의 임시 처리 전략과 동일합니다. 등록된 아이콘 이름은 아래의 「아이콘 이름」 섹션을 참조하세요.
:::

## 크기

3단계 크기가 `icon.size` 토큰에 매핑됩니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icon 크기</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--small">⌕</span>
      <span class="sim-icon">⌕</span>
      <span class="sim-icon sim-icon--large">⌕</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var icon = new LhIcon
{
    Name = "search",
    Size = LhIconSize.Large,
};
```

## 색조

`Tone`이 글리프 색상을 결정하며, 시맨틱 색 토큰과 1:1로 대응합니다. 기본 색조는 `color.text.primary`를 사용합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Icon 색조</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">✓ 기본</span>
      <span class="sim-icon sim-icon--primary">✓ 주요</span>
      <span class="sim-icon sim-icon--success">✓ 성공</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--warning">⚠ 경고</span>
      <span class="sim-icon sim-icon--danger">✕ 위험</span>
      <span class="sim-icon sim-icon--info">ℹ 정보</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon sim-icon--regular">✓ 일반</span>
      <span class="sim-icon sim-icon--secondary">✓ 보조</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 아이콘 이름

`GlyphFor`에 현재 등록된 아이콘 이름과 글리프: `search` ⌕, `refresh` ↻, `check` ✓, `close` ✕,
`warning` ⚠, `info` ℹ, `back` ←, `more` ⋯, `star` ★, `edit` ✎, `delete` ⌫,
`plus` ＋, `minus` －, `arrow` →. 미등록 이름은 `◆`로 대체됩니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>등록된 아이콘</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon">⌕</span><span class="sim-icon">↻</span><span class="sim-icon">✓</span>
      <span class="sim-icon">✕</span><span class="sim-icon">⚠</span><span class="sim-icon">ℹ</span>
      <span class="sim-icon">←</span><span class="sim-icon">⋯</span>
    </div>
    <div class="sim-row">
      <span class="sim-icon">★</span><span class="sim-icon">✎</span><span class="sim-icon">⌫</span>
      <span class="sim-icon">＋</span><span class="sim-icon">－</span><span class="sim-icon">→</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 라이트 / 다크 테마

사이트 우측 상단에서 테마를 전환하면 시뮬레이터 폰 프레임 내의 아이콘 색상이 함께 바뀝니다. 실기기에서는 `LhTheme.Apply(true)`로 전환합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>다크 아이콘</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-icon sim-icon--primary">★</span>
      <span class="sim-icon sim-icon--success">✓</span>
      <span class="sim-icon sim-icon--warning">⚠</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님. 라이트 / 다크 값은 모두 core의 토큰 스냅샷에서 가져옵니다</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `Name` | 아이콘 이름(로컬 아이콘 테이블에서 해석, 미등록 이름은 `◆`로 대체) | `string` | `""` |
| `Size` | 아이콘 크기, `icon.size.*` 토큰에 매핑 | `LhIconSize` | `Medium` |
| `Tone` | 아이콘 색조, 시맨틱 색 / 글자색 토큰에 매핑 | `LhIconTone` | `Regular` |

### LhIconSize

| 값 | 크기 토큰 |
| --- | --- |
| `Small` | `icon.size.small` (16epx) |
| `Medium` | `icon.size.medium` (18epx) |
| `Large` | `icon.size.large` (20epx) |

### LhIconTone

| 값 | 색상 토큰 |
| --- | --- |
| `Default` | `color.text.primary` |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `icon.size.{small,medium,large}` | 3단계 크기 |
| `color.text.{primary,regular,secondary}` | 중성 색조 |
| `color.{primary,success,warning,danger,info}.default` | 시맨틱 색조 |
| `typography.fontFamily` | 글리프 폰트 스택 |

## 골든 케이스 매핑

`cases/icon.json`의 5건 어서션과 xUnit 테스트의 대응 관계(`tests/LhIconTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `click-emits-ononclick` | 게이트가 통과함(아이콘이 인터랙티브) | `Click_Emits_OnClick` |
| `default-tone-uses-text-primary` | 기본 색조가 주요 글자색 토큰을 사용 | `Default_Tone_Uses_Text_Primary` |
| `primary-tone-uses-primary-token` | 주요 색조가 주요 색 토큰을 사용 | `Primary_Tone_Uses_Primary_Token` |
| `small-size-uses-small-token` | 소형이 small 크기 토큰을 소비 | `Small_Size_Uses_Small_Token` |
| `render-emits-icon-command` | 아이콘 명령이 정확히 하나 발생 | `Render_Emits_Single_Icon_Command` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
