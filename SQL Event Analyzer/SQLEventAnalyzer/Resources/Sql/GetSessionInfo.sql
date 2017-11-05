use [{0}]

;with cte as
(
	select t.name TableName, sum(s.used_page_count) used_pages_count, sum
	(
		case
			when i.index_id < 2 then in_row_data_page_count + lob_used_page_count + row_overflow_used_page_count
			else lob_used_page_count + row_overflow_used_page_count
		end
	) pages, t.create_date
	from sys.dm_db_partition_stats s
	inner join sys.tables t on t.object_id = s.object_id
	inner join sys.indexes i on i.object_id = t.object_id and s.index_id = i.index_id
	group by t.name, t.create_date
)

select right(c.TableName, len(c.TableName) - 10) TableName, c.pages * 8 TableSizeInKB
, case
	when c.used_pages_count > c.pages then (c.used_pages_count - c.pages) * 8 
	else 0
end IndexSizeInKB, c.create_date CreateDate
from cte c
order by c.create_date desc
