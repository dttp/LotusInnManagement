CREATE PROCEDURE [dbo].[RoomTypeDelete]
	@id nvarchar(15)	
AS
BEGIN
	DELETE RoomType
	WHERE Id = @id
END