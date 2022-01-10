-- MySQL dump 10.13  Distrib 8.0.27, for Win64 (x86_64)
--
-- Host: localhost    Database: footballclub
-- ------------------------------------------------------
-- Server version	8.0.27

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `coaches`
--

DROP TABLE IF EXISTS `coaches`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `coaches` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `PersonId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `HoursPayment` float NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Coaches_to_Persons_idx` (`PersonId`),
  CONSTRAINT `FK_Coaches_to_Persons` FOREIGN KEY (`PersonId`) REFERENCES `persons` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `coaches`
--

LOCK TABLES `coaches` WRITE;
/*!40000 ALTER TABLE `coaches` DISABLE KEYS */;
INSERT INTO `coaches` VALUES ('08d9a521-dd17-4281-875a-1f074b6d6a90','08d9a521-9ebf-41fe-8902-cb8adbb9ea90',1000.8),('08d9a521-e4a8-4de5-845d-a03718c7285a','08d9a521-8ac0-408d-8bce-7ea5b3b97221',1000),('08d9c910-ee83-417a-825c-bf48c8c0ad7a','08d9c910-ee83-4337-8c7e-8aec5af18f91',4000);
/*!40000 ALTER TABLE `coaches` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contracts`
--

DROP TABLE IF EXISTS `contracts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `contracts` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Sum` float NOT NULL,
  `StartDate` datetime(6) NOT NULL,
  `EndDate` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contracts`
--

LOCK TABLES `contracts` WRITE;
/*!40000 ALTER TABLE `contracts` DISABLE KEYS */;
INSERT INTO `contracts` VALUES ('08d9c22b-8512-49e4-81c3-b1a9cf82082e',1000000,'2020-11-12 00:00:00.000000','2021-11-12 00:00:00.000000'),('08d9c22b-a933-4338-8ba0-4595c443f0de',1000000,'2020-11-12 00:00:00.000000','2021-11-12 00:00:00.000000');
/*!40000 ALTER TABLE `contracts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `disqualifications`
--

DROP TABLE IF EXISTS `disqualifications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `disqualifications` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `DisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `StartDate` datetime(6) NOT NULL,
  `EndDate` datetime(6) NOT NULL,
  `PersonId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Disqualifications_to_Persons_idx` (`PersonId`),
  CONSTRAINT `FK_Disqualifications_to_Persons` FOREIGN KEY (`PersonId`) REFERENCES `persons` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `disqualifications`
--

LOCK TABLES `disqualifications` WRITE;
/*!40000 ALTER TABLE `disqualifications` DISABLE KEYS */;
INSERT INTO `disqualifications` VALUES ('08d9c8a4-6aa4-4c1e-8df7-d8b7c85f890b','Неспортивное поведение','2021-11-30 00:00:00.000000','2021-12-16 00:00:00.000000','08d9a2cb-c54c-40b5-89b9-5f356102b827'),('08d9c8a4-770c-4e7e-84e1-cca778cd92f4','Драка на поле','2022-01-01 00:00:00.000000','2022-01-18 00:00:00.000000','08d9c881-5c4a-43ec-8bfc-d8015aca76aa'),('08d9c8a5-3ce2-478f-8106-6da319b5ed8e','Драка на поле','2021-12-30 00:00:00.000000','2022-02-19 00:00:00.000000','08d9c910-6e34-4d3d-87e4-d044acf7bce2'),('08d9c8a6-9931-4f70-8dd4-41a4c6174bf5','Драка на поле','2021-12-01 00:00:00.000000','2021-12-11 00:00:00.000000','08d9a522-74da-4eed-826d-747c882cef1d'),('08d9c8aa-2b1d-4712-82ea-990587800cb9','Драка на поле','2021-12-29 00:00:00.000000','2022-01-01 00:00:00.000000','08d9a522-912b-44b7-80a2-5760004edd87'),('08d9c8aa-2f89-4baa-8398-5c8f1f87be4f','Драка на поле','2021-12-30 00:00:00.000000','2022-01-18 00:00:00.000000','08d9a521-9ebf-41fe-8902-cb8adbb9ea90'),('08d9c8aa-33f7-41a5-8db5-fc5b0fb41627','Драка на поле','2021-12-02 00:00:00.000000','2021-12-11 00:00:00.000000','08d9c87e-ca97-472c-8070-be8629e5a911'),('08d9c8aa-3864-42c0-834d-52d40f10820b','Драка на поле','2021-12-01 00:00:00.000000','2021-12-04 00:00:00.000000','08d9a522-912b-44b7-80a2-5760004edd89'),('08d9c8aa-3d84-49ef-897f-ebf6defbcd80','Драка на поле','2021-12-02 00:00:00.000000','2021-12-11 00:00:00.000000','08d9a522-912b-44b7-80a2-5760004edd87'),('08d9c8aa-58fb-4105-802b-9f4ac59d3266','Драка на поле','2021-12-01 00:00:00.000000','2021-12-11 00:00:00.000000','08d9a522-912b-44b7-80a2-5760004edd89'),('08d9c8aa-5da1-4001-884c-9dc4b53916b0','Драка на поле','2021-12-02 00:00:00.000000','2021-12-12 00:00:00.000000','08d9a522-912b-44b7-80a2-5760004edd89'),('08d9c910-4672-4978-86c7-31cf4b2b4f07','Нецензурная лексика','2021-11-29 00:00:00.000000','2021-12-15 00:00:00.000000',NULL),('08d9cede-236c-4919-8b00-6c37ce1889db','Драка на поле','2022-01-03 00:00:00.000000','2022-01-07 00:00:00.000000','08d9ced3-f7e6-41f9-84f2-4591b4d6eacf'),('08d9d354-45a4-4baa-832d-a0915a4a32c4','Драка на поле','2021-12-28 00:00:00.000000','2021-12-28 00:00:00.000000','08d9d354-1ade-4a24-890b-6f3178e9a3ea'),('348c502e-4ae0-11ec-9092-34cff6503196','Драка на поле','2021-05-02 00:00:00.000000','2021-05-18 00:00:00.000000','08d9a522-912b-44b7-80a2-5760004edd89'),('53b040b6-4ae0-11ec-9092-34cff6503196','Нападение на судью','2021-06-02 00:00:00.000000','2021-10-28 00:00:00.000000','08d9a2cb-c54c-40b5-89b9-5f356102b827'),('8af6cf00-4ae0-11ec-9092-34cff6503196','Нарушение эпидемиологических требований','2021-06-02 00:00:00.000000','2021-10-28 00:00:00.000000','08d9a50a-99b8-46a1-8162-01a1c66190a5');
/*!40000 ALTER TABLE `disqualifications` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employeerecoveries`
--

DROP TABLE IF EXISTS `employeerecoveries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employeerecoveries` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `PersonId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `RecoveryReasonId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `Sum` float DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_EmployeeRecoveries_to_Persons_idx` (`PersonId`),
  KEY `FK_EmployeeRecoveries_to_RecoveriesReasons_idx` (`RecoveryReasonId`),
  CONSTRAINT `FK_EmployeeRecoveries_to_Persons` FOREIGN KEY (`PersonId`) REFERENCES `persons` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_EmployeeRecoveries_to_RecoveriesReasons` FOREIGN KEY (`RecoveryReasonId`) REFERENCES `recoveryreasons` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employeerecoveries`
--

LOCK TABLES `employeerecoveries` WRITE;
/*!40000 ALTER TABLE `employeerecoveries` DISABLE KEYS */;
INSERT INTO `employeerecoveries` VALUES ('08d9a532-6233-41b5-8b70-8224d81d9cd5','08d9c881-b9ff-4724-800a-e838d19b4d0a','c9e2b720-4304-11ec-9315-34cff6503196',10001,'2022-04-24 00:00:00'),('08d9a532-7306-49ce-8b28-cb66d990bb5f','08d9a2cb-c54c-40b5-89b9-5f356102b827','ea8f787a-4304-11ec-9315-34cff6503196',10000,'2021-11-04 00:00:00'),('08d9a532-9053-4f98-8ceb-d1222924a50c',NULL,'9e2738af-4304-11ec-9315-34cff6503196',1000,'2021-08-08 00:00:00'),('08d9d354-2624-44ec-85b9-bb8f573262ea','08d9a2cb-c54c-40b5-89b9-5f356102b827','c9e2b720-4304-11ec-9315-34cff6503196',10001,'2022-01-24 00:00:00');
/*!40000 ALTER TABLE `employeerecoveries` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `enemyteamgoals`
--

DROP TABLE IF EXISTS `enemyteamgoals`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `enemyteamgoals` (
  `Id` char(36) NOT NULL,
  `Time` int DEFAULT NULL,
  `MatchId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `Author` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `TouchdownPassFrom` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `OwnGoalPlayer` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_EnemyTeamGoal_to_Match_idx` (`MatchId`),
  KEY `FK_EnemyTeamGoal_to_Player(OwnGoalPlayer)_idx` (`OwnGoalPlayer`),
  CONSTRAINT `FK_EnemyTeamGoal_to_Match` FOREIGN KEY (`MatchId`) REFERENCES `matches` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_EnemyTeamGoal_to_Player(OwnGoalPlayer)` FOREIGN KEY (`OwnGoalPlayer`) REFERENCES `players` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `enemyteamgoals`
--

LOCK TABLES `enemyteamgoals` WRITE;
/*!40000 ALTER TABLE `enemyteamgoals` DISABLE KEYS */;
INSERT INTO `enemyteamgoals` VALUES ('08d9d126-77e2-48e3-8a64-ac3fa6776c2d',84,'109e237a-4acf-11ec-9092-34cff6503196',NULL,NULL,NULL),('08d9d12d-d562-4729-8b4c-92306d333997',84,'109e237a-4acf-11ec-9092-34cff6503196',NULL,NULL,'08d9a441-c5ae-40e0-84ae-3945b660d563');
/*!40000 ALTER TABLE `enemyteamgoals` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `matches`
--

DROP TABLE IF EXISTS `matches`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `matches` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Date` datetime(6) NOT NULL,
  `MatchResultId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `OurTeamName` longtext,
  `EnemyTeamName` longtext,
  `IsViziting` tinyint(1) DEFAULT NULL,
  `Duration` int DEFAULT NULL,
  `OurTeamScores` int NOT NULL,
  `EnemyTeamScores` int NOT NULL,
  `WithFirstOvertime` tinyint(1) NOT NULL,
  `WithSecondOvertime` tinyint(1) NOT NULL,
  `PenaltyShootOut` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Matches_to_MatchResult_idx` (`MatchResultId`),
  CONSTRAINT `FK_Matches_to_MatchResult` FOREIGN KEY (`MatchResultId`) REFERENCES `matchresults` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `matches`
--

LOCK TABLES `matches` WRITE;
/*!40000 ALTER TABLE `matches` DISABLE KEYS */;
INSERT INTO `matches` VALUES ('08d9d354-391b-477e-865f-db3c9497f0db','2021-12-28 00:00:00.000000','bec499e0-4acd-11ec-9092-34cff6503196','Витязи','Американцы',1,90,1,0,1,0,1),('109e237a-4acf-11ec-9092-34cff6503196','2023-07-11 00:00:00.000000','c844b5c8-4acd-11ec-9092-34cff6503196','Варяги','Витязи',1,95,1,1,0,1,0),('22f6e71d-4acf-11ec-9092-34cff6503196','2021-06-10 00:00:00.000000','c37a0009-4acd-11ec-9092-34cff6503196','Варяги','Торпедо',1,95,0,0,0,0,0),('e5fa031c-4ace-11ec-9092-34cff6503196','2021-06-05 00:00:00.000000','bec499e0-4acd-11ec-9092-34cff6503196','Варяги','Зенит',1,90,2,1,0,0,0);
/*!40000 ALTER TABLE `matches` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `matchresults`
--

DROP TABLE IF EXISTS `matchresults`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `matchresults` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `DisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `matchresults`
--

LOCK TABLES `matchresults` WRITE;
/*!40000 ALTER TABLE `matchresults` DISABLE KEYS */;
INSERT INTO `matchresults` VALUES ('bec499e0-4acd-11ec-9092-34cff6503196','Победа'),('c37a0009-4acd-11ec-9092-34cff6503196','Поражение'),('c844b5c8-4acd-11ec-9092-34cff6503196','Ничья');
/*!40000 ALTER TABLE `matchresults` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ourteamgoals`
--

DROP TABLE IF EXISTS `ourteamgoals`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ourteamgoals` (
  `Id` char(36) NOT NULL,
  `Time` int DEFAULT NULL,
  `Author` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `TouchdownPassFrom` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `OwnGoalPlayerName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `MatchId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_OurTeamGoal_to_Player(Author)_idx` (`Author`),
  KEY `FK_OurTeamGoal_to_Player(TouchdownPassFrom)_idx` (`TouchdownPassFrom`),
  KEY `FK_OurTeamGoal_to_Match_idx` (`MatchId`),
  CONSTRAINT `FK_OurTeamGoal_to_Match` FOREIGN KEY (`MatchId`) REFERENCES `matches` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_OurTeamGoal_to_Player(Author)` FOREIGN KEY (`Author`) REFERENCES `players` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_OurTeamGoal_to_Player(TouchdownPassFrom)` FOREIGN KEY (`TouchdownPassFrom`) REFERENCES `players` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ourteamgoals`
--

LOCK TABLES `ourteamgoals` WRITE;
/*!40000 ALTER TABLE `ourteamgoals` DISABLE KEYS */;
INSERT INTO `ourteamgoals` VALUES ('08d9d118-b3ab-4302-87b3-a3681e567b9e',12,'08d9a441-c5ae-40e0-84ae-3945b660d563','08d9a50b-0410-4c4f-8a6c-21505b688cf5',NULL,'22f6e71d-4acf-11ec-9092-34cff6503196'),('08d9d118-fc1f-4100-8104-0daa8b5a2a45',25,'08d9a50b-0410-4c4f-8a6c-21505b688cf5','08d9a441-c5ae-40e0-84ae-3945b660d563',NULL,'22f6e71d-4acf-11ec-9092-34cff6503196'),('08d9d119-0c60-43aa-8279-73a7f565afe2',87,'08d9a50b-0410-4c4f-8a6c-21505b688cf5','08d9a441-c5ae-40e0-84ae-3945b660d563',NULL,'109e237a-4acf-11ec-9092-34cff6503196'),('08d9d11e-670a-4c17-8f9e-e15a567e7058',84,NULL,NULL,'Заикин','109e237a-4acf-11ec-9092-34cff6503196'),('08d9d121-50c5-4c73-868e-bec952da2431',84,'08d9a50b-0410-4c4f-8a6c-21505b688cf5',NULL,'Заикин','109e237a-4acf-11ec-9092-34cff6503196'),('08d9d121-7371-4c37-892e-cbbeba7bec4b',84,'08d9a50b-0410-4c4f-8a6c-21505b688cf5',NULL,'Заикин','109e237a-4acf-11ec-9092-34cff6503196');
/*!40000 ALTER TABLE `ourteamgoals` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `persons`
--

DROP TABLE IF EXISTS `persons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `persons` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `HomePhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `WorkPhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Birthday` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `persons`
--

LOCK TABLES `persons` WRITE;
/*!40000 ALTER TABLE `persons` DISABLE KEYS */;
INSERT INTO `persons` VALUES ('08d9a2ca-f73b-40fe-8cfe-5d3291c2fb93','Пупкин Иван Васильевич','+7(893)453 32 31','+7(890)090 09 09','г. Курск, Ул. Ленина, 16А',NULL),('08d9a2cb-c54c-40b5-89b9-5f356102b827','Заикин Дмитрий Владимирович','+7(919)133 49 20','+7(919)133 49 20','г. Курск, Ул. Карла Маркса, 16А','2025-03-18 00:00:00.000000'),('08d9a50a-587d-49e2-83d5-97373b1451cc','Шабаров Андрей Михайлович','+7(844)536 75 34','+7(856)823 45 93','г. Курск, Ленина 19',NULL),('08d9a50a-81aa-4336-8f9f-15bbde2dcb83','Васиильев Андрей Станиславович','87653451234','87653451234','г. Курск, Челюскинцев 19',NULL),('08d9a50a-99b8-46a1-8162-01a1c66190a5','Крицкий Андрей Станиславович','86666666666','86666666666','г. Курск, Проспек Клыкова 17Б, 56',NULL),('08d9a50a-d0cc-43b0-8578-85854c98199b','Горов Генадий Владимирович','89512345678','89512345678','г. Курск, Проспек Клыкова 4, 1',NULL),('08d9a521-8ac0-408d-8bce-7ea5b3b97221','Гришилов Иван Алексеевич','+7(890)045 24 50','+7(895)157 82 90','г. Курск, Союзная 9, 45','2021-12-24 00:00:00.000000'),('08d9a521-9ebf-41fe-8902-cb8adbb9ea90','Гончаров Олег Владимирович','+7(895)123 49 80','+7(895)123 49 80','г. Курск, Деёнеки 6А, 85','2021-12-20 00:00:00.000000'),('08d9a522-74da-4eed-826d-747c882cef1d','Онов Виталий Викторович','+7(890)456 28 79','+7(890)456 28 79','г. Курск, Дейнеки 8Б, 85','2022-01-24 00:00:00.000000'),('08d9a522-912b-44b7-80a2-5760004edd87','Овезов Руслан',NULL,NULL,NULL,NULL),('08d9a522-912b-44b7-80a2-5760004edd89','Кононов Олег Васильевич','89083456893','89083456893','г. Курск, Проспект Победы 24, 34',NULL),('08d9c87e-ca97-472c-8070-be8629e5a911','Путин Владимир Владимирович','+7(000)000 00 00','+7(111)111 11 11','Кремль','2022-01-06 00:00:00.000000'),('08d9c881-5c4a-43ec-8bfc-d8015aca76aa','Огурцов Иван Владимирович','+7(951)234 23 23','+7(900)434 23 31','г. Льгов, Добролюбова 16А','2021-12-14 00:00:00.000000'),('08d9c881-b9ff-4724-800a-e838d19b4d0a','Картошкин Пётр Васильевич','+7(987)435 34 53','+7(934)563 42 34','г. Курск, Ленина 13','2021-08-11 00:00:00.000000'),('08d9c910-6e34-4d3d-87e4-d044acf7bce2','Иванов Пётр Фёдорович','+7(900)000 00 00','+7(800)000 00 00','ул. Ленина 18А','2021-11-29 00:00:00.000000'),('08d9c910-9acc-44a7-8b9b-372d52e502ed','Гришилов Пётр Иванович','+7(909)090 90 90','+7(909)090 90 90','ул. Ленина 19А','2021-12-28 00:00:00.000000'),('08d9c910-ee83-4337-8c7e-8aec5af18f91','Иванов Фёдор Петрович','+7(919)133 49 19','+7(919)133 49 20','ул. Ленина 23','2022-01-29 00:00:00.000000'),('08d9ced3-f7e6-41f9-84f2-4591b4d6eacf','Помидоров Григорий Олегович','+7(909)090 90 90','+7(345)345 34 53','Ул. Лилипутов, 16','2022-01-10 00:00:00.000000'),('08d9cedc-3331-488c-815e-1f485824cff9','Онищенко Валерий Генадьевич','+7(988)843 34 34','+7(907)887 43 43','г.Курск, ул. Большевиков 12','2021-12-27 00:00:00.000000'),('08d9d12c-cce4-423b-87a8-9189b0bdd744',NULL,NULL,NULL,NULL,NULL),('08d9d12c-d81a-471b-8821-8748ee2d31d8',NULL,NULL,NULL,NULL,NULL),('08d9d34f-ef16-4aa1-8b1b-2f15bc2eec5b','Заикин Дмитрий Владимирович','+7(919)133 49 20','+7(919)133 49 20','г. Курск, Ул. Карла Маркса, 16А','2025-04-18 00:00:00.000000'),('08d9d34f-f3e7-41a5-8896-3f07db0409f8','Заикин Дмитрий Дмитриевич','+7(919)133 49 20','+7(919)133 49 20','г. Курск, Ул. Карла Маркса, 16А','2025-04-18 00:00:00.000000'),('08d9d350-01ab-4a29-8c10-2aa74ac98343','Заикин Дмитрий Дмитриевич','+7(919)133 49 20','+7(919)133 49 20','г. Курск, Ул. Карла Маркса, 16А','2025-04-18 00:00:00.000000'),('08d9d350-3bae-48d7-879f-23c68a322953','Заикин Дмитрий Дмитриевич','+7(919)133 49 20','+7(919)133 49 20','г. Курск, Ул. Карла Маркса, 16А','2025-04-18 00:00:00.000000'),('08d9d353-08e0-4c00-89d9-73dab458830b','Ульянов Владимир Ильич','+7(909)039 40 23','+7(234)323 43 24','г. Курск, Ленина 17Б','2022-01-25 00:00:00.000000'),('08d9d353-1567-44d6-8c88-c1a142312b2f',NULL,NULL,NULL,NULL,NULL),('08d9d353-1c06-494b-8784-c587be01e04f',NULL,NULL,NULL,NULL,NULL),('08d9d353-1f28-4950-874c-0c1bc102d292',NULL,NULL,NULL,NULL,NULL),('08d9d353-265c-4c4a-8c78-b1d12aeae4ce',NULL,NULL,NULL,NULL,NULL),('08d9d353-5cfe-481e-8e22-8436c604314d',NULL,NULL,NULL,NULL,NULL),('08d9d353-f274-4262-8e0b-43de353d00bb','Заикин Дмитрий Владимирович','+7(919)133 49 20','+7(919)133 49 20','г. Курск, Ул. Карла Маркса, 16А','2025-05-18 00:00:00.000000'),('08d9d354-1ade-4a24-890b-6f3178e9a3ea','Ульянов Владимир Ильич','+7(346)456 45 64','+7(324)324 32 42','г. Курск, Ленина 17Б','2022-01-25 00:00:00.000000');
/*!40000 ALTER TABLE `persons` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `playermanagers`
--

DROP TABLE IF EXISTS `playermanagers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `playermanagers` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `PersonId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `PlayerId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `HoursPayment` float NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_PlayerManagers_to_Persons_idx` (`PersonId`),
  KEY `FK_PlayersManagers_to_Players_idx` (`PlayerId`),
  CONSTRAINT `FK_PlayerManagers_to_Persons` FOREIGN KEY (`PersonId`) REFERENCES `persons` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_PlayersManagers_to_Players` FOREIGN KEY (`PlayerId`) REFERENCES `players` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playermanagers`
--

LOCK TABLES `playermanagers` WRITE;
/*!40000 ALTER TABLE `playermanagers` DISABLE KEYS */;
INSERT INTO `playermanagers` VALUES ('08d9a522-ceb5-4c25-8064-66bba5cfcacd','08d9a522-74da-4eed-826d-747c882cef1d','08d9a441-c5ae-40e0-84ae-3945b660d563',2567),('08d9a522-e447-4fd2-835b-218f110706da','08d9a522-912b-44b7-80a2-5760004edd89','08d9a441-c5ae-40e0-84ae-3945b660d563',1890.34),('08d9c881-b9ff-45a9-8284-561664ebe128','08d9c881-b9ff-4724-800a-e838d19b4d0a','08d9c881-5c48-450e-8278-f870693d6d03',4000);
/*!40000 ALTER TABLE `playermanagers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `players`
--

DROP TABLE IF EXISTS `players`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `players` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `PersonId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `ContractId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `PlayerManagerId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Players_to_Persons_idx` (`PersonId`),
  KEY `FK_Players_to_Contracts_idx` (`ContractId`),
  KEY `FK_Players_to_PlayerManagers_idx` (`PlayerManagerId`),
  CONSTRAINT `FK_Players_to_Contracts` FOREIGN KEY (`ContractId`) REFERENCES `contracts` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_Players_to_Persons` FOREIGN KEY (`PersonId`) REFERENCES `persons` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Players_to_PlayerManagers` FOREIGN KEY (`PlayerManagerId`) REFERENCES `playermanagers` (`Id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `players`
--

LOCK TABLES `players` WRITE;
/*!40000 ALTER TABLE `players` DISABLE KEYS */;
INSERT INTO `players` VALUES ('08d9a441-c5ae-40e0-84ae-3945b660d563','08d9d353-f274-4262-8e0b-43de353d00bb','08d9c22b-8512-49e4-81c3-b1a9cf82082e','08d9a522-ceb5-4c25-8064-66bba5cfcacd'),('08d9a50b-0410-4c4f-8a6c-21505b688cf5','08d9a50a-587d-49e2-83d5-97373b1451cc',NULL,NULL),('08d9c881-5c48-450e-8278-f870693d6d03','08d9c881-5c4a-43ec-8bfc-d8015aca76aa','08d9c22b-8512-49e4-81c3-b1a9cf82082e','08d9a522-e447-4fd2-835b-218f110706da'),('08d9d354-1ade-49d0-8db8-d0babe6d2403','08d9d354-1ade-4a24-890b-6f3178e9a3ea','08d9c22b-8512-49e4-81c3-b1a9cf82082e','08d9a522-e447-4fd2-835b-218f110706da');
/*!40000 ALTER TABLE `players` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recoveryreasons`
--

DROP TABLE IF EXISTS `recoveryreasons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recoveryreasons` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `DisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recoveryreasons`
--

LOCK TABLES `recoveryreasons` WRITE;
/*!40000 ALTER TABLE `recoveryreasons` DISABLE KEYS */;
INSERT INTO `recoveryreasons` VALUES ('9e2738af-4304-11ec-9315-34cff6503196','Опоздание'),('c9e2b720-4304-11ec-9315-34cff6503196','Халтура'),('cca834d9-4304-11ec-9315-34cff6503196','Халатность'),('cfb78e24-4304-11ec-9315-34cff6503196','Невыполнение плана'),('d747a704-4304-11ec-9315-34cff6503196','Взыскание от организации лиги'),('db52027c-4304-11ec-9315-34cff6503196','Порча имущества клуба'),('e100790d-4304-11ec-9315-34cff6503196','Не тактичное поведение'),('e723f84f-4304-11ec-9315-34cff6503196','Прогул'),('ea8f787a-4304-11ec-9315-34cff6503196','Систематические опоздания');
/*!40000 ALTER TABLE `recoveryreasons` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-01-09 21:15:18
