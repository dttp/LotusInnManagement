CREATE PROCEDURE [dbo].WarehouseItemSelect
	@warehouseId nvarchar(15),
	@pageIndex int,
	@pageSize int
AS
	DECLARE @query nvarchar(2000), @condition nvarchar(2000), @start int, @end int
	SET @start = (@pageIndex - 1) * @pageSize + 1
	SET @end = @pageIndex * @pageSize

	SELECT * 
	FROM (SELECT ROW_NUMBER() OVER (ORDER BY Name) AS RowNum, w.* 
		  FROM WarehouseItem w 
		  WHERE w.WarehouseId = @warehouseId) AS warehouseitemwithRowNumber
	WHERE RowNum >= convert(nvarchar(10), @start) AND RowNum <= convert(nvarchar(10), @end)

	SELECT COUNT(Id) as Total FROM WarehouseItem WHERE WarehouseId = @warehouseId
