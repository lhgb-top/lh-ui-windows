# Text 텍스트

텍스트 컴포넌트입니다. 레벨, 폰트 굵기, 색조, 정렬에 따라 단일 텍스트 단락을 렌더링하며, 모든 타이포그래피 값은 typography 토큰에서 가져옵니다.

> 계약 출처: `lh-ui-core/specs/text.json` ·
> 골든 케이스: `lh-ui-core/cases/text.json` (5건, xUnit 테스트로 번역됨)

## 레벨

6단계 레벨이 `typography.fontSize.*` 토큰에 매핑됩니다. 기본 레벨은 `base`입니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Text 레벨</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--extra-large">青花瓷(청화백자) extraLarge</div>
    <div class="sim-text sim-text--large">青花瓷(청화백자) large</div>
    <div class="sim-text sim-text--medium">青花瓷(청화백자) medium</div>
    <div class="sim-text">青花瓷(청화백자) base</div>
    <div class="sim-text sim-text--small">青花瓷(청화백자) small</div>
    <div class="sim-text sim-text--extra-small">青花瓷(청화백자) extraSmall</div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var text = new LhText
{
    Text = "青花瓷(청화백자)",
    Level = LhTextLevel.Large,
};
```

## 폰트 굵기와 색조

`Weight`는 `typography.fontWeight.*` 토큰에 매핑됩니다. `Tone`은 `color.text.*`에 매핑됩니다(selected는 `color.selected` 사용).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>폰트 굵기와 색조</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--bold">굵게 bold</div>
    <div class="sim-text sim-text--secondary">보조 설명 secondary</div>
    <div class="sim-text sim-text--placeholder">플레이스홀더 placeholder</div>
    <div class="sim-text sim-text--selected">선택 상태 selected</div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 정렬

`Align`은 라인 박스 너비 내에서의 수평 배치를 결정합니다(디자인 토큰이 아니며 WinUI의 `TextAlignment`에 매핑됨).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>정렬</span></div>
  <div class="sim-screen">
    <div class="sim-text sim-text--start">좌측 정렬 start</div>
    <div class="sim-text sim-text--center">가운데 정렬 center</div>
    <div class="sim-text sim-text--end">우측 정렬 end</div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 라이트 / 다크 테마

사이트 우측 상단에서 테마를 전환하면 시뮬레이터 폰 프레임 내의 글자 색상이 함께 바뀝니다. 실기기에서는 `LhTheme.Apply(true)`로 전환합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>다크 텍스트</span></div>
  <div class="sim-screen">
    <div class="sim-text">일반 글자 regular</div>
    <div class="sim-text sim-text--secondary">보조 설명 secondary</div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님. 라이트 / 다크 값은 모두 core의 토큰 스냅샷에서 가져옵니다</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `Text` | 글자 내용(spec의 default 슬롯) | `string` | `""` |
| `Level` | 폰트 크기 레벨, `typography.fontSize.*`에 매핑 | `LhTextLevel` | `Base` |
| `Weight` | 폰트 굵기, `typography.fontWeight.*`에 매핑 | `LhTextWeight` | `Primary` |
| `Tone` | 글자 색조, `color.text.*` / `color.selected`에 매핑 | `LhTextTone` | `Regular` |
| `Align` | 수평 정렬(`TextAlignment`에 매핑) | `LhTextAlign` | `Start` |

### LhTextLevel

| 값 | 폰트 크기 토큰 |
| --- | --- |
| `ExtraLarge` | `typography.fontSize.extraLarge` (20epx) |
| `Large` | `typography.fontSize.large` (18epx) |
| `Medium` | `typography.fontSize.medium` (16epx) |
| `Base` | `typography.fontSize.base` (14epx) |
| `Small` | `typography.fontSize.small` (13epx) |
| `ExtraSmall` | `typography.fontSize.extraSmall` (12epx) |

### LhTextWeight

| 값 | 폰트 굵기 토큰 |
| --- | --- |
| `Primary` | `typography.fontWeight.primary` (400, 엔드 측에서 Normal로 매핑) |
| `Bold` | `typography.fontWeight.bold` (600, 엔드 측에서 SemiBold로 매핑) |

### LhTextTone

| 값 | 색상 토큰 |
| --- | --- |
| `Primary` | `color.text.primary` |
| `Regular` | `color.text.regular` |
| `Secondary` | `color.text.secondary` |
| `Placeholder` | `color.text.placeholder` |
| `Disabled` | `color.text.disabled` |
| `Selected` | `color.selected` |

### LhTextAlign

| 값 | WinUI 매핑 |
| --- | --- |
| `Start` | `TextAlignment.Left` |
| `Center` | `TextAlignment.Center` |
| `End` | `TextAlignment.Right` |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `typography.fontSize.{extraLarge…extraSmall}` | 6단계 폰트 크기 |
| `typography.fontWeight.{primary,bold}` | 2단계 폰트 굵기 |
| `typography.lineHeight.primary` | 줄 높이(라인 박스 높이 = 폰트 크기 × 줄 높이) |
| `typography.fontFamily` | 중국풍 세리프 폰트 스택 |
| `color.text.{primary,regular,secondary,placeholder,disabled}` | 중성 색조 |
| `color.selected` | 선택 상태 색조 |

## 골든 케이스 매핑

`cases/text.json`의 5건 어서션과 xUnit 테스트의 대응 관계(`tests/LhTextTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `default-level-uses-base-tokens` | 기본 레벨이 base 타이포그래피 토큰을 소비 | `Default_Level_Uses_Base_Tokens` |
| `large-level-uses-large-token` | large 레벨이 large 폰트 크기 토큰을 소비 | `Large_Level_Uses_Large_Token` |
| `bold-weight-uses-bold-token` | 굵게가 bold 폰트 굵기 토큰을 소비 | `Bold_Weight_Uses_Bold_Token` |
| `secondary-tone-uses-text-secondary` | 보조 색조가 보조 글자색 토큰을 사용 | `Secondary_Tone_Uses_Text_Secondary` |
| `render-emits-single-text-command` | TEXT 명령이 정확히 하나 발생 | `Render_Emits_Single_Text_Command` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
