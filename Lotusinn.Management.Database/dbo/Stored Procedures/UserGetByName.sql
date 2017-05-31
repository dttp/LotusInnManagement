CREATE PROCEDURE [dbo].[UserGetByName]
	@username nvarchar(50)
AS
BEGIN
	SELECT u.*
	FROM dbo.[User] u 
	WHERE UserName = @username
END