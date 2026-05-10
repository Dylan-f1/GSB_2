# Feature : Migration transparente SHA-256 → BCrypt (Mission 3)

## Description

Remplacement du hachage SHA-256 des mots de passe par BCrypt, avec migration automatique et transparente pour les utilisateurs existants au moment de leur connexion.

## Pourquoi cette feature ?

SHA-256 est un algorithme de hachage **rapide**, ce qui le rend vulnérable aux attaques par dictionnaire et par force brute. BCrypt est conçu spécifiquement pour les mots de passe : il est **lent par design** (work factor configurable) et intègre un **salt automatique**, rendant les attaques massivement plus coûteuses.

La migration est **transparente** : les utilisateurs n'ont pas à réinitialiser leur mot de passe. La bascule se fait silencieusement à la première connexion après déploiement.

## Fichiers modifiés

| Fichier | Modification |
|---|---|
| `GSB_2/DAO/UserDAO.cs` | Logique de login avec migration automatique |
| `GSB_2/GSB_2.csproj` | Ajout du package NuGet `BCrypt.Net-Next` |
| `sql/migration_mission3.sql` | Script ALTER TABLE à exécuter en base |

## Comment ça fonctionne ?

### Script SQL (`migration_mission3.sql`)
```sql
ALTER TABLE Users
  MODIFY COLUMN password VARCHAR(100) NOT NULL,
  ADD COLUMN is_migrated TINYINT(1) NOT NULL DEFAULT 0;
```
Le flag `is_migrated = 0` signifie que l'utilisateur a encore son hash SHA-256. Il passe à `1` après la première connexion post-migration.

### Logique de connexion (`UserDAO.cs`)

```
1. Récupérer l'utilisateur par email (sans vérifier le mot de passe en SQL)
2. Si is_migrated = 0 :
   a. Vérifier le mot de passe avec SHA-256
   b. Si correct → rehacher en BCrypt (work factor 12) et mettre à jour la BDD
   c. Passer is_migrated = 1
3. Si is_migrated = 1 :
   a. Vérifier directement avec BCrypt.Verify()
```

### Avant / Après

| Aspect | Avant | Après |
|---|---|---|
| Algorithme | SHA-256 (rapide, sans salt) | BCrypt (lent, salt automatique) |
| Vérification SQL | `WHERE password = SHA2(@pwd, 256)` | En C# avec `BCrypt.Verify()` |
| Migration | — | Automatique à la 1ère connexion |
| Work factor | — | 12 (≈ 250ms par vérification) |

## Sécurité

- Le mot de passe en clair n'est **jamais stocké**
- Le salt BCrypt est **unique par utilisateur** et intégré dans le hash
- La connexion SQL ne transmet **plus** le mot de passe dans la requête
