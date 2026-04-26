-- GSB2 - Script de création et peuplement de la base de données
-- Version du serveur : MySQL 8.0+
-- Généré le : 30/03/2026

CREATE DATABASE IF NOT EXISTS `GSB2` CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `GSB2`;

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";
/*!40101 SET NAMES utf8mb4 */;

-- --------------------------------------------------------
-- Structure de la table `Users`
-- --------------------------------------------------------

CREATE TABLE `Users` (
  `id_user` int NOT NULL AUTO_INCREMENT,
  `firstname` varchar(50) DEFAULT NULL,
  `role` tinyint(1) DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `password` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Données de la table `Users`
-- Mot de passe de tous les comptes : 123 (stocké en SHA-256)
INSERT INTO `Users` (`id_user`, `firstname`, `role`, `name`, `email`, `password`) VALUES
(1, 'Alice',  0, 'Martin',  'alice.martin@gsb.fr',   'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(2, 'Hugo',   1, 'Durand',  'hugo.durand@gsb.fr',    'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(3, 'Léa',   0, 'Petit',   'lea.petit@gsb.fr',      'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(4, 'Thomas', 1, 'Robert',  'thomas.robert@gsb.fr',  'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(6, 'Morgan', 1, 'Bourre',  'morganbourre@gsb.fr',   'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3');

-- --------------------------------------------------------
-- Structure de la table `Medicine`
-- --------------------------------------------------------

CREATE TABLE `Medicine` (
  `id_medicine` int NOT NULL AUTO_INCREMENT,
  `id_user` int DEFAULT NULL,
  `dosage` int DEFAULT NULL,
  `name` varchar(100) DEFAULT NULL,
  `description` text,
  `molecule` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id_medicine`),
  KEY `id_user` (`id_user`),
  CONSTRAINT `Medicine_ibfk_1` FOREIGN KEY (`id_user`) REFERENCES `Users` (`id_user`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `Medicine` (`id_medicine`, `id_user`, `dosage`, `name`, `description`, `molecule`) VALUES
(1,  3, 500, 'Doliprane',    'Antidouleur et antipyrétique',          'Paracétamol'),
(2,  3,  20, 'Ibuprofène',   'Anti-inflammatoire',                    'Ibuprofen'),
(3,  3,   5, 'Amlor',        'Traitement de l\'hypertension',         'Amlodipine'),
(4,  3, 500, 'Amoxicilline', 'Antibiotique à large spectre',          'Amoxicillin'),
(5,  3,  10, 'Lexomil',      'Anxiolytique léger',                    'Bromazépam'),
(6,  3,  50, 'Seroplex',     'Antidépresseur ISRS',                   'Escitalopram'),
(7,  3, 100, 'Levothyrox',   'Substitut hormonal thyroïdien',         'L-thyroxine'),
(8,  3, 500, 'Augmentin',    'Antibiotique',                          'Amoxicilline/Clavulanate'),
(9,  3,  75, 'Plavix',       'Antiagrégant plaquettaire',             'Clopidogrel'),
(10, 3,  10, 'Zyrtec',       'Antihistaminique',                      'Cetirizine');

-- --------------------------------------------------------
-- Structure de la table `Patients`
-- --------------------------------------------------------

CREATE TABLE `Patients` (
  `id_patient` int NOT NULL AUTO_INCREMENT,
  `id_user` int DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `age` int DEFAULT NULL,
  `firstname` varchar(50) DEFAULT NULL,
  `gender` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id_patient`),
  KEY `id_user` (`id_user`),
  CONSTRAINT `Patients_ibfk_1` FOREIGN KEY (`id_user`) REFERENCES `Users` (`id_user`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `Patients` (`id_patient`, `id_user`, `name`, `age`, `firstname`, `gender`) VALUES
(1,  1, 'Dupont',     34, 'Marie',        'F'),
(2,  1, 'Bernard',    45, 'Luc',          'M'),
(3,  2, 'Moreau',     52, 'Sophie',       'F'),
(4,  1, 'Roux',       29, 'Julien',       'M'),
(5,  4, 'Fournier',   60, 'Chantal',      'F'),
(6,  2, 'Girard',     38, 'Nicolas',      'M'),
(7,  1, 'Chevalier',  42, 'Laura',        'F'),
(8,  2, 'Blanc',      31, 'Alexandre',    'M'),
(9,  4, 'Faure',      47, 'Isabelle',     'F'),
(10, 1, 'Garnier',    28, 'Thomas',       'M'),
(11, 4, 'Renaud',     36, 'Caroline',     'F'),
(12, 2, 'Henry',      50, 'David',        'M'),
(13, 1, 'Masson',     27, 'Emma',         'F'),
(14, 4, 'Marchand',   40, 'Vincent',      'M'),
(15, 1, 'Lefevre',    33, 'Julie',        'F'),
(16, 2, 'Carpentier', 58, 'Alain',        'M'),
(17, 1, 'Perrot',     46, 'Nathalie',     'F'),
(18, 2, 'Michel',     37, 'Paul',         'M'),
(19, 4, 'Barbier',    41, 'Camille',      'F'),
(20, 1, 'Giraud',     35, 'Antoine',      'M');

-- --------------------------------------------------------
-- Structure de la table `Prescription`
-- --------------------------------------------------------

CREATE TABLE `Prescription` (
  `id_prescription` int NOT NULL AUTO_INCREMENT,
  `id_user` int DEFAULT NULL,
  `id_patient` int DEFAULT NULL,
  `validity` date DEFAULT NULL,
  PRIMARY KEY (`id_prescription`),
  KEY `id_user` (`id_user`),
  KEY `Prescription_ibfk_2` (`id_patient`),
  CONSTRAINT `Prescription_ibfk_1` FOREIGN KEY (`id_user`) REFERENCES `Users` (`id_user`) ON DELETE CASCADE,
  CONSTRAINT `Prescription_ibfk_2` FOREIGN KEY (`id_patient`) REFERENCES `Patients` (`id_patient`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `Prescription` (`id_prescription`, `id_user`, `id_patient`, `validity`) VALUES
(1,  1, 1,  '2026-11-30'),
(2,  2, 2,  '2026-12-15'),
(3,  1, 3,  '2026-11-05'),
(4,  4, 4,  '2026-12-01'),
(5,  1, 5,  '2026-12-20'),
(6,  2, 6,  '2026-11-15'),
(7,  1, 7,  '2026-11-10'),
(8,  4, 8,  '2026-12-05'),
(9,  2, 9,  '2026-11-25'),
(10, 1, 10, '2026-12-30'),
(11, 4, 11, '2026-11-18'),
(12, 1, 12, '2026-12-22'),
(13, 2, 13, '2026-11-29'),
(14, 1, 14, '2026-11-17'),
(15, 2, 15, '2026-12-12'),
(16, 4, 16, '2026-12-01'),
(17, 1, 17, '2026-11-30'),
(18, 2, 18, '2026-11-25'),
(19, 1, 19, '2026-12-15'),
(20, 4, 20, '2026-11-28');

-- --------------------------------------------------------
-- Structure de la table `Appartient`
-- --------------------------------------------------------

CREATE TABLE `Appartient` (
  `id_prescription` int DEFAULT NULL,
  `id_medicine` int DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  KEY `id_prescription` (`id_prescription`),
  KEY `id_medicine` (`id_medicine`),
  CONSTRAINT `Appartient_ibfk_1` FOREIGN KEY (`id_prescription`) REFERENCES `Prescription` (`id_prescription`) ON DELETE CASCADE,
  CONSTRAINT `Appartient_ibfk_2` FOREIGN KEY (`id_medicine`) REFERENCES `Medicine` (`id_medicine`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `Appartient` (`id_prescription`, `id_medicine`, `quantity`) VALUES
(1,  1,  1), (1,  2,  1), (2,  3,  5), (3,  4,  6),
(4,  5,  4), (5,  1,  1), (6,  2,  3), (7,  6,  2),
(8,  3,  1), (9,  7,  7), (10, 1,  2), (11, 4,  3),
(12, 8,  2), (13, 9,  3), (14, 2,  1), (15, 10, 3),
(16, 5,  8), (17, 3,  1), (18, 1,  6), (19, 7,  6),
(20, 6,  2);

COMMIT;
