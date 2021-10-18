
--CREATE DATABASE P3_NotFightClub;
--go

USE P3_NotFightClub;
go


DROP TABLE IF EXISTS Votes;
DROP TABLE IF EXISTS Fighter;
DROP TABLE IF EXISTS Fight;
DROP TABLE IF EXISTS [Location];
DROP TABLE IF EXISTS Weather;

CREATE TABLE [Location](
LocationId int not null identity(1,1) constraint PK_Location primary key,
[Location] nvarchar(100) not null
);

CREATE TABLE Weather(
WeatherId int not null identity(1,1) constraint PK_Weather primary key,
[Description] nvarchar(100) not null
);

CREATE TABLE Fight(
FightId int not null identity (1,1) constraint PK_Fight primary key,
[StartDate] DATETIME2 default getdate(),
[EndDate] DATETIME2 default getdate(),
[Location] int constraint FK_Fight_Location foreign key references [Location](LocationId),
Weather int constraint FK_Fight_Weather foreign key references Weather(WeatherId),
CreatorId uniqueidentifier null, -- if fight was created by a user, this is the userId
[Public] bit NOT NULL default 0,
Finished bit NOT NULL default 0
);

CREATE TABLE Fighter(
FighterId int not null identity(1,1) constraint PK_Fighter primary key,
FightId int not null constraint FK_Fighter_Fight FOREIGN KEY REFERENCES Fight(FightId) on delete no action,
CharacterId int NOT NULL,
isWinner bit NOT NULL default 0
);

CREATE TABLE Votes(
	VoteId int not null identity(1,1) constraint PK_Votes primary key,
	FightId int not null constraint FK_Votes_Fight Foreign key references Fight,
	FighterId int not null constraint FK_Votes_Fighter Foreign key references Fighter,
	UserId int not null, -- USER ID of voter
	CONSTRAINT Votes_UNQ UNIQUE (FightId, UserId)
);


INSERT INTO [Location] ([Location])
VALUES
('Palm Beach'),
('LAS VEGAS'),
('Subway'),
('Kathmandu');

INSERT INTO Weather ([Description])
VALUES
('Super sunny'),
('Hailstorm'),
('Tornado');

SELECT * FROM Fight;

SELECT * FROM Fighter WHERE FightId>12;

SELECT * FROM Votes;