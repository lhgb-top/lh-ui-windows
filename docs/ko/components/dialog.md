# Dialog 대화상자

모달 대화상자입니다. 마스크 + 가운데 정렬된 콘텐츠 카드 + 확인 / 취소 버튼으로 구성되며, 시맨틱 유형과 마스크 닫기를 지원합니다.

> 계약 출처: `lh-ui-core/specs/dialog.json` ·
> 골든 케이스: `lh-ui-core/cases/dialog.json` (8건, xUnit 테스트로 번역됨)

## 기본 사용법

선언적 표시 여부: `Visible`이 false인 경우 아무 내용도 렌더링하지 않습니다. 확인 / 취소 후 `Close`가 트리거되며,
호스트는 이에 따라 `Visible`을 false로 설정합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog 기본</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">삭제 확인</div>
      <div class="sim-dialog__content">삭제하면 되돌릴 수 없습니다. 이 레코드를 삭제하시겠습니까?</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">취소</button>
        <button class="sim-btn sim-btn--danger">확인</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var dialog = new LhDialog
{
    Title = "삭제 확인",
    Content = "삭제하면 되돌릴 수 없습니다. 이 레코드를 삭제하시겠습니까?",
    Type = LhDialogType.Danger,
    ConfirmText = "확인",
    CancelText = "취소",
};
dialog.Confirm += () => { /* 삭제 실행 */ };
dialog.Close += () => dialog.Visible = false;

dialog.Visible = true;
```

## 시맨틱 유형

`Type`이 확인 버튼의 유형(색상)을 결정합니다. 취소 버튼은 고정적으로 default 유형입니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Dialog 유형</span></div>
  <div class="sim-screen">
    <div class="sim-dialog-overlay"></div>
    <div class="sim-dialog">
      <div class="sim-dialog__title">알림</div>
      <div class="sim-dialog__content">주요 색상 확인 대화상자입니다</div>
      <div class="sim-dialog__actions">
        <button class="sim-btn">취소</button>
        <button class="sim-btn sim-btn--primary">확인</button>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `Confirm` | 확인 시 트리거 | `event Action` | — |
| `Cancel` | 취소 시 트리거 | `event Action` | — |
| `Close` | 확인 / 취소 후 또는(closable인 경우) 마스크 클릭 시 트리거, 호스트는 이에 따라 Visible을 false로 설정 | `event Action` | — |
| `Visible` | 표시 여부. false인 경우 아무 내용도 렌더링하지 않음 | `bool` | `false` |
| `Title` | 제목 | `string` | `""` |
| `Content` | 본문 내용 | `string` | `""` |
| `Type` | 시맨틱 유형, 확인 버튼의 유형(색상)을 결정 | `LhDialogType` | `Default` |
| `ConfirmText` | 확인 버튼 글자. 비어 있는 경우 확인 버튼을 표시하지 않음 | `string` | `"确认"` |
| `CancelText` | 취소 버튼 글자. 비어 있는 경우 취소 버튼을 표시하지 않음 | `string` | `"取消"` |
| `Closable` | 마스크 영역 클릭 시 Close를 트리거할지 여부 | `bool` | `false` |

### LhDialogType

| 값 | 확인 버튼 유형 |
| --- | --- |
| `Default` | `LhButtonType.Default` |
| `Primary` | `LhButtonType.Primary` |
| `Success` | `LhButtonType.Success` |
| `Warning` | `LhButtonType.Warning` |
| `Danger` | `LhButtonType.Danger` |
| `Info` | `LhButtonType.Info` |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `dialog.width` / `dialog.borderRadius` | 콘텐츠 카드 너비와 코너 반경 |
| `dialog.paddingHorizontal` / `paddingVertical` | 콘텐츠 카드 패딩 |
| `dialog.titleFontSize` / `contentFontSize` | 제목과 본문 폰트 크기 |
| `dialog.gap` | 제목 / 콘텐츠 / 버튼 영역 간격 |
| `color.background.default` | 콘텐츠 카드 배경색 |
| `color.background.overlay` | 마스크 색상 |
| `color.text.primary` / `color.text.regular` | 제목 / 본문 색상 |
| `zIndex.modal` / `zIndex.modalBackdrop` | 콘텐츠 카드 / 마스크 계층 |
| `shadow.base` | 콘텐츠 카드 그림자 |

## 골든 케이스 매핑

`cases/dialog.json`의 8건 어서션과 xUnit 테스트의 대응 관계(`tests/LhDialogTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `confirm-emits-onconfirm` | 확인이 onConfirm을 트리거 | `Confirm_Emits_OnConfirm` |
| `cancel-emits-oncancel` | 취소가 onCancel을 트리거 | `Cancel_Emits_OnCancel` |
| `confirm-also-emits-onclose` | 확인이 onClose도 함께 트리거 | `Confirm_Also_Emits_OnClose` |
| `closable-overlay-emits-onclose` | closable인 경우 마스크 클릭이 onClose를 트리거 | `Closable_Overlay_Emits_OnClose` |
| `not-closable-blocks-overlay-close` | closable이 아닌 경우 마스크로 닫지 않음 | `Not_Closable_Blocks_Overlay_Close` |
| `hidden-not-rendered` | 보이지 않을 때 아무 내용도 렌더링하지 않음 | `Hidden_Not_Rendered` |
| `title-renders-text` | 제목과 본문이 폰트 크기 토큰에 따라 렌더링 | `Title_Renders_Text` |
| `danger-type-uses-danger-token` | danger 유형이 위험 색 토큰을 소비 | `Danger_Type_Uses_Danger_Token` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
