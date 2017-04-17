﻿CREATE TABLE [dbo].[Role](
		[Id] [nvarchar](15) NOT NULL,
		[Name] [nvarchar](20) NOT NULL,
	 [PermissionSetsId] NVARCHAR(15) NOT NULL, 
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]