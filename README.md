# POOAspEntity

TP « Entity Framework Core, ASP.NET Core et .NET MAUI » — Enseignement .NET, DIC3 Génie Informatique, ESP / UCAD, 2026-2027.

- **Étudiant** : Mohamed LO (GitHub : Mohamedlo10)
- **Environnement de travail** : MacBook (Apple Silicon, macOS 26), VS Code avec l'extension C# Dev Kit, SDK .NET 10.0.403, workload .NET MAUI 10.0.110, Xcode 26.0.1. Le TP est prévu pour Visual Studio 2026 : l'équivalent Visual Studio est indiqué quand c'est utile.

## Sommaire

1. [Contenu de la solution](#1-contenu-de-la-solution)
2. [P5AspEntity : API minimale, EF Core et SQLite](#2-p5aspentity--api-minimale-ef-core-et-sqlite)
3. [P8Maui : application .NET MAUI](#3-p8maui--application-net-maui)
4. [Captures Microsoft Learn](#4-captures-microsoft-learn)

---

## 1. Contenu de la solution

La solution `POOAspEntity.slnx` (créée vide avec `dotnet new sln`, format `.slnx` par défaut sur .NET 10) contient deux projets :

| Projet | Type | Formation Microsoft Learn |
|---|---|---|
| **P5AspEntity** | API minimale ASP.NET Core (SDK `Microsoft.NET.Sdk.Web`) | Module [Utiliser une base de données avec une API minimale, Entity Framework Core et ASP.NET Core](https://learn.microsoft.com/fr-fr/training/modules/build-web-api-minimal-database/) |
| **P8Maui** | Application .NET MAUI (Android, iOS, Mac Catalyst, Windows) | Parcours [Créer des applications mobiles et de bureau avec .NET MAUI](https://learn.microsoft.com/fr-fr/training/paths/build-apps-with-dotnet-maui/) (8 modules) |

Le fichier `dotnet-tools.json` déclare l'outil local `dotnet-ef` (version 10.0.15) utilisé pour les migrations : `dotnet tool restore` l'installe, sans installation globale.

## 2. P5AspEntity : API minimale, EF Core et SQLite

Le projet reprend l'application **PizzaStore** du module, en deux étapes (un commit chacune) :

1. **EF Core en mémoire** (`UseInMemoryDatabase("items")`) et documentation **Swagger**.
2. **SQLite** : chaîne de connexion `Pizzas` dans `appsettings.json`, `AddSqlite<PizzaDb>`, migration `InitialCreate` (dossier `Migrations/`).

| Fichier | Contenu |
|---|---|
| `Pizza.cs` | Entité `Pizza` (Id, Name, Description) et contexte `PizzaDb` (`DbSet<Pizza> Pizzas`) |
| `Program.cs` | Services (SQLite, Swagger) et routes de l'API |
| `Migrations/` | Migration `InitialCreate` générée par `dotnet ef migrations add` |

Routes de l'API :

| Verbe | Route | Effet |
|---|---|---|
| GET | `/pizzas` | Liste des pizzas |
| POST | `/pizza` | Ajoute une pizza (201 Created) |
| GET | `/pizza/{id}` | Une pizza (`null` si l'id n'existe pas, comme dans le module) |
| PUT | `/pizza/{id}` | Modifie le nom et la description (204, ou 404) |
| DELETE | `/pizza/{id}` | Supprime la pizza (200, ou 404) |

**Adaptations à .NET 10** (le module est écrit pour .NET 8) :

- `dotnet new web` sans `-f net8.0` : le SDK 10 crée un projet `net10.0`.
- Paquets EF Core figés en **10.0.15** (`InMemory`, `Sqlite`, `Design`) : la version 11, proposée par défaut, ne fonctionne qu'avec .NET 11.
- **Swashbuckle.AspNetCore 10.3.1** : depuis la version 10, `OpenApiInfo` est dans le namespace `Microsoft.OpenApi` (et non plus `Microsoft.OpenApi.Models`), d'où `using Microsoft.OpenApi;` dans `Program.cs`.

**Lancer l'API** :

```
cd P5AspEntity
dotnet tool restore
dotnet ef database update
dotnet run --launch-profile http
```

`dotnet ef database update` crée la base `Pizzas.db` (non versionnée). L'interface Swagger est alors sur http://localhost:5018/swagger. Dans Visual Studio : profil `http` puis F5.

Exemple de test en ligne de commande :

```
curl -X POST http://localhost:5018/pizza -H "Content-Type: application/json" -d '{"name":"Pepperoni","description":"A classic pepperoni pizza"}'
curl http://localhost:5018/pizzas
```

Les données sont conservées dans `Pizzas.db` : elles sont toujours là après un redémarrage de l'API.

## 3. P8Maui : application .NET MAUI

Le parcours Learn compte 8 modules, chacun avec sa propre application d'exercice. P8Maui les regroupe dans **une seule application** : un menu latéral (Shell) donne accès à une entrée par module, et le code de chaque module est dans son dossier.

| Menu | Module Learn | Dossier | Pages | À tester |
|---|---|---|---|---|
| 1 - Phoneword | [Créer une application multiplateforme avec .NET MAUI](https://learn.microsoft.com/fr-fr/training/modules/build-mobile-and-desktop-apps/) | `M1Phoneword/` | PhonewordPage | *Translate* : `1-555-NETMAUI` devient `1-555-6386284`, puis *Call* demande confirmation |
| 2 - Notes (XAML) | [Créer une interface utilisateur avec XAML](https://learn.microsoft.com/fr-fr/training/modules/create-user-interface-xaml/) | `M2Notes/` | NotesPage | *Save* enregistre la note dans `notes.txt`, elle est relue au lancement ; *Delete* l'efface |
| 3 - Disposition | [Personnaliser la disposition dans les pages XAML](https://learn.microsoft.com/fr-fr/training/modules/customize-xaml-pages-layout/) | `M3Disposition/` | AlignmentPage, TipCalculatorPage | Boutons Start/Center/End/Fill ; calcul du pourboire (Grid de 7 lignes) |
| 4 - Météo | [Liaison de données](https://learn.microsoft.com/fr-fr/training/modules/separate-ui-and-logic-with-data-binding/) | `M4Meteo/` | WeatherPage | *Refresh* remplit les champs liés et affiche l'image du temps (convertisseur) |
| 5 - Films (MVVM) | [Concevoir un ViewModel MVVM](https://learn.microsoft.com/fr-fr/training/modules/design-mvvm-viewmodel/) | `M5Films/` | MoviesPage, MovieDetailPage | Clic sur un film : page de détail ; glisser une ligne vers la gauche : *Delete* |
| 6 - Pourboire | [Ressources partagées](https://learn.microsoft.com/fr-fr/training/modules/use-shared-resources/) | `M6Pourboire/` | StandardTipPage, CustomTipPage | Boutons *Dark* / *Light* (DynamicResource) ; styles `baseLabelStyle` / `infoLabelStyle` |
| 7 - Astronomie | [Applications multipages avec onglets et menu volant](https://learn.microsoft.com/fr-fr/training/modules/create-multi-page-apps/) | `M7Astronomie/` | MoonPhasePage, SunrisePage, AstronomicalBodiesPage, AstronomicalBodyPage, AboutPage | Onglets à icônes ; *Bodies* ouvre le détail par la route `astronomicalbodydetails` ; *Sunrise* demande la position |
| 8 - Contacts (SQLite) | [Stocker des données locales avec SQLite](https://learn.microsoft.com/fr-fr/training/modules/store-local-data/) | `M8Contacts/` | PeoplePage | *Add Person* puis *Get All People* ; un nom en double est refusé ; les données restent après redémarrage |

Le code de chaque page est celui des exercices Learn (état final), avec leurs textes en anglais. Les adaptations nécessaires pour tout réunir dans une seule application .NET 10 :

- Chaque module a son dossier et son namespace (`P8Maui.M1Phoneword`, …) ; les images sont préfixées par le module (`m4_sunny.png`, `m7_moon.png`, …).
- `App.MainViewModel` (module 5) et `App.PersonRepo` (module 8) sont remplacés par des propriétés des classes `M5Setup` et `M8Setup`, pour ne pas mélanger les modules dans `App`. Les routes des pages de détail sont enregistrées par `M5Setup` et `M7Setup`, appelées dans `AppShell`.
- Les styles du module 6 sont dans `M6Pourboire/M6Styles.xaml`, fusionné dans `App.xaml` (styles avec clé uniquement, pour ne pas modifier les autres pages).
- .NET 10 : `DisplayAlertAsync`, `CollectionView` au lieu de `ListView` (obsolète), tailles de police numériques au lieu de `Medium` (obsolète), `object? sender` (types nullables activés).
- Module 7 : les nombres envoyés au service sunrise-sunset.org sont écrits avec un point même sur un Mac en français, et les erreurs (position refusée, réseau) sont affichées au lieu de faire planter la page.

**Lancer l'application sur Mac (Mac Catalyst)** :

```
cd P8Maui
dotnet build -t:Run -f net10.0-maccatalyst
```

Dans Visual Studio 2026 sous Windows : choisir la cible *Windows Machine* (ou un émulateur Android), puis F5.

**Xcode** : le workload MAUI installé (pack Mac Catalyst 27.2) demande Xcode 27.2, alors que Xcode 26.0.1 est installé sur le Mac de travail. Le projet désactive donc ce contrôle pour Mac Catalyst uniquement (`ValidateXcodeVersion=false`) et limite l'édition de liens aux bibliothèques du SDK (`MtouchLink=SdkOnly`), sans quoi la compilation échoue (erreur MT0180). Ces réglages n'ont aucun effet sur Windows et Android.

## 4. Captures Microsoft Learn

Les captures demandées par le TP (photo, titre de la formation et points obtenus sur Microsoft Learn) sont jointes au rendu sur Classroom :

- **P5AspEntity** : module *Utiliser une base de données avec une API minimale, Entity Framework Core et ASP.NET Core* terminé (« Évaluation du module réussie »).
- **P8Maui** : trophée du parcours *Créer des applications mobiles et de bureau avec .NET MAUI* (les 8 modules terminés).
