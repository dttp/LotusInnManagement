CREATE PROCEDURE [dbo].[UserObjectPermissionUpdate]
	@id nvarchar(15),
	@userId nvarchar(15),
	@objectType nvarchar(50),
	@objectId nvarchar(15) = NULL,
	@permission int
AS
	UPDATE UserObjectPermission
	SET UserId = @userId,
		ObjectType = @objectType,
		Permission = @permission,
		ObjectId = @objectId
	WHERE Id = @id
