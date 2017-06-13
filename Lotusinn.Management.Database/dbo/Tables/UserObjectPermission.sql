CREATE TABLE [dbo].[UserObjectPermission]
(
	[Id] NVARCHAR(15) NOT NULL PRIMARY KEY, 
    [UserId] NVARCHAR(15) NOT NULL, 
    [Object] NVARCHAR(50) NOT NULL, 
    [Permission] INT NOT NULL DEFAULT 0
)
