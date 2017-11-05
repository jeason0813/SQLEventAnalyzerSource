create function [dbo].[MatchRegEx]
(
	@input nvarchar(4000),
	@regExPattern nvarchar(4000)
)
returns bit
as
external name [SQLEventAnalyzerCLR].[UserDefinedFunctions].[MatchRegEx]
