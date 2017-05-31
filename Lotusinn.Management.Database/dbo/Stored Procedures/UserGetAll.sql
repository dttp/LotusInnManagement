CREATE PROCEDURE [dbo].[UserGetAll]	
AS
BEGIN
	SELECT u.*
	FROM dbo.[User] u		
END