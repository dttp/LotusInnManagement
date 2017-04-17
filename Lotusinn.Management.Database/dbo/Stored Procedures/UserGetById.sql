CREATE PROCEDURE [dbo].[UserGetById]
	@id nvarchar(15)
AS
BEGIN
	SELECT u.*, r.Name as RoleName
	FROM dbo.[User] u 
		INNER JOIN [dbo].[Role] r ON u.RoleId = r.Id
	WHERE u.Id = @id
END