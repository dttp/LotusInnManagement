CREATE PROCEDURE [dbo].[MoneySourceUpdate]
	@id nvarchar(15),
	@name nvarchar(50),
	@houseId nvarchar(15),
	@balanceVnd float,
	@balanceUsd float
AS
	UPDATE MoneySource
	SET Name = @name,
		HouseId = @houseId,
		BalanceVND = @balanceVnd,
		BalanceUSD = @balanceUsd
	WHERE Id = @id
