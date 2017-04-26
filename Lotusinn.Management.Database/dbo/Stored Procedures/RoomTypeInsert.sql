
CREATE PROCEDURE [dbo].[RoomTypeInsert]	
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
	INSERT RoomType(Id, Name, HouseId, Price, Unit, PricePerWeek, UnitPerWeek, PricePerNight, UnitPerNight, Square) 
	VALUES(@id, @name, @houseId, @price, @unit, @pricePerWeek, @unitPerWeek, @pricePerNight, @unitPerNight, @square)
END