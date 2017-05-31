CREATE PROCEDURE [dbo].[RoleInsert]
	@id nvarchar(15),
	@name nvarchar(50),
	@type nvarchar(50)
AS
	INSERT INTO Role (Id, Name, Type)
	VALUES (@id, @name, @type)
