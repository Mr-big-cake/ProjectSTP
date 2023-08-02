USE [DatabaseProductsSTP]
GO

SELECT * FROM [dbo].[Manager]
SELECT * FROM [dbo].[ClientStatus]
SELECT * FROM [dbo].[ProductType]
SELECT * FROM [dbo].[SubscriptionDuration]
SELECT * FROM [dbo].[Client]
SELECT * FROM [dbo].[Product]
SELECT * FROM [dbo].[PurchasedProduct]



--1. ������ �������� �� ����������:

SELECT c.ClientID AS ClientID, c.Name AS ClientName, m.ManagerID AS ManagerID,  m.Name AS ManagerName
FROM Client c
JOIN Manager m ON c.ManagerID = m.ManagerID;

--2. ������ ������� �� ��������:
SELECT c.ClientID AS ClientID, c.Name AS ClientName, p.ProductID AS ProductID, p.Name AS ProductName
FROM Client c
JOIN PurchasedProduct pp ON c.ClientID = pp.ClientID
JOIN Product p ON pp.ProductID = p.ProductID;

--3. ������ �������� �� ��������:

SELECT c.ClientID AS ClientID, c.Name AS ClientName, cs.Status AS ClientStatus
FROM Client c
JOIN ClientStatus cs ON c.StatusID = cs.StatusID;

--4. ���������

SELECT [ManagerID], [Name] FROM [dbo].[Manager]

--5. �������

SELECT [ClientID], [Name] FROM [dbo].[Client]

--6. ������

SELECT [ProductID], [Name]  FROM [dbo].[Product]


EXEC [dbo].[GetClientListByManager]