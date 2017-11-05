use [{0}]

declare @objectSourceName varchar(255)
set @objectSourceName = '{1}'

declare @objectTargetName varchar(255)
set @objectTargetName = '{2}'

declare @numberofrows int
set @numberofrows = 0

declare @i int
set @i = 1

declare @numberofrows1 int
declare @i1 int

declare @indexname varchar(255)
declare @indextype varchar(255)
declare @isunique bit
declare @fillfactor tinyint
declare @colname varchar(255)
declare @is_descending_key bit

create table dbo.#temp1
(
	id int unique identity not null,
	colname varchar(255) not null,
	is_descending_key bit not null
)

create table dbo.#temp2
(
	id int unique identity not null,
	indexname varchar(255) not null,
	indextype varchar(255) not null,
	is_unique bit not null,
	fill_factor tinyint not null
)

insert into dbo.#temp2 (indexname, indextype, is_unique, fill_factor)
select distinct i.name, i.type_desc, i.is_unique, i.fill_factor
from sys.indexes i
inner join sys.index_columns ic on ic.index_id = i.index_id and ic.object_id = i.object_id
inner join sys.objects o on i.object_id = o.object_id
inner join sys.schemas s on s.schema_id = o.schema_id
where o.name = @objectSourceName
and i.is_primary_key = 0 and s.name = 'dbo'
order by i.type_desc

set @numberofrows = @@rowcount
set @i = 1

declare @objectIX varchar(max)
set @objectIX = ''

while @i <= @numberofrows
begin
	select @indexname = t.indexname, @indextype = t.indextype, @isunique = t.is_unique, @fillfactor = t.fill_factor
	from dbo.#temp2 t
	where t.id = @i

	declare @indexcols varchar(max)
	set @indexcols = ''

	set @i1 = 1

	insert into dbo.#temp1 (colname, is_descending_key)
	select c.name, ic.is_descending_key
	from sys.indexes i
	inner join sys.index_columns ic on ic.index_id = i.index_id and ic.object_id = i.object_id
	inner join sys.columns c on c.column_id = ic.column_id and c.object_id = ic.object_id
	inner join sys.objects o on i.object_id = o.object_id
	inner join sys.schemas s on s.schema_id = o.schema_id
	where o.name = @objectSourceName and i.name = @indexname
	and i.is_primary_key = 0 and s.name = 'dbo'
	order by ic.key_ordinal

	set @numberofrows1 = @@rowcount

	while @i1 <= @numberofrows1
	begin
		select @colname = t.colname, @is_descending_key = t.is_descending_key
		from dbo.#temp1 t
		where t.id = @i1

		if @is_descending_key = 1
		begin
			set @indexcols = @indexcols + '[' + @colname + '] DESC'
		end
		else
		begin
			set @indexcols = @indexcols + '[' + @colname + ']'
		end

		if @i1 < @numberofrows1
		begin
			set @indexcols = @indexcols + ', '
		end

		set @i1 = @i1 + 1
	end

	if @isunique = 1
	begin
		set @indextype = 'unique ' + @indextype
	end

	if @fillfactor = 0
	begin
		set @fillfactor = 100
	end
	
	if @indextype = 'NONCLUSTERED COLUMNSTORE'
	begin
		set @objectIX = @objectIX + 'create ' + @indextype + ' index [' + @indexname + '] on dbo.[' + @objectTargetName + '] (' + @indexcols + ') on [primary] '
	end
	else
	begin
		set @objectIX = @objectIX + 'create ' + @indextype + ' index [' + @indexname + '] on dbo.[' + @objectTargetName + '] (' + @indexcols + ') with (fillfactor = ' + convert(varchar(3), @fillfactor) + ') on [primary] '
	end

	truncate table dbo.#temp1

	set @i = @i + 1
end

drop table dbo.#temp1
drop table dbo.#temp2

select @objectIX ObjectScript
