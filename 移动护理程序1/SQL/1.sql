configuration  

--创建 配置表 结构
--此表有的 列 
-- NURSING_NO  流水号
--DEPT_CODE 科室号
--configNAME  显示名称
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
  is '参数配置表';
-- Add comments to the columns 
comment on column MOBILE.configuration.NURSING_NO
  is '记录流水号 ';
comment on column MOBILE.configuration.DEPT_CODE
  is '科室';
comment on column MOBILE.configuration.configNAME
  is '配置名称';
comment on column MOBILE.configuration.configparameters
  is '配置参数';


  
grant select on MOBILE.configuration to PUBLIC;
grant select, insert, update, delete on MOBILE.configuration to ROLE_MOBILE;


select * from   mobile.configuration ;

Create Public Synonym configuration FOR MOBILE.configuration;

select *  from   configuration