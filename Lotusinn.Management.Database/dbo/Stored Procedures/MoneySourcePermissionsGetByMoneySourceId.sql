CREATE PROCEDURE [dbo].[MoneySourcePermissionsGetByMoneySourceId]
	@moneySourceId nvarchar(15)
AS
	SELECT * FROM MoneySourcePermission WHERE MoneySourceId = @moneySourceId
