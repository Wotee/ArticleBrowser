CREATE TABLE Item
(
	ID	INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	Title	TEXT(256) NOT NULL,
	Author	TEXT(256) NOT NULL,
	Year	INT NOT NULL,
	File	BLOB NOT NULL
);

CREATE TABLE Category (
	ID	INT NOT NULL PRIMARY KEY,
	Category	INT NOT NULL
);

CREATE TABLE ItemCategory (
	ItemID	INT NOT NULL,
	CategoryID	INT NOT NULL,
	FOREIGN KEY(ItemID) REFERENCES Item(ID),
	FOREIGN KEY(CategoryID) REFERENCES Category(ID)
);
