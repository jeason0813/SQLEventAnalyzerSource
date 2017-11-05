use [{0}]

select s.spid, rtrim(convert(varchar(36), replace(s.program_name, '{1} - ', ''))) sessionid
from sys.sysprocesses s
where s.program_name like '{1} - %'
