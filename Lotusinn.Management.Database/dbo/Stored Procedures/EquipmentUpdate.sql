CREATE PROCEDURE [dbo].[EquipmentUpdate]
	@id nvarchar(15),
	@houseId nvarchar(15),
	@roomId nvarchar(15),
	@name nvarchar(100),
	@category nvarchar(100),
	@quantity int,
	@status nvarchar(50),
	@price nvarchar(20),
	@unit nvarchar(3)
AS
BEGIN
	
	UPDATE Equipment
	SET HouseId = @houseId,
		RoomId = @roomId,
		Name = @name,
		Category = @category,
		Quantity = @quantity,
		Status = @status,
		Price = @price,
		Unit = @unit
	WHERE Id = @id

END