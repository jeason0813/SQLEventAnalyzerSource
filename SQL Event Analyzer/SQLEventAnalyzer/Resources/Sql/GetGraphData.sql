use [{0}]

declare @count int

{5}{6}
select @count = count(t.ID)
from dbo.[{3}] t
where 1 = 1
{1}

if @count <= {4}
begin	
	select t.ID, t.TextData, t.Duration, t.StartTime, t.Reads, t.Writes, t.CPU, t.Rows{2}
	from dbo.[{3}] t
	where 1 = 1
	{1}
	order by t.StartTime

	select @count NumberOfRows
end
else
begin
	select 0 Dummy

	select @count NumberOfRows
end
{7}{8}
