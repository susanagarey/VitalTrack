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
-- Table structure for table `alertas`
--

DROP TABLE IF EXISTS `alertas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `alertas` (
  `alerta_id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `paciente_id` int unsigned NOT NULL,
  `tipo_alerta` enum('TA','FC','SPO2','TEMP','GLUCOSA','GENERAL') CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `severidad` enum('BAJA','MEDIA','ALTA','CRITICA') CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `mensaje` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `disparada_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `reconocida` tinyint(1) NOT NULL DEFAULT '0',
  `reconocida_en` datetime DEFAULT NULL,
  `reconocida_por` int unsigned DEFAULT NULL,
  `creado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `actualizado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`alerta_id`),
  KEY `fk_alertas_reconocida_por` (`reconocida_por`),
  KEY `idx_alertas_paciente_tiempo` (`paciente_id`,`disparada_en` DESC),
  KEY `idx_alertas_reconocida` (`reconocida`,`reconocida_en`),
  CONSTRAINT `fk_alertas_paciente` FOREIGN KEY (`paciente_id`) REFERENCES `pacientes` (`paciente_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_alertas_reconocida_por` FOREIGN KEY (`reconocida_por`) REFERENCES `usuarios` (`usuario_id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `alertas`
--

LOCK TABLES `alertas` WRITE;
/*!40000 ALTER TABLE `alertas` DISABLE KEYS */;
INSERT INTO `alertas` VALUES (1,1,'TA','MEDIA','Revisar cuanto antes posible','2025-11-10 10:34:00',1,'2025-11-10 10:35:00',2,'2025-11-10 10:34:00','2025-11-10 12:35:58');
/*!40000 ALTER TABLE `alertas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `auditoria`
--

DROP TABLE IF EXISTS `auditoria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `auditoria` (
  `auditoria_id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `usuario_id` int unsigned DEFAULT NULL,
  `accion` enum('CREAR','ACTUALIZAR','ELIMINAR','LOGIN','LOGOUT','ASIGNAR_ROL') CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `detalles` json DEFAULT NULL,
  `creado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`auditoria_id`),
  KEY `fk_auditoria_usuario` (`usuario_id`),
  CONSTRAINT `fk_auditoria_usuario` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`usuario_id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `auditoria`
--

LOCK TABLES `auditoria` WRITE;
/*!40000 ALTER TABLE `auditoria` DISABLE KEYS */;
INSERT INTO `auditoria` VALUES (1,2,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.34\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 12:45:12'),(2,3,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.14\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 12:45:12'),(3,4,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.25\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 13:45:12'),(4,5,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.26\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 07:25:12'),(5,6,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.27\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 08:15:12'),(6,7,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.28\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 09:05:12');
/*!40000 ALTER TABLE `auditoria` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pacientes`
--

DROP TABLE IF EXISTS `pacientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pacientes` (
  `paciente_id` int unsigned NOT NULL AUTO_INCREMENT,
  `nhc` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `dni` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `nombre` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `apellidos` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `fecha_nacimiento` date DEFAULT NULL,
  `sexo` enum('M','F','X','N/D') CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `telefono` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `email` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `direccion` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `observaciones` text CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  `creado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `actualizado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `creado_por` int unsigned DEFAULT NULL,
  `actualizado_por` int unsigned DEFAULT NULL,
  PRIMARY KEY (`paciente_id`),
  UNIQUE KEY `nhc` (`nhc`),
  UNIQUE KEY `dni` (`dni`),
  KEY `fk_pacientes_creado_por` (`creado_por`),
  KEY `fk_pacientes_actualizado_por` (`actualizado_por`),
  KEY `idx_pacientes_nombre` (`apellidos`,`nombre`),
  KEY `idx_pacientes_activo` (`activo`),
  CONSTRAINT `fk_pacientes_actualizado_por` FOREIGN KEY (`actualizado_por`) REFERENCES `usuarios` (`usuario_id`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `fk_pacientes_creado_por` FOREIGN KEY (`creado_por`) REFERENCES `usuarios` (`usuario_id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pacientes`
--

LOCK TABLES `pacientes` WRITE;
/*!40000 ALTER TABLE `pacientes` DISABLE KEYS */;
INSERT INTO `pacientes` VALUES (1,'367834744','36000001','Alfonso','Dolores Fuertes','1990-01-01','M','500000001','alfonsodolfue@gmail.com','Avd. Ourense nº 37, Vigo','Alérgico a penicilina',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1),(2,'36000002','36000002','Lucas','García García','1990-01-01','M','500000001','lucasgargar@gmail.com','Avd. Ourense nº 40, Vigo','Lupus',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1),(3,'36000003','36000003','María','Dolores Tizón','1990-01-01','M','500000001','mariadoltiz@gmail.com','Avd. Camelias nº 37, Vigo','sin alergias conocidas',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1),(4,'36000004','36000004','David','Gutierrez Rodríguez','1990-01-01','M','500000001','davidgutrod@gmail.com','Rúa Portobelo nº 133, Vigo','sin alergias conocidas',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1),(5,'36000005','36000005','Susana','Rodríguez Alvarez','1990-01-01','M','500000001','susanarodalv@gmail.com','Avd. Bueu nº 47, Cangas','sin alergias conocidas',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1);
/*!40000 ALTER TABLE `pacientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `rol_id` int unsigned NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `descripcion` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `creado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `actualizado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`rol_id`),
  UNIQUE KEY `nombre` (`nombre`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'Administrador','Acceso completo a la administración del sistema','2025-11-03 23:28:10','2025-11-03 23:28:10'),(2,'Medico','Profesional médico','2025-11-03 23:28:10','2025-11-03 23:28:10'),(3,'Enfermera','Profesional de enfermería','2025-11-03 23:28:10','2025-11-03 23:28:10'),(4,'Recepcion','Gestión administrativa básica','2025-11-03 23:28:10','2025-11-03 23:28:10'),(5,'Paciente','Usuario paciente con acceso limitado a sus datos','2025-11-03 23:28:10','2025-11-03 23:28:10');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `umbrales_paciente`
--

DROP TABLE IF EXISTS `umbrales_paciente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `umbrales_paciente` (
  `umbral_id` int unsigned NOT NULL AUTO_INCREMENT,
  `paciente_id` int unsigned NOT NULL,
  `min_tension_sistolica` smallint DEFAULT NULL,
  `max_tension_sistolica` smallint DEFAULT NULL,
  `min_tension_diastolica` smallint DEFAULT NULL,
  `max_tension_diastolica` smallint DEFAULT NULL,
  `min_frecuencia_cardiaca` smallint DEFAULT NULL,
  `max_frecuencia_cardiaca` smallint DEFAULT NULL,
  `min_spo2` tinyint unsigned DEFAULT NULL,
  `max_spo2` tinyint unsigned DEFAULT NULL,
  `min_temperatura_c` decimal(4,1) DEFAULT NULL,
  `max_temperatura_c` decimal(4,1) DEFAULT NULL,
  `min_glucosa` smallint unsigned DEFAULT NULL,
  `max_glucosa` smallint unsigned DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  `creado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `actualizado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `creado_por` int unsigned DEFAULT NULL,
  `actualizado_por` int unsigned DEFAULT NULL,
  PRIMARY KEY (`umbral_id`),
  KEY `fk_umbrales_paciente_creado_por` (`creado_por`),
  KEY `fk_umbrales_paciente_actualizado_por` (`actualizado_por`),
  KEY `idx_umbrales_paciente_activo` (`paciente_id`,`activo`),
  CONSTRAINT `fk_umbrales_paciente_actualizado_por` FOREIGN KEY (`actualizado_por`) REFERENCES `usuarios` (`usuario_id`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `fk_umbrales_paciente_creado_por` FOREIGN KEY (`creado_por`) REFERENCES `usuarios` (`usuario_id`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `fk_umbrales_paciente_paciente` FOREIGN KEY (`paciente_id`) REFERENCES `pacientes` (`paciente_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `umbrales_paciente`
--

LOCK TABLES `umbrales_paciente` WRITE;
/*!40000 ALTER TABLE `umbrales_paciente` DISABLE KEYS */;
INSERT INTO `umbrales_paciente` VALUES (1,1,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',2,2),(2,2,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',2,2),(3,3,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',3,2),(4,4,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',3,3),(5,5,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',4,4),(6,5,160,170,80,900,70,110,34,40,36.8,38.3,10,120,1,'2025-09-28 10:10:13','2025-12-31 10:35:00',7,2);
/*!40000 ALTER TABLE `umbrales_paciente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `usuario_id` int unsigned NOT NULL AUTO_INCREMENT,
  `nombre_usuario` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `email` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `hash_password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `nombre` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `apellidos` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `telefono` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  `ultimo_acceso` datetime DEFAULT NULL,
  `creado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `actualizado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`usuario_id`),
  UNIQUE KEY `nombre_usuario` (`nombre_usuario`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'juanrodalc','juanrodalc@gmail.com','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Juan','Rodríguez Alcántara','6160123456',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),(2,'anagarlop@gmail.com','anagarlop','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Ana','García López','616000000',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),(3,'monicarufcas@vitaltrack.com','monicarufcas','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Mónica','Rufián Castejón','616000003',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),(4,'albarogarmen@vitaltrack.com','albarogarmen','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Albaro','García Méndez','616000004',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),(5,'lucasgarmun@vitaltrack.com','lucasgarmun','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Lucas','García Muñóz','616000005',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),(6,'mariacasgar@vitaltrack.com','mariacasgar','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','María','Castro Garmúndia','616000006',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),(7,'luismenlop@vitaltrack.com','luismenlop','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Luis','Mendiño López','616000007',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),(8,'susanarodalv@gmail.com','susanarodalv','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Susana','Rodríguez Alvarez','500000001',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios_roles`
--

DROP TABLE IF EXISTS `usuarios_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios_roles` (
  `usuario_id` int unsigned NOT NULL,
  `rol_id` int unsigned NOT NULL,
  `asignado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `asignado_por` int unsigned DEFAULT NULL,
  PRIMARY KEY (`usuario_id`,`rol_id`),
  KEY `fk_usuarios_roles_rol` (`rol_id`),
  KEY `fk_usuarios_roles_asignado_por` (`asignado_por`),
  CONSTRAINT `fk_usuarios_roles_asignado_por` FOREIGN KEY (`asignado_por`) REFERENCES `usuarios` (`usuario_id`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `fk_usuarios_roles_rol` FOREIGN KEY (`rol_id`) REFERENCES `roles` (`rol_id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_usuarios_roles_usuario` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`usuario_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios_roles`
--

LOCK TABLES `usuarios_roles` WRITE;
/*!40000 ALTER TABLE `usuarios_roles` DISABLE KEYS */;
INSERT INTO `usuarios_roles` VALUES (1,1,'2024-12-31 12:35:00',NULL),(2,2,'2024-12-31 12:35:00',1),(3,3,'2024-12-31 12:35:00',1),(4,3,'2024-12-31 12:35:00',1),(5,4,'2024-12-31 12:35:00',1),(6,2,'2024-12-31 12:35:00',1),(7,3,'2024-12-31 12:35:00',1),(8,5,'2024-12-31 12:35:00',1);
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

-- Dump completed on 2025-11-10 13:31:22
