INSERT INTO `mooddb`.`device` (`Label`) VALUES ('mon pc');
INSERT INTO `mooddb`.`moodface`(`Key`,`Picture`,`Label`) VALUES('1','smiley-good-svgrepo-com.png','heureux');
INSERT INTO `mooddb`.`moodface`(`Key`,`Picture`,`Label`) VALUES('2','smiley-good-svgrepo-com.png','content');
INSERT INTO `mooddb`.`moodface`(`Key`,`Picture`,`Label`) VALUES('3','smiley-neutral-svgrepo-com.png','normal');
INSERT INTO `mooddb`.`moodface`(`Key`,`Picture`,`Label`) VALUES('4','smiley-sad-svgrepo-com.png','triste');

INSERT INTO `mooddb`.`moodentry` (`MoodFaceId`,`Date`,`MoodDeviceId`) VALUES (1,now(),1);
INSERT INTO `mooddb`.`moodentry` (`MoodFaceId`,`Date`,`MoodDeviceId`) VALUES (2,now(),1);
INSERT INTO `mooddb`.`moodentry` (`MoodFaceId`,`Date`,`MoodDeviceId`) VALUES (3,now(),1);
INSERT INTO `mooddb`.`moodentry` (`MoodFaceId`,`Date`,`MoodDeviceId`) VALUES (3,now(),1);




