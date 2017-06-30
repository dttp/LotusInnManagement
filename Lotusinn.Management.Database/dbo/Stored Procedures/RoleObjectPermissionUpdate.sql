CREATE PROCEDURE [dbo].[RoleObjectPermissionUpdate]
	@id nvarchar(15),
	@roleId nvarchar(15),
	@objectType nvarchar(50),
	@objectId nvarchar(15) = NULL,
	@permission int
AS
	UPDATE RoleObjectPermission
	SET RoleId = @roleId, 
		ObjectType = @objectType,
		ObjectId = @objectId,
		Permission = @permission
	WHERE Id = @id