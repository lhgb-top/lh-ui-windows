---
layout: home

hero:
  image:
    src: /logo-banner.png
    alt: lh-ui 品牌横幅
  name: lh-ui-windows
  text: Lignée native · volet Windows
  tagline: Bibliothèque de composants C# + WinUI 3 (.NET 8 / Windows App SDK), pilotée par les jetons, alignée sur les contrats et gardée par les cas dorés
  actions:
    - theme: brand
      text: Démarrage rapide
      link: /fr/guide/quickstart
    - theme: alt
      text: Aperçu des composants
      link: /fr/components/button

features:
  - title: Pilotée par les jetons
    details: "Aucune valeur de couleur ou d'espacement codée en dur dans les composants : tout consomme les LhTokens / LhDarkTokens générés par le codegen de core ; modifier un jeton se répercute immédiatement sur toutes les plateformes."
  - title: Contrats cohérents
    details: Le nommage des propriétés est aligné mot pour mot avec lh-ui-core/specs et partage les comportements avec les volets Android / iOS / HarmonyOS et la lignée web.
  - title: Gardée par les cas dorés
    details: Les assertions de comportement de cases/*.json sont traduites en tests xUnit, exécutables en régression avec une seule commande dotnet test.
  - title: Thèmes clair et sombre
    details: Bascule en une ligne avec LhTheme.Apply (palette statique + redessin à chaud sur l'événement ThemeChanged) ; les jetons sombres sont précalculés par core, aucun mélange de couleurs à l'exécution.
  - title: Langage de design style chinois
    details: Couleur principale en bleu 青花瓷 (porcelaine bleue et blanche), fond blanc 宣纸白 (blanc papier de riz), accents en 藤黄 (gamboge) et 朱砂 (cinabre), pile de polices avec empattements ; mêmes sources et mêmes valeurs que la lignée web.
  - title: WinUI 3 natif
    details: UI écrite à la main en C# pur + WinUI 3, sans runtime web JS ; citoyen de première classe du système de design Fluent.
---
