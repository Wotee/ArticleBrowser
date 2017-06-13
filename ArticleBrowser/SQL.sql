BEGIN TRANSACTION;
CREATE TABLE `ItemCategory` (
	`ItemID`	INTEGER NOT NULL,
	`CategoryID`	INTEGER NOT NULL,
	FOREIGN KEY(`ItemID`) REFERENCES Item(ID),
	FOREIGN KEY(`CategoryID`) REFERENCES Category(ID)
);
INSERT INTO `ItemCategory` VALUES (1,1);
INSERT INTO `ItemCategory` VALUES (1,2);
CREATE TABLE `Item` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Title`	TEXT NOT NULL,
	`Author`	TEXT NOT NULL,
	`Filepath`	TEXT NOT NULL
);
INSERT INTO `Item` VALUES (1,'TestTitle','Wote','test.pdf');
CREATE TABLE `Category` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Category`	INTEGER NOT NULL
);
INSERT INTO `Category` VALUES (1,'Big Data');
INSERT INTO `Category` VALUES (2,'Small Data');
COMMIT;