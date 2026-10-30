# Toast 알림

표시 여부와 타이머를 자체 관리하는 잉크색 캡슐 오버레이입니다. 시맨틱 유형, 자동 숨김, 위치 지정을 지원합니다.

> 계약 출처: `lh-ui-core/specs/toast.json` ·
> 골든 케이스: `lh-ui-core/cases/toast.json` (6건, xUnit 테스트로 번역됨)

## 기본 사용법

명령형 인터페이스(spec의 show / hide / tick)는 엔드 측에서 메서드 + 내부 타이머로 구현됩니다.
`Show(text)`는 표시하고 타이머를 재설정하며, `Hide()`는 즉시 숨기지만 `Close`를 트리거하지 않습니다.
타이머는 `DispatcherTimer`로 구동되며 만료 시 자동으로 숨기고 `Close`를 트리거합니다.

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast 기본</span></div>
  <div class="sim-screen">
    <div class="sim-toast">저장됨</div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

```csharp
var controller = new LhToastController();
var toast = new LhToast
{
    Duration = 2000,
    Position = LhToastPosition.Center,
    Controller = controller,
};
toast.Close += () => { /* 타이머 만료 후 자동 숨김 뒤 트리거 */ };

// 호스트는 toast를 페이지 Grid의 마지막 자식으로 배치해야 함(계층 zIndex.toast)
controller.Show("저장됨");
```

## 시맨틱 유형

`Type`이 default가 아닌 경우 글자 앞에 대응하는 시맨틱 색 원점을 그립니다(지름은 폰트 크기 사용).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast 유형</span></div>
  <div class="sim-screen">
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--success"></span>저장 성공</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--warning"></span>네트워크 연결을 확인하세요</div>
    <div class="sim-toast"><span class="sim-toast__dot sim-toast__dot--danger"></span>삭제 실패</div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## 위치

`Position`이 사용 가능 영역 내의 위치를 결정합니다. 상하 양끝에는 `spacing.xl` 여백을 둡니다(center인 경우 여백 없음).

<div class="sim-phone">
  <div class="sim-statusbar"><span>9:41</span><span>Toast 위치</span></div>
  <div class="sim-screen">
    <div class="sim-toast sim-toast--top">상단 알림</div>
    <div class="sim-toast sim-toast--bottom">하단 알림</div>
  </div>
</div>
<p class="sim-note">HTML 시뮬레이션 미리보기(Windows / WinUI 3 외형), 실기기 렌더링이 아님</p>

## API

### 속성

| 속성 | 설명 | 타입 | 기본값 |
| --- | --- | --- | --- |
| `Close` | 타이머 만료 후 자동 숨김 시 트리거. Hide()로 수동 숨김 시에는 트리거하지 않음 | `event Action` | — |
| `Type` | 시맨틱 유형. default가 아닌 경우 글자 앞에 대응하는 시맨틱 색 원점을 그림 | `LhToastType` | `Default` |
| `Duration` | 자동 숨김 시간(밀리초) | `int` | `2000` |
| `Position` | 사용 가능 영역 내의 위치 | `LhToastPosition` | `Center` |
| `Controller` | 호스트 호출 핸들(선택. Loaded 후 Show/Hide 등록) | `LhToastController?` | `null` |

### 메서드

| 메서드 | 설명 |
| --- | --- |
| `Show(string text)` | 알림 글자를 표시하고 자동 숨김 타이머를 재설정 |
| `Hide()` | 즉시 숨기지만 Close를 트리거하지 않음 |

### LhToastType

| 값 | 원점 색 |
| --- | --- |
| `Default` | 원점을 그리지 않음 |
| `Success` | `color.success.default` |
| `Warning` | `color.warning.default` |
| `Danger` | `color.danger.default` |
| `Info` | `color.info.default` |

### LhToastPosition

| 값 | 위치 |
| --- | --- |
| `Top` | 상단(상단에 `spacing.xl` 여백) |
| `Center` | 가운데(여백 없음) |
| `Bottom` | 하단(하단에 `spacing.xl` 여백) |

## 토큰 매핑

| 토큰 | 용도 |
| --- | --- |
| `toast.fontSize` | 글자 폰트 크기(색점 지름이기도 함) |
| `toast.paddingHorizontal` / `paddingVertical` | 캡슐 패딩 |
| `toast.borderRadius` | 캡슐 코너 반경 |
| `toast.gap` | 색점과 글자 사이의 간격 |
| `color.ink` | 캡슐 배경색 |
| `color.onPrimary` | 글자색 |
| `color.{success,warning,danger,info}.default` | 시맨틱 색점 |
| `zIndex.toast` | 오버레이 계층 |
| `spacing.xl` | 상하 양끝 여백 |
| `typography.fontFamily` | 중국풍 세리프 폰트 스택 |

## 골든 케이스 매핑

`cases/toast.json`의 6건 어서션과 xUnit 테스트의 대응 관계(`tests/LhToastTests.cs`):

| case id | 어서션 | 로컬 테스트 |
| --- | --- | --- |
| `show-makes-visible` | show 후 보이고 글자를 렌더링 | `Show_Makes_Visible` |
| `auto-hides-after-duration` | 타이머 만료 후 자동으로 숨기고 onClose를 트리거 | `Auto_Hides_After_Duration` |
| `tick-before-duration-keeps-visible` | 타이머 만료 전에는 계속 보임 | `Tick_Before_Duration_Keeps_Visible` |
| `hide-is-silent` | hide로 수동 숨김 시 onClose를 트리거하지 않음 | `Hide_Is_Silent` |
| `success-type-uses-success-token` | success 유형이 시맨틱 색과 잉크 색 토큰을 소비 | `Success_Type_Uses_Success_Token` |
| `hidden-emits-no-commands` | 보이지 않을 때 그리기 명령을 발생하지 않음 | `Hidden_Emits_No_Commands` |

다크 전용 테스트가 추가로 있습니다: `Dark_Colors_Come_From_Dark_Tokens` / `Dimensions_Are_Identical_In_Dark_Mode`.
실행: `cd lh-ui-windows && dotnet test`.
