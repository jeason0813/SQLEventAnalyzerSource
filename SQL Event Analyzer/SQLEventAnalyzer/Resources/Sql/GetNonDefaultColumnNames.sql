use [{0}]

select i.column_name
from information_schema.columns i
where i.table_name = '{1}' and i.table_schema = 'dbo'
and i.column_name not in ('ID', 'FileName', 'Type', 'TextData', 'SPID', 'Duration', 'StartTime', 'Reads' ,'Writes', 'CPU', 'Rows')
