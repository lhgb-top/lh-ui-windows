# 빠른 시작

## 최소 예제

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Top.Lhgb.Ui;
using Top.Lhgb.Ui.Common;

public sealed class DemoPage : Page
{
    public DemoPage()
    {
        // 라이트 팔레트 주입. 다크로 전환하려면 true 전달. 모든 Lh 컴포넌트가 자동으로 따라감(LhTheme.ThemeChanged 핫 리프레시)
        LhTheme.Apply(false);

        var button = new LhButton
        {
            Type = LhButtonType.Primary,
            Text = "주요 버튼",
        };
        button.Click += OnButtonClick;

        Content = new StackPanel { Children = { button } };
    }

    private void OnButtonClick() => { /* 클릭 처리 */ }
}
```

`LhTheme.Apply(isDark)`는 정적 팔레트를 전환하고 `ThemeChanged`를 트리거하며, 이미 로드된 모든 Lh 컴포넌트가 자동으로 다시 그려집니다. 호출하지 않은 경우 라이트 팔레트로 대체됩니다.

## 유형과 크기

```csharp
// 6가지 유형
new LhButton { Text = "기본" };                                     // Default
new LhButton { Type = LhButtonType.Primary,   Text = "주요" };
new LhButton { Type = LhButtonType.Success,   Text = "성공" };
new LhButton { Type = LhButtonType.Warning,   Text = "경고" };
new LhButton { Type = LhButtonType.Danger,    Text = "위험" };
new LhButton { Type = LhButtonType.Info,      Text = "정보" };

// 3가지 크기
new LhButton { Size = LhButtonSize.Small,  Text = "소형" };
new LhButton { Size = LhButtonSize.Medium, Text = "중형" };   // 기본값
new LhButton { Size = LhButtonSize.Large,  Text = "대형" };
```

## 상태

```csharp
// 비활성화: 클릭에 응답하지 않음(Tapped 게이트가 차단), 비활성화 스타일 적용
new LhButton { Disabled = true, Text = "비활성화" };

// 로딩 중: ProgressRing 표시, Click을 트리거하지 않음
new LhButton { Loading = true, Text = "로딩 중" };

// 둘 다 지정된 경우: 비활성화가 우선(골든 케이스 disabled-precedes-loading)
new LhButton { Disabled = true, Loading = true, Text = "비활성화 우선" };
```

## 아이콘과 문양 배경

```csharp
// 아이콘 이름은 로컬 아이콘 체계에서 해석됨(LhButtonTokens.IconFor, 현재 search / refresh / check / close를 지원하며,
// 미등록 이름은 그려지지 않음)
new LhButton { Type = LhButtonType.Primary, Icon = "search", Text = "검색" };

// 중국풍 문양 배경(color.decoration 토큰을 소비)
new LhButton { Type = LhButtonType.Primary, BgPattern = true, Text = "문양 배경" };
```

## 라이트 / 다크 테마

```csharp
LhTheme.Apply(true);  // 전역을 다크로 전환. 모든 컴포넌트는 다음 외관 해석 시 자동으로 따라감
```

## 폼과 피드백 컴포넌트

```csharp
// 제어 입력: 호스트가 ValueChange를 수신하고 Value에 재기록
var input = new LhInput { Placeholder = "키워드를 입력하세요" };
input.ValueChange += value => input.Value = value;

// 체크박스
var checkbox = new LhCheckbox { Label = "青花瓷(청화백자)" };
checkbox.Change += isChecked => { };

// 스위치
var sw = new LhSwitch { Checked = true };
sw.Change += isChecked => { };

// 명령형 알림: 먼저 호스트 컨트롤에 배치한 뒤 컨트롤러로 구동
var toast = new LhToast();
toast.Close += () => { };
var panel = new StackPanel { Children = { toast } };
toast.Show("저장됨");   // 만료 후 자동으로 숨겨지고 Close를 트리거
```

## 컴포넌트 예제 미리보기

네이티브 UI는 브라우저에서 실시간 렌더링할 수 없으므로, 이 사이트는 **인라인 정적 HTML 폰 프레임 시뮬레이터**로 외관을 보여줍니다:

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>lh-ui-windows</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <button class="sim-btn">기본 버튼</button>
      <button class="sim-btn sim-btn--primary">주요 버튼</button>
      <button class="sim-btn sim-btn--success">성공</button>
      <button class="sim-btn sim-btn--warning">경고</button>
      <button class="sim-btn sim-btn--danger">위험</button>
      <button class="sim-btn sim-btn--info">정보</button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--small">소형</button>
      <button class="sim-btn sim-btn--primary sim-btn--large">대형</button>
      <button class="sim-btn sim-btn--primary sim-btn--disabled">비활성화</button>
      <button class="sim-btn sim-btn--primary sim-btn--loading">
        <span class="sim-btn__spinner"></span>로딩 중
      </button>
    </div>
    <div class="sim-row">
      <button class="sim-btn sim-btn--primary"><span class="sim-btn__icon"></span>검색</button>
      <button class="sim-btn sim-btn--primary sim-btn--pattern">문양 배경</button>
    </div>
  </div>
</div>
<p class="sim-note">Windows / WinUI 3 외형의 HTML 시뮬레이션 미리보기, 실기기 렌더링이 아님. 스타일 값은 모두 core의 토큰 스냅샷에서 가져왔으며 실기기와 동일한 소스, 동일한 값입니다. 실기기 외관은 데모 앱과 기기 스크린샷을 기준으로 합니다.</p>

> 시뮬레이터의 다크는 사이트와 연동됩니다: 우측 상단에서 테마를 전환하면 폰 프레임 내의 색상이 함께 바뀝니다(다크 토큰은 core가 미리 계산합니다).
