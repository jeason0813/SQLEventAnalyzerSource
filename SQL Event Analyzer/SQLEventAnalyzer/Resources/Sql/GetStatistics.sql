use [{8}]

declare @PageNum int
declare @PageSize int

set @PageNum = {0}
set @PageSize = {1}

{4}{5}
select {13}
from
(
	select row_number() over(order by {2}) RowNum, {12}
	from
	(
		select count(t.ID) TotalCount
		, min(t.Duration) MinDuration, max(t.Duration) MaxDuration, avg(t.Duration) AvgDuration, round(stdev(t.Duration), 0) DevDuration, round(var(t.Duration), 0) VarDuration, sum(t.Duration) SumDuration
		, min(t.Reads) MinReads, max(t.Reads) MaxReads, avg(t.Reads) AvgReads, round(stdev(t.Reads), 0) DevReads, round(var(t.Reads), 0) VarReads, sum(t.Reads) SumReads
		, min(t.Writes) MinWrites, max(t.Writes) MaxWrites, avg(t.Writes) AvgWrites, round(stdev(t.Writes), 0) DevWrites, round(var(t.Writes), 0) VarWrites, sum(t.Writes) SumWrites
		, min(t.CPU) MinCPU, max(t.CPU) MaxCPU, avg(t.CPU) AvgCPU, round(stdev(t.CPU), 0) DevCPU, round(var(t.CPU), 0) VarCPU, sum(t.CPU) SumCPU
		, min(t.Rows) MinRows, max(t.Rows) MaxRows, avg(t.Rows) AvgRows, round(stdev(t.Rows), 0) DevRows, round(var(t.Rows), 0) VarRows, sum(t.Rows) SumRows
		{11}
		from dbo.[{9}] t
		where 1 = 1
		{3}
	) v
) r
where r.RowNum between (@PageNum - 1) * @PageSize + 1 and @PageNum * @PageSize

select count(*) TotalRows, Min(c.MinStartTime) MinStartTime, Max(c.MaxStartTime) MaxStartTime
from
(
	select count(*) TotalRows, Min(t.StartTime) MinStartTime, Max(t.StartTime) MaxStartTime
	from dbo.[{9}] t
	where 1 = 1
	{3}
) c

{6}{7}
