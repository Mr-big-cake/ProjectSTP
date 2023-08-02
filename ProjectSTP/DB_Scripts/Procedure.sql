USE [DatabaseProductsSTP]
GO

--1. Список клиентов по менеджерам:
CREATE PROCEDURE [dbo].[GetClientListByManager]
AS
BEGIN
    SELECT 
        c.[ClientID] AS ClientID, 
        c.[Name] AS ClientName, 
        m.[ManagerID] AS ManagerID,  
        m.[Name] AS ManagerName
    FROM [dbo].[Client] c
    JOIN [Manager] m ON c.[ManagerID] = m.[ManagerID];
END
GO

--2. Список товаров по клиентам:
CREATE PROCEDURE [dbo].[GetProductListByClient]
AS
BEGIN
    SELECT 
        c.[ClientID] AS ClientID, 
        c.[Name] AS ClientName, 
        p.[ProductID] AS ProductID, 
        p.[Name] AS ProductName
    FROM [dbo].[Client] c
    JOIN [PurchasedProduct] pp ON c.[ClientID] = pp.[ClientID]
    JOIN [Product] p ON pp.[ProductID] = p.[ProductID];
END
GO

--3. Список клиентов по статусам:
CREATE PROCEDURE [dbo].[GetClientListByStatus]
AS
BEGIN
    SELECT 
        c.[ClientID] AS ClientID, 
        c.[Name] AS ClientName, 
        cs.[Status] AS ClientStatus
    FROM [dbo].[Client] c
    JOIN [ClientStatus] cs ON c.[StatusID] = cs.[StatusID];
END
GO

--4. Менеджеры
CREATE PROCEDURE [dbo].[GetManagers]
AS
BEGIN
    SELECT [ManagerID], [Name] FROM [dbo].[Manager]
END
GO

--5. Клиенты
CREATE PROCEDURE [dbo].[GetClients]
AS
BEGIN
    SELECT [ClientID], [Name] FROM [dbo].[Client]
END
GO

--6. Товары
CREATE PROCEDURE [dbo].[GetProducts]
AS
BEGIN
    SELECT [ProductID], [Name]  FROM [dbo].[Product]
END
GO



--7. (+)Менеджер 

CREATE PROCEDURE  [dbo].[InsertManager]
    @Name NVARCHAR(50)
AS
BEGIN
    INSERT INTO [dbo].[Manager] ([Name])
    VALUES (@Name)
END
GO

--8. (%)Менеджер

CREATE PROCEDURE [dbo].[UpdateManager]
    @ManagerID INT,
    @Name NVARCHAR(50)
AS
BEGIN
    UPDATE [dbo].[Manager]
    SET [Name] = @Name
    WHERE [ManagerID] = @ManagerID
END
GO

--9. (-)Менеджер

CREATE PROCEDURE [dbo].[DeleteManager]
    @ManagerID INT
AS
BEGIN
    DELETE FROM [dbo].[Manager]
    WHERE [ManagerID] = @ManagerID
END
GO

--10. (+) Клиент
CREATE PROCEDURE [dbo].[InsertClient]
    @Name VARCHAR(255),
    @ManagerID INT,
    @StatusID INT
AS
BEGIN
    INSERT INTO [dbo].[Client] ([Name], [ManagerID], [StatusID])
    VALUES (@Name, @ManagerID, @StatusID)
END
GO

--11. (%) Клиент
CREATE PROCEDURE [dbo].[UpdateClient]
    @ClientID INT,
    @Name VARCHAR(255),
    @ManagerID INT,
    @StatusID INT
AS
BEGIN
    UPDATE [dbo].[Client]
    SET [Name] = @Name, [ManagerID] = @ManagerID, [StatusID] = @StatusID
    WHERE [ClientID] = @ClientID
END
GO

--12. (-) Клиент
CREATE PROCEDURE [dbo].[DeleteClient]
    @ClientID INT
AS
BEGIN
    DELETE FROM [dbo].[Client]
    WHERE [ClientID] = @ClientID
END
GO

--13. (+) Товар

CREATE PROCEDURE [dbo].[InsertProduct]
    @Name VARCHAR(255),
    @Price DECIMAL(10, 2),
    @TypeID INT,
    @SubscriptionDurationID INT
AS
BEGIN
    INSERT INTO [dbo].[Product] ([Name], [Price], [TypeID], [SubscriptionDurationID])
    VALUES (@Name, @Price, @TypeID, @SubscriptionDurationID)
END
GO

--14. (%) Товар

CREATE PROCEDURE [dbo].[UpdateProduct]
    @ProductID INT,
    @Name VARCHAR(255),
    @Price DECIMAL(10, 2),
    @TypeID INT,
    @SubscriptionDurationID INT
AS
BEGIN
    UPDATE [dbo].[Product]
    SET [Name] = @Name, [Price] = @Price, [TypeID] = @TypeID, [SubscriptionDurationID] = @SubscriptionDurationID
    WHERE [ProductID] = @ProductID
END

--15. (-) Товар

CREATE PROCEDURE [dbo].[DeleteProduct]
    @ProductID INT
AS
BEGIN
    DELETE FROM [dbo].[Product]
    WHERE [ProductID] = @ProductID
END
GO

--16. (%) Обновление только Менеджера у Клиента

CREATE PROCEDURE [dbo].[UpdateClientManager]
    @ClientID INT,
    @ManagerID INT
AS
BEGIN
    UPDATE [dbo].[Client]
    SET [ManagerID] = @ManagerID
    WHERE [ClientID] = @ClientID
END
GO


--17. (%) Обновление только Статуса у Клиента
CREATE PROCEDURE [dbo].[UpdateClientStatus]
    @ClientID INT,
    @StatusID INT
AS
BEGIN
    UPDATE [dbo].[Client]
    SET [StatusID] = @StatusID
    WHERE [ClientID] = @ClientID
END
GO

--19. (+)PurchasedProduct
CREATE PROCEDURE [dbo].[CreatePurchasedProduct]
    @ClientID INT,
    @ProductID INT
AS
BEGIN
    INSERT INTO [dbo].[PurchasedProduct] ([ClientID], [ProductID])
    VALUES (@ClientID, @ProductID)
END
GO
--20. (%)PurchasedProduct
CREATE PROCEDURE [dbo].[DeletePurchasedProduct]
    @ClientID INT,
    @ProductID INT
AS
BEGIN
    DELETE FROM [dbo].[PurchasedProduct]
    WHERE [ClientID] = @ClientID AND [ProductID] = @ProductID
END
GO
--21. (-)PurchasedProduct
CREATE PROCEDURE [dbo].[UpdatePurchasedProduct]
    @ClientID INT,
    @ProductID INT,
    @NewProductID INT
AS
BEGIN
    UPDATE [dbo].[PurchasedProduct]
    SET [ProductID] = @NewProductID
    WHERE [ClientID] = @ClientID AND [ProductID] = @ProductID
END
