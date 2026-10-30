# Input 입력 상자

단일 행 텍스트 입력입니다. 유형, 크기, 비활성화, 읽기 전용, 지우기, 최대 글자 수를 지원합니다.

> 계약 출처: `lh-ui-core/specs/input.json` ·
> 골든 케이스: `lh-ui-core/cases/input.json` (8건, xUnit 테스트로 번역됨)

## 기본 사용법

제어 컴포넌트입니다. 호스트는 `ValueChange`를 수신하고 새 값을 `Value`에 재기록합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input 기본</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷(청화백자)</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field sim-input__field--placeholder">키워드를 입력하세요</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var input = new LhInput
{
    Placeholder = "키워드를 입력하세요",
};
input.ValueChange += value => { /* 제어: Value에 재기록 */ };
```

## 크기

3단계 크기가 `input.height` / `paddingHorizontal` / `fontSize` 토큰에 매핑됩니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input 크기</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input sim-input--small"><div class="sim-input__field">소형</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">중형</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--large"><div class="sim-input__field">대형</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 유형과 상태

`Password`는 원점 마스크로 표시합니다. `Disabled`와 `Readonly` 모두 `ValueChange`를 트리거하지 않지만,
`Disabled`만 비활성화 스타일을 적용합니다. 포커스 시 테두리는 `color.focus` + `input.focusRingWidth`를 사용합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Input 상태</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input"><div class="sim-input__field">●●●●●●●●</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--disabled"><div class="sim-input__field">비활성화</div></div>
    </div>
    <div class="sim-row">
      <div class="sim-input sim-input--focus"><div class="sim-input__field">포커스 테두리</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 지우기와 글자 수

`Clearable`이 true이고 값이 비어 있지 않은 경우 지우기 버튼을 표시하며, 클릭하면 `ValueChange("")`를 트리거합니다.
`Maxlength`를 초과하면 새 입력을 자릅니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>지우기와 글자 수</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-input">
        <div class="sim-input__field">青花瓷(청화백자)</div>
        <span class="sim-input__clear">✕</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `ValueChange` | 값 변화 콜백(제어). disabled / readonly일 때 트리거하지 않음 | `event Action<string>` | — |
| `Focus` | 포커스를 얻었을 때 트리거 | `event Action` | — |
| `Blur` | 포커스를 잃었을 때 트리거 | `event Action` | — |
| `Value` | 현재 텍스트 값(제어) | `string` | `""` |
| `Placeholder` | 플레이스홀더 힌트 텍스트, `color.text.placeholder` 토큰 사용 | `string` | `""` |
| `Type` | 입력 유형. password인 경우 원점 마스크로 표시 | `LhInputType` | `Text` |
| `Size` | 입력 상자 크기, 크기 토큰에 매핑 | `LhInputSize` | `Medium` |
| `Disabled` | 비활성화 여부(입력에 응답하지 않고 비활성화 스타일 적용) | `bool` | `false` |
| `Readonly` | 읽기 전용 여부(포커스 가능하지만 입력을 받지 않음) | `bool` | `false` |
| `Clearable` | 지우기 버튼 표시 여부 | `bool` | `false` |
| `Maxlength` | 최대 글자 수. 초과 시 새 입력을 자름 | `int?` | `null` |

### LhInputSize

| 값 | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `input.height.small` (32epx) | `input.paddingHorizontal.small` (12epx) | `input.fontSize.small` (13) |
| `Medium` | `input.height.medium` (40epx) | `input.paddingHorizontal.medium` (16epx) | `input.fontSize.medium` (14) |
| `Large` | `input.height.large` (48epx) | `input.paddingHorizontal.large` (20epx) | `input.fontSize.large` (16) |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `input.height.{small,medium,large}` | 3단계 높이 |
| `input.paddingHorizontal.{small,medium,large}` | 3단계 가로 패딩 |
| `input.fontSize.{small,medium,large}` | 3단계 폰트 크기 |
| `input.borderRadius` / `input.borderWidth` | 코너 반경과 포커스 해제 테두리 두께 |
| `input.focusRingWidth` / `color.focus` | 포커스 테두리 두께와 색상 |
| `input.clearIconSize` | 지우기 버튼 아이콘 크기 |
| `color.background.default` / `color.text.primary` | 컨테이너 배경 / 글자색 |
| `color.text.placeholder` | 플레이스홀더 글자색 |
| `color.border.base` | 포커스 해제 테두리색 |
| `color.disabled.{bg,text}` | 비활성화 상태의 배경 / 글자 |
| `typography.fontFamily` | 중국풍 세리프 폰트 스택 |

## 골든 케이스 매핑

`cases/input.json`의 8건 어서션과 xUnit 테스트의 대응 관계(`tests/LhInputTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `input-emits-onvaluechange` | 입력이 onValueChange를 트리거 | `Input_Emits_OnValueChange` |
| `clearable-emits-empty-value` | 지우기가 onValueChange("")를 트리거 | `Clearable_Emits_Empty_Value` |
| `disabled-blocks-input` | 비활성화 상태는 트리거하지 않고 비활성화 스타일 적용 | `Disabled_Blocks_Input` |
| `readonly-blocks-input` | 읽기 전용은 트리거하지 않지만 비활성화 스타일 없음 | `Readonly_Blocks_Input` |
| `placeholder-uses-placeholder-token` | 플레이스홀더 텍스트가 플레이스홀더 토큰을 사용 | `Placeholder_Uses_Placeholder_Token` |
| `focus-uses-focus-token` | 포커스 테두리가 focus 토큰을 사용 | `Focus_Uses_Focus_Token` |
| `small-size-uses-small-tokens` | 소형이 small 크기 토큰을 소비 | `Small_Size_Uses_Small_Tokens` |
| `maxlength-truncates-input` | maxlength 초과 시 자름 | `Maxlength_Truncates_Input` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
