# Cell 셀

리스트 행의 셀입니다. 좌측 아이콘 + 제목(필수 빨간 별표 가능) + 설명, 우측 값과 지시 화살표, 하단의 선택적 구분선으로 구성됩니다.

> 계약 출처: `lh-ui-core/specs/cell.json` ·
> 골든 케이스: `lh-ui-core/cases/cell.json` (6건, xUnit 테스트로 번역됨)

## 기본 사용법

제목은 `color.text.primary`, 값과 설명은 `color.text.secondary`를 사용하며, 세 가지 모두 토큰에 의해 구동됩니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Cell 기본</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">행 제목</div>
        <div class="sim-cell__label">설명 텍스트</div>
      </div>
      <div class="sim-cell__value">행 내용</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">구분선 있는 셀</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var cell = new LhCell
{
    Title = "행 제목",
    Label = "설명 텍스트",
    Value = "행 내용",
};
```

## 아이콘과 필수 표시

`Icon`에 로컬 아이콘 이름을 전달하면(Icon 문서의 글리프 표 참조) 좌측에 그려집니다.
`Required`가 true인 경우 제목 앞에 `color.danger.default` 색의 빨간 별을 그립니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>아이콘과 필수 표시</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <span class="sim-cell__icon">★</span>
      <div class="sim-cell__main">
        <div class="sim-cell__title">아이콘 포함</div>
      </div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title"><span class="sim-cell__required">✱</span>필수 항목</div>
      </div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 구분선과 클릭

`Bordered`가 true인 경우 하단에 `color.border.lighter` 구분선을 그립니다. `Clickable`이 true인 경우
우측에 지시 화살표를 그리고 클릭하면 `Click` 이벤트를 트리거합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>구분선과 클릭</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">클릭 가능</div></div>
      <div class="sim-cell__arrow">›</div>
    </div>
    <div class="sim-cell">
      <div class="sim-cell__main"><div class="sim-cell__title">클릭 불가</div></div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var cell = new LhCell
{
    Title = "클릭 가능",
    Clickable = true,
    Bordered = true,
};
cell.Click += () => { /* Clickable이 true일 때만 트리거 */ };
```

## 라이트 / 다크 테마

사이트 우측 상단에서 테마를 전환하면 시뮬레이터 폰 프레임 내의 글자색과 구분선 색상이 함께 바뀝니다. 실기기에서는 `LhTheme.Apply(true)`로 전환합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>다크 셀</span></div>
  <div class="sim-screen">
    <div class="sim-cell">
      <div class="sim-cell__main">
        <div class="sim-cell__title">행 제목</div>
        <div class="sim-cell__label">설명 텍스트</div>
      </div>
      <div class="sim-cell__value">행 내용</div>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님. 라이트 / 다크 값은 모두 core의 토큰 스냅샷에서 가져옵니다</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `Click` | 클릭 이벤트. `Clickable`이 true일 때만 트리거 | `event Action` | — |
| `Title` | 제목(spec의 title 슬롯) | `string` | `""` |
| `Label` | 제목 아래의 설명 텍스트(label 슬롯) | `string` | `""` |
| `Value` | 우측 값 텍스트(value 슬롯) | `string` | `""` |
| `Icon` | 좌측 아이콘 이름(로컬 아이콘 테이블에서 해석) | `string?` | `null` |
| `Required` | 제목 앞에 필수 빨간 별을 그릴지 여부 | `bool` | `false` |
| `Clickable` | 클릭 가능 여부(화살표 표시, Click 게이트) | `bool` | `false` |
| `Bordered` | 하단 구분선 그리기 여부 | `bool` | `true` |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `cell.paddingHorizontal` / `cell.paddingVertical` | 패딩 |
| `cell.gap` | 제목과 설명 사이의 수직 간격 |
| `cell.fontSizeTitle` / `fontSizeLabel` / `fontSizeValue` | 3단계 폰트 크기 |
| `color.text.{primary,secondary,regular}` | 제목 / 설명 / 값 / 아이콘 색 |
| `color.danger.default` | 필수 빨간 별 |
| `color.border.lighter` | 하단 구분선 |
| `icon.size.medium` | 좌측 아이콘 크기 |
| `spacing.{sm,xs}` | 아이콘 간격 / 내부 요소 간격 |
| `typography.lineHeight.primary` | 글자 줄 높이 |

## 골든 케이스 매핑

`cases/cell.json`의 6건 어서션과 xUnit 테스트의 대응 관계(`tests/LhCellTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `title-and-value-render` | 제목과 값이 모두 렌더링됨 | `Title_And_Value_Render` |
| `label-renders-secondary-tone` | 설명 텍스트가 보조 글자색 토큰을 사용 | `Label_Renders_Secondary_Tone` |
| `required-uses-danger-token` | 필수 빨간 별이 위험 색 토큰을 소비 | `Required_Uses_Danger_Token` |
| `bordered-emits-line-command` | 구분선 형태가 LINE 명령을 발생 | `Bordered_Emits_Line_Command` |
| `clickable-emits-onclick` | 클릭 가능할 때 클릭이 onClick을 트리거 | `Clickable_Emits_OnClick` |
| `not-clickable-no-onclick` | 클릭 불가일 때 onClick을 트리거하지 않음 | `Not_Clickable_No_OnClick` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
