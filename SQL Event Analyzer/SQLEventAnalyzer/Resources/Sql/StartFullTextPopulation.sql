use [{0}]

alter fulltext index on dbo.[{1}] set change_tracking auto
exec dbo.sp_fulltext_catalog N'SQLEventAnalyzerFullText', N'start_incremental'
