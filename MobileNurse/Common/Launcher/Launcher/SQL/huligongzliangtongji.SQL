SELECT NURSE ��ʿ, 
       SUM(COUNT_ORDER_EXECUTE) ҽ��ִ�д��� , 
       SUM(COUNT_VITAL_SIGNS) ���������ɼ�����, 
       SUM(EVAL_IN) ��Ժ��������, 
       SUM(EVAL_EACH) ÿ����������
FROM (
     select EXECUTE_NURSE NURSE, count(*) COUNT_ORDER_EXECUTE , null COUNT_VITAL_SIGNS, null EVAL_IN, null EVAL_EACH
     from orders_execute where IS_EXECUTE = '1' and ORDER_SUB_NO = '1' and EXECUTE_DATE_TIME >={DATE_BEGIN} AND EXECUTE_DATE_TIME < {DATE_END} 
     group by EXECUTE_NURSE
     union
     select NURSE NURSE, null COUNT_ORDER_EXECUTE , count(*) COUNT_VITAL_SIGNS, null EVAL_IN, null EVAL_EACH
     from vital_signs_rec where TIME_POINT >={DATE_BEGIN} AND TIME_POINT < {DATE_END}  group by NURSE
     union
     select ITEM_VALUE NURSE, null COUNT_ORDER_EXECUTE , null COUNT_VITAL_SIGNS, count(*) EVAL_IN, null EVAL_EACH
     from PAT_EVALUATION_M where dict_id = '02' and RECORD_DATE >={DATE_BEGIN} AND RECORD_DATE < {DATE_END} AND item_name = 'RECORDER' 
     group by ITEM_VALUE
     union
     select ITEM_VALUE NURSE, null COUNT_ORDER_EXECUTE , null COUNT_VITAL_SIGNS, NULL EVAL_IN, COUNT(*) EVAL_EACH
     from PAT_EVALUATION_M where dict_id = '01' and RECORD_DATE >={DATE_BEGIN} AND RECORD_DATE < {DATE_END} AND item_name = 'RECORDER' 
     group by ITEM_VALUE
)
GROUP BY 
     NURSE