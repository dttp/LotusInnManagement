CREATE PROCEDURE [dbo].[UserChangePassword]
	@id nvarchar(15),
	@password nvarchar(50)	
AS
BEGIN
	UPDATE [dbo].[User]
	SET Password = @password
	WHERE Id = @id
END