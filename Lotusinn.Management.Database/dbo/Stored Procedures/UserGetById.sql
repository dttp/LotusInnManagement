CREATE PROCEDURE [dbo].[UserGetById]
	@id nvarchar(15)
AS
BEGIN
	SELECT u.*
	FROM dbo.[User] u 
	WHERE u.Id = @id
END