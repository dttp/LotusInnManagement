CREATE PROCEDURE [dbo].[CustomerSelectReserved]
	@name nvarchar(50),
	@email nvarchar(50),
	@passportOrId nvarchar(50),
	@room nvarchar(10),
	@pageSize int = 50,
	@pageIndex int = 1,
	@houseId nvarchar(15)	
AS
BEGIN

	DECLARE @reservedCustomerTable TABLE (
		Id nvarchar(15),
		Name nvarchar(50),
		Phone nvarchar(20),
		Email nvarchar(50),
		Address nvarchar(200),
		Country nvarchar(20),
		PassportOrId nvarchar(50),
		OrderId nvarchar(15),
		CheckinDate DateTime,
		CheckoutDate DateTime,
		Rooms nvarchar(50)
	)
	DECLARE @orderRoomNumber TABLE 
	(
		OrderId nvarchar(15),
		RoomNumber nvarchar(10)
	)

	DECLARE @odr TABLE 
	(
		OrderId nvarchar(15),
		Rooms nvarchar(50)
	)
	INSERT INTO @orderRoomNumber
		SELECT OrderId, r.RoomNumber
		FROM OrderRoom odr INNER JOIN Room r ON odr.RoomId = r.Id

	INSERT INTO @odr
		SELECT OrderId,
			STUFF(
			(
				SELECT ','+ [RoomNumber] FROM @orderRoomNumber WHERE OrderId = odr.OrderId FOR XML PATH('')
			),1,1,'') as Rooms
		FROM (SELECT DISTINCT OrderId FROM @orderRoomNumber) odr

	INSERT INTO @reservedCustomerTable
		SELECT c.*, o.Id as OrderId, o.CheckinDate, o.CheckoutDate, odr.Rooms
		FROM Customer c INNER JOIN OrderCustomer oc ON c.Id = oc.CustomerId 
						INNER JOIN BookingOrder o ON oc.OrderId = o.Id 					
						INNER JOIN @odr odr ON o.Id = odr.OrderId 	
		WHERE o.HouseId = @houseId AND o.Status = 'Reserved'
		ORDER BY c.Name 

	DECLARE @start int, @end int	

	SET @start = (@pageIndex - 1) * @pageSize + 1
	SET @end = @pageIndex * @pageSize

	IF @name IS NOT NULL AND @name != ''
		DELETE FROM @reservedCustomerTable 
		WHERE Name Not like '%' + @name + '%'

	IF @email IS NOT NULL AND @email != ''
		DELETE FROM @reservedCustomerTable 
		WHERE Email NOT LIKE '%' + @email + '%'
	
	IF @passportOrId IS NOT NULL AND @passportOrId != ''
		DELETE FROM @reservedCustomerTable
		WHERE PassportOrId NOT LIKE '%' + @passportOrId + '%'

	IF (@room IS NOT NULL AND @room != '')
		DELETE FROM @reservedCustomerTable
		WHERE Rooms NOT LIKE '%' + @room + '%'
	
	SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Name) AS RowNum, * FROM @reservedCustomerTable) as customerwithrow WHERE RowNum >= CONVERT(nvarchar(10), @start) AND RowNum <= CONVERT(nvarchar(10), @end)

	SELECT COUNT(Id) as Total FROM @reservedCustomerTable

	SELECT odr.OrderId, odr.RoomId
	FROM OrderRoom odr INNER JOIN BookingOrder o ON odr.OrderId = o.Id
	WHERE o.Status = 'Reserved' AND o.HouseId = @houseId


END