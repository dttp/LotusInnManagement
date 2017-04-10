CREATE PROCEDURE [dbo].[WarehouseUpdate]
	@id nvarchar(15),
	@name nvarchar(50)
AS
	Update Warehouse
	SET Name = @name 
	WHERE Id = @id
