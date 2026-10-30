# 테마 및 다크 모드

## 토큰 모델

네이티브 혈통의 토큰 원본은 `lh-ui-core/tokens/*.json`에 있으며, codegen이 Windows 산출물(`src/Tokens/`, 생성물은 gitignore 대상, `pnpm link:tokens`가 동기화)을 생성합니다:

| 산출물 | 내용 | 다크 형태 |
| --- | --- | --- |
| `LhTokens` | 라이트 전체: Button 크기, Color 색판, Motion, Radius, Shadow, Spacing, Typography, ZIndex | — |
| `LhDarkTokens` | `Color` 서브클래스만(**병렬 객체** 모델) | 다크는 색상만 덮어쓰고 크기 / 모션 / 코너 반경은 다크에서도 변하지 않음 |
| `LhShadow` | 그림자 운반체(color/radius/offsetX/offsetY/spread) | — |
| `LhEasing` | 모션 이징 | — |

> 이 구조는 android / ios / harmony와 동일합니다(병렬 객체). docs 엔드(얕은 병합 JSON)와는 형태가 다릅니다.
> 테마 계층은 이에 따라 설계되었습니다: `LhColors` 레코드는 색상만을 담고, 크기는 컴포넌트가 `LhTokens`에서 직접 가져옵니다.

## LhColors

`LhColorsFactory.Light()` / `Dark()`가 생성된 두 정적 클래스를 하나의 불변 레코드로 평탄화합니다:

```csharp
public sealed record LhColors
{
    public Color Primary { get; init; }
    public Color OnPrimary { get; init; }
    // ... 모든 시맨틱 색
}

public static class LhColorsFactory
{
    public static LhColors Light() => new()
    {
        Primary = LhTokens.Color.Primary.Default,
        // ...
    };

    public static LhColors Dark() => new()
    {
        Primary = LhDarkTokens.Color.Primary.Default,
        // ...
    };
}
```

팩토리 메서드는 공개되어 있습니다: xUnit 단위 테스트에서 색판을 직접 구성해 어서션할 수 있습니다(각 테스트의 `Dark_Colors_Come_From_Dark_Tokens` 참조). 앱 측에서는 `LhTheme`를 통해 소비합니다.

## LhTheme.Apply

```csharp
LhTheme.Apply(isDark);  // 색판을 전환하고 ThemeChanged를 트리거
```

컴포넌트는 `Loaded` 시 `LhTheme.ThemeChanged`를 구독하고 `Unloaded` 시 구독을 해제하며, 이벤트를 받으면 `ApplyAppearance()`를 호출해 다시 계산하고 다시 그립니다. **테마 핫 리프레시, 페이지 트리를 재구성할 필요 없음**. Apply한 적이 없는 경우 `LhTheme.Current`는 라이트 팔레트로 대체됩니다.

## 다크 사전 계산(중요)

Web 혈통의 다크는 CSS `color-mix()`로 동적 계산하지만, 네이티브에는 CSS 캐스케이드가 없으므로 core가 다크 표현식을
**리터럴로 미리 계산**해 둡니다(`#AARRGGBB` 형태의 `Color` 상수). 결과(마스터 플랜 R-n6):

> **프리셋 메인 컬러를 변경할 때 다크 사전 계산값은 자동으로 따라가지 않습니다.** 반드시 다시 수동으로 계산해서
> `lh-ui-core/tokens/color-dark.json`을 업데이트하고 *디자인 언어 기준선*을 새로 고쳐야 합니다.

## Windows 엔드 고유 규칙

- **크기 단위는 epx**: `LhEpx.Px(token)`이 토큰 수치를 유효 픽셀로 표시합니다(WinUI의 유효 픽셀은
  스케일 팩터에 따라 달라지며, 표시 함수를 통일적으로 사용하고 컴포넌트 내에는 노출된 숫자 리터럴이 없습니다).
- **색상 타입은 `Windows.UI.Color`**: `LhColors` 필드는 모두 `Color`이며, 브러시는 컴포넌트가
  `SolidColorBrush`로 감쌉니다. `#AARRGGBB`는 codegen이 `Color.FromArgb(...)`로 직접 출력합니다.
- **폰트 굵기 매핑 필요**: 토큰의 `typography.fontWeight.*`는 숫자이고 WinUI의 `FontWeights`는
  미리 정의된 상수이므로, `LhFontWeight.FromToken(token)`(`Common/LhFontWeight.cs`)을 통일적으로 사용합니다.
- **그림자는 Elevation**: Dialog 콘텐츠 카드 / Picker 패널은 `Border.Elevation` +
  `ThemeShadow`로 계층을 표현합니다(Fluent 언어: 버튼은 평면, 오버레이는 그림자 있음).
- **포커스 시각**: 입력 상자 포커스는 `focus` 색 + `input.focusRingWidth` 테두리를 사용합니다(WinUI의
  FocusVisualKind 고가시성 포커스 프레임에 대응).

## 토큰 빠른 참조

전체 토큰 표(라이트 / 다크 / 설명)는 core 저장소의 `dist/docs/tokens.md`(`pnpm codegen` 후 생성)를 참조하세요.
컴포넌트별 토큰 매핑은 [Button 토큰 매핑](/ko/components/button#토큰-매핑)을 참조하세요.
