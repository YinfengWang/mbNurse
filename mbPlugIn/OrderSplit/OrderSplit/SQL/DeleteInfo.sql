[删除医嘱执行单表45天前数据成功!,delete from ORDERS_EXECUTE E where E.SCHEDULE_PERFORM_TIME<TO_DATE(SYSDATE-45) and 1=2]
[删除医嘱备份表7天前数据成功!,delete from orders_m_tombstone where create_timestamp<TO_DATE(SYSDATE-7) and 1=2]
[删除医嘱执行单备份表5天前数据成功!,delete from ORDERS_EXECUTE_TOMBSTONE E where E.ORDERS_PERFORM_SCHEDULE<TO_DATE(SYSDATE-5) and 1=2]