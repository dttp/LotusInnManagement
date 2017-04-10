CREATE PROCEDURE [dbo].[WarehouseActivityInsert]
	@id nvarchar(15),
	@warehouseId nvarchar(15),
	@name nvarchar(50),
	@description nvarchar(500),
	@userId nvarchar(15)
AS
	INSERT INTO WarehouseActivity(Id, Name, Description, WarehouseId, Date, UserId)
	VALUES (@id, @name, @description, @warehouseId, GETDATE(), @userId)
