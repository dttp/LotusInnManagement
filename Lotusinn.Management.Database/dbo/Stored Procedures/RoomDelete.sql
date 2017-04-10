CREATE PROCEDURE [dbo].[RoomDelete]
	@id nvarchar(15)	
AS
BEGIN
	DELETE Room
	WHERE Id = @id
END