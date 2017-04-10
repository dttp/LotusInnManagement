CREATE PROCEDURE [dbo].[WarehouseDelete]
	@id nvarchar(15)
AS
	DELETE Warehouse
	WHERE Id = @id
