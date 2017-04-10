CREATE PROCEDURE [dbo].[RoomUpdate]
	@id nvarchar(15),
	@roomNumber nvarchar(5),
	@roomTypeId nvarchar(15)		
AS
BEGIN
	UPDATE Room
	SET RoomNumber = @roomNumber,
		RoomTypeId = @roomTypeId
	WHERE Id = @id
END