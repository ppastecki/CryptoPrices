CREATE TYPE dbo.CryptoCurrency
AS TABLE
(
	Id int NOT NULL,
	Name nvarchar(max) NOT NULL,
	Symbol nvarchar(max) NOT NULL,
	MaxSupply decimal(18, 5) NULL,
	CirculatingSupply decimal(18, 5) NULL,
	TotalSupply decimal(18, 5) NULL,
	Rank int NULL,
	LastUpdated datetime2(7) NOT NULL
)