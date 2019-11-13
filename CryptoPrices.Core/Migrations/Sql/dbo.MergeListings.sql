CREATE PROCEDURE dbo.MergeListings
	@currencies dbo.CryptoCurrency READONLY,
	@quotes dbo.Quote READONLY
AS
BEGIN
	MERGE dbo.CryptoCurrencies t
	USING @currencies s
		ON t.Id = s.Id
	WHEN MATCHED THEN
		UPDATE SET
			t.Name = s.Name,
			t.Symbol = s.Symbol,
			t.MaxSupply = s.MaxSupply,
			t.CirculatingSupply = s.CirculatingSupply,
			t.Rank = s.Rank,
			t.LastUpdated = s.LastUpdated
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (Id, Name, Symbol, MaxSupply, CirculatingSupply, Rank, LastUpdated)
		VALUES (s.Id, s.Name, s.Symbol, s.MaxSupply, s.CirculatingSupply, s.Rank, s.LastUpdated)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	MERGE dbo.Quotes t
	USING @quotes s
		ON t.CryptoCurrencyId = s.CryptoCurrencyId
	WHEN MATCHED THEN
		UPDATE SET
			t.Price = s.Price,
			t.Volume24h = s.Volume24h,
			t.PercentChange1h = s.PercentChange1h,
			t.PercentChange24h = s.PercentChange24h,
			t.PercentChange7d = s.PercentChange7d,
			t.MarketCap = s.MarketCap,
			t.LastUpdated = s.LastUpdated
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (CryptoCurrencyId, Price, Volume24h, PercentChange1h, PercentChange24h, PercentChange7d, MarketCap, LastUpdated)
		VALUES (s.CryptoCurrencyId, s.Price, s.Volume24h, s.PercentChange1h, s.PercentChange24h, s.PercentChange7d, s.MarketCap, s.LastUpdated)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;
END
