CREATE PROCEDURE [dbo].[RoleObjectPermissionInsert]
	@id nvarchar(15),
	@roleId nvarchar(15),
	@objectType nvarchar(50),
	@objectId nvarchar(15) = NULL,
	@permission int
AS
	INSERT INTO RoleObjectPermission(Id, RoleId, ObjectType, Permission, ObjectId)
	VALUES (@id, @roleId, @objectType, @permission, @objectid)
