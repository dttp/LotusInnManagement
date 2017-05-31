CREATE PROCEDURE [dbo].[RoleDelete]
	@id nvarchar(15)
AS
	DELETE FROM Role WHERE Id = @id
