create function [dbo].[MatchStoredProcedureParameter]
(
	@input nvarchar(4000),
	@position int,
	@value nvarchar(4000),
	@occurrence int
)
returns bit
as
external name [SQLEventAnalyzerCLR].[UserDefinedFunctions].[MatchStoredProcedureParameter]
