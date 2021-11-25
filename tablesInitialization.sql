
--DROP TABLE Plants;
CREATE TABLE Plants
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NCHAR (25) NOT NULL,
	[Description] NCHAR (25) NOT NULL,
	[PricePerUnit] NUMERIC (3,1) NOT NULL,
	[MaxTemperature] NUMERIC (3,1) NOT NULL,
	[MinTemperature] NUMERIC (3,1) NOT NULL,
	[Humidity] NUMERIC (3,1) NOT NULL
);
--DROP TABLE Plantation;
CREATE TABLE Plantation
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NCHAR (25) NOT NULL,
	[Description] NCHAR (25) NOT NULL,
	[PlantId] INT NOT NULL,

	CONSTRAINT fk_plant FOREIGN KEY(PlantId) REFERENCES Plants(id)
);

ALTER TABLE Plantation DROP CONSTRAINT fk_plant;
