create function [dbo].[MatchLogParameter]
(
	@input nvarchar(4000),
	@position int,
	@value nvarchar(4000)
)
returns bit
as
external name [SQLEventAnalyzerCLR].[UserDefinedFunctions].[MatchLogParameter]
