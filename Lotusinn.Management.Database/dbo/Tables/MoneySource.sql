CREATE TABLE [dbo].[MoneySource]
([Id] NVARCHAR(15) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [HouseId] NVARCHAR(15) NULL, 
    [BalanceVND] FLOAT NOT NULL, 
    [BalanceUSD] FLOAT NOT NULL, 
    [OwnerId] NVARCHAR(15) NOT NULL, 
    CONSTRAINT [PK_MoneySource] PRIMARY KEY ([Id]) 
	
)
