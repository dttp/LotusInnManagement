CREATE PROCEDURE [dbo].[UserGetByName]
	@username nvarchar(50)
AS
BEGIN
	SELECT u.*, r.Name as RoleName
	FROM dbo.[User] u 
		INNER JOIN [dbo].[Role] r ON u.RoleId = r.Id
	WHERE UserName = @username
END