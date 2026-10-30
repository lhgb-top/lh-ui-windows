# Navbar 내비게이션 바

페이지 상단의 고정 막대입니다. 좌 / 중 / 우 3단 구성이며, 글자와 아이콘을 지원합니다.

> 계약 출처: `lh-ui-core/specs/navbar.json` ·
> 골든 케이스: `lh-ui-core/cases/navbar.json` (6건, xUnit 테스트로 번역됨)

## 기본 사용법

계층은 `zIndex.sticky`를 사용하며, 호스트가 이를 페이지 상단의 고정 막대로 사용합니다(Grid 첫 행 또는 Canvas 상단).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar 기본</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">← 뒤로</span>
      <span class="sim-navbar__title">青花瓷(청화백자)</span>
      <span class="sim-navbar__right">더보기</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var navbar = new LhNavbar
{
    Title = "青花瓷(청화백자)",
    LeftText = "뒤로",
    LeftIcon = "back",
    RightText = "더보기",
};
navbar.LeftClick += () => { };
navbar.RightClick += () => { };
```

## 아이콘

`LeftIcon` / `RightIcon`에 로컬 아이콘 이름을 전달하며(Icon 문서의 글리프 표 참조), 글자와 아이콘 중 어느 하나라도 비어 있지 않으면 모두 표시합니다.
양쪽에 내용이 없는 경우 대응하는 클릭 이벤트를 트리거하지 않습니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar 아이콘</span></div>
  <div class="sim-screen">
    <div class="sim-navbar">
      <span class="sim-navbar__left">←</span>
      <span class="sim-navbar__title">아이콘만</span>
      <span class="sim-navbar__right">⋯</span>
    </div>
    <div class="sim-navbar">
      <span class="sim-navbar__title">양쪽 콘텐츠 없음</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 구분선

`Bordered`가 true인 경우 하단에 `color.border.lighter` 구분선을 그립니다. 기본값은 켜짐입니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Navbar 구분선</span></div>
  <div class="sim-screen">
    <div class="sim-navbar sim-navbar--bordered">
      <span class="sim-navbar__title">구분선 있음</span>
    </div>
    <div class="sim-navbar sim-navbar--borderless">
      <span class="sim-navbar__title">구분선 없음</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `LeftClick` | 좌측 영역 클릭 시 트리거(좌측 내용이 없으면 트리거하지 않음) | `event Action` | — |
| `RightClick` | 우측 영역 클릭 시 트리거(우측 내용이 없으면 트리거하지 않음) | `event Action` | — |
| `Title` | 중앙 제목 | `string` | `""` |
| `LeftText` | 좌측 버튼 글자. LeftIcon과 어느 하나라도 비어 있지 않으면 모두 표시 | `string` | `""` |
| `LeftIcon` | 좌측 아이콘 이름(로컬 아이콘 체계에서 렌더링) | `string?` | `null` |
| `RightText` | 우측 버튼 글자. RightIcon과 어느 하나라도 비어 있지 않으면 모두 표시 | `string` | `""` |
| `RightIcon` | 우측 아이콘 이름(로컬 아이콘 체계에서 렌더링) | `string?` | `null` |
| `Bordered` | 하단 구분선 그리기 여부 | `bool` | `true` |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `navbar.height` | 높이 |
| `navbar.paddingHorizontal` | 좌우 패딩 |
| `navbar.titleFontSize` | 제목과 양쪽 글자의 폰트 크기 |
| `navbar.iconSize` | 양쪽 아이콘 크기 |
| `navbar.borderWidth` | 하단 구분선 두께 |
| `color.background.default` | 배경색 |
| `color.text.primary` | 제목과 양쪽 글자 색상 |
| `color.border.lighter` | 하단 구분선 색상 |
| `zIndex.sticky` | 고정 계층 |
| `spacing.xs` | 아이콘과 글자 사이의 간격 |
| `typography.fontFamily` | 중국풍 세리프 폰트 스택 |

## 골든 케이스 매핑

`cases/navbar.json`의 6건 어서션과 xUnit 테스트의 대응 관계(`tests/LhNavbarTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `title-renders-text` | 제목이 폰트 크기 토큰에 따라 렌더링 | `Title_Renders_Text` |
| `leftclick-emits-onleftclick` | 좌측 클릭이 onLeftClick을 트리거 | `LeftClick_Emits_OnLeftClick` |
| `rightclick-emits-onrightclick` | 우측 클릭이 onRightClick을 트리거 | `RightClick_Emits_OnRightClick` |
| `no-left-content-no-onleftclick` | 좌측 내용이 없으면 트리거하지 않음 | `No_Left_Content_No_OnLeftClick` |
| `bordered-uses-border-token` | 구분선이 border.lighter 토큰을 소비 | `Bordered_Uses_Border_Token` |
| `height-uses-navbar-height-token` | 높이가 navbar.height 토큰을 소비 | `Height_Uses_Navbar_Height_Token` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
