CREATE PROCEDURE [dbo].[UserObjectPermissionUpdate]
	@id nvarchar(15),
	@userId nvarchar(15),
	@object nvarchar(50),
	@permission int
AS
	UPDATE UserObjectPermission
	SET UserId = @userId,
		Object = @object,
		Permission = @permission
	WHERE Id = @id
