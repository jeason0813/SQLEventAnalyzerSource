use [{0}]

if exists (select * from dbo.sysobjects where id = object_id(N'dbo.[{1}]') and objectproperty(id, N'IsUserTable') = 1)
begin
	drop table dbo.[{2}]
end

create table dbo.[{2}]
(
	ID int identity(1, 1) not null,
	FileName varchar(255) not null,
	Type varchar(44) not null,
	TextData varchar(max) not null,
	SPID int not null,
	Duration bigint not null,
	StartTime DateTime not null,
	Reads bigint not null,
	Writes bigint not null,
	CPU bigint not null,
	Rows bigint not null
)

create unique clustered index ix_ID on dbo.[{2}] (ID) with (fillfactor = 100)

exec dbo.sp_fulltext_table N'dbo.[{1}]', N'create', N'SQLEventAnalyzerFullText', N'ix_ID'
exec dbo.sp_fulltext_column N'dbo.[{1}]', N'TextData', N'add', 1033
exec dbo.sp_fulltext_table N'dbo.[{1}]', N'activate'
