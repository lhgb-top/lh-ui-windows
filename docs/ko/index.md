---
layout: home

hero:
  image:
    src: /logo-banner.png
    alt: lh-ui 品牌横幅
  name: lh-ui-windows
  text: 네이티브 혈통 · Windows 엔드
  tagline: C# + WinUI 3 컴포넌트 라이브러리(.NET 8 / Windows App SDK), 토큰 구동, 계약 정렬, 골든 케이스로 보호
  actions:
    - theme: brand
      text: 빠른 시작
      link: /ko/guide/quickstart
    - theme: alt
      text: 컴포넌트 개요
      link: /ko/components/button

features:
  - title: 토큰 구동
    details: 컴포넌트에 하드코딩된 색상이나 간격이 없으며, 모두 core codegen이 생성한 LhTokens / LhDarkTokens를 소비합니다. 토큰을 변경하면 전체 엔드에 즉시 반영됩니다.
  - title: 계약 일관성
    details: 프로퍼티 이름이 lh-ui-core/specs와 문자 그대로 일치하며, Android / iOS / HarmonyOS 엔드 및 Web 혈통과 동작을 공유합니다.
  - title: 골든 케이스 보호
    details: cases/*.json의 동작 어서션을 xUnit 테스트로 번역하여 dotnet test 한 줄로 회귀할 수 있습니다.
  - title: 라이트 / 다크 듀얼 테마
    details: LhTheme.Apply 한 줄로 전환(정적 색판 + ThemeChanged 이벤트 핫 리프레시). 다크 토큰은 core가 미리 계산하며 런타임 조색이 없습니다.
  - title: 중국풍 디자인 언어
    details: 青花瓷(청화백자) 블루 메인 컬러, 宣纸白(선지 흰색) 배경, 藤黄(등황)과 朱砂(주사) 포인트, 세리프 폰트 스택. Web 혈통과 동일한 소스, 동일한 값.
  - title: WinUI 3 네이티브
    details: 순수 C# + WinUI 3로 직접 작성한 UI. JS Web 런타임이 없으며 Fluent 디자인 체계의 일등 시민입니다.
---
