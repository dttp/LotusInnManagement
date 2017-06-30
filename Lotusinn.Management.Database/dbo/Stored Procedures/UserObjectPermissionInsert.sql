CREATE PROCEDURE [dbo].[UserObjectPermissionInsert]
	@id nvarchar(15),
	@userId nvarchar(15),
	@objectType nvarchar(50),
	@objectId nvarchar(15) = NULL,
	@permission int
AS
	INSERT INTO UserObjectPermission (Id, UserId, ObjectType, Permission, ObjectId)
	VALUES(@id, @userId, @objectType, @permission, @objectId)
