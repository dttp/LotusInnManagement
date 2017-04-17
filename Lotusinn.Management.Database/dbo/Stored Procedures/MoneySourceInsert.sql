CREATE PROCEDURE [dbo].[MoneySourceInsert]
	@id nvarchar(15),
	@name nvarchar(50),
	@houseId nvarchar(15),
	@balanceVnd float,
	@balanceUsd float,
	@ownerId nvarchar(15)	
AS
	INSERT INTO MoneySource(Id, Name, HouseId, BalanceVND, BalanceUSD, OwnerId)
	VALUES (@id, @name, @houseId, @balanceVnd, @balanceUsd, @ownerId)
