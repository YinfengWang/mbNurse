SELECT 
   EMP_NO,
   DEPT_CODE,
   NAME,
   JOB,
   TITLE,
   USER_NAME,
   ID ID,
   PASSWORD  
FROM
   STAFF_DICT
WHERE EMP_NO IS NOT NULL

// 功能:
       获取人员管理表
   
   返回值: (必须返回如下字段)
      