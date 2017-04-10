CREATE TABLE [dbo].[PaymentRecord]
(
	[Id] NVARCHAR(15) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Date] DATETIME NOT NULL, 
    [Amount] NVARCHAR(20) NOT NULL, 
    [Unit] NVARCHAR(3) NOT NULL, 
    [Method] NVARCHAR(50) NOT NULL, 
    [Note] NVARCHAR(500) NULL, 
    [Type] NVARCHAR(20) NOT NULL, 
    [MoneySourceId] NVARCHAR(15) NOT NULL, 
    [OrderId] NVARCHAR(15) NULL, 
    CONSTRAINT [PK_PaymentRecord] PRIMARY KEY ([Id]) 
)
