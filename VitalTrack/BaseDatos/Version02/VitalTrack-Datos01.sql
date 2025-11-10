USE DATABASE vitaltrack;

-- Datos usuarios
LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES 
(1,'juanrodalc','juanrodalc@gmail.com','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Juan','Rodríguez Alcántara','6160123456',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00'),
(2,'anagarlop@gmail.com','anagarlop','df733656293a19c54f69093ba916f0a1a2a3c151fc95c13f3a794c2631eeb3a6','Ana','García López','616000000',1,'2024-12-01 23:03:00','2024-12-01 23:03:00','2024-12-01 23:03:00')
;
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;



-- Datos pacientes
LOCK TABLES `pacientes` WRITE;
/*!40000 ALTER TABLE `pacientes` DISABLE KEYS */;
INSERT INTO `pacientes` VALUES 
(1,'367834744','36000001','Alfonso','Dolores Fuertes','1990-01-01','M','500000001','alfonsodolfue@gmail.com','Avd. Ourense nº 37, Vigo','Alérgico a penicilina',1,'2025-09-01 13:40:00','2025-09-01 13:40:00',1,1)
;
/*!40000 ALTER TABLE `pacientes` ENABLE KEYS */;
UNLOCK TABLES;


-- Datos roles
LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES 
(1,'Administrador','Acceso completo a la administración del sistema','2025-11-03 23:28:10','2025-11-03 23:28:10'),
(2,'Medico','Profesional médico','2025-11-03 23:28:10','2025-11-03 23:28:10'),
(3,'Enfermera','Profesional de enfermería','2025-11-03 23:28:10','2025-11-03 23:28:10'),
(4,'Recepcion','Gestión administrativa básica','2025-11-03 23:28:10','2025-11-03 23:28:10'),
(5,'Paciente','Usuario paciente con acceso limitado a sus datos','2025-11-03 23:28:10','2025-11-03 23:28:10')
;
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;


--  Datos tipos roles
LOCK TABLES `usuarios_roles` WRITE;
/*!40000 ALTER TABLE `usuarios_roles` DISABLE KEYS */;
INSERT INTO `usuarios_roles` VALUES 
(1,1,'2024-12-31 12:35:00',NULL),
(2,2,'2024-12-31 12:35:00',1)
;
/*!40000 ALTER TABLE `usuarios_roles` ENABLE KEYS */;
UNLOCK TABLES;


LOCK TABLES `alertas` WRITE;
/*!40000 ALTER TABLE `alertas` DISABLE KEYS */;
INSERT INTO `alertas` VALUES 
(1,1,'TA','MEDIA','Revisar cuanto antes posible','2025-11-10 10:34:00',1,'2025-11-10 10:35:00',2,'2025-11-10 10:34:00','2025-11-10 12:35:58')
;
/*!40000 ALTER TABLE `alertas` ENABLE KEYS */;
UNLOCK TABLES;





LOCK TABLES `auditoria` WRITE;
/*!40000 ALTER TABLE `auditoria` DISABLE KEYS */;
INSERT INTO `auditoria` VALUES 
(1,2,'LOGIN','{\"\": \"DESKTOP\", \"IP\": \"80.34.54.34\", \"SENSORS\": [\"IPCAM\", \"TEMPSENSOR\"]}','2025-11-10 12:45:12')
;
/*!40000 ALTER TABLE `auditoria` ENABLE KEYS */;
UNLOCK TABLES;





LOCK TABLES `umbrales_paciente` WRITE;
/*!40000 ALTER TABLE `umbrales_paciente` DISABLE KEYS */;
INSERT INTO `umbrales_paciente` VALUES
(1,1,170,180,90,100,60,110,34,40,36.8,38.3,10,120,1,'2025-09-28 09:10:13','2025-12-31 12:35:00',2,2)
;
/*!40000 ALTER TABLE `umbrales_paciente` ENABLE KEYS */;
UNLOCK TABLES;






