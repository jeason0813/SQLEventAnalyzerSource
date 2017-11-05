use [{0}]

select top 1 t.FileName
from dbo.[TraceData_{1}] t
order by t.ID desc
