ALTER TABLE PropertyMismanagement.dbo.Leases DROP CONSTRAINT Lease_Person_ID_fk

GO

ALTER TABLE PropertyMismanagement.dbo.Leases DROP CONSTRAINT Lease_ApartmentComplexUnit_ID_fk

GO

ALTER TABLE PropertyMismanagement.dbo.ApartmentComplexUnits DROP CONSTRAINT ApartmentComplexUnit_ApartmentComplex_ID_fk

GO

DROP TABLE PropertyMismanagement.dbo.Leases

GO

DROP TABLE PropertyMismanagement.dbo.ApartmentComplexUnits

GO

DROP TABLE PropertyMismanagement.dbo.People

GO

DROP TABLE PropertyMismanagement.dbo.ApartmentComplexes

GO