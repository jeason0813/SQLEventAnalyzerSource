use [{0}]

if (select databaseproperty(db_name(), N'IsFullTextEnabled')) != 1
begin
	exec dbo.sp_fulltext_database N'enable'
end

if not exists (select * from dbo.sysfulltextcatalogs where name = N'SQLEventAnalyzerFullText')
begin
	exec dbo.sp_fulltext_catalog N'SQLEventAnalyzerFullText', N'create'
end
