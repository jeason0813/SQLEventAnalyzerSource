declare @TraceID int
declare @rc int
declare @maxfilesize bigint
set @maxfilesize = 50000

exec @rc = sp_trace_create @TraceID output, 2, N'{0}', @maxfilesize, null
select @TraceID TraceID, @rc ReturnCode
