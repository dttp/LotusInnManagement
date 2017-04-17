CREATE TABLE [dbo].[MoneySourcePermission]
(
	[MoneySourceId] NVARCHAR(15) NOT NULL PRIMARY KEY, 
    [UserId] NVARCHAR(15) NOT NULL, 
    [Permissions] NVARCHAR(MAX) NULL
)
