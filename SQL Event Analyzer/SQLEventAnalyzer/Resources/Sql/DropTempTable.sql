use [{0}]

if exists (select * from dbo.sysobjects where id = object_id(N'dbo.[{1}]') and objectproperty(id, N'IsUserTable') = 1)
begin
	drop table dbo.[{2}]
end
