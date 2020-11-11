using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using System.IO;


/// <summary>
///AutoUpdateWebSrv 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class AutoUpdateWebSrv : System.Web.Services.WebService
{

    private string updFileName = "UpdFileList.xml";


    public AutoUpdateWebSrv()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    /// <summary>
    /// 检查是否有更新
    /// </summary>
    /// <param name="appCode"></param>
    /// <param name="ds"></param>
    /// <returns></returns>
    [WebMethod]
    public bool CheckUpdated(string appCode, DataSet ds)
    {
        

        bool _rflag;
        // 获取源文件夹
        string srcPath = getSrcPath(appCode);

        // 读取本地同步文件
        if (File.Exists(Path.Combine(srcPath, updFileName)) == false)
        {
            return false;
        }

        if (ds == null)
        {
            return true;
        }

        // 比较是否有更新
        DataSet dsSrc = new DataSet();
        dsSrc.ReadXml(Path.Combine(srcPath, updFileName), XmlReadMode.ReadSchema);

        _rflag = isChanged(dsSrc, ds);

        #region 日志
        //日志
        //if (appCode == "002")
        //    OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "appCode:" + appCode
        //        + " " + "ds:" + OperationLog.DataTableConvertJson.Dataset2Json(ds), _rflag.ToString());

        #endregion

        return _rflag;
    }


    /// <summary>
    /// 获取更新源文件夹
    /// </summary>
    /// <returns></returns>
    private string getSrcPath(string appCode)
    {
        string filePathValue = string.Empty;
        string filePath = System.Configuration.ConfigurationManager.AppSettings["filePath"];
        switch (appCode)
        {
            case "001":     // 移动护理报表
                filePathValue = filePath + @"\移动护理PC自动更新";
                break;
            case "002":
                filePathValue = filePath + @"\MOBILEUPDATE";
                break;
            default:
                filePathValue = string.Empty;
                break;

        }

        //日志
        //if (appCode == "002")
        //    OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "appCode:" + appCode
        //        , filePathValue);
        return filePathValue;
    }


    /// <summary>
    /// 比较两个数据集
    /// </summary>
    /// <param name="dsSrc"></param>
    /// <param name="dsDest"></param>
    /// <returns></returns>
    private bool isChanged(DataSet dsSrc, DataSet dsDest)
    {
        DataRow[] drSrc = dsSrc.Tables[0].Select(string.Empty, "FILE_NAME");
        DataRow[] drDest = dsDest.Tables[0].Select(string.Empty, "FILE_NAME");
        int idxSrc = 0;
        int idxDest = 0;
        int compare = 0;
        while (idxSrc < drSrc.Length && idxDest < drDest.Length)
        {
            compare = drSrc[idxSrc]["FILE_NAME"].ToString().CompareTo(drDest[idxDest]["FILE_NAME"].ToString());

            // 如果服务器与客户端文件相同
            if (compare == 0)
            {
                // 版本号比较
                if (drSrc[idxSrc]["VERSION"].ToString().Equals(drDest[idxDest]["VERSION"].ToString()) == false)
                {
                    return true;
                }

                idxDest++;
                idxSrc++;
                continue;
            }

            // 如果服务器新增文件
            if (compare < 0)
            {
                return true;
            }

            // 如果服务器删除文件, 不删除客户端的文件
            idxDest++;
            continue;
        }

        // 如果服务器新增文件
        if (idxSrc < drSrc.Length)
        {
            return true;
        }

        // 如果服务器删除文件, 不删除客户端的文件
        return false;
    }


    /// <summary>
    /// 获取服务器的更新更表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetServerFileList(string appCode)
    {
        // 获取源文件夹
        string srcPath = getSrcPath(appCode);

        // 读取本地同步文件
        if (File.Exists(Path.Combine(srcPath, updFileName)) == false)
        {
            return null;
        }

        // 比较是否有更新
        DataSet dsSrc = new DataSet();
        dsSrc.ReadXml(Path.Combine(srcPath, updFileName), XmlReadMode.ReadSchema);

        #region 日志
        if (appCode == "002")
        {
            string logJsonValue = string.Empty;
            if (dsSrc != null && dsSrc.Tables[0].Rows.Count > 0)
                logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(dsSrc);
            OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
                "appCode:" + appCode, logJsonValue);
        }
        #endregion

        return dsSrc;
    }


    /// <summary>
    /// 获取服务器的文件
    /// </summary>
    /// <param name="appCode"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    [WebMethod]
    public byte[] GetServerFile(string appCode, string fileName)
    {
        fileName = Path.Combine(getSrcPath(appCode), fileName);

        return getBinaryFile(fileName);
    }


    /// <summary>
    /// getBinaryFile：返回所给文件路径的字节数组。
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public byte[] getBinaryFile(string filename)
    {
        if (File.Exists(filename))
        {
            try
            {
                ///打开现有文件以进行读取。
                FileStream s = File.OpenRead(filename);

                int b1;
                MemoryStream stream = new MemoryStream();
                while ((b1 = s.ReadByte()) != -1)
                {
                    stream.WriteByte(((byte)b1));
                }

                s.Close();
                s.Dispose();
                return stream.ToArray();
            }
            catch (Exception e)
            {
                return new byte[0];
            }
        }
        else
        {
            return new byte[0];
        }
    }
}

