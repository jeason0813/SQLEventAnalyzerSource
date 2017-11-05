if exists (select * from sys.server_event_sessions e where e.name = '{0}')
begin
	drop event session [{0}] on server
end
