CREATE PROCEDURE [dbo].[WarehouseActivitySelect]
	@warehouseId nvarchar(15),
	@pageIndex int,
	@pageSize int
AS

	DECLARE @start int, @end int

	SET @start = (@pageIndex - 1)*@pageSize+1
	SET @end = @pageIndex * @pageSize

	SELECT * 
	FROM (SELECT ROW_NUMBER() OVER (ORDER BY Date Desc) as RowNum, wa.*
		  FROM WarehouseActivity wa
		  WHERE wa.WarehouseId = @warehouseId)
	AS WarehouseActivityRowNum
	WHERE RowNum >= Convert(nvarchar(10), @start) AND RowNum <= CONVERT(nvarchar(10), @end)

	SELECT COUNT(Id) as Total FROM WarehouseActivity
	WHERE WarehouseId = @warehouseId
