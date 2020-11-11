configuration  

--���� ���ñ� �ṹ
--�˱��е� �� 
-- NURSING_NO  ��ˮ��
--DEPT_CODE ���Һ�
--configNAME  ��ʾ����
--configparameters

select *  from  nursing_orders_m ;

select  * from  patient_info 


-- Create table
create table  MOBILE.configuration
(
  NURSING_NO         NUMBER(5) not null,
  DEPT_CODE       VARCHAR2(100),
  configNAME          VARCHAR2(20),
  configparameters    VARCHAR2(20)
)
tablespace TSP_MOBILE
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 16
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate primary, unique and foreign key constraints 
alter table MOBILE.configuration
  add constraint NURSING_NO primary key (NURSING_NO)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );
  
  
  -- Add comments to the table 
comment on table MOBILE.configuration
  is '�������ñ�';
-- Add comments to the columns 
comment on column MOBILE.configuration.NURSING_NO
  is '��¼��ˮ�� ';
comment on column MOBILE.configuration.DEPT_CODE
  is '����';
comment on column MOBILE.configuration.configNAME
  is '��������';
comment on column MOBILE.configuration.configparameters
  is '���ò���';


  
grant select on MOBILE.configuration to PUBLIC;
grant select, insert, update, delete on MOBILE.configuration to ROLE_MOBILE;


select * from   mobile.configuration ;

Create Public Synonym configuration FOR MOBILE.configuration;

select *  from   configuration