create function [dbo].[GetStoredProcedureName]
(
	@input nvarchar(4000),
	@occurrence int
)
returns nvarchar(4000)
as
external name [SQLEventAnalyzerCLR].[UserDefinedFunctions].[GetStoredProcedureName]
