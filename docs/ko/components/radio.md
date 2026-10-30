# Radio 라디오 버튼

라디오 버튼입니다. 원환 + 내부 원점으로 구성되며, 클릭 영역은 행 전체입니다. 컴포넌트 자체는 그룹 로직을 포함하지 않고 호스트가 라디오 그룹을 관리합니다.

> 계약 출처: `lh-ui-core/specs/radio.json` ·
> 골든 케이스: `lh-ui-core/cases/radio.json` (6건, xUnit 테스트로 번역됨)

## 기본 사용법

호스트가 `Change`를 수신한 뒤 같은 그룹의 다른 항목 `Checked`를 false로 설정하면 라디오 그룹이 구현됩니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio 기본</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">青花瓷(청화백자)</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"></span>
        <span class="sim-radio__label">粉彩瓷(분채자기)</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var radio = new LhRadio
{
    Label = "青花瓷(청화백자)",
    Value = "qinghua",
    Checked = true,
};
radio.Change += isChecked => { /* 선택되지 않고 비활성화되지 않았을 때만 트리거; 호스트가 라디오 그룹을 관리 */ };
```

## 크기

3단계 크기가 `radio.size.*` 원환 크기 토큰에 매핑됩니다. 내부 원점 지름 = 크기 × `radio.dotScale`.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio 크기</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--small"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">소형</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">중형</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--large"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">대형</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 상태

`Disabled`인 경우 클릭에 응답하지 않으며, 외환은 `color.disabled.border`를, 원점은 `color.disabled.text`를 사용합니다.
이미 선택된 상태에서 다시 클릭해도 `Change`를 트리거하지 않습니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Radio 상태</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"></span>
        <span class="sim-radio__label">비활성화됨(선택 안 됨)</span>
      </span>
    </div>
    <div class="sim-row">
      <span class="sim-radio">
        <span class="sim-radio__ring sim-radio__ring--disabled"><span class="sim-radio__dot"></span></span>
        <span class="sim-radio__label">비활성화됨(선택)</span>
      </span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `Change` | 선택 상태 변화 콜백. 선택되지 않고 비활성화되지 않았을 때만 트리거(payload는 항상 true) | `event Action<bool>` | — |
| `Checked` | 선택 여부 | `bool` | `false` |
| `Disabled` | 비활성화 여부(클릭에 응답하지 않고 비활성화 스타일 적용) | `bool` | `false` |
| `Label` | 글자 라벨(원환 우측에 그려짐) | `string` | `""` |
| `Value` | 라디오 값(그룹 내 식별자, 컴포넌트 자체는 그룹 로직을 포함하지 않음) | `string` | `""` |
| `Size` | 원환 크기, `radio.size.*` 토큰에 매핑 | `LhRadioSize` | `Medium` |

### LhRadioSize

| 값 | 크기 토큰 |
| --- | --- |
| `Small` | `radio.size.small` (14epx) |
| `Medium` | `radio.size.medium` (16epx) |
| `Large` | `radio.size.large` (18epx) |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `radio.size.{small,medium,large}` | 3단계 원환 크기 |
| `radio.ringWidth` | 외환 테두리 두께 |
| `radio.dotScale` | 내부 원점 지름 비율(크기 × dotScale) |
| `radio.gap` | 원환과 글자 사이의 간격 |
| `color.primary.default` | 선택 상태의 외환과 원점 색 |
| `color.border.base` | 선택 해제 상태의 외환 색 |
| `color.disabled.{border,text}` | 비활성화 상태의 외환 / 원점 |
| `color.text.{primary,disabled}` | 라벨 글자색 |
| `typography.fontSize.base` / `fontFamily` | 라벨 폰트 크기와 폰트 스택 |

## 골든 케이스 매핑

`cases/radio.json`의 6건 어서션과 xUnit 테스트의 대응 관계(`tests/LhRadioTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `tap-unchecked-emits-onchange` | 선택 안 됨 상태에서 클릭이 onChange를 트리거 | `Tap_Unchecked_Emits_OnChange` |
| `tap-checked-keeps-checked` | 선택된 상태에서 다시 클릭해도 트리거하지 않음 | `Tap_Checked_Keeps_Checked` |
| `disabled-blocks-change` | 비활성화 상태는 인터랙티브하지 않음 | `Disabled_Blocks_Change` |
| `checked-uses-primary-tokens` | 선택 상태가 주요 색 토큰을 소비 | `Checked_Uses_Primary_Tokens` |
| `unchecked-uses-border-token` | 선택 해제 상태가 테두리 토큰을 소비 | `Unchecked_Uses_Border_Token` |
| `small-size-uses-small-token` | 소형이 small 크기 토큰을 소비 | `Small_Size_Uses_Small_Token` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
