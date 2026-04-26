# GSB 2 - Système de Gestion Médicale

Application de bureau développée en **C# / Windows Forms** pour la gestion médicale (patients, médicaments, prescriptions).

---

## Comptes de démonstration

| Rôle           | Email                     | Mot de passe |
|----------------|---------------------------|--------------|
| Docteur        | alice.martin@gsb.fr       | 123          |
| Administrateur | hugo.durand@gsb.fr        | 123          |

> Ces comptes sont insérés automatiquement par le script SQL fourni dans `/sql/gsb2.sql`.

---

## Prérequis

| Composant          | Version requise              |
|--------------------|------------------------------|
| .NET Framework     | 4.8                          |
| Visual Studio      | 2022 (Community ou supérieur)|
| MySQL Server       | 8.0 ou supérieur             |
| NuGet MySql.Data   | 9.x (MySql.Data.MySqlClient) |

---

## Installation

### 1. Cloner le dépôt

```bash
git clone https://github.com/Dylan-f1/GSB_2.git
cd GSB_2
```

### 2. Créer la base de données

Dans MySQL (phpMyAdmin, MySQL Workbench ou CLI) :

```sql
SOURCE sql/gsb2.sql;
```

Ce script crée automatiquement la base `GSB2`, toutes les tables et insère les données de démo.

### 3. Configurer la connexion

Ouvrir `GSB_2/App.config` et modifier les valeurs `uid` et `pwd` selon votre installation MySQL :

```xml
<add name="GSB2"
     connectionString="server=localhost;port=3306;uid=root;pwd=;database=GSB2;charset=utf8mb4"
     providerName="MySql.Data.MySqlClient" />
```

Par défaut : utilisateur `root` sans mot de passe (installation standard MySQL).

### 4. Ouvrir et compiler

1. Ouvrir `GSB_2.sln` dans Visual Studio 2022
2. Restaurer les packages NuGet : clic droit sur la solution → *Restaurer les packages NuGet*
3. Compiler (`Ctrl+Shift+B`) puis exécuter (`F5`)

---

## Rôles et Accès

#### Docteur (role = 0)
- Consultation des patients, médicaments et prescriptions
- Accès en **lecture seule**
- Interface via `FormDoctor`

#### Administrateur (role = 1)
- Gestion complète **CRUD** des patients, médicaments et prescriptions
- Attribution des médicaments aux prescriptions avec quantités
- Interface via `FormAdmin`

---

## Fonctionnalités Principales

- **Authentification** : connexion sécurisée, redirection automatique selon le rôle
- **Patients** : création, modification, suppression, recherche par nom
- **Médicaments** : catalogue complet, recherche, dosage et molécule
- **Prescriptions** : création, association patient/médicament, date de validité
- **Association Prescription-Médicament** : gestion des quantités, détection des doublons, statistiques

---

## Architecture Technique

```
GSB_2/
├── Forms/          # Interfaces utilisateur (MainForm, FormAdmin, FormDoctor)
├── Models/         # Modèles de données (User, Patient, Medicine, Prescription, Appartient)
├── DAO/            # Accès aux données (Database, UserDAO, PatientDAO, MedicineDAO…)
└── Utils/          # Utilitaires (ExporterPDF)
```

## Base de Données

**Serveur :** MySQL 8.0+ (localhost:3306)  
**Base :** `GSB2`

| Table          | Description                                     |
|----------------|-------------------------------------------------|
| `Users`        | Utilisateurs (email, rôle, password SHA-256)    |
| `Patients`     | Dossiers patients                               |
| `Medicine`     | Catalogue de médicaments                        |
| `Prescription` | Prescriptions médicales                         |
| `Appartient`   | Liaison prescription ↔ médicament (+ quantité) |

---

## Sécurité

- Authentification obligatoire
- Contrôle d'accès basé sur les rôles
- Mots de passe stockés en SHA-256
- Protection contre les injections SQL via paramètres MySqlCommand
- Chaîne de connexion externalisée dans `App.config`

---

## Documentation Technique

Pour la documentation complète (schéma BDD, API des DAO, diagrammes UML, guide de développement) :

**[Consulter TECHNICAL_DOCUMENTATION.md](./TECHNICAL_DOCUMENTATION.md)**
