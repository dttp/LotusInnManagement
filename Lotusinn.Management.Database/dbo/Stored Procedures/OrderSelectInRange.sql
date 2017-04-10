CREATE PROCEDURE [dbo].[OrderSelectInRange]
	@houseId nvarchar(15),
	@startDate DateTime,
	@endDate DateTime
AS
BEGIN
	DECLARE @tmpTable TABLE ( 
		Id nvarchar(15)
	)
	INSERT INTO @tmpTable 
		SELECT o.Id
		FROM BookingOrder o 
		WHERE o.HouseId = @houseId 
			AND (o.Status = 'Active' OR o.Status = 'Reserved')
			AND ((o.CheckinDate BETWEEN @startDate AND @endDate) OR 
				(o.CheckoutDate BETWEEN @startDate AND @endDate) OR
				((@startDate BETWEEN o.CheckinDate AND o.CheckoutDate) AND (@endDate BETWEEN o.CheckinDate AND o.CheckoutDate)))
			--AND ((DATEDIFF(day, @startDate, o.CheckinDate) > 0 AND DATEDIFF(day, o.CheckinDate, @endDate) > 0) OR 
			--	 (DATEDIFF(day, @startDate, o.CheckoutDate) > 0 AND DATEDIFF(day, o.CheckoutDate, @endDate) > 0))

	SELECT o.*
	FROM BookingOrder o 
	WHERE o.Id in (SELECT Id FROM @tmpTable)

	SELECT * FROM OrderRoom 
	WHERE OrderId IN (SELECT Id FROM @tmpTable)
END