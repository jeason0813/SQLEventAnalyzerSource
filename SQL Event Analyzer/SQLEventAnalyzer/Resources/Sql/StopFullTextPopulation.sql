use [{0}]

alter fulltext index on dbo.[{1}] set change_tracking off
exec dbo.sp_fulltext_catalog N'SQLEventAnalyzerFullText', N'stop'
