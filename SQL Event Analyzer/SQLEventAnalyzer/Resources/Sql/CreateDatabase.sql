use [master]

if not exists (select d.* from sys.databases d where d.name = '{0}')
begin
	create database [{0}]
	alter database [{0}] set recovery simple
end
