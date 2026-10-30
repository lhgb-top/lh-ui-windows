# Picker 피커

하단에서 올라오는 단일 열 휠 피커입니다. 후보 항목, 인덱스 클램프, 툴바를 지원합니다.

> 계약 출처: `lh-ui-core/specs/picker.json` ·
> 골든 케이스: `lh-ui-core/cases/picker.json` (8건, xUnit 테스트로 번역됨)

## 기본 사용법

선언적 표시 여부: `Visible`이 false인 경우 아무 내용도 렌더링하지 않습니다. 확인 / 취소 후 `Close`가 트리거되며,
호스트는 이에 따라 `Visible`을 false로 설정합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Picker 기본</span></div>
  <div class="sim-screen">
    <div class="sim-picker-overlay"></div>
    <div class="sim-picker">
      <div class="sim-picker__toolbar">
        <span class="sim-picker__cancel">취소</span>
        <span class="sim-picker__title">선택</span>
        <span class="sim-picker__confirm">확인</span>
      </div>
      <div class="sim-picker__wheel">
        <span class="sim-picker__item">青花瓷(청화백자)</span>
        <span class="sim-picker__item sim-picker__item--active">粉彩瓷(분채자기)</span>
        <span class="sim-picker__item">玲珑瓷(영롱자기)</span>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var picker = new LhPicker
{
    Options = new[] { "青花瓷(청화백자)", "粉彩瓷(분채자기)", "玲珑瓷(영롱자기)" },
    Value = 1,
    Title = "선택",
};
picker.ValueChange += index => { /* 휠이 멈춘 후의 선택 인덱스 */ };
picker.Confirm += index => { /* 현재 선택 인덱스 */ };
picker.Close += () => picker.Visible = false;

picker.Visible = true;
```

## 인덱스 클램프

`Value`가 범위를 벗어나면 후보 항목 범위 내로 클램프됩니다. 음수는 0, 후보 항목 수 - 1을 초과하면 마지막 항목을 사용합니다.

```csharp
Assert.Equal(0, LhPickerTokens.ClampIndex(-1, 3));   // 음수는 0
Assert.Equal(2, LhPickerTokens.ClampIndex(100, 3));  // 범위 초과 시 마지막 항목
```

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `ValueChange` | 휠이 멈추고 선택 항목이 변경되었을 때 트리거 | `event Action<int>` | — |
| `Confirm` | 확인 버튼 클릭 시 트리거, 현재 선택 인덱스를 전달 | `event Action<int>` | — |
| `Cancel` | 취소 버튼 클릭 시 트리거 | `event Action` | — |
| `Close` | 확인 / 취소 후 트리거, 호스트는 이에 따라 Visible을 false로 설정 | `event Action` | — |
| `Visible` | 표시 여부. false인 경우 아무 내용도 렌더링하지 않음 | `bool` | `false` |
| `Options` | 후보 항목 텍스트 배열(단일 열) | `string[]?` | `null` |
| `Value` | 선택 항목 인덱스(0부터 시작). 범위를 벗어나면 후보 항목 범위 내로 클램프 | `int` | `0` |
| `Title` | 툴바 중앙의 제목 | `string` | `""` |
| `ConfirmText` | 확인 버튼 글자 | `string` | `"确认"` |
| `CancelText` | 취소 버튼 글자 | `string` | `"取消"` |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `picker.height` | 패널 높이 |
| `picker.toolbarHeight` | 상단 툴바 높이(휠 영역 높이 = 패널 높이 - 툴바 높이) |
| `picker.itemHeight` | 후보 항목 높이(중앙 인디케이터 간격도 동일) |
| `picker.itemFontSize` | 후보 항목과 툴바의 폰트 크기 |
| `picker.borderRadius` | 패널 상단 코너 반경 |
| `color.background.default` | 패널 배경색 |
| `color.background.overlay` | 마스크 색상 |
| `color.text.primary` | 제목과 후보 항목 색상 |
| `color.text.secondary` | 취소 버튼 색상 |
| `color.primary.default` | 확인 버튼 색상 |
| `color.border.lighter` | 중앙 인디케이터 선 색상 |
| `zIndex.modal` / `zIndex.modalBackdrop` | 패널 / 마스크 계층 |

## 골든 케이스 매핑

`cases/picker.json`의 8건 어서션과 xUnit 테스트의 대응 관계(`tests/LhPickerTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `confirm-emits-onconfirm-with-value` | 확인이 현재 선택 인덱스를 전달 | `Confirm_Emits_OnConfirm_With_Value` |
| `confirm-also-emits-onclose` | 확인이 onClose도 함께 트리거 | `Confirm_Also_Emits_OnClose` |
| `cancel-emits-oncancel` | 취소가 onCancel을 트리거 | `Cancel_Emits_OnCancel` |
| `scroll-emits-onvaluechange` | 휠 정지가 onValueChange를 트리거 | `Scroll_Emits_OnValueChange` |
| `clamp-negative-value-to-zero` | 음수 인덱스를 0으로 클램프 | `Clamp_Negative_Value_To_Zero` |
| `clamp-overflow-value-to-last` | 범위 초과 인덱스를 마지막 항목으로 클램프 | `Clamp_Overflow_Value_To_Last` |
| `title-renders-text` | 제목이 폰트 크기 토큰에 따라 렌더링 | `Title_Renders_Text` |
| `hidden-not-rendered` | 보이지 않을 때 아무 내용도 렌더링하지 않음 | `Hidden_Not_Rendered` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
