CREATE TABLE [ApartmentComplexes] (
    [ID] int NOT NULL IDENTITY,
    [Address] varchar(256) NOT NULL,
    [Name] varchar(256) NOT NULL,
    [Size] int NOT NULL,
    CONSTRAINT [PK_ApartmentComplexes] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [People] (
    [ID] int NOT NULL IDENTITY,
    [Email] varchar(256) NOT NULL,
    [FirstName] varchar(64) NOT NULL,
    [LastName] varchar(64) NOT NULL,
    [Phone] varchar(10) NOT NULL,
    CONSTRAINT [PK_People] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [ApartmentComplexUnits] (
    [ID] int NOT NULL IDENTITY,
    [ApartmentComplexID] int NOT NULL,
    [Area] int NOT NULL,
    [BathRooms] int NOT NULL,
    [BedRooms] int NOT NULL,
    [UnitNumber] int NOT NULL,
    CONSTRAINT [PK_ApartmentComplexUnits] PRIMARY KEY ([ID]),
    CONSTRAINT [ApartmentComplexUnit_ApartmentComplex_ID_fk] FOREIGN KEY ([ApartmentComplexID]) REFERENCES [ApartmentComplexes] ([ID]) ON DELETE CASCADE
);

GO

CREATE TABLE [Leases] (
    [ID] int NOT NULL IDENTITY,
    [ApartmentComplexUnitID] int NOT NULL,
    [DurationMonths] int NOT NULL,
    [PersonID] int NOT NULL,
    [RentMonthly] int NOT NULL,
    [DateMillis] bigint NOT NULL,
    CONSTRAINT [PK_Leases] PRIMARY KEY ([ID]),
    CONSTRAINT [Lease_ApartmentComplexUnit_ID_fk] FOREIGN KEY ([ApartmentComplexUnitID]) REFERENCES [ApartmentComplexUnits] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [Lease_Person_ID_fk] FOREIGN KEY ([PersonID]) REFERENCES [People] ([ID]) ON DELETE CASCADE
);

GO

CREATE UNIQUE INDEX [ApartmentComplex_ID_uindex] ON [ApartmentComplexes] ([ID]);

GO

CREATE INDEX [IX_ApartmentComplexUnits_ApartmentComplexID] ON [ApartmentComplexUnits] ([ApartmentComplexID]);

GO

CREATE UNIQUE INDEX [ApartmentComplexUnit_ID_uindex] ON [ApartmentComplexUnits] ([ID]);

GO

CREATE INDEX [IX_Leases_ApartmentComplexUnitID] ON [Leases] ([ApartmentComplexUnitID]);

GO

CREATE UNIQUE INDEX [Lease_ID_uindex] ON [Leases] ([ID]);

GO

CREATE INDEX [IX_Leases_PersonID] ON [Leases] ([PersonID]);

GO

CREATE UNIQUE INDEX [Person_ID_uindex] ON [People] ([ID]);

GO


