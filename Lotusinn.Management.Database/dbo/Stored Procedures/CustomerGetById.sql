CREATE PROCEDURE [dbo].[CustomerGetById]
	@id nvarchar(15)
AS
BEGIN
	SELECT * FROM Customer 
	WHERE Id = @id
END