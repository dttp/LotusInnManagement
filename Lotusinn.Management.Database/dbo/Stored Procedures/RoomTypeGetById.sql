
CREATE PROCEDURE [dbo].[RoomTypeGetById]	
	@id nvarchar(15)
AS
BEGIN
	SELECT *
	FROM dbo.[RoomType]	
	WHERE Id = @id
END