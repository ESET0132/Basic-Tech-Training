CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "User" (
  UserId         BIGSERIAL PRIMARY KEY,
  Username       VARCHAR(100) NOT NULL UNIQUE,
  PasswordHash   BYTEA NOT NULL,
  DisplayName    VARCHAR(150) NOT NULL,
  Email          VARCHAR(200) NULL,
  Phone          VARCHAR(30) NULL,
  LastLoginUtc   TIMESTAMP(3) NULL,
  IsActive       BOOLEAN NOT NULL DEFAULT true
);

CREATE TABLE OrgUnit (
  OrgUnitId SERIAL PRIMARY KEY,
  Type VARCHAR(20) NOT NULL CHECK (Type IN ('Zone','Substation','Feeder','DTR')),
  Name VARCHAR(100) NOT NULL,
  ParentId INT NULL REFERENCES OrgUnit(OrgUnitId)
);

CREATE TABLE Tariff (
  TariffId SERIAL PRIMARY KEY,
  Name VARCHAR(100) NOT NULL,
  EffectiveFrom DATE NOT NULL,
  EffectiveTo DATE NULL,
  BaseRate DECIMAL(18,4) NOT NULL,
  TaxRate DECIMAL(18,4) NOT NULL DEFAULT 0
);


CREATE TABLE TodRule (
  TodRuleId      SERIAL PRIMARY KEY,
  TariffId       INT NOT NULL REFERENCES Tariff(TariffId),
  Name           VARCHAR(50) NOT NULL,
  StartTime      TIME(0) NOT NULL,
  EndTime        TIME(0) NOT NULL,
  RatePerKwh     DECIMAL(18,6) NOT NULL
);


CREATE TABLE TariffSlab (
  TariffSlabId   SERIAL PRIMARY KEY,
  TariffId       INT NOT NULL REFERENCES Tariff(TariffId),
  FromKwh        DECIMAL(18,6) NOT NULL,
  ToKwh          DECIMAL(18,6) NOT NULL,
  RatePerKwh     DECIMAL(18,6) NOT NULL,
  CONSTRAINT CK_TariffSlab_Range CHECK (FromKwh >= 0 AND ToKwh > FromKwh)
);


CREATE TABLE Consumer (
  ConsumerId BIGSERIAL PRIMARY KEY,
  Name VARCHAR(200) NOT NULL,
  Address VARCHAR(500) NULL,
  Phone VARCHAR(30) NULL,
  Email VARCHAR(200) NULL,
  OrgUnitId INT NOT NULL REFERENCES OrgUnit(OrgUnitId),
  TariffId INT NOT NULL REFERENCES Tariff(TariffId),
  Status VARCHAR(20) NOT NULL DEFAULT 'Active' CHECK (Status IN ('Active','Inactive')),
  CreatedAt TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CreatedBy VARCHAR(100) NOT NULL DEFAULT 'system',
  UpdatedAt TIMESTAMP(3) NULL,
  UpdatedBy VARCHAR(100) NULL
);


CREATE TABLE Meter (
  MeterSerialNo VARCHAR(50) NOT NULL PRIMARY KEY,
  IpAddress VARCHAR(45) NOT NULL,
  ICCID VARCHAR(30) NOT NULL,
  IMSI VARCHAR(30) NOT NULL,
  Manufacturer VARCHAR(100) NOT NULL,
  Firmware VARCHAR(50) NULL,
  Category VARCHAR(50) NOT NULL,
  InstallTsUtc TIMESTAMP(3) NOT NULL,
  Status VARCHAR(20) NOT NULL DEFAULT 'Active'
           CHECK (Status IN ('Active','Inactive','Decommissioned')),
  ConsumerId BIGINT NULL REFERENCES Consumer(ConsumerId)
);


CREATE TABLE MeterDetails (
  MeterDetailId BIGSERIAL PRIMARY KEY,
  MeterSerialNo VARCHAR(50) NOT NULL REFERENCES Meter(MeterSerialNo),
  ParameterName VARCHAR(100) NOT NULL,
  ParameterValue VARCHAR(500) NOT NULL,
  ParameterType VARCHAR(50) NOT NULL CHECK (ParameterType IN ('Technical','Configuration','Communication','Security','Calibration')),
  DataType VARCHAR(30) NOT NULL CHECK (DataType IN ('String','Number','Boolean','Date','JSON')),
  Unit VARCHAR(30) NULL,
  MinValue DECIMAL(18,6) NULL,
  MaxValue DECIMAL(18,6) NULL,
  DefaultValue VARCHAR(200) NULL,
  IsReadOnly BOOLEAN NOT NULL DEFAULT false,
  LastUpdated TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  UpdatedBy VARCHAR(100) NOT NULL DEFAULT 'system',
  Description VARCHAR(500) NULL,
  
  CONSTRAINT UQ_MeterDetails_Parameter UNIQUE (MeterSerialNo, ParameterName),
  CONSTRAINT CK_MeterDetails_ValueRange CHECK (
    (MinValue IS NULL AND MaxValue IS NULL) OR 
    (DataType != 'Number' AND MinValue IS NULL AND MaxValue IS NULL) OR
    (DataType = 'Number' AND MinValue IS NOT NULL AND MaxValue IS NOT NULL AND MinValue <= MaxValue)
  )
);


CREATE TABLE Arrears (
  ArrearsId SERIAL PRIMARY KEY,
  ConsumerId BIGINT NOT NULL REFERENCES Consumer(ConsumerId),
  AType VARCHAR(50) NOT NULL CHECK (AType IN ('Principal','Penalty','Interest','Other')),
  Amount DECIMAL(18,2) NOT NULL,
  PaidStatus VARCHAR(20) NOT NULL DEFAULT 'Pending' CHECK (PaidStatus IN ('Pending','Paid','Partially_Paid','Waived')),
  BillId BIGINT NULL, -- Will reference Bill table after creation
  CreatedDate TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  DueDate DATE NOT NULL,
  PaidDate TIMESTAMP(3) NULL,
  Remarks VARCHAR(500) NULL
);


CREATE TABLE TariffDetails (
  TariffDetailId SERIAL PRIMARY KEY,
  TariffId INT NOT NULL REFERENCES Tariff(TariffId),
  TariffType VARCHAR(50) NOT NULL CHECK (TariffType IN ('Fixed','Slab','TOD','Demand','Other')),
  TariffRate DECIMAL(18,6) NOT NULL,
  EffectiveFrom DATE NOT NULL,
  EffectiveTo DATE NULL,
  MinimumCharge DECIMAL(18,2) NULL,
  MaximumCharge DECIMAL(18,2) NULL,
  Description VARCHAR(300) NULL,
  CreatedAt TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CreatedBy VARCHAR(100) NOT NULL DEFAULT 'system'
);




CREATE TABLE Bill (
  BillId BIGSERIAL PRIMARY KEY,
  BillDate DATE NOT NULL,
  BillAmount DECIMAL(18,2) NOT NULL,
  MeterId VARCHAR(50) NOT NULL REFERENCES Meter(MeterSerialNo),
  TariffId INT NOT NULL REFERENCES Tariff(TariffId),
  CreatedDate TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PaymentDate TIMESTAMP(3) NULL,
  DueDate DATE NOT NULL,
  CreatedBy VARCHAR(100) NOT NULL,
  PreviousReading DECIMAL(18,6) NOT NULL,
  CurrentReading DECIMAL(18,6) NOT NULL,
  CurrentReadingDate TIMESTAMP(3) NOT NULL,
  PreviousReadingDate TIMESTAMP(3) NOT NULL,
  PowerFactor DECIMAL(5,4) NULL CHECK (PowerFactor BETWEEN 0 AND 1),
  LoadFactor DECIMAL(5,4) NULL CHECK (LoadFactor BETWEEN 0 AND 1),
  DisconnectionDate DATE NULL,
  Status VARCHAR(20) NOT NULL DEFAULT 'Generated' CHECK (Status IN ('Generated','Paid','Overdue','Cancelled')),
  Consumption DECIMAL(18,6) GENERATED ALWAYS AS (CurrentReading - PreviousReading) STORED,
  CONSTRAINT CK_Bill_Reading CHECK (CurrentReading >= PreviousReading)
);


ALTER TABLE Arrears ADD CONSTRAINT FK_Arrears_Bill 
  FOREIGN KEY (BillId) REFERENCES Bill(BillId);



  