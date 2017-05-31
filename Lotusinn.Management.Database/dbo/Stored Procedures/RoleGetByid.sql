CREATE PROCEDURE [dbo].[RoleGetById]
	@id nvarchar(15)
AS
	SELECT * FROM Role
	WHERE Id = @id
