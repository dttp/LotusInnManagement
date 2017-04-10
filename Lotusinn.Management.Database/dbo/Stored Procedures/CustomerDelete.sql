CREATE PROCEDURE [dbo].[CustomerDelete]
	@id nvarchar(15)	
AS
BEGIN
	DELETE Customer
	WHERE Id = @id
END