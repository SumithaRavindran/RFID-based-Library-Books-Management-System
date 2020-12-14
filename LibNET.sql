/* RFID based Library Books Management System
TCSSS 559 : Project Group 2
Authors: Sumitha and Jordan
Last Updated : 12/14/2020 */

Drop database LibNet;
CREATE DATABASE LibNET;
USE LibNET;

/* Creation of employees Table*/
CREATE TABLE `employees` (
  `EmpID` int NOT NULL AUTO_INCREMENT,
  `Fname` varchar(200) NOT NULL,
  `Lname` varchar(200) NOT NULL,
  `email` varchar(200) NOT NULL,
  `gender` varchar(30) DEFAULT NULL,
  `phoneNum` varchar(10) NOT NULL,
  `userName` varchar(35) NOT NULL,
  `pass` varchar(40) NOT NULL,
  PRIMARY KEY (`EmpID`),
  UNIQUE KEY `email_uk` (`email`)
);



/* Creation of books Table*/
CREATE TABLE `books` (
  `Rfid` int NOT NULL AUTO_INCREMENT,
  `name` varchar(250) NOT NULL,
  `Author` varchar(250) NOT NULL,
  `ebook_url` varchar(300),
  `genre` varchar(50) NOT NULL,
  `userID` int NOT NULL,
  `publishedOn` date NOT NULL,
  `publicationCompany` varchar(250) NOT NULL,
  PRIMARY KEY (`Rfid`)
);


/* Creation of locations Table*/
CREATE TABLE `locations` (
  `libraryID` int NOT NULL AUTO_INCREMENT,
  `name` varchar(250) NOT NULL,
  `address` varchar(250) NOT NULL,
  `longitude` double NOT NULL,
  `latitude` double NOT NULL,
   PRIMARY KEY (`libraryID`)
);

/* Creation of user Table*/
CREATE TABLE `user` (
  `userID` int NOT NULL AUTO_INCREMENT,
  `Fname` varchar(50) NOT NULL,
  `Lname` varchar(50) NOT NULL,
  `address` varchar(250) NOT NULL,
  `birthdate` date NOT NULL,
  `email` varchar(100) NOT NULL,
  PRIMARY KEY (`userID`),
  UNIQUE KEY `email_uk` (`email`)
);


/* Insert statements for employees Table*/
INSERT INTO employees VALUES ('1', 'Sumitha', 'Ravindran', 'sumi@gmail.com', 'Female', '1234567890', 'Sumitha','Sumitha12345');
INSERT INTO employees VALUES ('2', 'Jordan', 'Overbo', 'jao@gmail.com', 'Male', '2345678901', 'Jordan','Jordan12345');
INSERT INTO employees VALUES ('3', 'Arun', 'Raj', 'arun@gmail.com', 'Male', '4567890123', 'Arun','Arun12345');
INSERT INTO employees VALUES ('4', 'Micheal', 'Little', 'micheal@gmail.com', 'Male', '7890123456', 'Micheal','Micheal12345');
INSERT INTO employees VALUES ('5', 'Priya', 'Raj', 'priya@gmail.com', 'Female', '9012345678', 'Priya','Priya12345');

/* Insert statements for books Table*/
INSERT INTO books VALUES(6, 'The Handmaids tale', 'Margaret Atwood','https://the-eye.eu/public/Books/robot.bolink.org/PHPInterface.pdf', 'Fantasy', -1, '2010-08-07', 'Anchor Books');
INSERT INTO books VALUES(7, 'Harry Potter', 'J.k.Rowling', 'https://the-eye.eu/public/Books/robot.bolink.org/PHPInterface.pdf', 'Fantasy', 2, '2008-09-07', 'Scholistic Press');
INSERT INTO books VALUES(8, 'Lord of files', 'William Golding', 'https://the-eye.eu/public/Books/scifi%2C%20cult%2C%20horror%20and%20fantasy%20film%20and%20fiction%20zines/Cinefantastique/Cinefantastique%20v26n06-v27n01.pdf', 'Psychological Fiction', -1, '2020-12-03', 'Faber and faber');
INSERT INTO books VALUES(9, 'Lord of files', 'William Golding', 'https://the-eye.eu/public/Books/scifi%2C%20cult%2C%20horror%20and%20fantasy%20film%20and%20fiction%20zines/Cinefantastique/Cinefantastique%20v26n06-v27n01.pdf', 'Psychological Fiction', -1, '2020-12-03', 'Faber and faber');

/* Insert statements for locations Table*/
INSERT INTO `locations` VALUES (1,'Puyallup Public Library','324 S Meridian, Puyallup, WA 98371',-122.295169,47.189588);
INSERT INTO `locations` VALUES (2,'Seattle Public Library-Central Library','1000 4th Ave, Seattle, WA 98104',-122.3325,47.6067);
INSERT INTO `locations` VALUES (3,'South Hill Pierce County Library','15420 Meridian E, Puyallup, WA 98375',-122.2941,47.1163);
INSERT INTO `locations` VALUES (4,'Bellevue Library','1111 110th Ave NE, Bellevue, WA 98004',-122.1943,47.6202);

/* Insert statements for user Table*/
INSERT INTO `user` VALUES (1,'Roshi','Ram','123, Seattle WA, 12345','2000-01-01','roshi@gmail.com');
INSERT INTO `user` VALUES (2,'Sam','Sandy','321, Bellevue WA, 98005','2008-01-10','sam@gmail.com');
INSERT INTO `user` VALUES (3,'Neil','Davis','234, Seattle WA, 34590','1995-01-01','neil@gmail.com');
INSERT INTO `user` VALUES (4,'Amala','Ravi','123, Tacoma WA, 12345','1999-01-01','amala@gmail.com');

/*Selection statements*/
select * from employees;
select * from books;
select * from locations;
select * from user;

