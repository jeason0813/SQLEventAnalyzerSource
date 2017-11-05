use [{0}]

if (select count(*) from sys.fn_xe_file_target_read_file('{1}', null, null, null) x) = 0
begin
	return
end

insert into dbo.[{3}] (FileName, Type, TextData, SPID, Duration, StartTime, Reads, Writes, CPU, Rows)
select '{2}'
, replace(replace(event_xml.value('(./@name)', 'varchar(44)'), 'rpc_completed', 'RPC:Completed'), 'sql_batch_completed', 'SQL:BatchCompleted') Type
, isnull(event_xml.value('(./data[@name="batch_text"]/value)[1]', 'varchar(max)')
, event_xml.value('(./data[@name="statement"]/value)[1]', 'varchar(max)')) TextData
, event_xml.value('(./action[@name="session_id"]/value)[1]', 'int') SPID
, event_xml.value('(./data[@name="duration"]/value)[1]', 'bigint') / 1000 Duration
, dateadd(hh, datediff(hh, getutcdate(), current_timestamp), event_xml.value('(@timestamp)[1]', 'datetime2')) StartTime
, event_xml.value('(./data[@name="logical_reads"]/value)[1]', 'bigint') Reads
, event_xml.value('(./data[@name="writes"]/value)[1]', 'bigint') Writes
, event_xml.value('(./data[@name="cpu_time"]/value)[1]', 'bigint') CPU
, event_xml.value('(./data[@name="row_count"]/value)[1]', 'bigint') Rows
from
(
	select x.object_name Type, convert(xml, event_data) event_data_xml
	from sys.fn_xe_file_target_read_file('{1}', null, null, null) x
) t
cross apply event_data_xml.nodes('//event') n (event_xml)
