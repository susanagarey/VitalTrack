USE  vitaltrack;

-- Datos usuarios
LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES 
-- (1,'juanrodalc','juanrodalc@vitaltrack.com','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Juan','Rodríguez Alcántara','6160123456',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),
-- (2,'anagarlop@vitaltrack.com','anagarlop','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Ana','García López','616000000',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),
(3,'monicarufcas@vitaltrack.com','monicarufcas','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Mónica', 'Rufián Castejón','616000003',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),
(4,'albarogarmen@vitaltrack.com','albarogarmen','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Albaro','García Méndez','616000004',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),
(5,'lucasgarmun@vitaltrack.com','lucasgarmun','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Lucas','García Muñóz','616000005',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),
(6,'mariacasgar@vitaltrack.com','mariacasgar','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','María','Castro Garmúndia','616000006',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),
(7,'luismenlop@vitaltrack.com','luismenlop','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Luis','Mendiño López','616000007',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),
(8,'susanarodalv@gmail.com','susanarodalv','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Susana','Rodríguez Alvarez','500000001',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00')
;
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;



-- Datos pacientes
LOCK TABLES `pacientes` WRITE;
/*!40000 ALTER TABLE `pacientes` DISABLE KEYS */;
INSERT INTO `pacientes` VALUES 
-- (1,'367834744','36000001','Alfonso','Dolores Fuertes','1990-01-01','M','500000001','alfonsodolfue@gmail.com','Avd. Ourense nº 37, Vigo','Alérgico a penicilina',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1),
(2,'36000002','36000002','Lucas','García García','1990-01-01','M','500000001','lucasgargar@gmail.com','Avd. Ourense nº 40, Vigo','Lupus',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1),
(3,'36000003','36000003','María','Dolores Tizón','1990-01-01','M','500000001','mariadoltiz@gmail.com','Avd. Camelias nº 37, Vigo','sin alergias conocidas',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1),
(4,'36000004','36000004','David','Gutierrez Rodríguez','1990-01-01','M','500000001','davidgutrod@gmail.com','Rúa Portobelo nº 133, Vigo','sin alergias conocidas',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1),
(5,'36000005','36000005','Susana','Rodríguez Alvarez','1990-01-01','M','500000001','susanarodalv@gmail.com','Avd. Bueu nº 47, Cangas','sin alergias conocidas',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1)
;
/*!40000 ALTER TABLE `pacientes` ENABLE KEYS */;
UNLOCK TABLES;


-- Datos roles
-- LOCK TABLES `roles` WRITE;
-- /*!40000 ALTER TABLE `roles` DISABLE KEYS */;
-- INSERT INTO `roles` VALUES 
-- (1,'Administrador','Acceso completo a la administración del sistema','2025-11-03 23:28:10','2025-11-03 23:28:10'),
-- (2,'Medico','Profesional médico','2025-11-03 23:28:10','2025-11-03 23:28:10'),
-- (3,'Enfermera','Profesional de enfermería','2025-11-03 23:28:10','2025-11-03 23:28:10'),
-- (4,'Recepcion','Gestión administrativa básica','2025-11-03 23:28:10','2025-11-03 23:28:10'),
-- (5,'Paciente','Usuario paciente con acceso limitado a sus datos','2025-11-03 23:28:10','2025-11-03 23:28:10')
-- ;
-- /*!40000 ALTER TABLE `roles` ENABLE KEYS */;
-- UNLOCK TABLES;


--  Datos tipos roles
LOCK TABLES `usuarios_roles` WRITE;
/*!40000 ALTER TABLE `usuarios_roles` DISABLE KEYS */;
INSERT INTO `usuarios_roles` VALUES 
-- (1,1,'2024-12-31 12:35:00',NULL),
-- (2,2,'2024-12-31 12:35:00',1),
(3,3,'2024-12-31 12:35:00',1),
(4,3,'2024-12-31 12:35:00',1),
(5,4,'2024-12-31 12:35:00',1),
(6,2,'2024-12-31 12:35:00',1),
(7,3,'2024-12-31 12:35:00',1),
(8,5,'2024-12-31 12:35:00',1)
;
/*!40000 ALTER TABLE `usuarios_roles` ENABLE KEYS */;
UNLOCK TABLES;


-- LOCK TABLES `alertas` WRITE;
-- /*!40000 ALTER TABLE `alertas` DISABLE KEYS */;
-- INSERT INTO `alertas` VALUES 
-- (1,1,'TA','MEDIA','Revisar cuanto antes posible','2025-11-10 10:34:00',1,'2025-11-10 10:35:00',2,'2025-11-10 10:34:00','2025-11-10 12:35:58')
-- ;
-- /*!40000 ALTER TABLE `alertas` ENABLE KEYS */;
-- UNLOCK TABLES;





LOCK TABLES `auditoria` WRITE;
/*!40000 ALTER TABLE `auditoria` DISABLE KEYS */;
INSERT INTO `auditoria` VALUES 
-- (1,2,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.34\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 12:45:12'),
(2,3,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.14\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 12:45:12'),
(3,4,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.25\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 13:45:12'),
(4,5,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.26\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 07:25:12'),
(5,6,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.27\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 08:15:12'),
(6,7,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.28\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 09:05:12')
;
/*!40000 ALTER TABLE `auditoria` ENABLE KEYS */;
UNLOCK TABLES;





LOCK TABLES `umbrales_paciente` WRITE;
/*!40000 ALTER TABLE `umbrales_paciente` DISABLE KEYS */;
INSERT INTO `umbrales_paciente` VALUES
-- (1,1,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',2,2),
(1,2,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',2,2),
(2,3,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',3,2),
(3,4,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',3,3),
(4,5,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',4,4),
(5,5,160,170,80,900,70,110,34,40,36.8,38.3,10,120,1,'2025-09-28 10:10:13','2025-12-31 10:35:00',7,2)
;
/*!40000 ALTER TABLE `umbrales_paciente` ENABLE KEYS */;
UNLOCK TABLES;






