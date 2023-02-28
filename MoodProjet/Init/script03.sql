-- -----------------------------------------------------
-- Table `mooddb`.`user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mooddb`.`user` ;

CREATE TABLE `user` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Login` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `CanAdminDevices` tinyint DEFAULT '0',
  `CanAdminMoodFaces` tinyint DEFAULT '0',
  `CanAdminMoodEntries` tinyint DEFAULT '0',
  `CanSeeCharts` tinyint DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `mooddb`.`user` (`Login`,`Password`,`CanAdminDevices`,`CanAdminMoodFaces`,`CanAdminMoodEntries`,`CanSeeCharts`) VALUES ('bob','bob',0,0,0,0);

INSERT INTO `mooddb`.`user` (`Login`,`Password`,`CanAdminDevices`,`CanAdminMoodFaces`,`CanAdminMoodEntries`,`CanSeeCharts`) VALUES ('john','john',0,0,0,1);

INSERT INTO `mooddb`.`user` (`Login`,`Password`,`CanAdminDevices`,`CanAdminMoodFaces`,`CanAdminMoodEntries`,`CanSeeCharts`) VALUES ('zac','zac',1,1,1,1);