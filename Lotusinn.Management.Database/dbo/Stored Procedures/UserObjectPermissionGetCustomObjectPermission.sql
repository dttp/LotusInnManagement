CREATE PROCEDURE [dbo].[UserObjectPermissionGetCustomObjectPermission]
	@userId nvarchar(15),
	@objectType nvarchar(50)
AS
	SELECT * FROM UserObjectPermission
	WHERE UserId = @userId AND ObjectType = @objectType AND ObjectId IS NOT NULL
