CREATE PROCEDURE [dbo].[EquipmentInsert]
	@id nvarchar(15),
	@houseId nvarchar(15),
	@roomId nvarchar(15),
	@name nvarchar(100),
	@category nvarchar(100),
	@quantity int,
	@status nvarchar(50),
	@price nvarchar(20),
	@unit nvarchar(3),
	@createdDate DateTime
AS
BEGIN
	
	INSERT INTO Equipment(Id, HouseId, RoomId, Name, Category, Quantity, Status, Price, Unit, CreatedDate)
	VALUES (@id, @houseId, @roomId, @name, @category, @quantity, @status, @price, @unit, @createdDate)

END