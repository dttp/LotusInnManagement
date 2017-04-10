CREATE TABLE [dbo].[RolePermission](
		[RoleId] [nvarchar](15) NOT NULL,
		[Object] [nvarchar](10) NOT NULL,
		[Permission] [nvarchar](20) NOT NULL
	) ON [PRIMARY]