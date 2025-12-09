# GSB 2 - Documentation Technique

## 📐 Architecture du Projet

### Vue d'ensemble de l'architecture

GSB 2 suit une architecture en couches (Layered Architecture) séparant clairement la présentation, la logique métier et l'accès aux données.

```
┌─────────────────────────────────────┐
│     Couche Présentation (Forms)     │
│   MainForm, FormAdmin, FormDoctor   │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Couche Modèles (Models)        │
│  User, Patient, Medicine,           │
│  Prescription, Appartient           │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│    Couche Accès Données (DAO)       │
│  UserDAO, PatientDAO, MedicineDAO,  │
│  PrescriptionDAO, AppartientDAO     │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│       Base de Données MySQL         │
│            GSB2 Database            │
└─────────────────────────────────────┘
```

---

## 🗄️ Schéma de Base de Données

### Modèle Relationnel

```sql
┌─────────────────────┐
│       Users         │
├─────────────────────┤
│ PK id_user (INT)    │
│    firstname (TEXT) │
│    name (TEXT)      │
│    email (TEXT)     │
│    password (TEXT)  │
│    role (BOOLEAN)   │
└─────────┬───────────┘
          │
          │ 1
          │
          │ N
┌─────────▼───────────┐         ┌─────────────────────┐
│      Patients       │         │      Medicine       │
├─────────────────────┤         ├─────────────────────┤
│ PK id_patient (INT) │         │ PK id_medicine (INT)│
│ FK id_user (INT)    │         │ FK id_user (INT)    │
│    name (TEXT)      │         │    name (TEXT)      │
│    firstname (TEXT) │         │    description (TXT)│
│    age (INT)        │         │    molecule (TEXT)  │
│    gender (TEXT)    │         │    dosage (INT)     │
└─────────┬───────────┘         └─────────┬───────────┘
          │                               │
          │ 1                             │
          │                               │
          │ N                             │ N
┌─────────▼────────────┐                 │
│    Prescription      │                 │
├──────────────────────┤                 │
│ PK id_prescription   │                 │
│    (INT)             │                 │
│ FK id_user (INT)     │                 │
│ FK id_patient (INT)  │                 │
│    validity (DATE)   │                 │
└─────────┬────────────┘                 │
          │                              │
          │ N                            │ N
          │                              │
          └──────────┬───────────────────┘
                     │
          ┌──────────▼──────────┐
          │     Appartient      │
          ├─────────────────────┤
          │ PK,FK id_prescription│
          │ PK,FK id_medicine    │
          │       quantity (INT) │
          └─────────────────────┘
```

### Scripts SQL de Création

```sql
-- Table Users
CREATE TABLE Users (
    id_user INT AUTO_INCREMENT PRIMARY KEY,
    firstname VARCHAR(100) NOT NULL,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    role BOOLEAN NOT NULL DEFAULT 0
    -- role: 0 = Docteur, 1 = Admin
);

-- Table Patients
CREATE TABLE Patients (
    id_patient INT AUTO_INCREMENT PRIMARY KEY,
    id_user INT NOT NULL,
    name VARCHAR(100) NOT NULL,
    firstname VARCHAR(100) NOT NULL,
    age INT NOT NULL,
    gender VARCHAR(10) NOT NULL,
    FOREIGN KEY (id_user) REFERENCES Users(id_user) ON DELETE CASCADE
);

-- Table Medicine
CREATE TABLE Medicine (
    id_medicine INT AUTO_INCREMENT PRIMARY KEY,
    id_user INT NOT NULL,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    molecule VARCHAR(255),
    dosage INT NOT NULL,
    FOREIGN KEY (id_user) REFERENCES Users(id_user) ON DELETE CASCADE
);

-- Table Prescription
CREATE TABLE Prescription (
    id_prescription INT AUTO_INCREMENT PRIMARY KEY,
    id_user INT,
    id_patient INT,
    validity DATE NOT NULL,
    FOREIGN KEY (id_user) REFERENCES Users(id_user) ON DELETE SET NULL,
    FOREIGN KEY (id_patient) REFERENCES Patients(id_patient) ON DELETE CASCADE
);

-- Table Appartient (Many-to-Many)
CREATE TABLE Appartient (
    id_prescription INT NOT NULL,
    id_medicine INT NOT NULL,
    quantity INT NOT NULL DEFAULT 1,
    PRIMARY KEY (id_prescription, id_medicine),
    FOREIGN KEY (id_prescription) REFERENCES Prescription(id_prescription) ON DELETE CASCADE,
    FOREIGN KEY (id_medicine) REFERENCES Medicine(id_medicine) ON DELETE CASCADE
);
```

---

## 📦 Modèles de Données (Models)

### User.cs

**Description :** Représente un utilisateur du système (Docteur ou Administrateur)

**Propriétés :**
| Propriété | Type | Description |
|-----------|------|-------------|
| Id_user | int | Identifiant unique de l'utilisateur |
| Firstname | string | Prénom de l'utilisateur |
| Name | string | Nom de l'utilisateur |
| Email | string | Email de connexion (unique) |
| Password | string | Mot de passe |
| Role | bool | false (0) = Docteur, true (1) = Admin |

**Constructeurs :**
```csharp
// Constructeur par défaut
public User()

// Constructeur avec paramètres
public User(int id_user, string firstname, string name, 
            string email, string password, bool role)
```

---

### Patient.cs

**Description :** Représente un patient enregistré dans le système

**Propriétés :**
| Propriété | Type | Description |
|-----------|------|-------------|
| Id_patient | int | Identifiant unique du patient |
| Id_user | int | ID de l'utilisateur créateur |
| Name | string | Nom du patient |
| Firstname | string | Prénom du patient |
| Age | int | Âge du patient |
| Gender | string | Sexe du patient |

**Constructeurs :**
```csharp
// Constructeur par défaut
public Patient()

// Constructeur avec paramètres
public Patient(int id_patient, int id_user, string name, 
               string firstname, int age, string gender)
```

---

### Medicine.cs

**Description :** Représente un médicament du catalogue

**Propriétés :**
| Propriété | Type | Description |
|-----------|------|-------------|
| Id_medicine | int | Identifiant unique du médicament |
| Id_user | int | ID de l'utilisateur créateur |
| Dosage | int | Dosage du médicament |
| Name | string | Nom du médicament |
| Description | string | Description du médicament |
| Molecule | string | Molécule active |

**Constructeurs :**
```csharp
// Constructeur par défaut
public Medicine()

// Constructeur avec paramètres
public Medicine(int id_medicine, int id_user, int dosage, 
                string name, string description, string molecule)
```

---

### Prescription.cs

**Description :** Représente une prescription médicale

**Propriétés :**
| Propriété | Type | Description |
|-----------|------|-------------|
| Id_prescription | int | Identifiant unique de la prescription |
| Id_user | int | ID du médecin prescripteur |
| Id_patient | int | ID du patient |
| Validity | DateTime | Date de validité de la prescription |

**Constructeurs :**
```csharp
// Constructeur par défaut
public Prescription()

// Constructeur avec paramètres
public Prescription(int id_prescription, int id_user, 
                    int id_patient, DateTime validity)
```

---

### Appartient.cs

**Description :** Représente la relation many-to-many entre Prescription et Medicine avec quantité

**Propriétés :**
| Propriété | Type | Description |
|-----------|------|-------------|
| Id_prescription | int | ID de la prescription |
| Id_medicine | int | ID du médicament |
| Quantity | int | Quantité prescrite |

**Constructeurs :**
```csharp
// Constructeur par défaut
public Appartient()

// Constructeur avec paramètres
public Appartient(int id_prescription, int id_medicine, int quantity)
```

---

## 🔌 Couche d'Accès aux Données (DAO)

### Database.cs

**Description :** Gère la connexion à la base de données MySQL

**Configuration :**
```csharp
private readonly string myConnectionString = 
    "server=localhost;uid=root;pwd=rootpassword;database=GSB2";
```

**Méthodes :**

#### GetConnection()
```csharp
public MySqlConnection GetConnection()
```
**Retour :** `MySqlConnection` - Nouvelle instance de connexion MySQL

**Utilisation :**
```csharp
Database db = new Database();
using (var connection = db.GetConnection())
{
    connection.Open();
    // Opérations sur la base de données
}
```

---

### UserDAO.cs

**Description :** Gère les opérations CRUD sur la table Users

#### Méthodes

##### createUser()
```csharp
public bool createUser(string firstname, string name, 
                       string email, string password, bool role)
```
**Paramètres :**
- `firstname` : Prénom de l'utilisateur
- `name` : Nom de l'utilisateur
- `email` : Email de connexion
- `password` : Mot de passe
- `role` : false (Docteur) ou true (Admin)

**Retour :** `bool` - true si succès, false sinon

**Exemple :**
```csharp
UserDAO userDAO = new UserDAO();
bool success = userDAO.createUser("Jean", "Dupont", 
    "jean.dupont@gsb.fr", "password123", false);
```

---

##### updateUser()
```csharp
public bool updateUser(int id_user, string firstname, string name, 
                       string email, string password, bool role)
```
**Paramètres :**
- `id_user` : ID de l'utilisateur à modifier
- Autres paramètres : Nouvelles valeurs

**Retour :** `bool` - true si mise à jour réussie

---

##### deleteUser()
```csharp
public bool deleteUser(int id_user)
```
**Paramètres :**
- `id_user` : ID de l'utilisateur à supprimer

**Retour :** `bool` - true si suppression réussie

---

##### GetAll()
```csharp
public List<User> GetAll()
```
**Retour :** `List<User>` - Liste de tous les utilisateurs

**Exemple :**
```csharp
UserDAO userDAO = new UserDAO();
List<User> allUsers = userDAO.GetAll();
foreach(User user in allUsers)
{
    Console.WriteLine($"{user.Firstname} {user.Name} - {user.Email}");
}
```

---

##### GetUserById()
```csharp
public User GetUserById(int id_user)
```
**Paramètres :**
- `id_user` : ID de l'utilisateur recherché

**Retour :** `User` - Objet utilisateur ou null si non trouvé

---

##### GetUserByEmail()
```csharp
public User GetUserByEmail(string email)
```
**Paramètres :**
- `email` : Email de l'utilisateur recherché

**Retour :** `User` - Objet utilisateur ou null si non trouvé

**Exemple (Authentification) :**
```csharp
UserDAO userDAO = new UserDAO();
User user = userDAO.GetUserByEmail("admin@gsb.fr");
if (user != null && user.Password == inputPassword)
{
    // Connexion réussie
    if (user.Role) // Admin
    {
        FormAdmin adminForm = new FormAdmin();
        adminForm.Show();
    }
    else // Docteur
    {
        FormDoctor doctorForm = new FormDoctor();
        doctorForm.Show();
    }
}
```

---

### PatientDAO.cs

**Description :** Gère les opérations CRUD sur la table Patients

#### Méthodes

##### createPatient()
```csharp
public bool createPatient(int id_user, string name, int age, 
                          string firstname, string gender, bool userRole)
```
**Paramètres :**
- `id_user` : ID de l'utilisateur créateur
- `name` : Nom du patient
- `age` : Âge du patient
- `firstname` : Prénom du patient
- `gender` : Sexe du patient
- `userRole` : Rôle de l'utilisateur (doit être Admin = true)

**Retour :** `bool` - true si création réussie, false si utilisateur non autorisé

**Sécurité :** Méthode protégée, seuls les Admins peuvent créer des patients

**Exemple :**
```csharp
PatientDAO patientDAO = new PatientDAO();
bool success = patientDAO.createPatient(1, "Martin", 35, 
    "Sophie", "F", true); // true = Admin autorisé
```

---

##### updatePatient()
```csharp
public bool updatePatient(int id_patient, int id_user, string name, 
                          int age, string firstname, string gender, bool userRole)
```
**Paramètres :**
- `id_patient` : ID du patient à modifier
- Autres paramètres : Nouvelles valeurs
- `userRole` : Doit être Admin (true)

**Retour :** `bool` - true si mise à jour réussie

---

##### deletePatient()
```csharp
public bool deletePatient(int id_patient, bool userRole)
```
**Paramètres :**
- `id_patient` : ID du patient à supprimer
- `userRole` : Doit être Admin (true)

**Retour :** `bool` - true si suppression réussie

---

##### GetAll()
```csharp
public List<Patient> GetAll()
```
**Retour :** `List<Patient>` - Liste de tous les patients

**Exemple :**
```csharp
PatientDAO patientDAO = new PatientDAO();
List<Patient> patients = patientDAO.GetAll();
dataGridView.DataSource = patients;
```

---

##### GetPatientById()
```csharp
public Patient GetPatientById(int id_patient)
```
**Paramètres :**
- `id_patient` : ID du patient recherché

**Retour :** `Patient` - Objet patient ou null

---

##### searchPatientByName()
```csharp
public List<Patient> searchPatientByName(string name)
```
**Paramètres :**
- `name` : Nom à rechercher (recherche partielle avec LIKE)

**Retour :** `List<Patient>` - Liste des patients correspondants

**Exemple :**
```csharp
PatientDAO patientDAO = new PatientDAO();
List<Patient> results = patientDAO.searchPatientByName("Mart");
// Retourne tous les patients dont le nom contient "Mart"
```

---

##### getPatientsByUserId()
```csharp
public List<Patient> getPatientsByUserId(int id_user)
```
**Paramètres :**
- `id_user` : ID de l'utilisateur créateur

**Retour :** `List<Patient>` - Liste des patients créés par cet utilisateur

---

##### getPatientCount()
```csharp
public int getPatientCount()
```
**Retour :** `int` - Nombre total de patients dans la base

---

### MedicineDAO.cs

**Description :** Gère les opérations CRUD sur la table Medicine

#### Méthodes

##### createMedicine()
```csharp
public bool createMedicine(int id_user, string name, string description, 
                           string molecule, int dosage, bool userRole)
```
**Paramètres :**
- `id_user` : ID de l'utilisateur créateur
- `name` : Nom du médicament
- `description` : Description du médicament
- `molecule` : Molécule active
- `dosage` : Dosage en mg
- `userRole` : Doit être Admin (true)

**Retour :** `bool` - true si création réussie

**Exemple :**
```csharp
MedicineDAO medicineDAO = new MedicineDAO();
bool success = medicineDAO.createMedicine(1, "Paracétamol", 
    "Antalgique et antipyrétique", "Paracétamol", 500, true);
```

---

##### updateMedicine()
```csharp
public bool updateMedicine(int id_medicine, int id_user, string name, 
                           string description, string molecule, int dosage, bool userRole)
```
**Paramètres :**
- `id_medicine` : ID du médicament à modifier
- Autres paramètres : Nouvelles valeurs
- `userRole` : Doit être Admin (true)

**Retour :** `bool` - true si mise à jour réussie

---

##### deleteMedicine()
```csharp
public bool deleteMedicine(int id_medicine, bool userRole)
```
**Paramètres :**
- `id_medicine` : ID du médicament à supprimer
- `userRole` : Doit être Admin (true)

**Retour :** `bool` - true si suppression réussie

---

##### GetAll()
```csharp
public List<Medicine> GetAll()
```
**Retour :** `List<Medicine>` - Liste de tous les médicaments

---

##### getMedicineById()
```csharp
public Medicine getMedicineById(int id_medicine)
```
**Paramètres :**
- `id_medicine` : ID du médicament recherché

**Retour :** `Medicine` - Objet médicament ou null

---

##### searchMedicineByName()
```csharp
public List<Medicine> searchMedicineByName(string name)
```
**Paramètres :**
- `name` : Nom à rechercher (recherche partielle)

**Retour :** `List<Medicine>` - Liste des médicaments correspondants

**Exemple :**
```csharp
MedicineDAO medicineDAO = new MedicineDAO();
List<Medicine> results = medicineDAO.searchMedicineByName("Para");
// Retourne Paracétamol, Paramol, etc.
```

---

##### getMedicinesByUserId()
```csharp
public List<Medicine> getMedicinesByUserId(int id_user)
```
**Paramètres :**
- `id_user` : ID de l'utilisateur créateur

**Retour :** `List<Medicine>` - Liste des médicaments créés par cet utilisateur

---

### PrescriptionDAO.cs

**Description :** Gère les opérations CRUD sur la table Prescription

#### Méthodes

##### createPrescription()
```csharp
public bool createPrescription(int id_user, int id_patient, 
                               DateTime validity, bool userRole)
```
**Paramètres :**
- `id_user` : ID du médecin prescripteur
- `id_patient` : ID du patient
- `validity` : Date de validité de la prescription
- `userRole` : Doit être Admin (true)

**Retour :** `bool` - true si création réussie

**Exemple :**
```csharp
PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
DateTime validity = DateTime.Now.AddMonths(1);
bool success = prescriptionDAO.createPrescription(1, 5, validity, true);
```

---

##### updatePrescription()
```csharp
public bool updatePrescription(int id_prescription, int id_user, 
                               int id_patient, DateTime validity, bool userRole)
```
**Paramètres :**
- `id_prescription` : ID de la prescription à modifier
- Autres paramètres : Nouvelles valeurs
- `userRole` : Doit être Admin (true)

**Retour :** `bool` - true si mise à jour réussie

---

##### deletePrescription()
```csharp
public bool deletePrescription(int id_prescription, bool userRole)
```
**Paramètres :**
- `id_prescription` : ID de la prescription à supprimer
- `userRole` : Doit être Admin (true)

**Retour :** `bool` - true si suppression réussie

**Note :** La suppression en cascade supprime aussi les entrées dans Appartient

---

##### getAllPrescription()
```csharp
public List<Prescription> getAllPrescription()
```
**Retour :** `List<Prescription>` - Liste de toutes les prescriptions

---

##### getPrescriptionById()
```csharp
public Prescription getPrescriptionById(int id_prescription)
```
**Paramètres :**
- `id_prescription` : ID de la prescription recherchée

**Retour :** `Prescription` - Objet prescription ou null

---

##### getPrescriptionByPatientId()
```csharp
public List<Prescription> getPrescriptionByPatientId(int id_patient)
```
**Paramètres :**
- `id_patient` : ID du patient

**Retour :** `List<Prescription>` - Liste des prescriptions du patient

**Exemple :**
```csharp
PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
List<Prescription> patientPrescriptions = 
    prescriptionDAO.getPrescriptionByPatientId(5);
```

---

##### getPrescriptionByUserId()
```csharp
public List<Prescription> getPrescriptionByUserId(int id_user)
```
**Paramètres :**
- `id_user` : ID du médecin

**Retour :** `List<Prescription>` - Liste des prescriptions créées par ce médecin

---

##### getValidPrescriptions()
```csharp
public List<Prescription> getValidPrescriptions()
```
**Retour :** `List<Prescription>` - Liste des prescriptions non expirées (validity >= date actuelle)

**Exemple :**
```csharp
PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
List<Prescription> validPrescriptions = 
    prescriptionDAO.getValidPrescriptions();
// Retourne uniquement les prescriptions encore valides
```

---

##### getLastInsertedId()
```csharp
public int getLastInsertedId()
```
**Retour :** `int` - ID de la dernière prescription insérée

**Utilisation :** Utilisé après createPrescription() pour récupérer l'ID auto-généré

**Exemple :**
```csharp
PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
bool created = prescriptionDAO.createPrescription(1, 5, DateTime.Now.AddMonths(1), true);
if (created)
{
    int newPrescriptionId = prescriptionDAO.getLastInsertedId();
    // Utiliser cet ID pour ajouter des médicaments
}
```

---

### AppartientDAO.cs

**Description :** Gère la relation many-to-many entre Prescription et Medicine avec quantités

#### Méthodes

##### addMedicineToPrescrition()
```csharp
public bool addMedicineToPrescrition(int id_prescription, 
                                     int id_medicine, int quantity)
```
**Paramètres :**
- `id_prescription` : ID de la prescription
- `id_medicine` : ID du médicament à ajouter
- `quantity` : Quantité prescrite

**Retour :** `bool` - true si ajout réussi

**Important :** Vérifier avec `isMedicineInPrescription()` avant d'ajouter pour éviter les doublons

**Exemple :**
```csharp
AppartientDAO appartientDAO = new AppartientDAO();
if (!appartientDAO.isMedicineInPrescription(10, 3))
{
    bool success = appartientDAO.addMedicineToPrescrition(10, 3, 2);
    // Ajoute 2 unités du médicament #3 à la prescription #10
}
```

---

##### removeMedicineFromPrescription()
```csharp
public bool removeMedicineFromPrescription(int id_prescription, int id_medicine)
```
**Paramètres :**
- `id_prescription` : ID de la prescription
- `id_medicine` : ID du médicament à retirer

**Retour :** `bool` - true si suppression réussie

---

##### updateMedicineQuantity()
```csharp
public bool updateMedicineQuantity(int id_prescription, 
                                   int id_medicine, int quantity)
```
**Paramètres :**
- `id_prescription` : ID de la prescription
- `id_medicine` : ID du médicament
- `quantity` : Nouvelle quantité

**Retour :** `bool` - true si mise à jour réussie

**Exemple :**
```csharp
AppartientDAO appartientDAO = new AppartientDAO();
bool success = appartientDAO.updateMedicineQuantity(10, 3, 5);
// Change la quantité du médicament #3 à 5 unités
```

---

##### removeAllMedicinesFromPrescription()
```csharp
public bool removeAllMedicinesFromPrescription(int id_prescription)
```
**Paramètres :**
- `id_prescription` : ID de la prescription

**Retour :** `bool` - true si suppression réussie

**Utilisation :** Appelé avant la suppression d'une prescription ou pour réinitialiser une prescription

---

##### getMedicinesByPrescriptionId()
```csharp
public List<Medicine> getMedicinesByPrescriptionId(int id_prescription)
```
**Paramètres :**
- `id_prescription` : ID de la prescription

**Retour :** `List<Medicine>` - Liste des médicaments de cette prescription

**Note :** Les quantités sont disponibles via `getMedicineQuantity()` pour chaque médicament

**Exemple :**
```csharp
AppartientDAO appartientDAO = new AppartientDAO();
List<Medicine> medicines = 
    appartientDAO.getMedicinesByPrescriptionId(10);

foreach (Medicine medicine in medicines)
{
    int quantity = appartientDAO.getMedicineQuantity(10, medicine.Id_medicine);
    Console.WriteLine($"{medicine.Name}: {quantity} unités");
}
```

---

##### getMedicineQuantity()
```csharp
public int getMedicineQuantity(int id_prescription, int id_medicine)
```
**Paramètres :**
- `id_prescription` : ID de la prescription
- `id_medicine` : ID du médicament

**Retour :** `int` - Quantité prescrite (0 si non trouvé)

---

##### isMedicineInPrescription()
```csharp
public bool isMedicineInPrescription(int id_prescription, int id_medicine)
```
**Paramètres :**
- `id_prescription` : ID de la prescription
- `id_medicine` : ID du médicament

**Retour :** `bool` - true si le médicament est déjà dans la prescription

**Utilisation :** Validation avant ajout pour éviter les doublons

---

##### getMedicineCountByPrescriptionId()
```csharp
public int getMedicineCountByPrescriptionId(int id_prescription)
```
**Paramètres :**
- `id_prescription` : ID de la prescription

**Retour :** `int` - Nombre de médicaments différents dans la prescription

**Exemple :**
```csharp
AppartientDAO appartientDAO = new AppartientDAO();
int count = appartientDAO.getMedicineCountByPrescriptionId(10);
Console.WriteLine($"Cette prescription contient {count} médicaments");
```

---

##### getTotalQuantityByPrescriptionId()
```csharp
public int getTotalQuantityByPrescriptionId(int id_prescription)
```
**Paramètres :**
- `id_prescription` : ID de la prescription

**Retour :** `int` - Somme totale des quantités de tous les médicaments

**Exemple :**
```csharp
AppartientDAO appartientDAO = new AppartientDAO();
int total = appartientDAO.getTotalQuantityByPrescriptionId(10);
Console.WriteLine($"Quantité totale prescrite: {total} unités");
```

---

## 🎨 Couche Présentation (Forms)

### MainForm.cs

**Description :** Formulaire de connexion et point d'entrée de l'application

**Fonctionnalités :**
- Authentification par email/mot de passe
- Validation des credentials via UserDAO
- Redirection selon le rôle utilisateur

**Flux d'authentification :**
```csharp
private void btnLogin_Click(object sender, EventArgs e)
{
    UserDAO userDAO = new UserDAO();
    User user = userDAO.GetUserByEmail(txtEmail.Text);
    
    if (user != null && user.Password == txtPassword.Text)
    {
        if (user.Role) // Admin
        {
            FormAdmin formAdmin = new FormAdmin(user);
            formAdmin.Show();
        }
        else // Docteur
        {
            FormDoctor formDoctor = new FormDoctor(user);
            formDoctor.Show();
        }
        this.Hide();
    }
    else
    {
        MessageBox.Show("Email ou mot de passe incorrect");
    }
}
```

---

### FormAdmin.cs

**Description :** Interface d'administration complète avec gestion CRUD

**Fonctionnalités :**
- Gestion complète des patients (Create, Read, Update, Delete)
- Gestion complète des médicaments (Create, Read, Update, Delete)
- Gestion complète des prescriptions (Create, Read, Update, Delete)
- Attribution de médicaments aux prescriptions avec quantités
- Recherche et filtrage des données

**Structure recommandée :**
- TabControl avec onglets : Patients, Médicaments, Prescriptions
- DataGridView pour l'affichage des listes
- Panels de formulaires pour les opérations CRUD
- Boutons : Ajouter, Modifier, Supprimer, Rechercher

**Exemple - Création d'un patient :**
```csharp
private void btnCreatePatient_Click(object sender, EventArgs e)
{
    PatientDAO patientDAO = new PatientDAO();
    
    bool success = patientDAO.createPatient(
        currentUser.Id_user,
        txtName.Text,
        int.Parse(txtAge.Text),
        txtFirstname.Text,
        cmbGender.SelectedItem.ToString(),
        currentUser.Role // true pour Admin
    );
    
    if (success)
    {
        MessageBox.Show("Patient créé avec succès");
        RefreshPatientList();
    }
}
```

**Exemple - Ajout de médicament à une prescription :**
```csharp
private void btnAddMedicineToPrescription_Click(object sender, EventArgs e)
{
    AppartientDAO appartientDAO = new AppartientDAO();
    
    int prescriptionId = int.Parse(txtPrescriptionId.Text);
    int medicineId = ((Medicine)cmbMedicines.SelectedItem).Id_medicine;
    int quantity = int.Parse(numQuantity.Value.ToString());
    
    // Vérifier si le médicament n'est pas déjà dans la prescription
    if (!appartientDAO.isMedicineInPrescription(prescriptionId, medicineId))
    {
        bool success = appartientDAO.addMedicineToPrescrition(
            prescriptionId, medicineId, quantity);
        
        if (success)
        {
            MessageBox.Show("Médicament ajouté à la prescription");
            RefreshPrescriptionMedicines(prescriptionId);
        }
    }
    else
    {
        MessageBox.Show("Ce médicament est déjà dans la prescription");
    }
}
```

---

### FormDoctor.cs

**Description :** Interface en lecture seule pour les médecins

**Fonctionnalités :**
- Consultation de la liste des patients
- Consultation du catalogue de médicaments
- Consultation des prescriptions
- Recherche et filtrage des données
- **AUCUNE opération de modification/suppression/création**

**Structure recommandée :**
- TabControl avec onglets : Patients, Médicaments, Prescriptions
- DataGridView en mode ReadOnly
- Champs de recherche uniquement
- Pas de boutons de modification

**Exemple - Affichage des patients :**
```csharp
private void LoadPatients()
{
    PatientDAO patientDAO = new PatientDAO();
    List<Patient> patients = patientDAO.GetAll();
    
    dataGridViewPatients.DataSource = patients;
    dataGridViewPatients.ReadOnly = true;
}
```

---

## 🔒 Sécurité et Contrôle d'Accès

### Système de Rôles

**Architecture de sécurité :**
```
┌─────────────────────────────────────────┐
│           User.Role (bool)              │
├─────────────────────────────────────────┤
│  false (0) → Docteur (Read Only)        │
│  true (1)  → Admin (Full CRUD)          │
└─────────────────────────────────────────┘
```

### Protection des Méthodes DAO

Toutes les méthodes de modification (Create, Update, Delete) vérifient le rôle utilisateur :

```csharp
public bool createPatient(..., bool userRole)
{
    if (userRole == false) // Si Docteur
        return false; // Opération refusée
    
    // Continuer avec la création pour les Admins
}
```

### Validation au Niveau UI

Les formulaires doivent également implémenter des contrôles :

```csharp
// Dans FormDoctor - Désactiver tous les boutons de modification
private void InitializeForm()
{
    btnCreate.Enabled = false;
    btnUpdate.Enabled = false;
    btnDelete.Enabled = false;
    dataGridView.ReadOnly = true;
}
```

### Protection contre les Injections SQL

Toutes les requêtes utilisent des **paramètres MySQL** :

```csharp
// CORRECT - Utilisation de paramètres
myCommand.CommandText = "SELECT * FROM Patients WHERE name = @name";
myCommand.Parameters.AddWithValue("@name", searchText);

// INCORRECT - Injection SQL possible
myCommand.CommandText = "SELECT * FROM Patients WHERE name = '" + searchText + "'";
```

---

## 🔄 Flux de Travail Typiques

### Flux 1 : Création d'une Prescription Complète

```
1. Authentification Admin
   └─> MainForm → FormAdmin

2. Créer le patient (si nouveau)
   └─> PatientDAO.createPatient()

3. Créer la prescription
   └─> PrescriptionDAO.createPrescription()
   └─> PrescriptionDAO.getLastInsertedId() → id_prescription

4. Ajouter des médicaments
   └─> Pour chaque médicament:
       └─> AppartientDAO.isMedicineInPrescription()
       └─> AppartientDAO.addMedicineToPrescrition(id, medicine_id, quantity)

5. Affichage final
   └─> AppartientDAO.getMedicinesByPrescriptionId()
```

**Code complet :**
```csharp
// 1. Créer patient
PatientDAO patientDAO = new PatientDAO();
patientDAO.createPatient(userId, "Dupont", 45, "Marie", "F", true);
int patientId = patientDAO.GetAll().Last().Id_patient;

// 2. Créer prescription
PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
DateTime validity = DateTime.Now.AddMonths(1);
prescriptionDAO.createPrescription(userId, patientId, validity, true);
int prescriptionId = prescriptionDAO.getLastInsertedId();

// 3. Ajouter médicaments
AppartientDAO appartientDAO = new AppartientDAO();
appartientDAO.addMedicineToPrescrition(prescriptionId, 1, 2); // Paracétamol x2
appartientDAO.addMedicineToPrescrition(prescriptionId, 3, 1); // Ibuprofène x1

// 4. Afficher résultat
List<Medicine> medicines = appartientDAO.getMedicinesByPrescriptionId(prescriptionId);
int totalQuantity = appartientDAO.getTotalQuantityByPrescriptionId(prescriptionId);
Console.WriteLine($"Prescription créée avec {medicines.Count} médicaments ({totalQuantity} unités total)");
```

---

### Flux 2 : Modification d'une Prescription

```
1. Sélectionner prescription existante
   └─> PrescriptionDAO.getPrescriptionById(id)

2. Afficher médicaments actuels
   └─> AppartientDAO.getMedicinesByPrescriptionId(id)

3. Modifier quantité d'un médicament
   └─> AppartientDAO.updateMedicineQuantity(prescription_id, medicine_id, new_quantity)

4. Ajouter un nouveau médicament
   └─> AppartientDAO.isMedicineInPrescription()
   └─> AppartientDAO.addMedicineToPrescrition()

5. Supprimer un médicament
   └─> AppartientDAO.removeMedicineFromPrescription()

6. Modifier validité
   └─> PrescriptionDAO.updatePrescription()
```

---

### Flux 3 : Consultation par un Docteur

```
1. Authentification Docteur
   └─> MainForm → FormDoctor

2. Rechercher un patient
   └─> PatientDAO.searchPatientByName("nom")

3. Voir les prescriptions du patient
   └─> PrescriptionDAO.getPrescriptionByPatientId(patient_id)

4. Voir les médicaments d'une prescription
   └─> AppartientDAO.getMedicinesByPrescriptionId(prescription_id)
   └─> Pour chaque médicament:
       └─> AppartientDAO.getMedicineQuantity(prescription_id, medicine_id)

5. Vérifier validité
   └─> PrescriptionDAO.getValidPrescriptions()
```

---

## 📊 Exemples de Requêtes Complexes

### Statistiques par utilisateur

```csharp
// Nombre de patients créés par un utilisateur
PatientDAO patientDAO = new PatientDAO();
List<Patient> userPatients = patientDAO.getPatientsByUserId(userId);
int patientCount = userPatients.Count;

// Nombre de prescriptions créées
PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
List<Prescription> userPrescriptions = prescriptionDAO.getPrescriptionByUserId(userId);
int prescriptionCount = userPrescriptions.Count;

Console.WriteLine($"Utilisateur #{userId}:");
Console.WriteLine($"- {patientCount} patients créés");
Console.WriteLine($"- {prescriptionCount} prescriptions émises");
```

---

### Analyse d'une prescription

```csharp
int prescriptionId = 10;
AppartientDAO appartientDAO = new AppartientDAO();

// Statistiques
int medicineCount = appartientDAO.getMedicineCountByPrescriptionId(prescriptionId);
int totalQuantity = appartientDAO.getTotalQuantityByPrescriptionId(prescriptionId);

// Détails des médicaments
List<Medicine> medicines = appartientDAO.getMedicinesByPrescriptionId(prescriptionId);

Console.WriteLine($"Prescription #{prescriptionId}:");
Console.WriteLine($"- {medicineCount} médicaments différents");
Console.WriteLine($"- {totalQuantity} unités au total");
Console.WriteLine("
Détails:");

foreach (Medicine medicine in medicines)
{
    int quantity = appartientDAO.getMedicineQuantity(prescriptionId, medicine.Id_medicine);
    Console.WriteLine($"  • {medicine.Name} ({medicine.Molecule} {medicine.Dosage}mg) x{quantity}");
}
```

---

### Recherche multicritères

```csharp
// Rechercher patients + leurs prescriptions valides
PatientDAO patientDAO = new PatientDAO();
PrescriptionDAO prescriptionDAO = new PrescriptionDAO();

List<Patient> patients = patientDAO.searchPatientByName("Dupont");

foreach (Patient patient in patients)
{
    Console.WriteLine($"
{patient.Firstname} {patient.Name} ({patient.Age} ans)");
    
    List<Prescription> prescriptions = 
        prescriptionDAO.getPrescriptionByPatientId(patient.Id_patient);
    
    var validPrescriptions = prescriptions
        .Where(p => p.Validity >= DateTime.Now)
        .ToList();
    
    Console.WriteLine($"  Prescriptions valides: {validPrescriptions.Count}");
    
    foreach (var prescription in validPrescriptions)
    {
        Console.WriteLine($"  - Valide jusqu'au {prescription.Validity:dd/MM/yyyy}");
    }
}
```

---

## ⚠️ Gestion des Erreurs

### Stratégie de Gestion d'Erreurs

Toutes les méthodes DAO utilisent des blocs try-catch :

```csharp
public bool createPatient(...)
{
    using (var connection = db.GetConnection())
    {
        try
        {
            connection.Open();
            // Opérations SQL
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors de la création : " + ex.Message);
            return false;
        }
    }
}
```

### Erreurs Communes et Solutions

| Erreur | Cause | Solution |
|--------|-------|----------|
| `MySqlException: Unable to connect` | Serveur MySQL éteint | Démarrer MySQL Server |
| `MySqlException: Access denied` | Mauvais credentials | Vérifier user/password dans Database.cs |
| `MySqlException: Unknown database` | Base GSB2 inexistante | Créer la base de données |
| `MySqlException: Foreign key constraint` | Suppression avec références | Supprimer les dépendances d'abord |
| `SqlException: Duplicate entry` | Clé primaire dupliquée | Vérifier l'existence avant insertion |
| `NullReferenceException` | Objet non trouvé | Vérifier null avant utilisation |

### Validation UI Recommandée

```csharp
private void btnCreatePatient_Click(object sender, EventArgs e)
{
    // Validation des champs
    if (string.IsNullOrWhiteSpace(txtName.Text))
    {
        MessageBox.Show("Le nom est obligatoire", "Erreur", 
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }
    
    if (!int.TryParse(txtAge.Text, out int age) || age < 0 || age > 150)
    {
        MessageBox.Show("L'âge doit être un nombre valide", "Erreur", 
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }
    
    // Création
    PatientDAO patientDAO = new PatientDAO();
    bool success = patientDAO.createPatient(...);
    
    if (success)
    {
        MessageBox.Show("Patient créé avec succès", "Succès", 
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        RefreshPatientList();
    }
    else
    {
        MessageBox.Show("Erreur lors de la création du patient", "Erreur", 
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

---

## 🧪 Tests Recommandés

### Tests Unitaires DAO

```csharp
[TestClass]
public class PatientDAOTests
{
    [TestMethod]
    public void TestCreatePatient()
    {
        PatientDAO dao = new PatientDAO();
        bool result = dao.createPatient(1, "Test", 30, "Patient", "M", true);
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void TestGetPatientById()
    {
        PatientDAO dao = new PatientDAO();
        Patient patient = dao.GetPatientById(1);
        Assert.IsNotNull(patient);
    }
    
    [TestMethod]
    public void TestDoctorCannotCreate()
    {
        PatientDAO dao = new PatientDAO();
        bool result = dao.createPatient(1, "Test", 30, "Patient", "M", false); // false = Docteur
        Assert.IsFalse(result); // Doit échouer
    }
}
```

### Tests d'Intégration

```csharp
[TestMethod]
public void TestCompletePrescriptionWorkflow()
{
    // 1. Créer patient
    PatientDAO patientDAO = new PatientDAO();
    bool patientCreated = patientDAO.createPatient(1, "Test", 40, "Patient", "F", true);
    Assert.IsTrue(patientCreated);
    
    // 2. Créer prescription
    PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
    bool prescriptionCreated = prescriptionDAO.createPrescription(1, 1, DateTime.Now.AddMonths(1), true);
    Assert.IsTrue(prescriptionCreated);
    
    int prescriptionId = prescriptionDAO.getLastInsertedId();
    
    // 3. Ajouter médicament
    AppartientDAO appartientDAO = new AppartientDAO();
    bool medicineAdded = appartientDAO.addMedicineToPrescrition(prescriptionId, 1, 2);
    Assert.IsTrue(medicineAdded);
    
    // 4. Vérifier
    int count = appartientDAO.getMedicineCountByPrescriptionId(prescriptionId);
    Assert.AreEqual(1, count);
}
```

---

## 🚀 Performance et Optimisation

### Bonnes Pratiques

1. **Utiliser using pour les connexions**
```csharp
using (var connection = db.GetConnection())
{
    // Connexion fermée automatiquement
}
```

2. **Fermer les DataReader**
```csharp
using (MySqlDataReader reader = myCommand.ExecuteReader())
{
    // Reader fermé automatiquement
}
```

3. **Éviter les requêtes N+1**
```csharp
// MAUVAIS - N+1 requêtes
List<Prescription> prescriptions = prescriptionDAO.getAllPrescription();
foreach (var prescription in prescriptions)
{
    Patient patient = patientDAO.GetPatientById(prescription.Id_patient); // N requêtes
}

// BON - Jointure SQL ou cache
// Créer une méthode getAllPrescriptionsWithPatients() avec JOIN
```

4. **Indexation de la base de données**
```sql
-- Index sur les colonnes de recherche
CREATE INDEX idx_patient_name ON Patients(name);
CREATE INDEX idx_medicine_name ON Medicine(name);
CREATE INDEX idx_prescription_validity ON Prescription(validity);
```

---

## 📝 Convention de Nommage

### C# Conventions

- **Classes** : PascalCase (ex: `PatientDAO`, `Medicine`)
- **Méthodes** : camelCase (ex: `createPatient()`, `getMedicineById()`)
- **Propriétés** : PascalCase (ex: `Id_patient`, `Firstname`)
- **Variables privées** : camelCase (ex: `myCommand`, `patientDAO`)
- **Constantes** : UPPER_SNAKE_CASE (ex: `MAX_AGE`)

### Base de Données

- **Tables** : PascalCase (ex: `Patients`, `Medicine`)
- **Colonnes** : snake_case (ex: `id_patient`, `first_name`)
- **Clés étrangères** : id_[table] (ex: `id_user`, `id_patient`)

---

## 📚 Ressources Complémentaires

### Documentation MySQL
- [MySQL Connector/NET](https://dev.mysql.com/doc/connector-net/en/)
- [MySql.Data Reference](https://dev.mysql.com/doc/dev/connector-net/latest/)

### Windows Forms
- [Microsoft Windows Forms Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
- [DataGridView Best Practices](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/datagridview-control-windows-forms)

### Design Patterns
- DAO Pattern (Data Access Object)
- Repository Pattern
- Singleton Pattern (pour Database)

---

## 🔧 Maintenance et Évolutions

### Évolutions Futures Possibles

1. **Logging System**
   - Ajouter un système de logs pour tracer les opérations
   - Logger les connexions, modifications, erreurs

2. **Validation Avancée**
   - Ajouter des règles métier complexes
   - Validation des dosages médicamenteux
   - Vérification des interactions médicamenteuses

3. **Historique**
   - Tracer les modifications (audit trail)
   - Version des prescriptions
   - Historique des changements de dosage

4. **Export/Import**
   - Export PDF des prescriptions
   - Export Excel des listes
   - Import CSV de médicaments

5. **Améliorations UI**
   - Autocomplétion sur les recherches
   - Graphiques statistiques
   - Dashboard avec indicateurs

---

