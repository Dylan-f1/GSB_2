-- Mission 4 : Régime alimentaire des patients
-- À exécuter une seule fois sur la base GSB2

USE `GSB2`;

-- --------------------------------------------------------
-- Structure de la table `Regimes`
-- Table de référence : contient la liste prédéfinie des régimes alimentaires
-- --------------------------------------------------------

CREATE TABLE `Regimes` (
  `id_regime` int NOT NULL AUTO_INCREMENT,
  `label` varchar(50) NOT NULL,
  PRIMARY KEY (`id_regime`),
  UNIQUE KEY `label` (`label`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Données de la table `Regimes`
-- La liste prédéfinie des régimes (le UNIQUE sur label empêche les doublons)
INSERT INTO `Regimes` (`id_regime`, `label`) VALUES
(1, 'Diabétique'),
(2, 'Sans sel'),
(3, 'Sans gluten'),
(4, 'Végétarien'),
(5, 'Sans lactose');

-- --------------------------------------------------------
-- Modification de la table `Patients`
-- Ajout de la clé étrangère vers `Regimes`
-- NULL autorisé : un patient peut ne pas avoir de régime,
-- les patients existants restent donc valides sans reprise de données
-- ON DELETE SET NULL : si un régime est supprimé, le patient
-- redevient simplement "sans régime" (surtout pas de CASCADE ici !)
-- --------------------------------------------------------

ALTER TABLE `Patients`
  ADD COLUMN `id_regime` int DEFAULT NULL,
  ADD KEY `id_regime` (`id_regime`),
  ADD CONSTRAINT `Patients_ibfk_2` FOREIGN KEY (`id_regime`) REFERENCES `Regimes` (`id_regime`) ON DELETE SET NULL;
