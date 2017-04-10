CREATE PROCEDURE [dbo].[WarehouseItemUpdate]
	@id nvarchar(15),
	@name nvarchar(100),
	@description nvarchar(500),
	@count int
AS
	UPDATE WarehouseItem
	SET Name = @name,
		Description = @description,
		Count = @count
	WHERE Id = @id
