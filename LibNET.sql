/* This is the SQL statements that will create and populate LibNET's database. */


/* These blocks will create the tables */
CREATE TABLE EMPLOYEES (
	EmpID int NOT NULL,
	Fname varchar(50) NOT NULL,
	Lname varchar(50) NOT NULL,
	email varchar(75) NOT NULL,
	gender varchar(30),
	phoneNum varchar(10) NOT NULL,
	userName varchar(35) NOT NULL,
	pass varchar(40) NOT NULL,
	PRIMARY KEY (EmpID)
);

CREATE TABLE BOOKS (
	RFID varchar(30) NOT NULL,
	title varchar(100) NOT NULL,
	author varchar(100) NOT NULL,
	genre varchar(30) NOT NULL,
	avail varchar(5) NOT NULL,
	userID int,
	publishedOn DATE,
	bookType varchar(30) NOT NULL,
	publishedBy varvhar(50) NOT NULL,
	PRIMARY KEY (RFID)
); 

CREATE TABLE LOCATIONS (
	LibID int NOT NULL,
	libAddress varchar(100) NOT NULL,
	latitude double precision NOT NULL,
	longitude double precision NOT NULL,
	PRIMARY KEY (LibID)
);

CREATE TABLE USERS (
	UserID int NOT NULL,
	Fname varchar(50) NOT NULL,
	Lname varchar(50) NOT NULL,
	userAddress varchar(100) NOT NULL,
	birthdate date NOT NULL,
	email varchar(75) NOT NULL,
	PRIMARY KEY (UserID)
);

/* Insert statements to populate data in the tables. */
INSERT INTO EMPLOYEES (EmpID, Fname, Lname, email, gender, phoneNum, userName, pass)
VALUES ('567192221', 'James', 'Smith', 'jsmith@gmail.com', 'male', '2531234567', 'jsmith','testpassword');

INSERT INTO BOOKS (RFID, title, author, genre, avail, bookType, publishedBy)
VALUES ('152635147', 'Test Book', 'Test Author', 'Fantasy', 'True', 'paperback', 'Test Publishing House');

INSERT INTO LOCATIONS (LibID, libAddress, latitude, longitude)
VALUES ('1569874567', '324 S Meridian, Puyallup, WA 98371', 47.189588, -122.295169);

INSERT INTO USERS (UserId, Fname, Lname, userAddress, birthdate, email)
VALUES ('456978452', 'Jane', 'Doe', '1524 test ave e, Puyallup, WA 98375', '2000-05-06', 'testUser@gmail.com');
