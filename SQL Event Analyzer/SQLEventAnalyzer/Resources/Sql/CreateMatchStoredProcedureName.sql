create function [dbo].[MatchStoredProcedureName]
(
	@input nvarchar(4000),
	@name nvarchar(4000),
	@occurrence int
)
returns bit
as
external name [SQLEventAnalyzerCLR].[UserDefinedFunctions].[MatchStoredProcedureName]
