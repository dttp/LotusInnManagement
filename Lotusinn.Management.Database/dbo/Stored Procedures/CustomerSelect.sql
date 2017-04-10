CREATE PROCEDURE [dbo].[CustomerSelect]	
	@name nvarchar(50),
	@email nvarchar(50),
	@passportOrId nvarchar(50),
	@pageSize int = 50,
	@pageIndex int = 1,
	@houseId nvarchar(15)
AS
BEGIN
	DECLARE @query nvarchar(2000), @condition nvarchar(2000), @start int, @end int
	SET @query = 'SELECT ROW_NUMBER() OVER (ORDER BY Name) AS RowNum, c.* FROM CUSTOMER c'
	SET @condition = ''

	SET @start = (@pageIndex - 1) * @pageSize + 1
	SET @end = @pageIndex * @pageSize

	IF @name IS NOT NULL AND @name != ''
		SET @condition = 'Name Like ''%' + @name + '%'''	
	IF @condition != '' 
		SET @condition = @condition + ' AND '
	IF @email IS NOT NULL AND @email != ''
		SET @condition = @condition + 'Email Like ''%' + @email + '%'''
	IF @condition != '' 
		SET @condition = @condition + ' AND '
	IF @passportOrId IS NOT NULL AND @passportOrId != ''
		SET @condition = @condition + 'PassportOrId LIKE ''%' + @passportOrId + '%'''
	
	IF @condition != '' SET @condition = @condition + ' AND '
	SET @condition = @condition + ' Id IN (SELECT CustomerId FROM OrderCustomer oc INNER JOIN BookingOrder o ON oc.OrderId = o.Id WHERE o.HouseId = ''' + @houseId	+ ''')'
	 
	SET @query = @query + ' WHERE ' + @condition

	SET @query = 'SELECT * FROM (' + @query + ') as customerwithrow WHERE RowNum >= ' + convert(nvarchar(10),@start) + ' AND RowNum <= ' + convert(nvarchar(10),@end)

	EXEC (@query)

	SELECT COUNT(CustomerId) as Total FROM OrderCustomer oc INNER JOIN BookingOrder o ON oc.OrderId = o.Id WHERE o.HouseId = @houseId
END