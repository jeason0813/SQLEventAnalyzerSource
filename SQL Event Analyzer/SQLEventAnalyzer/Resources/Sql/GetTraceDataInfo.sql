use [{0}]

create table dbo.#temp
(
	TotalRows int not null,
	MinID int null,
	MaxID int null,
	MinSPID int null,
	MaxSPID int null,
	MinDuration bigint null,
	MaxDuration bigint null,
	MinStartTime datetime null,
	MaxStartTime datetime null,
	MinReads bigint null,
	MaxReads bigint null,
	MinWrites bigint null,
	MaxWrites bigint null,
	MinCPU bigint null,
	MaxCPU bigint null,
	MinRows bigint null,
	MaxRows bigint null
)

insert into dbo.#temp (TotalRows)
select count(*) TotalRows
from dbo.[{1}] t

update dbo.#temp
set MinID = t1.MinID, MaxID = t1.MaxID
from
(
	select min(t.ID) MinID, max(t.ID) MaxID
	from dbo.[{1}] t
) t1

update dbo.#temp
set MinSPID = t1.MinSPID, MaxSPID = t1.MaxSPID
from
(
	select min(t.SPID) MinSPID, max(t.SPID) MaxSPID
	from dbo.[{1}] t
) t1

update dbo.#temp
set MinDuration = t1.MinDuration, MaxDuration = t1.MaxDuration
from
(
	select min(t.Duration) MinDuration, max(t.Duration) MaxDuration
	from dbo.[{1}] t
) t1

update dbo.#temp
set MinStartTime = t1.MinStartTime, MaxStartTime = t1.MaxStartTime
from
(
	select min(t.StartTime) MinStartTime, max(t.StartTime) MaxStartTime
	from dbo.[{1}] t
) t1

update dbo.#temp
set MinReads = t1.MinReads, MaxReads = t1.MaxReads
from
(
	select min(t.Reads) MinReads, max(t.Reads) MaxReads
	from dbo.[{1}] t
) t1

update dbo.#temp
set MinWrites = t1.MinWrites, MaxWrites = t1.MaxWrites
from
(
	select min(t.Writes) MinWrites, max(t.Writes) MaxWrites
	from dbo.[{1}] t
) t1

update dbo.#temp
set MinCPU = t1.MinCPU, MaxCPU = t1.MaxCPU
from
(
	select min(t.CPU) MinCPU, max(t.CPU) MaxCPU
	from dbo.[{1}] t
) t1

update dbo.#temp
set MinRows = t1.MinRows, MaxRows = t1.MaxRows
from
(
	select min(t.Rows) MinRows, max(t.Rows) MaxRows
	from dbo.[{1}] t
) t1

select t.TotalRows, t.MinID, t.MaxID, t.MinSPID, t.MaxSPID, t.MinDuration, t.MaxDuration, t.MinStartTime, t.MaxStartTime, t.MinReads, t.MaxReads, t.MinWrites, t.MaxWrites, t.MinCPU, t.MaxCPU, t.MinRows, t.MaxRows
from dbo.#temp t

drop table dbo.#temp

if {2} = 1
begin
	select distinct t.Type
	from dbo.[{1}] t
	order by t.Type

	select distinct t.FileName
	from dbo.[{1}] t
	order by t.FileName
end
