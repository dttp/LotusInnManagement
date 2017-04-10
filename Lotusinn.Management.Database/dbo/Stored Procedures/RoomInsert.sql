CREATE PROCEDURE [dbo].[RoomInsert]
	@id nvarchar(15),
	@houseId nvarchar(15),
	@roomNumber nvarchar(5),
	@roomTypeId nvarchar(15)		
AS
BEGIN
	INSERT Room(Id, HouseId, RoomNumber, RoomTypeId) 
	VALUES (@id, @houseId, @roomNumber, @roomTypeId)
END