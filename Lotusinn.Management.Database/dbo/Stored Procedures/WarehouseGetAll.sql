CREATE PROCEDURE [dbo].[WarehouseGetAll]	
AS
	SELECT * FROM Warehouse (NOLOCK)
