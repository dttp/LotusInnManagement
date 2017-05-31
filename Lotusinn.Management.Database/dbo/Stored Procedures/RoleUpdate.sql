CREATE PROCEDURE [dbo].[RoleUpdate]
	@id nvarchar(15),
	@name nvarchar(15)
AS
	UPDATE Role SET Name = @name 
	WHERE Id = @id
