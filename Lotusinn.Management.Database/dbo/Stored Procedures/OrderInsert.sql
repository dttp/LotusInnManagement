
CREATE PROCEDURE [dbo].[OrderInsert]
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
	INSERT INTO BookingOrder(Id, Note, CheckinDate, CheckoutDate, TotalGuest, StayLength, Status, HouseId, PaymentCycle) 
	VALUES (@id, @note, @checkinDate, @checkoutDate, @totalGuest, @stayLength, @status, @houseId, @paymentCycle)
END