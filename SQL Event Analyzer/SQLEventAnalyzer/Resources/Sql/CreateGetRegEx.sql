create function [dbo].[GetRegEx]
(
	@input nvarchar(4000),
	@regExPattern nvarchar(4000)
)
returns nvarchar(4000)
as
external name [SQLEventAnalyzerCLR].[UserDefinedFunctions].[GetRegEx]
