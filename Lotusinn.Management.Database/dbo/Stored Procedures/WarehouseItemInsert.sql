CREATE PROCEDURE [dbo].[WarehouseItemInsert]
	@id nvarchar(15),
	@name nvarchar(100),
	@description nvarchar(500),
	@count int,
	@warehouseId nvarchar(15),
	@condition nvarchar(50)
AS
	INSERT INTO WarehouseItem(Id, Name, Description, Count, WarehouseId, Condition)
	VALUES (@id, @name, @description, @count, @warehouseId, @condition)
