use [{0}]

insert into dbo.[{3}] (FileName, Type, TextData, SPID, Duration, StartTime, Reads, Writes, CPU, Rows)
select '{2}', e.name Type, convert(varchar(max), t.TextData), t.SPID, t.Duration / 1000 Duration, t.StartTime, isnull(t.Reads, -1) Reads, isnull(t.Writes, -1) Writes, isnull(t.CPU, -1) CPU, isnull(t.RowCounts, -1) Rows
from fn_trace_gettable('{1}.trc', default) t
inner join sys.trace_events e on e.trace_event_id = t.EventClass
where t.TextData is not null and t.StartTime is not null and t.EventClass is not null and t.Duration is not null
order by t.StartTime
