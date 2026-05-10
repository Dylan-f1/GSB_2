-- Mission 3 : Durcissement des mots de passe
-- À exécuter une seule fois sur la base GSB2

USE `GSB2`;

-- Agrandir password à VARCHAR(100) (déjà 100, mais explicite)
-- et ajouter le flag de migration BCrypt
ALTER TABLE `Users`
  MODIFY COLUMN `password` VARCHAR(100) NOT NULL,
  ADD COLUMN `is_migrated` TINYINT(1) NOT NULL DEFAULT 0;
