# AuthApiCleanArchitecture

## Introduction

**AuthApiCleanArchitecture** est une API d'authentification conçue selon les principes de la **Clean Architecture** et du **Repository Pattern**. Ce projet utilise **ASP.NET Core** pour la création d'API RESTful avec une gestion des utilisateurs via **ASP.NET Core Identity** et **SQLite** comme base de données légère.

## Objectifs du projet

- **Authentification et Autorisation** : Fournir des fonctionnalités d'inscription et de connexion sécurisées.
- **Architecture propre** : Utiliser la Clean Architecture pour assurer une séparation claire des responsabilités et faciliter la maintenance.
- **Modularité** : Appliquer le Repository Pattern pour isoler les opérations d'accès aux données et faciliter les tests.
- **Documentation** : Offrir une API bien documentée grâce à Swagger.

---

## Architecture du Projet

### Clean Architecture

La Clean Architecture est un modèle d'architecture logiciel qui sépare les responsabilités en différentes couches. Ce modèle vise à :

- **Séparer les préoccupations** : Chaque couche a une responsabilité claire et n'interagit qu'avec les couches adjacentes.
- **Faciliter les tests** : En isolant les différentes parties du code, il est plus facile de tester les composants individuellement.
- **Rendre le code maintenable et évolutif** : Les changements dans une couche n'affectent pas directement les autres couches.

Les principales couches de la Clean Architecture sont :

1. **Domain** : Contient les entités métiers et les interfaces des repositories. Cette couche ne dépend d'aucune autre.
2. **Application** : Contient les services métiers et les cas d'utilisation. Elle orchestre les règles métiers et les interactions entre les couches.
3. **Infrastructure** : Implémente les détails techniques comme l'accès aux données et les intégrations avec des services externes.
4. **API (WebApi)** : Expose les services via des contrôleurs d'API REST.

### Repository Pattern

Le Repository Pattern est un modèle de conception qui vise à :

- **Isoler l'accès aux données** : Les opérations d'accès aux données sont regroupées dans des classes spécifiques (repositories) qui interagissent avec la source de données.
- **Simplifier les tests** : En isolant les détails de l'accès aux données, il est plus facile de tester les services métiers sans avoir à interagir avec une base de données réelle.
- **Faciliter les changements** : Les changements dans la source de données (comme le changement de base de données) n'affectent pas directement le reste de l'application.

### Combinaison de Clean Architecture et Repository Pattern

La combinaison de la Clean Architecture et du Repository Pattern permet de :

- **Organiser le code de manière modulaire** : Les responsabilités sont clairement séparées, ce qui facilite la gestion et l'évolution du code.
- **Assurer une séparation claire des préoccupations** : La Clean Architecture définit des couches avec des responsabilités distinctes, tandis que le Repository Pattern encapsule les opérations d'accès aux données.
- **Faciliter les tests et l'évolutivité** : En isolant les couches et les détails d'accès aux données, il est plus facile de tester, maintenir et étendre l'application.

---

## Installation et Configuration

### Prérequis

- **.NET 7 SDK**
- **SQLite** (inclus via `Microsoft.EntityFrameworkCore.Sqlite`)
- **Entity Framework Core** (pour la gestion de la base de données)
- **ASP.NET Core Identity** (pour la gestion des utilisateurs et de l'authentification)
- **Swagger** (pour la documentation de l'API)

### Étapes de Mise en Place

1. **Cloner le Projet**

    ```bash
    git clone https://github.com/votre-utilisateur/AuthApiCleanArchitecture.git
    cd AuthApiCleanArchitecture
    ```

2. **Restaurer les Packages**

    ```bash
    dotnet restore
    ```

3. **Configurer la Base de Données**

    Modifiez le fichier `appsettings.json` pour configurer la connexion SQLite :

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Data Source=authApi.db"
      }
    }
    ```

4. **Effectuer les Migrations**

    Si vous avez ajouté ou modifié des modèles, mettez à jour la base de données :

    ```bash
    dotnet ef migrations add InitialCreate --project Infrastructure --startup-project WebApi
    dotnet ef database update --project Infrastructure --startup-project WebApi
    ```

5. **Exécuter l'Application**

    ```bash
    dotnet run --project WebApi
    ```

    L'API sera accessible à l'adresse `https://localhost:5001` ou `http://localhost:5000`.

---

## Fonctionnalités de l'API

1. **Enregistrement d'Utilisateur (Register)**

    - **Route** : `POST /api/auth/register`
    - **Description** : Permet à un nouvel utilisateur de s'enregistrer.
    - **Exemple de Requête** :
        ```json
        {
          "email": "user@example.com",
          "password": "Password123!",
          "username": "username"
        }
        ```

2. **Connexion (Login)**

    - **Route** : `POST /api/auth/login`
    - **Description** : Permet à un utilisateur existant de se connecter et de recevoir un token JWT.
    - **Exemple de Requête** :
        ```json
        {
          "email": "user@example.com",
          "password": "Password123!"
        }
        ```

---
