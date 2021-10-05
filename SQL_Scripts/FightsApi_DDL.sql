
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
LocationId int not null identity(1,1) primary key,
[Location] nvarchar(100) not null
);

CREATE TABLE Weather(
WeatherId int not null identity(1,1) primary key,
[Description] nvarchar(100) not null
);

CREATE TABLE Fight(
FightId int not null identity (1,1) primary key,
[StartDate] DATETIME2 default getdate(),
[EndDate] DATETIME2 default getdate(),
[Location] int foreign key references [Location](LocationId),
Weather int foreign key references Weather(WeatherId)
);

CREATE TABLE Fighter(
FighterId int not null identity(1,1) primary key,
FightId int not null FOREIGN KEY REFERENCES Fight(FightId) on delete no action,
CharacterId int NOT NULL,
isWinner bit NOT NULL default 0
);

CREATE TABLE Votes(
	VoteId int not null identity(1,1) primary key,
	FightId int not null Foreign key references Fight,
	FighterId int not null Foreign key references Fighter,
	UserId int not null, -- USER ID of voter
	CONSTRAINT Votes_UNQ UNIQUE (FightId, UserId)
);