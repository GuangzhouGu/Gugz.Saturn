CREATE TABLE `alarmdetail` (
  `AlarmId` varchar(128) NOT NULL,
  `AlarmLevel` int(11) NOT NULL,
  `Description` longtext,
  `DescriptionCN` longtext,
  `UpdateTime` datetime NOT NULL,
  `PlcAddr` varchar(100) DEFAULT NULL,
  `AlarmType` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`AlarmId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `alarmhistory` (
  `AlarmId` varchar(128) NOT NULL,
  `AlarmTime` datetime NOT NULL,
  `AlarmCancelTime` datetime DEFAULT NULL,
  `Alive` int(11) NOT NULL,
  `EqpId` varchar(50) DEFAULT NULL,
  `UnitId` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`AlarmId`,`AlarmTime`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
