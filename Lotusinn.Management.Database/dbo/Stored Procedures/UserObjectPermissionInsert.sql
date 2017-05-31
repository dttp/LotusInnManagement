CREATE PROCEDURE [dbo].[UserObjectPermissionInsert]
	@id nvarchar(15),
	@userId nvarchar(15),
	@object nvarchar(50),
	@permission int
AS
	INSERT INTO UserObjectPermission (Id, UserId, Object, Permission)
	VALUES(@id, @userId, @object, @permission)
