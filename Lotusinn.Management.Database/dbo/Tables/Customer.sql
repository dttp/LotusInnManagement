CREATE TABLE [dbo].[Customer](
		[Id] [nvarchar](15) NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Phone] [nvarchar](20) NULL,		
		[Email] [nvarchar](50) NULL,
		[Address] [nvarchar](200) NULL,
		[Country] [nvarchar](20) NULL,
		[PassportOrId] [nvarchar](50) NOT NULL
	 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]