[ɾ��ҽ��ִ�е���45��ǰ���ݳɹ�!,delete from ORDERS_EXECUTE E where E.SCHEDULE_PERFORM_TIME<TO_DATE(SYSDATE-45) and 1=2]
[ɾ��ҽ�����ݱ�7��ǰ���ݳɹ�!,delete from orders_m_tombstone where create_timestamp<TO_DATE(SYSDATE-7) and 1=2]
[ɾ��ҽ��ִ�е����ݱ�5��ǰ���ݳɹ�!,delete from ORDERS_EXECUTE_TOMBSTONE E where E.ORDERS_PERFORM_SCHEDULE<TO_DATE(SYSDATE-5) and 1=2]