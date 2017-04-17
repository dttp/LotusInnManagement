CREATE TABLE [dbo].[ObjectPermission]
(
    [PermissionSetsId] NVARCHAR(15) NOT NULL, 
    [ObjectName] NVARCHAR(50) NOT NULL, 
    [Permissions] NVARCHAR(MAX) NOT NULL
)
