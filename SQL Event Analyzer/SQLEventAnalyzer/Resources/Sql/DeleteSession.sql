use [{1}]

if exists (select * from sys.tables t where t.name = 'TraceData_{2}')
begin
	drop table dbo.[TraceData_{0}]
end
