# GSB 2 - Système de Gestion Médicale

## Documentation Fonctionnelle

### Vue d'ensemble

GSB 2 est une application de bureau développée en C# utilisant Windows Forms pour la gestion médicale. Elle permet aux professionnels de santé de gérer les patients, les médicaments et les prescriptions de manière sécurisée et efficace.

### Rôles et Accès

L'application propose deux niveaux d'accès distincts :

#### Docteur (Role = false/0)
- Consultation des patients, médicaments et prescriptions
- Accès en lecture seule aux données
- Interface via `FormDoctor`

#### Administrateur (Role = true/1)
- Gestion complète (CRUD) des patients
- Gestion complète (CRUD) des médicaments
- Gestion complète (CRUD) des prescriptions
- Attribution des médicaments aux prescriptions avec quantités
- Interface via `FormAdmin`

### Fonctionnalités Principales

#### Authentification
- Connexion sécurisée via `MainForm`
- Validation par email et mot de passe
- Redirection automatique selon le rôle utilisateur

#### Gestion des Patients
- Création, modification et suppression de patients (Admin uniquement)
- Consultation de la liste complète des patients
- Recherche de patients par nom
- Informations stockées : nom, prénom, âge, sexe
- Filtrage par utilisateur créateur

#### Gestion des Médicaments
- Création, modification et suppression de médicaments (Admin uniquement)
- Consultation du catalogue de médicaments
- Recherche de médicaments par nom
- Informations stockées : nom, description, molécule, dosage
- Association avec l'utilisateur créateur

#### Gestion des Prescriptions
- Création, modification et suppression de prescriptions (Admin uniquement)
- Association prescription-patient
- Date de validité pour chaque prescription
- Vérification des prescriptions expirées
- Attribution de médicaments avec quantités spécifiques

#### Association Prescription-Médicament
- Ajout de médicaments à une prescription avec quantité
- Modification des quantités
- Suppression de médicaments d'une prescription
- Consultation des médicaments d'une prescription
- Vérification des doublons
- Statistiques (nombre de médicaments, quantité totale)

### Architecture Technique

Le projet suit une architecture en couches :

```
GSB_2/
├── Forms/          # Interfaces utilisateur
│   ├── MainForm    # Authentification
│   ├── FormAdmin   # Interface administrateur
│   └── FormDoctor  # Interface docteur
├── Models/         # Modèles de données
│   ├── User
│   ├── Patient
│   ├── Medicine
│   ├── Prescription
│   └── Appartient
└── DAO/           # Accès aux données
    ├── Database
    ├── UserDAO
    ├── PatientDAO
    ├── MedicineDAO
    ├── PrescriptionDAO
    └── AppartientDAO
```

### Base de Données

**Serveur :** MySQL (localhost)  
**Base :** GSB2  
**Tables principales :**
- `Users` - Utilisateurs du système
- `Patients` - Patients enregistrés
- `Medicine` - Catalogue de médicaments
- `Prescription` - Prescriptions médicales
- `Appartient` - Table de liaison prescription-médicament (avec quantités)

### Technologies Utilisées

- **Langage :** C# (.NET Framework)
- **Interface :** Windows Forms
- **Base de données :** MySQL
- **Connecteur :** MySql.Data.MySqlClient

### Prérequis

- .NET Framework
- MySQL Server
- MySql.Data NuGet package
- Base de données GSB2 configurée

### Installation

1. Cloner le projet
2. Installer MySQL Server
3. Créer la base de données GSB2
4. Configurer la chaîne de connexion dans `Database.cs`
5. Compiler et exécuter le projet

### Utilisation

1. Lancer l'application via `MainForm`
2. Se connecter avec vos identifiants (email/mot de passe)
3. Accéder aux fonctionnalités selon votre rôle
4. Gérer les patients, médicaments et prescriptions

---

## Documentation Technique Complète

Pour une documentation technique détaillée incluant :
- Schéma de la base de données
- Description des modèles
- API des DAO avec exemples de code
- Diagrammes UML
- Guide de développement

**[Consulter la Documentation Technique](./TECHNICAL_DOCUMENTATION.md)**

---

## Sécurité

- Authentification obligatoire
- Contrôle d'accès basé sur les rôles
- Validation des permissions avant chaque opération
- Protection contre les injections SQL via paramètres