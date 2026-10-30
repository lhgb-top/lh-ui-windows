# Button 버튼

기본적인 액션 버튼입니다. 즉시 액션을 트리거하며, 유형, 크기, 로딩, 비활성화, 아이콘, 중국풍 문양 배경을 지원합니다.

> 계약 출처: `lh-ui-core/specs/button.json` ·
> 골든 케이스: `lh-ui-core/cases/button.json` (8건, xUnit 테스트로 번역됨)

## 유형

6가지 유형이 배경색과 글자색을 결정합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button 유형</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">기본 버튼</button>
      <button class="sim-btn sim-btn--primary">주요 버튼</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--success">성공 버튼</button>
      <button class="sim-btn sim-btn--warning">경고 버튼</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--danger">위험 버튼</button>
      <button class="sim-btn sim-btn--info">정보 버튼</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var button = new LhButton
{
    Type = LhButtonType.Primary,
    Text = "주요 버튼",
};
button.Click += OnButtonClick;
```

## 크기

3단계 크기가 `button.height` / `paddingHorizontal` / `fontSize` / `iconSize` 토큰에 매핑됩니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button 크기</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">소형 버튼</button>
      <button class="sim-btn">중형 버튼</button>
      <button class="sim-btn sim-btn--large">대형 버튼</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 상태

비활성화와 로딩 중 모두 `Click`을 트리거하지 않습니다. 둘 다 지정된 경우 **비활성화가 우선**합니다(골든 케이스 `disabled-precedes-loading`).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Button 상태</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">보통</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">비활성화</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>로딩 중
      </button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">비활성화 우선</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 아이콘과 문양 배경

아이콘 이름은 로컬 아이콘 체계에서 해석됩니다(`LhButtonTokens.IconFor`, 현재 search / refresh / check / close를 지원하며, 미등록 이름은 그려지지 않습니다).
문양 배경은 `color.decoration` 토큰을 소비하며, 45도 사선으로 겹쳐 그려집니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>아이콘과 문양</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>검색</button>
      <button class="sim-btn"><span class="sim-btn__icon"></span>기본</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--pattern">주요 문양 배경</button>
      <button class="sim-btn sim-btn--pattern">기본 문양 배경</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 라이트 / 다크 테마

사이트 우측 상단에서 테마를 전환하면 시뮬레이터 폰 프레임 내의 버튼 색상이 함께 바뀝니다. 실기기에서는 `LhTheme.Apply(true)`로 전환합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>다크 버튼</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary">주요 버튼</button>
      <button class="sim-btn">기본 버튼</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary sim-btn--disabled">비활성화</button>
      <button class="sim-btn sim-btn--success">성공</button>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님. 라이트 / 다크 값은 모두 core의 토큰 스냅샷에서 가져옵니다</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `Click` | 클릭 이벤트. `Disabled` 또는 `Loading`이 true인 경우 트리거하지 않음 | `event Action` | — |
| `Type` | 버튼 유형. 배경색과 글자색을 결정 | `LhButtonType` | `Default` |
| `Size` | 버튼 크기. 크기 토큰에 매핑 | `LhButtonSize` | `Medium` |
| `Disabled` | 비활성화 여부 | `bool` | `false` |
| `Loading` | 로딩 중 여부(인디케이터를 표시하고 Click을 트리거하지 않음) | `bool` | `false` |
| `Icon` | 아이콘 이름(로컬 아이콘 체계에서 해석) | `string?` | `null` |
| `BgPattern` | 중국풍 문양 배경을 그릴지 여부 | `bool` | `false` |
| `Text` | 버튼 글자 내용(spec의 default 슬롯) | `string` | `""` |

### LhButtonType

| 값 | 배경색 | 글자색 |
| --- | --- | --- |
| `Default` | `color.background.default` | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.onPrimary` |
| `Success` | `color.success.default` | `color.onPrimary` |
| `Warning` | `color.warning.default` | `color.onPrimary` |
| `Danger` | `color.danger.default` | `color.onPrimary` |
| `Info` | `color.info.default` | `color.onPrimary` |

### LhButtonSize

| 값 | height | paddingHorizontal | fontSize | iconSize |
| --- | --- | --- | --- | --- |
| `Small` | `button.height.small` (32epx) | `button.paddingHorizontal.small` (16epx) | `button.fontSize.small` (13) | `button.iconSize.small` (16) |
| `Medium` | `button.height.medium` (40epx) | `button.paddingHorizontal.medium` (20epx) | `button.fontSize.medium` (14) | `button.iconSize.medium` (18) |
| `Large` | `button.height.large` (48epx) | `button.paddingHorizontal.large` (24epx) | `button.fontSize.large` (16) | `button.iconSize.large` (20) |

비활성화 상태 오버라이드: 배경 `color.disabled.bg`, 글자 `color.disabled.text`, 테두리 `color.disabled.border`.

## 토큰 매핑

버튼이 소비하는 토큰(`specs/button.json`의 `tokenRefs`와 1:1로 대응):

| 토큰 | 용도 |
| --- | --- |
| `button.height.{small,medium,large}` | 3단계 높이 |
| `button.paddingHorizontal.{small,medium,large}` | 3단계 가로 패딩 |
| `button.fontSize.{small,medium,large}` | 3단계 폰트 크기 |
| `button.iconSize.{small,medium,large}` | 3단계 아이콘 크기 |
| `button.borderRadius` / `button.borderWidth` | 코너 반경(`radius.base`를 따름)과 테두리 두께 |
| `button.fontWeight` | 폰트 굵기(`typography.fontWeight.primary`를 따름, 엔드 측에서 FontWeights로 매핑) |
| `color.primary.default` / `color.onPrimary` | 주요 유형의 배경 / 글자 |
| `color.{success,warning,danger,info}.default` | 시맨틱 유형의 배경색 |
| `color.background.default` / `color.text.primary` / `color.border.base` | 기본 유형의 배경 / 글자 / 테두리 |
| `color.disabled.{bg,text,border}` | 비활성화 상태의 세 가지 색 |
| `color.decoration` | 문양 색 |
| `spacing.xs` | 아이콘과 글자 사이의 간격 |
| `typography.fontFamily` | 중국풍 세리프 폰트 스택 |

## 골든 케이스 매핑

`cases/button.json`의 8건 어서션과 xUnit 테스트의 대응 관계(`tests/LhButtonTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `click-emits-ononclick` | 게이트가 통과함(인터랙티브) | `Enabled_Button_Is_Interactive` |
| `disabled-blocks-click` | 비활성화 상태는 인터랙티브하지 않음 | `Disabled_Is_Not_Interactive` |
| `loading-blocks-click` | 로딩 상태는 인터랙티브하지 않음 | `Loading_Is_Not_Interactive` |
| `disabled-precedes-loading` | 비활성화가 우선 + 비활성화 스타일 | `Disabled_Precedes_Loading` |
| `default-type-renders-text` | 기본 유형의 중성적인 배경 / 글자 | `Default_Type_Uses_Neutral_Tokens` |
| `primary-type-uses-primary-tokens` | 주요 유형이 주요 토큰을 소비 | `Primary_Type_Uses_Primary_Tokens` |
| `small-size-uses-small-tokens` | 소형이 small 크기 토큰을 소비 | `Small_Size_Uses_Small_Tokens` |
| `bgpattern-uses-decoration-token` | 문양 배경이 장식 색을 소비 | `BgPattern_Uses_Decoration_Token` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
