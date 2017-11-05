use [{0}]

if exists (select i.* from sys.indexes i where i.object_id = object_id(N'dbo.[{1}]') and i.name = N'ix_CS')
begin
	alter index [ix_CS] on dbo.[{2}] disable
end
