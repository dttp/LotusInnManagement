CREATE TABLE [dbo].[RoleObjectPermission]
(
	[Id] NVARCHAR(15) NOT NULL PRIMARY KEY, 
    [RoleId] NVARCHAR(15) NOT NULL, 
    [Object] NVARCHAR(50) NOT NULL, 
    [Permission] INT NOT NULL
)
