create function [dbo].[GetLogParameter]
(
	@input nvarchar(4000),
	@position int
)
returns nvarchar(4000)
as
external name [SQLEventAnalyzerCLR].[UserDefinedFunctions].[GetLogParameter]
