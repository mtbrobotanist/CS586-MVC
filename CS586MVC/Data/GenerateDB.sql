CREATE TABLE [Complex] (
    [ID] int NOT NULL IDENTITY,
    [Address] varchar(256) NULL,
    [Size] int NOT NULL,
    CONSTRAINT [PK_Complex] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [Person] (
    [ID] int NOT NULL IDENTITY,
    [Email] varchar(256) NOT NULL,
    [FirstName] varchar(64) NOT NULL,
    [LastName] varchar(64) NOT NULL,
    [Phone] varchar(10) NOT NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [Unit] (
    [ID] int NOT NULL IDENTITY,
    [Area] int NULL,
    [Bathrooms] int NOT NULL,
    [BedRooms] int NOT NULL,
    CONSTRAINT [PK_Unit] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [ComplexUnit] (
    [ID] int NOT NULL IDENTITY,
    [ComplexID] int NULL,
    [UnitID] int NULL,
    [UnitNumber] int NOT NULL,
    CONSTRAINT [PK_ComplexUnit] PRIMARY KEY ([ID]),
    CONSTRAINT [ComplexUnit_Complex_ID_fk] FOREIGN KEY ([ComplexID]) REFERENCES [Complex] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [ComplexUnit_Unit_ID_fk] FOREIGN KEY ([UnitID]) REFERENCES [Unit] ([ID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Lease] (
    [ID] int NOT NULL IDENTITY,
    [ComplexUnitID] int NOT NULL,
    [DurationMonths] int NOT NULL,
    [PersonID] int NOT NULL,
    [RentMonthly] int NOT NULL,
    [StartDate] date NOT NULL,
    CONSTRAINT [PK_Lease] PRIMARY KEY ([ID]),
    CONSTRAINT [Lease_ComplexUnit_ID_fk] FOREIGN KEY ([ComplexUnitID]) REFERENCES [ComplexUnit] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [Lease_Person_ID_fk] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([ID]) ON DELETE NO ACTION
);

GO

CREATE UNIQUE INDEX [Complex_ID_uindex] ON [Complex] ([ID]);

GO

CREATE INDEX [IX_ComplexUnit_ComplexID] ON [ComplexUnit] ([ComplexID]);

GO

CREATE UNIQUE INDEX [ComplexUnit_ID_uindex] ON [ComplexUnit] ([ID]);

GO

CREATE INDEX [IX_ComplexUnit_UnitID] ON [ComplexUnit] ([UnitID]);

GO

CREATE INDEX [IX_Lease_ComplexUnitID] ON [Lease] ([ComplexUnitID]);

GO

CREATE UNIQUE INDEX [Lease_ID_uindex] ON [Lease] ([ID]);

GO

CREATE INDEX [IX_Lease_PersonID] ON [Lease] ([PersonID]);

GO

CREATE UNIQUE INDEX [Person_ID_uindex] ON [Person] ([ID]);

GO
