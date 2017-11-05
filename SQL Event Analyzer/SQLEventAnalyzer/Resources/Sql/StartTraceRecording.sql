{1}
declare @on bit
set @on = 1

-- SQL:BatchCompleted
exec sp_trace_setevent {0}, 12, 1, @on -- TextData
exec sp_trace_setevent {0}, 12, 13, @on -- Duration
exec sp_trace_setevent {0}, 12, 14, @on -- StartTime
exec sp_trace_setevent {0}, 12, 16, @on -- Reads
exec sp_trace_setevent {0}, 12, 17, @on -- Writes
exec sp_trace_setevent {0}, 12, 18, @on -- CPU
exec sp_trace_setevent {0}, 12, 48, @on -- Rows

-- RPC:Completed
exec sp_trace_setevent {0}, 10, 1, @on -- TextData
exec sp_trace_setevent {0}, 10, 13, @on -- Duration
exec sp_trace_setevent {0}, 10, 14, @on -- StartTime
exec sp_trace_setevent {0}, 10, 16, @on -- Reads
exec sp_trace_setevent {0}, 10, 17, @on -- Writes
exec sp_trace_setevent {0}, 10, 18, @on -- CPU
exec sp_trace_setevent {0}, 10, 48, @on -- Rows

exec sp_trace_setfilter {0}, 1, 0, 7, N'{1}%'
exec sp_trace_setfilter {0}, 1, 0, 7, N'exec sp[_]reset[_]connection'
exec sp_trace_setfilter {0}, 1, 0, 7, N'SET STATISTICS IO ON'
exec sp_trace_setfilter {0}, 1, 0, 7, N'SET STATISTICS IO OFF'
exec sp_trace_setfilter {0}, 1, 0, 7, N'SET NO_BROWSETABLE ON'
exec sp_trace_setfilter {0}, 1, 0, 7, N'SET NO_BROWSETABLE OFF'
exec sp_trace_setfilter {0}, 1, 0, 7, N'SET ARITHABORT ON'
exec sp_trace_setfilter {0}, 1, 0, 7, N'SET ARITHABORT OFF'
exec sp_trace_setfilter {0}, 1, 0, 1, NULL

exec sp_trace_setstatus {0}, 1
