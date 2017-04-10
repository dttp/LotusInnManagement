CREATE PROCEDURE [dbo].[PaymentRecordSelect]	
	@pageIndex int,
	@pageSize int,
	@moneySourceId nvarchar(15),
	@type nvarchar(20),
	@startDate datetime,
	@endDate datetime
AS
	DECLARE @skip int = (@pageIndex - 1) * @pageSize

	IF @type != 'All' 
	BEGIN
		SELECT * FROM PaymentRecord
		WHERE MoneySourceId = @moneySourceId 
			  AND Type = @type 
			  AND DATEDIFF(d, @startDate, Date) >= 0
			  AND DATEDIFF(d, Date, @endDate) >= 0
		ORDER BY Date DESC
		OFFSET @skip ROWS
		FETCH NEXT @pageSize ROWS ONLY

		SELECT COUNT(1) as Total FROM PaymentRecord
		WHERE MoneySourceId = @moneySourceId 
			  AND Type = @type 
			  AND DATEDIFF(d, @startDate, Date) >= 0
			  AND DATEDIFF(d, Date, @endDate) >= 0
	END
	ELSE 
	BEGIN
		SELECT * FROM PaymentRecord
		WHERE MoneySourceId = @moneySourceId 
			  AND DATEDIFF(d, @startDate, Date) >= 0
			  AND DATEDIFF(d, Date, @endDate) >= 0
		ORDER BY Date DESC
		OFFSET @skip ROWS
		FETCH NEXT @pageSize ROWS ONLY

		SELECT COUNT(1) as Total FROM PaymentRecord
		WHERE MoneySourceId = @moneySourceId 
			  AND DATEDIFF(d, @startDate, Date) >= 0
			  AND DATEDIFF(d, Date, @endDate) >= 0
	END
