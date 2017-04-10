CREATE TABLE [dbo].[WarehouseItem]
(
	[Id] NVARCHAR(15) NOT NULL PRIMARY KEY, 
    [WarehouseId] NVARCHAR(15) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Count] INT NOT NULL, 
    [Condition] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(500) NULL 
)
