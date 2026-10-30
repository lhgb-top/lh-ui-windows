# Textarea 여러 줄 입력

여러 줄 텍스트 입력입니다. `Rows`로 초기 표시 행 수를 지정하고, 내용이 넘어가면 자동으로 높이가 늘어나며, 글자 수 세기를 지원합니다.

> 계약 출처: `lh-ui-core/specs/textarea.json` ·
> 골든 케이스: `lh-ui-core/cases/textarea.json` (6건, xUnit 테스트로 번역됨)

## 기본 사용법

제어 컴포넌트입니다. 호스트는 `ValueChange`를 수신하고 새 값을 `Value`에 재기록합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea 기본</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷(청화백자)<br/>粉彩瓷(분채자기)</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field sim-textarea__field--placeholder">설명을 입력하세요</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var textarea = new LhTextarea
{
    Rows = 3,
    Placeholder = "설명을 입력하세요",
};
textarea.ValueChange += value => { /* 제어: Value에 재기록 */ };
```

## 글자 수 세기

`ShowCount`가 true인 경우 우하단에 글자 수 세기를 그립니다. `Maxlength`를 선언한 경우 형식은 「현재/최대」가 되고,
그렇지 않은 경우 현재 길이만 표시합니다. `Maxlength`를 초과하면 새 입력을 자릅니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>글자 수 세기</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷(청화백자)</div>
        <div class="sim-textarea__count">3/10</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea">
        <div class="sim-textarea__field">青花瓷(청화백자)</div>
        <div class="sim-textarea__count">3</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 상태

`Disabled`와 `Readonly` 모두 `ValueChange`를 트리거하지 않지만, `Disabled`만 비활성화 스타일을 적용합니다.
포커스 시 테두리는 `color.focus` + `textarea.focusRingWidth`를 사용합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Textarea 상태</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--disabled">
        <div class="sim-textarea__field">비활성화</div>
      </div>
    </div>
    <div class="sim-row">
      <div class="sim-textarea sim-textarea--focus">
        <div class="sim-textarea__field">포커스 테두리</div>
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
| `Rows` | 초기 표시 행 수. 행 수와 내용에 따라 높이가 증가 | `int` | `3` |
| `Disabled` | 비활성화 여부(입력에 응답하지 않고 비활성화 스타일 적용) | `bool` | `false` |
| `Readonly` | 읽기 전용 여부(포커스 가능하지만 입력을 받지 않음) | `bool` | `false` |
| `ShowCount` | 우하단에 글자 수 세기 표시 여부 | `bool` | `false` |
| `Maxlength` | 최대 글자 수. 초과 시 새 입력을 자르고 showCount의 분모로 사용 | `int?` | `null` |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `textarea.paddingVertical` / `paddingHorizontal` | 패딩 |
| `textarea.fontSize` | 폰트 크기 |
| `textarea.borderRadius` / `borderWidth` | 코너 반경과 포커스 해제 테두리 두께 |
| `textarea.focusRingWidth` / `color.focus` | 포커스 테두리 두께와 색상 |
| `textarea.countFontSize` / `color.text.secondary` | 글자 수 세기 폰트 크기 / 색상 |
| `textarea.minHeight` | 최소 높이(행 수로 계산한 결과는 이 값보다 작지 않음) |
| `color.background.default` / `color.text.primary` | 컨테이너 배경 / 글자색 |
| `color.text.placeholder` | 플레이스홀더 글자색 |
| `color.border.base` | 포커스 해제 테두리색 |
| `color.disabled.{bg,text}` | 비활성화 상태의 배경 / 글자 |
| `typography.lineHeight.primary` | 줄 높이(rows로 높이를 계산) |
| `typography.fontFamily` | 중국풍 세리프 폰트 스택 |

## 골든 케이스 매핑

`cases/textarea.json`의 6건 어서션과 xUnit 테스트의 대응 관계(`tests/LhTextareaTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `input-emits-onvaluechange` | 입력이 onValueChange를 트리거 | `Input_Emits_OnValueChange` |
| `disabled-blocks-input` | 비활성화 상태는 트리거하지 않고 비활성화 스타일 적용 | `Disabled_Blocks_Input` |
| `showcount-renders-count` | 「현재/최대」가 countFontSize 토큰을 사용 | `ShowCount_Renders_Count` |
| `showcount-without-maxlength-renders-length-only` | maxlength가 없으면 길이만 표시 | `ShowCount_Without_Maxlength_Renders_Length_Only` |
| `focus-uses-focus-token` | 포커스 테두리가 focus 토큰을 사용 | `Focus_Uses_Focus_Token` |
| `maxlength-truncates-input` | maxlength 초과 시 자름 | `Maxlength_Truncates_Input` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
