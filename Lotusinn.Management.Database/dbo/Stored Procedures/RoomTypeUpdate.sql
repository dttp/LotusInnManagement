
CREATE PROCEDURE [dbo].[RoomTypeUpdate]
	@id nvarchar(15),
	@name nvarchar(50),
	@houseId nvarchar(15),
	@price nvarchar(50),
	@unit nvarchar(3),
	@pricePerWeek nvarchar(50),
	@unitPerWeek nvarchar(3),
	@pricePerNight nvarchar(50),
	@unitPerNight nvarchar(3),
	@square nvarchar(5)
AS
BEGIN
	UPDATE RoomType
	SET Name = @name, HouseId = @houseId, Price = @price, Unit = @unit, PricePerWeek = @pricePerWeek, UnitPerWeek = @unitPerWeek, PricePerNight = @pricePerNight, UnitPerNight = @unitPerNight, Square = @square
	WHERE Id = @id
END