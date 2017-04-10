﻿CREATE TABLE [dbo].[Room](
		[Id] nvarchar(15) NOT NULL,
		[RoomNumber] nvarchar(5) NOT NULL,
		[HouseId] nvarchar(15) NOT NULL,
		[RoomTypeId] nvarchar(15) NOT NULL		
	 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC		
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]