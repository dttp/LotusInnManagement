CREATE PROCEDURE [dbo].[RoomGetById]	
	@id nvarchar(15)
AS
BEGIN
	SELECT r.*
	FROM Room r 
	WHERE r.Id = @id
END