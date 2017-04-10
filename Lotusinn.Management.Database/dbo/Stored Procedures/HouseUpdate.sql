CREATE PROCEDURE [dbo].[HouseUpdate]	
	@id nvarchar(15),
	@name nvarchar(50),
	@address nvarchar(200)
AS
BEGIN
	UPDATE House
	SET Name = @name, Address=@address
	WHERE Id = @id
END