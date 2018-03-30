CREATE TABLE [USER]
(
	id INT PRIMARY KEY IDENTITY(1,1),
	username VARCHAR(256),
	password VARCHAR(256),
	userlevel INT
);

CREATE TABLE CLIENT
(
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(256),
	icn VARCHAR(256),
	pnc VARCHAR(256),
	address VARCHAR(256)
);

CREATE TABLE RESERVATION
(
	id INT PRIMARY KEY IDENTITY(1,1),
	clientid INT,
	userid INT,
	destination VARCHAR(256),
	hotelname VARCHAR(256),
	personcount INT,
	details VARCHAR(4096),
	totalprice INT,
	paidamount INT,
	finalpaymentdate BIGINT,
	reservationdate BIGINT,

	FOREIGN KEY (clientid) REFERENCES dbo.CLIENT(id),
	FOREIGN KEY (userid) REFERENCES dbo.[USER](id)
);