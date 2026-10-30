# Checkbox 체크박스

체크박스입니다. 선택하면 체크 표시, 중간 상태는 가로 막대를 그리며, 클릭 영역은 행 전체(상자 + 라벨 영역)입니다.

> 계약 출처: `lh-ui-core/specs/checkbox.json` ·
> 골든 케이스: `lh-ui-core/cases/checkbox.json` (6건, xUnit 테스트로 번역됨)

## 기본 사용법

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox 기본</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box"></span>
        <span class="sim-checkbox__label">선택 안 됨</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">선택됨</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">─</span>
        <span class="sim-checkbox__label">중간 상태</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var checkbox = new LhCheckbox
{
    Label = "青花瓷(청화백자)",
    Checked = true,
};
checkbox.Change += isChecked => { };
```

## 크기

3단계 크기가 `checkbox.size.*` 상자 크기 토큰에 매핑됩니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox 크기</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--small sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">소형</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">중형</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--large sim-checkbox__box--checked">✓</span>
        <span class="sim-checkbox__label">대형</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 상태

`Disabled`인 경우 클릭에 응답하지 않으며, 배경 / 테두리 / 표시자 / 글자 모두 `color.disabled.*` 토큰을 사용합니다.
`Indeterminate`는 중간 상태(가로 막대)이며, 클릭하면 선택됨으로 바뀝니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Checkbox 상태</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--checked sim-checkbox__box--disabled">✓</span>
        <span class="sim-checkbox__label">비활성화됨(선택)</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-checkbox">
        <span class="sim-checkbox__box sim-checkbox__box--disabled"></span>
        <span class="sim-checkbox__label">비활성화됨(선택 안 됨)</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `Change` | 선택 상태 변화 콜백. disabled일 때 트리거하지 않음 | `event Action<bool>` | — |
| `Checked` | 선택 여부 | `bool` | `false` |
| `Indeterminate` | 중간 상태 여부(가로 막대 표시). 클릭 후 선택됨으로 전환 | `bool` | `false` |
| `Disabled` | 비활성화 여부(클릭에 응답하지 않고 비활성화 스타일 적용) | `bool` | `false` |
| `Label` | 글자 라벨(상자 우측에 그려짐) | `string` | `""` |
| `Size` | 상자 크기, `checkbox.size.*` 토큰에 매핑 | `LhCheckboxSize` | `Medium` |

### LhCheckboxSize

| 값 | 크기 토큰 |
| --- | --- |
| `Small` | `checkbox.size.small` (14epx) |
| `Medium` | `checkbox.size.medium` (16epx) |
| `Large` | `checkbox.size.large` (18epx) |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `checkbox.size.{small,medium,large}` | 3단계 상자 크기 |
| `checkbox.borderRadius` / `checkbox.borderWidth` | 상자 코너 반경과 테두리 두께 |
| `checkbox.checkWidth` | 체크 / 가로 막대 테두리 두께 |
| `checkbox.gap` | 상자와 글자 사이의 간격 |
| `color.primary.default` / `color.onPrimary` | 선택 / 중간 상태의 배경색과 표시자 색 |
| `color.border.base` | 선택 해제 상태의 테두리색 |
| `color.disabled.{bg,border,text}` | 비활성화 상태의 배경 / 테두리 / 표시자 |
| `color.text.{primary,disabled}` | 라벨 글자색 |
| `typography.fontSize.base` / `fontFamily` | 라벨 폰트 크기와 폰트 스택 |

## 골든 케이스 매핑

`cases/checkbox.json`의 6건 어서션과 xUnit 테스트의 대응 관계(`tests/LhCheckboxTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | 선택 안 됨 상태에서 클릭하면 선택됨 | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-unchecks` | 선택됨 상태에서 클릭하면 선택 해제 | `Tap_Checked_Unchecks` |
| `disabled-blocks-change` | 비활성화 상태는 인터랙티브하지 않음 | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | 선택 상태가 주요 색 토큰을 소비 | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | 선택 해제 상태가 테두리 토큰을 소비 | `Unchecked_Uses_Border_Token` |
| `indeterminate-becomes-checked` | 중간 상태에서 클릭하면 선택됨 | `Indeterminate_Becomes_Checked` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
