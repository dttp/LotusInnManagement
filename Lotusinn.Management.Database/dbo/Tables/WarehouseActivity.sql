CREATE TABLE [dbo].[WarehouseActivity]
(
	[Id] NVARCHAR(15) NOT NULL PRIMARY KEY, 
    [WarehouseId] NVARCHAR(15) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Date] DATETIME NOT NULL, 
    [UserId] NVARCHAR(15) NOT NULL, 
    [Description] NVARCHAR(500) NULL
)
