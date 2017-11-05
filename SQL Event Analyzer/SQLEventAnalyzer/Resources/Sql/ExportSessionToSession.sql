use [{8}]

declare @PageNum int
declare @PageSize int

set @PageNum = {0}
set @PageSize = {1}

{4}{5}
select r.RowNum, r.RowId
into dbo.#temp
from
(
	select row_number() over(order by {2}, v.RowId) RowNum, v.RowId
	from
	(
		select t.ID RowId, {10}
		from dbo.[{9}] t
		where 1 = 1
		{3}
	) v
) r
{11}where r.RowNum between (@PageNum - 1) * @PageSize + 1 and @PageNum * @PageSize

select t.*
into dbo.[TraceData_{12}]
from dbo.#temp t1
inner join dbo.[{9}] t on t.ID = t1.RowId
order by t1.RowNum

drop table dbo.#temp{6}{7}

alter table dbo.[TraceData_{12}] add ID_new int identity(1, 1)
alter table dbo.[TraceData_{12}] drop column ID
exec dbo.sp_rename 'dbo.[TraceData_{12}].ID_new', 'ID', 'Column'

{14}
{15}

exec dbo.sp_fulltext_table N'dbo.[TraceData_{13}]', N'create', N'SQLEventAnalyzerFullText', N'ix_ID'
exec dbo.sp_fulltext_column N'dbo.[TraceData_{13}]', N'TextData', N'add', 1033
exec dbo.sp_fulltext_table N'dbo.[TraceData_{13}]', N'activate'

alter fulltext index on dbo.[TraceData_{12}] set change_tracking auto

select 1 Completed
