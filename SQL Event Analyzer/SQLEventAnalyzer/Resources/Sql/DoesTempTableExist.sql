use [{0}]

if exists (select * from sys.tables t where t.name = 'TraceData_{1}')
begin
	select 1 Result
end
else
begin
	select 0 Result
end
