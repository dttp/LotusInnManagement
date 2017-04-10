
CREATE PROCEDURE [dbo].[RoomTypeGetAll]	
	@houseId nvarchar(15)
AS
BEGIN
	SELECT *
	FROM dbo.[RoomType]	
	WHERE HouseId = @houseId
	ORDER BY Price ASC	
END