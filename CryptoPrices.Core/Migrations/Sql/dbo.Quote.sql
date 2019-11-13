CREATE TYPE dbo.Quote
AS TABLE
(
	CryptoCurrencyId int NOT NULL,
	Price decimal(18, 0) NULL,
	Volume24h decimal(18, 0) NULL,
	PercentChange1h decimal(18, 0) NULL,
	PercentChange24h decimal(18, 0) NULL,
	PercentChange7d decimal(18, 0) NULL,
	MarketCap decimal(18, 0) NULL,
	LastUpdated datetime2(7) NOT NULL
)
