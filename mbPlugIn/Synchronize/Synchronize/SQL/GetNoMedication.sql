SELECT PATS_IN_HOSPITAL.WARD_CODE,
       ORDERS.PATIENT_ID,
       ORDERS.VISIT_ID,
       ORDERS.ORDER_NO,
       ORDERS.ORDER_SUB_NO,
       ORDERS.REPEAT_INDICATOR,
       ORDERS.ORDER_CLASS,
       (SELECT CLASS_NAME
          FROM CLINIC_ITEM_CLASS_DICT
         WHERE CLASS_CODE = ORDERS.ORDER_CLASS) ORDER_CLASS_NAME,
       ORDERS.ORDER_STATUS,
       (SELECT ORDER_STATUS_NAME
          FROM ORDER_STATUS_DICT
         WHERE ORDER_STATUS_CODE = ORDERS.ORDER_STATUS) ORDER_STATUS_NAME,
       ORDERS.ORDER_TEXT,
       ORDERS.ORDER_CODE,
       ORDERS.DOSAGE,
       ORDERS.DOSAGE_UNITS,
       substr(ORDERS.administration,1,decode(instr(replace(ORDERS.administration,'{000}','('),'('),0,length(ORDERS.administration),
instr(replace(ORDERS.administration,'{000}','('),'(')-1)) ADMINISTRATION,
       ORDERS.DURATION,
       ORDERS.DURATION_UNITS,
       ORDERS.START_DATE_TIME,
       ORDERS.STOP_DATE_TIME,
       ORDERS.FREQUENCY,
       ORDERS.FREQ_COUNTER,
       ORDERS.FREQ_INTERVAL,
       ORDERS.FREQ_INTERVAL_UNIT,
       ORDERS.FREQ_DETAIL,
       ORDERS.PERFORM_SCHEDULE,
       ORDERS.PERFORM_RESULT,
       ORDERS.ORDERING_DEPT,
       ORDERS.DOCTOR,
       ORDERS.STOP_DOCTOR,
       ORDERS.NURSE,
       ORDERS.STOP_NURSE
  FROM ORDERS, PATS_IN_HOSPITAL
 WHERE ORDERS.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID AND
       ORDERS.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID AND
       (ORDERS.ORDER_STATUS='2' OR (ORDERS.ORDER_STATUS='3' AND ORDERS.STOP_DATE_TIME>SYSDATE AND repeat_indicator='1') AND
       (repeat_indicator='1' OR (repeat_indicator='0' and start_date_time>=to_date(sysdate-2)))) AND ORDERS.ORDER_CLASS IN ('H','I','Z','E','F')


//
// ����:
       ��ȡ��Ч��ҩ��ҽ�� ��������Լ�������Ҫ��ͬ����ҽ�� ���� ��  ����ֵ: (���뷵�������ֶ�)
PATIENT_ID,���˱�ʶ��
VISIT_ID,���˱���סԺ��ʶ
ORDER_NO,ҽ�����
ORDER_SUB_NO,ҽ�������
REPEAT_INDICATOR,����ҽ����־
ORDER_CLASS,ҽ�����
(SELECT CLASS_NAME
          FROM CLINIC_ITEM_CLASS_DICT
         WHERE CLASS_CODE = ORDERS.ORDER_CLASS) ORDER_CLASS_NAME,ҽ���������
ORDER_STATUS,ҽ��״̬
       (SELECT ORDER_STATUS_NAME
          FROM ORDER_STATUS_DICT
         WHERE ORDER_STATUS_CODE = ORDERS.ORDER_STATUS) ORDER_STATUS_NAME,ҽ��״̬����
ORDER_TEXT,ҽ������
       ORDER_CODE,ҽ������
       DOSAGE,ҩƷһ��ʹ�ü���
       DOSAGE_UNITS,������λ
       ADMINISTRATION,��ҩ;���ͷ���
       ----============ORDERS.ADMINISTRATION,��һ��д��==========-------
       DURATION,����ʱ��
       DURATION_UNITS,����ʱ�䵥λ
       START_DATE_TIME,��ʼ���ڼ�ʱ��
       STOP_DATE_TIME,ֹͣ���ڼ�ʱ��
       FREQUENCY,ִ��Ƶ������
       FREQ_COUNTER,Ƶ�ʴ���
       FREQ_INTERVAL,Ƶ�ʼ��
       FREQ_INTERVAL_UNIT,Ƶ�ʼ����λ
       FREQ_DETAIL,ִ��ʱ����ϸ����
       PERFORM_SCHEDULE,��ʿִ��ʱ��
       PERFORM_RESULT,ִ�н��
       ORDERING_DEPT,��ҽ������
       DOCTOR,��ҽ��ҽ��
       STOP_DOCTOR,ͣҽ��ҽ��
       NURSE,��ҽ��У�Ի�ʿ
       STOP_NURSE,ͣҽ��У�Ի�ʿ
      