CREATE PROCEDURE [dbo].[WarehouseInsert]
	@id nvarchar(15),
	@name nvarchar(50),
	@manager nvarchar(15)
AS 
	INSERT INTO Warehouse(Id, Name, Manager)
	VALUES(@id, @name, @manager)

