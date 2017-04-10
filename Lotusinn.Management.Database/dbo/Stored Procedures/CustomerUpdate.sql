CREATE PROCEDURE [dbo].[CustomerUpdate]
	@id nvarchar(15),
	@name nvarchar(50),
	@phone nvarchar(20),
	@email nvarchar(50),
	@address nvarchar(200),
	@country nvarchar(20),
	@passportOrId nvarchar(50)	
AS
BEGIN
	UPDATE Customer
	SET Name=@name, Phone=@phone,Address=@address, Email=@email,Country=@country, PassportOrId=@passportOrId
	WHERE Id=@id
END