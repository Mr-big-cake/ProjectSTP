USE [DatabaseProductsSTP]
GO

CREATE TABLE [dbo].[Manager]
(
	[ManagerID] int NOT NULL PRIMARY KEY IDENTITY,
	[Name] varchar(255) NOT NULL
);

CREATE TABLE [dbo].[ClientStatus]
(
	[StatusID] int PRIMARY KEY,
    [Status] varchar(255) NOT NULL
);

CREATE TABLE [dbo].[Client]
(
	[ClientID] int PRIMARY KEY IDENTITY,
    [Name] varchar(255) NOT NULL,
    [ManagerID] int NOT NULL,
    [StatusID] int NOT NULL,
    FOREIGN KEY (ManagerID) REFERENCES Manager(ManagerID),
    FOREIGN KEY (StatusID) REFERENCES ClientStatus(StatusID)
);

CREATE TABLE [dbo].[ProductType]
(
	[TypeID] int PRIMARY KEY,
    [Product] varchar(255) NOT NULL
);

CREATE TABLE [dbo].[SubscriptionDuration]
(
	[SubscriptionDurationID] int PRIMARY KEY,
    [Duration] varchar(255) NOT NULL
);
	
CREATE TABLE [dbo].[Product]
(
	[ProductID] int PRIMARY KEY IDENTITY,
    [Name] varchar(255) NOT NULL,
    [Price] decimal(10, 2) NOT NULL,
    [TypeID] int NOT NULL,
    [SubscriptionDurationID] int NULL, 
	FOREIGN KEY ([TypeID]) REFERENCES [ProductType]([TypeID]),
	FOREIGN KEY ([SubscriptionDurationID]) REFERENCES [SubscriptionDuration]([SubscriptionDurationID]),
);

CREATE TABLE [dbo].[PurchasedProduct]
(
	[ClientID] int,
    [ProductID] int,
    FOREIGN KEY (ClientID) REFERENCES Client(ClientID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);
