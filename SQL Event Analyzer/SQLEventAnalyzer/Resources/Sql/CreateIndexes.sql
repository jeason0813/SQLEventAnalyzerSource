use [{0}]

create nonclustered index [ix_FileName_asc] on dbo.[{1}] ([FileName] asc) with (fillfactor = 100)
create nonclustered index [ix_FileName_desc] on dbo.[{1}] ([FileName] desc) with (fillfactor = 100)

create nonclustered index [ix_Type_asc] on dbo.[{1}] ([type] asc) with (fillfactor = 100)
create nonclustered index [ix_Type_desc] on dbo.[{1}] ([type] desc) with (fillfactor = 100)

create nonclustered index [ix_SPID_asc] on dbo.[{1}] ([SPID] asc) with (fillfactor = 100)
create nonclustered index [ix_SPID_desc] on dbo.[{1}] ([SPID] desc) with (fillfactor = 100)

create nonclustered index [ix_Duration_asc] on dbo.[{1}] ([Duration] asc) with (fillfactor = 100)
create nonclustered index [ix_Duration_desc] on dbo.[{1}] ([Duration] desc) with (fillfactor = 100)

create nonclustered index [ix_StartTime_asc] on dbo.[{1}] ([StartTime] asc) with (fillfactor = 100)
create nonclustered index [ix_StartTime_desc] on dbo.[{1}] ([StartTime] desc) with (fillfactor = 100)

create nonclustered index [ix_Reads_asc] on dbo.[{1}] ([Reads] asc) with (fillfactor = 100)
create nonclustered index [ix_Reads_desc] on dbo.[{1}] ([Reads] desc) with (fillfactor = 100)

create nonclustered index [ix_Writes_asc] on dbo.[{1}] ([Writes] asc) with (fillfactor = 100)
create nonclustered index [ix_Writes_desc] on dbo.[{1}] ([Writes] desc) with (fillfactor = 100)

create nonclustered index [ix_Cpu_asc] on dbo.[{1}] ([CPU] asc) with (fillfactor = 100)
create nonclustered index [ix_Cpu_desc] on dbo.[{1}] ([CPU] desc) with (fillfactor = 100)

create nonclustered index [ix_Rows_asc] on dbo.[{1}] ([Rows] asc) with (fillfactor = 100)
create nonclustered index [ix_Rows_desc] on dbo.[{1}] ([Rows] desc) with (fillfactor = 100)
