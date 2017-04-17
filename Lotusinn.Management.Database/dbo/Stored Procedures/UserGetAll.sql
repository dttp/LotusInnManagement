CREATE PROCEDURE [dbo].[UserGetAll]	
AS
BEGIN
	SELECT u.*, r.Name as RoleName
	FROM dbo.[User] u 
		INNER JOIN [dbo].[Role] r ON u.RoleId = r.Id
END