CREATE PROCEDURE [dbo].[UserUpdateStatus]	
	@id nvarchar(15),
	@status nvarchar(20)
AS
BEGIN
	UPDATE [dbo].[User]
	SET Status = @status
	WHERE Id=@id
END