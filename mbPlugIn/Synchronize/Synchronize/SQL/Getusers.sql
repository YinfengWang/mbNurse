select  DB_USER ,
        USER_ID ,
        USER_NAME,
        USER_DEPT,
        CREATE_DATE
from  users
where USER_ID is not null
     and user_dept is not null
     and user_name is not null

// 功能:
       获取人员管理表
   
   返回值: (必须返回如下字段)
      