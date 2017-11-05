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
where r.RowNum between (@PageNum - 1) * @PageSize + 1 and @PageNum * @PageSize

select {11}
from dbo.#temp t1
inner join dbo.[{9}] t on t.ID = t1.RowId
order by t1.RowNum

if {12} = 1
begin
	select count(*) TotalRows
	from dbo.[{9}] t
	where 1 = 1
	{3}
end
else
begin
	select {13} TotalRows
end

drop table dbo.#temp{6}{7}
