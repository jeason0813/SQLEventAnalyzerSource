use [{0}]

insert into dbo.[{2}] (FileName, Type, TextData, SPID, Duration, StartTime, Reads, Writes, CPU, Rows{3})
select t.FileName, t.Type, t.TextData, t.SPID, t.Duration, t.StartTime, t.Reads, t.Writes, t.CPU, t.Rows{4}
from dbo.[TraceData_{1}] t
order by t.FileName, t.StartTime
