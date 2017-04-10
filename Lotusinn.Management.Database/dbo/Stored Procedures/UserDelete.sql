CREATE PROCEDURE [dbo].[UserDelete]	
	@id nvarchar(15)	
AS
BEGIN
	DELETE [dbo].[User]
	WHERE Id = @id
END