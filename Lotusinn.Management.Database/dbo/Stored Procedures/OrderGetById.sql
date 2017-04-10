CREATE PROCEDURE [dbo].[OrderGetById]
	@id nvarchar(15)
AS
BEGIN
	SELECT o.*, 
		   d.Id as DepositId, d.Amount as DepositAmount, d.Date as DepositDate, d.Unit as DepositUnit		   
		
	FROM BookingOrder o INNER JOIN Deposit d ON o.Id = d.OrderId
	WHERE o.Id = @id
	ORDER BY o.CheckinDate DESC

	SELECT * FROM OrderCustomer WHERE OrderId = @id;

	SELECT * FROM OrderRoom WHERE OrderId = @id;

END