-- MySQL dump 10.13  Distrib 8.0.43, for Win64 (x86_64)
--
-- Host: localhost    Database: vitaltrack
-- ------------------------------------------------------
-- Server version	8.0.43

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
-- Dumping data for table `alertas`
--

LOCK TABLES `alertas` WRITE;
/*!40000 ALTER TABLE `alertas` DISABLE KEYS */;
INSERT INTO `alertas` VALUES (1,1,'TA','MEDIA','Revisar cuanto antes posible','2025-11-10 10:34:00',1,'2025-11-10 10:35:00',2,'2025-11-10 10:34:00','2025-11-10 12:35:58');
/*!40000 ALTER TABLE `alertas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `auditoria`
--

LOCK TABLES `auditoria` WRITE;
/*!40000 ALTER TABLE `auditoria` DISABLE KEYS */;
INSERT INTO `auditoria` VALUES (1,2,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.34\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 12:45:12'),(2,3,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.14\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 12:45:12'),(3,4,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.25\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 13:45:12'),(4,5,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.26\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 07:25:12'),(5,6,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.27\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 08:15:12'),(6,7,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.28\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 09:05:12'),(7,1,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 17:49:15'),(8,3,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 17:59:20'),(9,6,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 18:26:11'),(10,1,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 18:39:32'),(11,7,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 18:40:12'),(12,1,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 18:54:52'),(13,9,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 19:09:07'),(14,4,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 19:21:42'),(15,2,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 19:22:52'),(16,16,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 19:26:55'),(17,2,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 19:34:59'),(18,1,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 19:41:25'),(19,11,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 19:51:40'),(20,11,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 19:53:02'),(21,13,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 19:54:47'),(22,13,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 19:56:44'),(23,13,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 20:03:13'),(24,13,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 20:07:09'),(25,5,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 20:10:10'),(26,5,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 20:11:36'),(27,1,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 21:37:33'),(28,1,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 21:41:36'),(29,1,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 21:43:01'),(30,4,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 21:43:46'),(31,5,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 21:45:26'),(32,5,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 21:50:33'),(33,5,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 21:57:16'),(34,4,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 22:05:19'),(35,1,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 22:05:39'),(36,5,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 22:07:03'),(37,8,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 22:34:55'),(38,8,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 22:40:58'),(39,8,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 22:43:12'),(40,8,'LOGIN','{\"ip\": \"192.168.1.145\"}','2025-11-16 22:44:43');
/*!40000 ALTER TABLE `auditoria` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `pacientes`
--

LOCK TABLES `pacientes` WRITE;
/*!40000 ALTER TABLE `pacientes` DISABLE KEYS */;
INSERT INTO `pacientes` VALUES (1,'367834744','36000001','Alfonso','Dolores Fuertes','1990-01-01','M','500000001','alfonsodolfue@gmail.com','Avd. Ourense nº 37, Vigo','Alérgico a penicilina',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1),(2,'36000002','36000002','Lucas','García García','1990-01-01','M','500000001','lucasgargar@gmail.com','Avd. Ourense nº 40, Vigo','Lupus',1,'2025-09-01 13:40:00','2025-11-15 20:09:02',2,1),(3,'36000003','36000003','María','Dolores Tizón','1990-01-01','F','500000001','mariadoltiz@gmail.com','Avd. Camelias nº 37, Vigo','sin alergias conocidas',1,'2025-09-01 13:40:00','2025-11-15 20:19:31',3,3),(4,'36000004','36000004','David','Gutierrez Rodríguez','1990-01-01','X','500000001','davidgutrod@gmail.com','Rúa Portobelo nº 133, Vigo','sin alergias conocidas',1,'2025-09-01 13:40:00','2025-11-15 20:19:31',1,2),(5,'36124345','36000005B','Susana','Rodríguez Alvarez','1990-01-01','F','986777777','susanarodalv@gmail.com','Avd. Bueu nº 47, Cangas','sin alergias conocidas',1,'2025-09-01 13:40:00','2025-11-16 22:45:20',1,8),(6,'45678987','347878787B','Manuel','Campos Magdaleno','1979-07-19','X','986343434','manuelcammag@gmail.com','C\\ Monila nº 34, Vigo','Posible pierna rota',1,'2025-11-16 13:10:27','2025-11-16 13:40:29',1,1),(7,'45677834','36344345a','Francisco','Mora Vázquez','1994-03-24','M','987454545','franciscomorvaz@gmail.com','R/ Atranco nº34, Moaña','Observado',1,'2025-11-16 13:42:12','2025-11-16 14:33:50',1,2),(8,'65777844','45444566a','Teresa','Currás López','2004-07-14','F','986343434','teresacurlop@gmail.com','R\\ María Berdiales nº34, Vigo','Prueba teresa',1,'2025-11-16 13:46:53','2025-11-16 14:33:50',2,2),(9,'N/C','36123454B','Amanda','Chupón Tosco','2010-06-24','X','987344344','','Rúa Castrelos nº34, Vigo','Obs amanda',1,'2025-11-16 14:29:55','2025-11-16 14:33:50',7,7);
/*!40000 ALTER TABLE `pacientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'Administrador','Acceso completo a la administración del sistema','2025-11-03 23:28:10','2025-11-03 23:28:10'),(2,'Medico','Profesional médico','2025-11-03 23:28:10','2025-11-03 23:28:10'),(3,'Enfermera','Profesional de enfermería','2025-11-03 23:28:10','2025-11-03 23:28:10'),(4,'Recepcion','Gestión administrativa básica','2025-11-03 23:28:10','2025-11-03 23:28:10'),(5,'Paciente','Usuario paciente con acceso limitado a sus datos','2025-11-03 23:28:10','2025-11-03 23:28:10');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `umbrales_paciente`
--

LOCK TABLES `umbrales_paciente` WRITE;
/*!40000 ALTER TABLE `umbrales_paciente` DISABLE KEYS */;
INSERT INTO `umbrales_paciente` VALUES (1,1,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',2,2),(2,2,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-10-28 11:10:13','2025-11-16 19:58:40',2,2),(3,3,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-30 13:30:31','2025-11-16 19:58:40',3,2),(4,4,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-08-21 00:00:13','2025-11-16 19:58:40',3,3),(5,5,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',4,4),(6,5,160,170,80,900,70,110,34,40,36.8,38.3,10,120,1,'2025-09-28 10:10:13','2025-12-31 10:35:00',7,2),(7,1,120,190,70,90,76,120,10,30,37.9,39.0,9,10,1,'2025-09-28 10:10:13','2025-12-31 12:35:00',2,2),(8,1,135,172,100,110,50,80,10,40,36.8,38.3,9,130,1,'2025-09-28 11:10:13','2025-12-31 12:35:00',2,2),(9,1,170,180,90,100,60,110,34,40,37.2,37.5,8,111,1,'2025-09-28 12:10:13','2025-12-31 12:35:00',2,2);
/*!40000 ALTER TABLE `umbrales_paciente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,1,'juanrodalc','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','Juan','Rodríguez Alcántara','6160123456','juanrodalc@vitaltrack.com','usuario01.png','2024-12-01 23:03:00','2024-12-01 23:03:00','2025-11-15 21:34:10'),(2,1,'anagarlop','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','Ana','García López','616000000','anagarlop@vitaltrack.com','usuario02.png','2024-12-01 23:03:00','2024-12-01 23:03:00','2025-11-15 21:34:10'),(3,1,'monicarufcas','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','Mónica','Rufián Castejón','616000003','monicarufcas@vitaltrack.com','usuario03.png','2024-12-01 23:03:00','2024-12-01 23:03:00','2025-11-15 21:34:10'),(4,1,'albarogarmen','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','AlbaroU2','García Méndez','616000045','albarogarmen@vitaltrack.com','icons8-salary-50.png','2024-12-01 23:03:00','2024-12-01 23:03:00','2025-11-15 12:39:45'),(5,1,'lucasgarmun','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','LucasUPDATED','García MuñózUPDATED','616000005','lucasgarmun@vitaltrack.com','icons8-employee-100-8.png','2024-12-01 23:03:00','2024-12-01 23:03:00','2025-11-16 10:34:07'),(6,1,'mariacasgar','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','María UPDATED','Castro Garmúndia','616000006','mariacasgar@vitaltrack.com',NULL,'2024-12-01 23:03:00','2024-12-01 23:03:00','2025-11-16 10:34:07'),(7,1,'luismenlop','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','Luis UPDATED2','Mendiño López','616000007','luismenlop@vitaltrack.com','usuario07.png','2024-12-01 23:03:00','2024-12-01 23:03:00','2025-11-15 21:34:10'),(8,1,'abonado.susanarodalv','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','Susana','Rodríguez Alvarez','986343434','susanarodalv@gmail.com','sheworker02_20251115_161643200.png','2024-12-01 23:03:00','2024-12-01 23:03:00','2025-11-16 22:34:10'),(9,1,'robertoroqalc','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','Roberto','Roquefort Alcántara','986787878','robertoroqalc@vitaltrack.com','Boss.jpg',NULL,'2025-11-11 17:34:26','2025-11-15 21:34:10'),(11,1,'anabeatrizcomlen','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','Ana Beatriz','Comesaña Lenguado','655343434','anabeatrizcomlen@vitaltrack.com','desconocido.png',NULL,'2025-11-11 17:41:22','2025-11-15 21:34:10'),(13,1,'rebecagomcal','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','Rebeca','Gómez Casal','678344444','rebecagomcal@vitaltrack.com','desconocido.png',NULL,'2025-11-11 17:45:11','2025-11-11 18:09:57'),(14,1,'davidrodgar','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','David','Rodríguez García','986787878','davidrodgar@vitaltrack.com',NULL,NULL,'2025-11-11 17:52:25','2025-11-11 17:52:25'),(15,1,'juanlopcar','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Juan','López Carballo','988787878','juanlopcar@vitaltrack.com',NULL,NULL,'2025-11-11 17:59:24','2025-11-16 14:36:57'),(16,1,'joseluisbahtor','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','José Luis','Bahamonte Tórculo','654454545','joseluisbahtor@vitaltrack.com',NULL,NULL,'2025-11-11 18:03:09','2025-11-15 21:34:10'),(17,1,'monicabeltop','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Mónica','Belluci Topacio','986343434','monicabeltop@vitaltrack.com','desconocido.png',NULL,'2025-11-11 18:04:04','2025-11-15 21:34:10'),(18,1,'susanagarrey','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Susana','García Rey','986343434','susanagarrey@vitaltrack.com','desconocido.png',NULL,'2025-11-11 19:18:23','2025-11-12 12:58:22'),(19,1,'danielgarlop','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Daniel','Garcan López','987333333','danielgarlop@vitaltrack.com','heWorker03.jpg',NULL,'2025-11-11 20:01:28','2025-11-11 20:01:28'),(20,1,'sonianovalc','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Sonia','Nóvoa Alcántara','986343434','sonianovalc@vitaltrack.com','sheWorker01.jpg',NULL,'2025-11-13 20:35:33','2025-11-13 20:35:33'),(21,1,'amaroprofor','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Amaro','Prosaude Forte','986343434','amaroprofor@vitaltrack.com','heWorker01.jpg',NULL,'2025-11-14 12:34:46','2025-11-14 12:34:46'),(22,0,'luzcasrod','cf052b175066301f32b1b65fa96c2b7a74c04714da4bb5b5bdf03d8251198705','Luz','Casal Rodríguez','678343434','luzcasrod@vitaltrack.com','icons8-clerk-100-4.png',NULL,'2025-11-14 12:56:14','2025-11-15 14:28:43'),(23,0,'beatrizcorca','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Beatriz','Cordeiro Casal','654343434','beatrizcorcas@vitaltrack.com','icons8-clerk-100-4.png',NULL,'2025-11-14 12:58:18','2025-11-16 22:06:46'),(24,0,'carlospuecas','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Carlos','Pueste Castrejón','986343434','carlospuecas@vitaltrack.com','icons8-employee-50-4.png',NULL,'2025-11-14 13:05:46','2025-11-15 14:20:57'),(25,0,'raulgarrey','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Raúl','García Rey','986343434','raulgarrey@vitaltrack.com','icons8-clerk-100.png',NULL,'2025-11-14 13:09:42','2025-11-15 13:30:45'),(26,0,'rodrigoloptop','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Rodrígo','López Topes','986343443','rodrigoloptop@vitaltrack.com','icons8-clerk-50-4.png',NULL,'2025-11-14 13:12:49','2025-11-15 13:27:47'),(27,0,'angelreybah','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Ángel','Rey Bahomonte','616343434','angelreybah@vitaltrack.com','icons8-employee-50-8.png',NULL,'2025-11-14 13:14:39','2025-11-15 13:27:42'),(28,0,'pedrocastri','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','Pedro','Castro Trigo','616123434','pedrocastri@vitraltrack.com','icons8-clerk-100-5.png',NULL,'2025-11-14 13:31:11','2025-11-15 13:27:39'),(29,0,'davidgarrey','2314ec44c47b02523437690805581816305fe3cb780f65fcef0d2b630c06bbcd','David','García Rey','9873434347676','davidgarrey@vitaltrack.com','icons8-clerk-50.png',NULL,'2025-11-14 20:39:34','2025-11-15 13:27:36'),(32,0,'borrame1ape','f2e53c927c66fe711e8e88ef9b37a8e3187f1652216b313fc8eb2513883dd360','Borrame1','Apellidos1','987343443','borrame1ape@vitaltrack.com','icons8-employee-100-11_20251115_121843657.png',NULL,'2025-11-15 12:18:59','2025-11-15 13:27:16');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `usuarios_roles`
--

LOCK TABLES `usuarios_roles` WRITE;
/*!40000 ALTER TABLE `usuarios_roles` DISABLE KEYS */;
INSERT INTO `usuarios_roles` VALUES (1,1,'2024-12-31 12:35:00',NULL),(2,2,'2024-12-31 12:35:00',1),(3,4,'2025-11-16 12:00:00',NULL),(4,3,'2024-12-31 12:35:00',1),(5,4,'2024-12-31 12:35:00',1),(6,2,'2024-12-31 12:35:00',1),(7,3,'2024-12-31 12:35:00',1),(8,5,'2024-12-31 12:35:00',1),(9,4,'2025-11-16 12:00:46',NULL),(11,3,'2025-11-16 12:00:55',NULL),(13,3,'2025-11-16 12:01:07',NULL),(14,3,'2025-11-16 12:01:11',NULL),(15,3,'2025-11-16 12:01:15',NULL),(16,1,'2025-11-16 12:01:20',NULL),(17,4,'2025-11-16 12:01:29',NULL),(18,2,'2025-11-16 12:01:34',NULL),(19,2,'2025-11-16 12:01:38',NULL),(20,2,'2025-11-16 12:01:41',NULL),(21,3,'2025-11-16 12:01:44',NULL),(22,3,'2025-11-16 12:01:49',NULL),(23,3,'2025-11-16 12:01:54',NULL),(24,3,'2025-11-16 12:01:57',NULL),(25,3,'2025-11-16 12:01:59',NULL),(26,2,'2025-11-16 12:02:03',NULL),(27,1,'2025-11-16 12:02:06',NULL),(28,2,'2025-11-16 12:02:10',NULL),(29,4,'2025-11-16 12:02:12',NULL),(32,2,'2025-11-16 12:02:14',NULL);
/*!40000 ALTER TABLE `usuarios_roles` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-11-16 22:49:04
