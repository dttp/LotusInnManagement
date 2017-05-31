CREATE TABLE [dbo].[User](
		[Id] [nvarchar](15) NOT NULL,
		[Username] [nvarchar](50) NOT NULL,
		[Phone] [nvarchar](20) NULL,
		[Password] [nvarchar](50) NOT NULL,
		[Email] [nvarchar](50) NOT NULL,
		[HouseId] [nvarchar](15) NULL,
		[Status] [nvarchar](20) NULL
	 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    [RoleId] NVARCHAR(15) NOT NULL
	) ON [PRIMARY]