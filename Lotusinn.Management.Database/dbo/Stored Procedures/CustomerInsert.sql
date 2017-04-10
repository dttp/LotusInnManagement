CREATE PROCEDURE [dbo].[CustomerInsert]
	@id nvarchar(15),
	@name nvarchar(50),
	@phone nvarchar(20),
	@email nvarchar(50),
	@address nvarchar(200),
	@country nvarchar(20),
	@passportOrId nvarchar(50)	
AS
BEGIN
	INSERT Customer(Id,Name,Phone,Email,Address,Country,PassportOrId) VALUES(@id, @name,@phone,@email,@address,@country,@passportOrId)
END