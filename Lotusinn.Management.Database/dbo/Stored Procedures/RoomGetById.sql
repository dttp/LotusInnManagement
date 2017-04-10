CREATE PROCEDURE [dbo].[RoomGetById]	
	@id nvarchar(15)
AS
BEGIN
	SELECT r.*, rt.Name as RoomTypeName, rt.Price as RoomTypePrice, rt.Unit as RoomTypeUnit
	FROM Room r INNER JOIN RoomType rt ON r.RoomTypeId = rt.Id
	WHERE r.Id = @id
END