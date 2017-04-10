CREATE PROCEDURE [dbo].[WarehouseItemGetById]
	@id nvarchar(15)
AS
	SELECT * FROM WarehouseItem
	WHERE Id = @id
