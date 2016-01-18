CREATE TABLE `GameScoreBoard` (
    `GameName` varchar(11) DEFAULT NULL,
    `PlayerName` varchar(11) DEFAULT NULL,
    `Score` int(11) unsigned,
    `recordId` int(11) unique key AUTO_INCREMENT,
    PRIMARY KEY (`recordId`))
ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8
