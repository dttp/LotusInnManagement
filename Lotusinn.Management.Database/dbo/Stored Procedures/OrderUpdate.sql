
CREATE PROCEDURE [dbo].[OrderUpdate]
	@id nvarchar(15),
	@note nvarchar(500),
	@checkinDate Datetime,
	@checkoutDate DateTime,
	@totalGuest int,
	@stayLength nvarchar(100),
	@status nvarchar(50),
	@houseId nvarchar(15),
	@paymentCycle nvarchar(50)
AS
BEGIN
	UPDATE BookingOrder
	SET Note = @note,
		CheckinDate = @checkinDate,
		CheckoutDate = @checkoutDate,
		TotalGuest = @totalGuest,
		StayLength = @stayLength,
		Status = @status,
		HouseId = @houseId,
		PaymentCycle = @paymentCycle
	WHERE Id = @id
END