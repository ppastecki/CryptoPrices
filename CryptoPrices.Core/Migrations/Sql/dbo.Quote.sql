CREATE TYPE dbo.Quote
AS TABLE
(
	CryptoCurrencyId int NOT NULL,
	Price decimal(18, 5) NULL,
	Volume24h decimal(18, 5) NULL,
	PercentChange1h decimal(18, 5) NULL,
	PercentChange24h decimal(18, 5) NULL,
	PercentChange7d decimal(18, 5) NULL,
	MarketCap decimal(18, 5) NULL,
	LastUpdated datetime2(7) NOT NULL
)
