create function [dbo].[GetStoredProcedureParameter]
(
	@input nvarchar(4000),
	@position int,
	@occurrence int
)
returns nvarchar(4000)
as
external name [SQLEventAnalyzerCLR].[UserDefinedFunctions].[GetStoredProcedureParameter]
