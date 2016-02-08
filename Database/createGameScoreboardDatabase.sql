CREATE TABLE `GameScoreBoard` (
    `GameName` varchar(15) DEFAULT NULL,
    `PlayerName` varchar(15) DEFAULT NULL,
    `Score` int(15) unsigned,
    `recordId` int(11) unique key AUTO_INCREMENT,
    PRIMARY KEY (`recordId`))
ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8
