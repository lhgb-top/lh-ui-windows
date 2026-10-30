# Tag 태그

표시와 선택에 사용하는 태그입니다. 6가지 시맨틱 유형, 3단계 크기, 아웃라인 형태, 코너 형태, 닫기 가능 형태를 지원합니다.

> 계약 출처: `lh-ui-core/specs/tag.json` ·
> 골든 케이스: `lh-ui-core/cases/tag.json` (6건, xUnit 테스트로 번역됨)

## 유형

6가지 유형이 배경색, 글자색, 테두리색을 결정합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Tag 유형</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag">기본</span>
      <span class="sim-tag sim-tag--primary">주요</span>
      <span class="sim-tag sim-tag--success">성공</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--warning">경고</span>
      <span class="sim-tag sim-tag--danger">위험</span>
      <span class="sim-tag sim-tag--info">정보</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var tag = new LhTag
{
    Text = "주요",
    Type = LhTagType.Primary,
};
```

## 아웃라인과 코너

`Plain`이 true인 경우 시맨틱 밝은 배경 + 시맨틱 글자 + 시맨틱 테두리로 전환됩니다. `Round`가 true인 경우
코너 반경이 높이의 절반이 됩니다(캡슐 형태, `tag.borderRadius` 토큰과 병립).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>아웃라인과 코너</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--plain">아웃라인</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">아웃라인</span>
      <span class="sim-tag sim-tag--danger sim-tag--plain">아웃라인</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary sim-tag--round">코너</span>
      <span class="sim-tag sim-tag--success sim-tag--round sim-tag--plain">코너 아웃라인</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 크기와 닫기

3단계 크기가 `tag.height` / `paddingHorizontal` / `fontSize` 토큰에 매핑됩니다. `Closable`이
true인 경우 우측에 닫기 기호를 그리고, 클릭하면 `Close` 이벤트를 트리거합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>크기와 닫기</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--small">소</span>
      <span class="sim-tag">중</span>
      <span class="sim-tag sim-tag--large">대</span>
    </div>
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">닫기 가능 <span class="sim-tag__close">✕</span></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var tag = new LhTag
{
    Text = "닫기 가능",
    Type = LhTagType.Primary,
    Closable = true,
};
tag.Close += () => { /* 닫기 기호 클릭 */ };
```

## 라이트 / 다크 테마

사이트 우측 상단에서 테마를 전환하면 시뮬레이터 폰 프레임 내의 태그 색상이 함께 바뀝니다. 실기기에서는 `LhTheme.Apply(true)`로 전환합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>다크 태그</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-tag sim-tag--primary">주요</span>
      <span class="sim-tag sim-tag--success sim-tag--plain">아웃라인</span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님. 라이트 / 다크 값은 모두 core의 토큰 스냅샷에서 가져옵니다</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `Close` | 닫기 기호 클릭 시 트리거(`Closable`이 true여야 함) | `event Action` | — |
| `Text` | 태그 글자 내용(spec의 default 슬롯) | `string` | `""` |
| `Type` | 시맨틱 유형, 배경색, 글자색, 테두리색을 결정 | `LhTagType` | `Default` |
| `Size` | 태그 크기, 크기 토큰에 매핑 | `LhTagSize` | `Medium` |
| `Plain` | 밝은 배경 아웃라인 형태 여부 | `bool` | `false` |
| `Round` | 캡슐 형태 여부(코너 반경이 높이의 절반) | `bool` | `false` |
| `Closable` | 닫기 기호 표시 여부 | `bool` | `false` |

### LhTagType

| 값 | 솔리드 배경 | 밝은 배경(plain) | 글자색 |
| --- | --- | --- | --- |
| `Default` | `color.background.default` | 동일 | `color.text.primary` |
| `Primary` | `color.primary.default` | `color.primary.bg` | `color.onPrimary` / `color.primary.default` |
| `Success` | `color.success.default` | `color.success.bg` | `color.onPrimary` / `color.success.default` |
| `Warning` | `color.warning.default` | `color.warning.bg` | `color.onPrimary` / `color.warning.default` |
| `Danger` | `color.danger.default` | `color.danger.bg` | `color.onPrimary` / `color.danger.default` |
| `Info` | `color.info.default` | `color.info.bg` | `color.onPrimary` / `color.info.default` |

### LhTagSize

| 값 | height | paddingHorizontal | fontSize |
| --- | --- | --- | --- |
| `Small` | `tag.height.small` (20epx) | `tag.paddingHorizontal.small` (8epx) | `tag.fontSize.small` (11) |
| `Medium` | `tag.height.medium` (24epx) | `tag.paddingHorizontal.medium` (12epx) | `tag.fontSize.medium` (12) |
| `Large` | `tag.height.large` (28epx) | `tag.paddingHorizontal.large` (16epx) | `tag.fontSize.large` (13) |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `tag.height.{small,medium,large}` | 3단계 높이 |
| `tag.paddingHorizontal.{small,medium,large}` | 3단계 가로 패딩 |
| `tag.fontSize.{small,medium,large}` | 3단계 폰트 크기 |
| `tag.borderRadius` / `tag.borderWidth` | 코너 반경과 테두리 두께 |
| `color.primary.default` / `color.onPrimary` | 주요 유형의 배경 / 글자 |
| `color.{success,warning,danger,info}.{default,bg}` | 시맨틱 유형의 솔리드 배경 / 밝은 배경 |
| `color.background.default` / `color.text.primary` / `color.border.base` | 기본 유형의 세 가지 색 |
| `spacing.xs` | 닫기 기호와 글자 사이의 간격 |
| `typography.fontFamily` | 중국풍 세리프 폰트 스택 |

## 골든 케이스 매핑

`cases/tag.json`의 6건 어서션과 xUnit 테스트의 대응 관계(`tests/LhTagTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `default-type-renders-text` | 기본 유형의 중성적인 배경 / 글자 / 테두리 | `Default_Type_Renders_Text` |
| `primary-type-uses-primary-tokens` | 주요 유형이 주요 토큰을 소비 | `Primary_Type_Uses_Primary_Tokens` |
| `plain-uses-bg-tokens` | 밝은 배경 형태가 시맨틱 밝은 배경 토큰을 소비 | `Plain_Uses_Bg_Tokens` |
| `small-size-uses-small-tokens` | 소형이 small 크기 토큰을 소비 | `Small_Size_Uses_Small_Tokens` |
| `round-makes-radius-half-height` | 캡슐 형태의 코너 반경이 높이의 절반 | `Round_Makes_Radius_Half_Height` |
| `closable-tap-emits-onclose` | 닫기 기호 클릭이 onClose를 트리거 | `Closable_Tap_Emits_OnClose` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
