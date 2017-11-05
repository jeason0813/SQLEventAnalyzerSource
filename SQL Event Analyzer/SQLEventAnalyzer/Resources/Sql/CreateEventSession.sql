if exists (select * from sys.server_event_sessions e where e.name = '{0}')
begin
	drop event session [{0}] on server
end

create event session [{0}] on server
add event sqlserver.rpc_completed
(
	action (sqlserver.session_id)
	where sqlserver.not_equal_i_sql_unicode_string ([statement], N'exec sp_reset_connection')
)
, add event sqlserver.sql_batch_completed
(
	action (sqlserver.session_id)
	where not sqlserver.like_i_sql_unicode_string(sqlserver.sql_text, N'{2}%')
	and sqlserver.not_equal_i_sql_unicode_string (sqlserver.sql_text, N'SET STATISTICS IO ON')
	and sqlserver.not_equal_i_sql_unicode_string (sqlserver.sql_text, N'SET STATISTICS IO OFF')
	and sqlserver.not_equal_i_sql_unicode_string (sqlserver.sql_text, N'SET NO_BROWSETABLE ON')
	and sqlserver.not_equal_i_sql_unicode_string (sqlserver.sql_text, N'SET NO_BROWSETABLE OFF')
	and sqlserver.not_equal_i_sql_unicode_string (sqlserver.sql_text, N'SET ARITHABORT ON')
	and sqlserver.not_equal_i_sql_unicode_string (sqlserver.sql_text, N'SET ARITHABORT OFF')
)
add target package0.event_file
(
	set filename = N'{1}.xel'
)
with
(
	max_memory = 4096 kb
	, event_retention_mode = no_event_loss
	, max_dispatch_latency = 30 seconds
	, max_event_size = 0 kb
	, memory_partition_mode = none
	, track_causality = off
)
