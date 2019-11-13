CREATE TYPE dbo.CryptoCurrency
AS TABLE
(
	Id int NOT NULL,
	Name nvarchar(max) NOT NULL,
	Symbol nvarchar(max) NOT NULL,
	MaxSupply decimal(18, 0) NULL,
	CirculatingSupply decimal(18, 0) NULL,
	Rank int NULL,
	LastUpdated datetime2(7) NOT NULL
)
