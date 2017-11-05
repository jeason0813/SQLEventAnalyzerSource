use [master]

declare @sessionIdToKill char(35)
set @sessionIdToKill = '{0}'

declare @numberOfRows int
declare @i int
declare @spid smallint
declare @spidText varchar(10)

create table dbo.#temp
(
	id int identity(1, 1) not null,
	spid smallint not null,
	program_name nchar(128) not null
)

insert into dbo.#temp (spid, program_name)
select s.spid, s.program_name
from sys.sysprocesses s
where s.program_name like '{1} - ' + @sessionIdToKill + '%'

set @numberOfRows = @@rowcount

set @i = 1

while @i <= @numberOfRows
begin
	select @spid = t.spid
	from dbo.#temp t
	where t.id = @i
	
	set @spidText = convert(varchar(10), @spid)

	exec ('kill ' + @spidText + '')

	set @i = @i + 1
end

drop table #temp
