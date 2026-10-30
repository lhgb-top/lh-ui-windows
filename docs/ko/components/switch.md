# Switch 스위치

스위치입니다. 캡슐 형태의 트랙 + 원형 썸으로 구성되며, 클릭으로 전환합니다. 비활성화와 로딩 중을 지원합니다.

> 계약 출처: `lh-ui-core/specs/switch.json` ·
> 골든 케이스: `lh-ui-core/cases/switch.json` (6건, xUnit 테스트로 번역됨)

## 기본 사용법

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch 기본</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };
```

## 크기

3단계 크기가 `switch.width` / `switch.height` 토큰에 매핑됩니다. 썸 지름 = 높이 - 2 × `switch.gap`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch 크기</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--small sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--checked"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--large sim-switch--checked"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 상태

`Disabled`와 `Loading` 모두 클릭에 응답하지 않습니다. `Loading`인 경우 썸 위치에 주요 색 로딩 링을 그립니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Switch 상태</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-switch sim-switch--checked sim-switch--disabled"><span class="sim-switch__thumb"></span></span>
      <span class="sim-switch sim-switch--loading"><span class="sim-switch__thumb"></span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `Change` | 스위치 상태 변화 콜백. disabled 또는 loading일 때 트리거하지 않음 | `event Action<bool>` | — |
| `Checked` | 켜짐 여부 | `bool` | `false` |
| `Disabled` | 비활성화 여부(클릭에 응답하지 않고 비활성화 스타일 적용) | `bool` | `false` |
| `Loading` | 로딩 중 여부(썸 위치에 로딩 링을 표시하고 클릭에 응답하지 않음) | `bool` | `false` |
| `Size` | 스위치 크기, `switch.width` / `switch.height` 토큰에 매핑 | `LhSwitchSize` | `Medium` |

### LhSwitchSize

| 값 | width | height |
| --- | --- | --- |
| `Small` | `switch.width.small` (40epx) | `switch.height.small` (22epx) |
| `Medium` | `switch.width.medium` (48epx) | `switch.height.medium` (26epx) |
| `Large` | `switch.width.large` (56epx) | `switch.height.large` (30epx) |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `switch.width.{small,medium,large}` | 3단계 트랙 너비 |
| `switch.height.{small,medium,large}` | 3단계 트랙 높이 |
| `switch.gap` | 썸과 트랙 사이의 여백(썸 지름 = 높이 - 2 × gap) |
| `color.primary.default` | 켜짐 트랙 색 |
| `color.border.base` | 꺼짐 트랙 색 |
| `color.onPrimary` | 썸 색 |
| `color.disabled.bg` / `color.text.disabled` | 비활성화 트랙 / 썸 색 |

## 골든 케이스 매핑

`cases/switch.json`의 6건 어서션과 xUnit 테스트의 대응 관계(`tests/LhSwitchTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `tap-toggles-and-emits-onchange` | 클릭이 상태를 전환하고 onChange를 트리거 | `Tap_Toggles_And_Emits_OnChange` |
| `tap-checked-turns-off` | 켜짐 상태에서 클릭하면 꺼짐 | `Tap_Checked_Turns_Off` |
| `disabled-blocks-change` | 비활성화 상태는 인터랙티브하지 않음 | `Disabled_Blocks_Change` |
| `loading-blocks-change` | 로딩 상태는 인터랙티브하지 않음 | `Loading_Blocks_Change` |
| `checked-uses-primary-token` | 켜짐 상태가 주요 색 토큰을 소비 | `Checked_Uses_Primary_Token` |
| `unchecked-uses-border-token` | 꺼짐 상태가 테두리 토큰을 소비 | `Unchecked_Uses_Border_Token` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
