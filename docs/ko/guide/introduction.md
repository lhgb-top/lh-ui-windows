# 소개

**lh-ui-windows**는 lh-ui 패밀리의 **네이티브 혈통**에 속하는 Windows 구현체입니다. **C# + WinUI 3**(.NET 8, Windows App SDK) 기반의 UI 컴포넌트 라이브러리입니다.

## 패밀리 내 위치

```
                        ┌── Web 혈통: share(TS core + SCSS) → plus / vue2 / react / uniapp
lh-ui 패밀리(동일 브랜드)─────┤
                        └── 네이티브 혈통: core(JSON 토큰 + 계약 + codegen) → android / ios / harmony / windows
                                                                    ↑ 이 저장소
```

- **빌드 시간의 중심**: `lh-ui-core`는 네이티브 혈통의 토큰 원본(`tokens/*.json`), 컴포넌트 계약(`specs/`), 골든 케이스(`cases/`), codegen 도구입니다. C# 토큰 골격(`LhTokens` / `LhDarkTokens` / `LhShadow` / `LhEasing`)을 생성하지만, **컴포넌트 구현은 생성하지 않습니다**.
- **이 저장소의 역할**: WinUI 3 컴포넌트 직접 작성, 테마 연결(`LhTheme.Apply` + `ThemeChanged` 이벤트), 프로퍼티 열거형과 기본값 바인딩, 골든 케이스의 xUnit 테스트 번역.
- **Web 혈통과는 서로 독립**: `@lh-ui/share`에 의존하지 않으며, 릴리스 순서의 제약도 없습니다. 시각적 일관성은 *디자인 언어 기준선*에 수동으로 맞춰 유지합니다.

> **역사**: 이 저장소는 2026-10-12에 기존의 `lh-ui-c`(C/C++ 임베디드 엔드)를 대체했습니다. 네이티브 혈통의 네 번째 멤버가 임베디드 엔드에서 Windows 데스크톱 엔드로 변경되었습니다(마스터 플랜 D10).

## 디자인 언어

중국풍 테마: 青花瓷(청화백자) 블루 메인 컬러(`#2B4C7C`), 宣纸白(선지 흰색) 배경(`#FAF6F0`), 성공은 松花绿(송화 녹색), 경고는 藤黄(등황), 위험은 朱砂(주사). 세리프 폰트 스택(Noto Serif SC → STSong → SimSun). 모든 시각적 값이 토큰이며, 컴포넌트에는 어떤 리터럴도 등장하지 않습니다. 시각 표현은 Fluent의 기하학적 절제로 마무리합니다(토큰 구동의 코너 반경 계층과 얇은 테두리).

## 왜 WinUI 3인가

- Windows App SDK의 모던한 선언형 UI로, Compose / SwiftUI / ArkUI와 사고 모델이 동형이라 크로스 플랫폼 계약을 맞추기 쉽습니다
- Web 런타임 없음, HTML/CSS 브리지 없음. 네이티브 성능
- NuGet 배포(nM6 개통 후)로 .NET 에코시스템에 원클릭 통합

## 현재 상태

- **P0 전체 15개 컴포넌트가 제공됨**(토큰 소비 + resolve 순수 함수 외관 해석 + WinUI 3 컨트롤 + xUnit 골든 케이스 + 데모 앱 + 문서 사이트): button / icon / tag / cell / text / input / textarea / checkbox / radio / switch / picker / dialog / toast / loading / navbar
