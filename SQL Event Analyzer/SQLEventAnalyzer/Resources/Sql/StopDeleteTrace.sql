if exists (select * from ::fn_trace_getinfo(0) where left(convert(nvarchar(245), value), len('{0}')) = '{0}')
begin
	declare @TraceID int

	select @TraceID = traceid
	from ::fn_trace_getinfo(0)
	where left(convert(nvarchar(245), value), len('{0}')) = '{0}'

	exec sp_trace_setstatus @TraceID, 0 -- stop trace
	exec sp_trace_setstatus @TraceID, 2 -- delete trace
end
