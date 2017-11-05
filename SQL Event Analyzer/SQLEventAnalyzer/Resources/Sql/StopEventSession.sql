declare @IsRunning bit

select @IsRunning = t.IsRunning
from
(
	select case
		when r.create_time is null then 0 else 1
	end IsRunning
	from sys.server_event_sessions s
	left outer join sys.dm_xe_sessions r on r.name = s.name
	where s.name = '{0}'
) t

if @IsRunning = 1
begin
	alter event session [{0}] on server state = stop
end
