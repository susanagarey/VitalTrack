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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;



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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;


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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;



--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `usuario_id` int unsigned NOT NULL AUTO_INCREMENT,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  `nombre_usuario` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `hash_password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `nombre` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `apellidos` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `telefono` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `email` varchar(120) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `foto` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `ultimo_acceso` datetime DEFAULT NULL,
  `creado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `actualizado_en` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`usuario_id`),
  UNIQUE KEY `nombre_usuario` (`nombre_usuario`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;



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


/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-11-10 12:47:26
