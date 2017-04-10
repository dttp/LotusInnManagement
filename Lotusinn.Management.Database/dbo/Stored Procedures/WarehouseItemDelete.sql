CREATE PROCEDURE [dbo].[WarehouseItemDelete]
	@id nvarchar(15)
AS
	DELETE WarehouseItem
	WHERE Id = @id
