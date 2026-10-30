# Loading 로딩

원형 회전 로딩 인디케이터입니다. 크기와 시맨틱 색조를 지원합니다.

> 계약 출처: `lh-ui-core/specs/loading.json` ·
> 골든 케이스: `lh-ui-core/cases/loading.json` (5건, xUnit 테스트로 번역됨)

::: tip 플랫폼 메모
회전은 WinUI의 `ProgressRing` 플랫폼 애니메이션이 담당합니다(담백한 톤: 지오미트리만 내보내고 애니메이션은 플랫폼이 채움).
토큰 계층은 지름, 테두리 두께, 링 색상만을 결정합니다.
:::

## 크기

3단계 크기가 `loading.size.*` 지름 토큰에 매핑됩니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Loading 크기</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--small"></span>
      <span class="sim-loading"></span>
      <span class="sim-loading sim-loading--large"></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var loading = new LhLoading
{
    Size = LhLoadingSize.Large,
    Tone = LhLoadingTone.Primary,
};
```

## 색조

`Tone`이 링 색상을 결정하며, 시맨틱 색 토큰과 1:1로 대응합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Loading 색조</span></div>
  <div class="sim-screen">
    <div class="sim-row">
      <span class="sim-loading sim-loading--primary"></span>
      <span class="sim-loading sim-loading--success"></span>
      <span class="sim-loading sim-loading--warning"></span>
    </div>
    <div class="sim-row">
      <span class="sim-loading sim-loading--danger"></span>
      <span class="sim-loading sim-loading--info"></span>
    </div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `Size` | 인디케이터 지름, `loading.size.*` 토큰에 매핑 | `LhLoadingSize` | `Medium` |
| `Tone` | 링 색상, 대응하는 시맨틱 색 토큰 사용 | `LhLoadingTone` | `Primary` |

### LhLoadingSize

| 값 | 지름 토큰 |
| --- | --- |
| `Small` | `loading.size.small` (20epx) |
| `Medium` | `loading.size.medium` (28epx) |
| `Large` | `loading.size.large` (36epx) |

### LhLoadingTone

| 값 | 링 색상 토큰 |
| --- | --- |
| `Primary` | `color.primary.default` |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `loading.size.{small,medium,large}` | 3단계 지름 |
| `loading.strokeWidth` | 로딩 링 테두리 두께 |
| `color.{primary,success,warning,danger,info}.default` | 5가지 링 색상 |

## 골든 케이스 매핑

`cases/loading.json`의 5건 어서션과 xUnit 테스트의 대응 관계(`tests/LhLoadingTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `medium-size-uses-medium-token` | 중형이 medium 지름 토큰을 소비 | `Medium_Size_Uses_Medium_Token` |
| `large-size-uses-large-token` | 대형이 large 지름 토큰을 소비 | `Large_Size_Uses_Large_Token` |
| `primary-tone-uses-primary-token` | 주요 색조가 주요 색 토큰을 사용 | `Primary_Tone_Uses_Primary_Token` |
| `success-tone-uses-success-token` | 성공 색조가 성공 색 토큰을 사용 | `Success_Tone_Uses_Success_Token` |
| `render-emits-ring-command` | RING 명령이 정확히 하나 발생 | `Render_Emits_Ring_Command` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
