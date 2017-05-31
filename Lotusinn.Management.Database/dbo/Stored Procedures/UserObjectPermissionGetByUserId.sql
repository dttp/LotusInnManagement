CREATE PROCEDURE [dbo].[UserObjectPermissionGetByUserId]
	@userId nvarchar(15)
AS
	SELECT * FROM UserObjectPermission
	WHERE UserId = @userId
