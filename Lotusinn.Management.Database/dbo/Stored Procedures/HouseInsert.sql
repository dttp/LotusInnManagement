CREATE PROCEDURE [dbo].[HouseInsert]	
	@id nvarchar(15),
	@name nvarchar(50),
	@address nvarchar(200)
AS
BEGIN
	INSERT House(Id, Name, Address) VALUES(@id, @name, @address)
END