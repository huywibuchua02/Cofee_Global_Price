USE [IPSTACK]
GO
/****** Object:  Table [dbo].[Ca_Phe_prices]    Script Date: 5/28/2024 7:08:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ca_Phe_prices](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [date] NULL,
	[value] [int] NULL,
 CONSTRAINT [PK_Ca_Phe_prices] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetCoffeePrices]    Script Date: 5/28/2024 7:08:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[GetCoffeePrices]
    @action VARCHAR(50)
AS
BEGIN
    -- Kiểm tra nếu action là 'get_all'
    IF @action = 'get_all_coffee'
     BEGIN
	 declare @json nvarchar(max) = '';
		-- Thêm các giá trị vào biến JSON
	SELECT @json += FORMATMESSAGE(N'{"date":"%s","value":%d},', CONVERT(NVARCHAR, [date], 23), CAST([value] AS int))
	FROM Ca_Phe_prices;


		IF ((@json IS NULL) OR (@json = ''))
		  SELECT N'{"ok": 0, "msg": "Không có dữ liệu", "data": []}' AS json;
		ELSE
		BEGIN
		  SELECT @json = REPLACE(@json, '(null)', 'null');
		  SELECT N'{"ok": 1, "msg": "OK", "data": [' + LEFT(@json, LEN(@json) - 1) + ']}' AS json;
        END
	END   
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertCoffeePrices]    Script Date: 5/28/2024 7:08:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertCoffeePrices]
    @json NVARCHAR(MAX)
AS
BEGIN
    -- Create a temporary table to hold the parsed JSON data
    CREATE TABLE #TempCoffeePrices (
        [date] DATE,
        [value] DECIMAL(18,2)
    );

    -- Insert data from JSON into the temporary table
    INSERT INTO #TempCoffeePrices ([date], [value])
    SELECT [date], [value]
    FROM OPENJSON(@json)
    WITH (
        [date] DATE '$.date',
        [value] DECIMAL(18,2) '$.value'
    );

    -- Insert data into the main table, avoiding duplicates
    INSERT INTO [IPSTACK].[dbo].[Ca_Phe_prices] ([date], [value])
    SELECT [date], [value]
    FROM #TempCoffeePrices
    WHERE [date] NOT IN (
        SELECT [date]
        FROM [IPSTACK].[dbo].[Ca_Phe_prices]
    );

    -- Clean up temporary table
    DROP TABLE #TempCoffeePrices;
END;
GO
