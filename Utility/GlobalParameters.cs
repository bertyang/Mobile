using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;


[assembly: log4net.Config.DOMConfigurator(ConfigFile = "Web.config", Watch = true)]

namespace Anchor.FA.Utility
{

    /// <summary>
    /// Config配置
    /// </summary>
    public class AppConfig
    {
        #region 配置文件

        /// <summary>
        /// Log配置文件目录
        /// </summary>
        //public static readonly string LogConfigFile = GetStringConfigValue("LogConfigFile");
        /// <summary>
        /// 调度数据库连接
        /// </summary>
        //public static readonly string MainConnectionString = ConfigurationManager.ConnectionStrings["Anchor120V7"].ConnectionString;

        /// <summary>
        /// 管理库连接
        /// </summary>
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        /// <summary>
        /// 报表数据库连接
        /// </summary>
        public static readonly string ConnectionStringReport = ConfigurationManager.ConnectionStrings["ConnectionStringReport"].ConnectionString;

        /// <summary>
        /// 调度数据库连接
        /// </summary>
        public static readonly string ConnectionStringDispatch = ConfigurationManager.ConnectionStrings["ConnectionStringDispatch"] == null ? ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString : ConfigurationManager.ConnectionStrings["ConnectionStringDispatch"].ConnectionString;


        /// <summary>
        /// 报表统计的时刻
        /// </summary>
        public static readonly string ReportTime = GetStringConfigValue("ReportTime");

        /// <summary>
        /// 初始化查询间隔天数
        /// </summary>
        public static readonly int DayDistance = GetInt32ConfigValue("DayDistance");

        /// <summary>
        /// 录音文件的读取路径
        /// </summary>
        public static readonly string MediaClientPath = GetStringConfigValue("MediaClientPath");
       
        /// <summary>
        /// 超规定时间出车流水表的规定时间
        /// </summary>
        public static readonly int ProvisionsTime = GetInt32ConfigValue("ProvisionsTime");
        /// <summary>
        /// 虚拟分站编码
        /// </summary>
        public static readonly string VirtualCode = GetStringConfigValue("VirtualCode");
        
        /// <summary>
        /// 新增、修改病历后，受理信息（包括事件名称）是否相应更新(True:更新；False:不更新)。如果大于等于2份病历，以最后一份病历的新增和修改为准
        /// </summary>
        //public static readonly bool IsUpdatePName = GetBoolConfigValue("IsUpdatePName");
        ///// <summary>
        ///// 是否启用新版病历(True:启用；False:不启用)。
        ///// </summary>
        //public static readonly bool IsPatient = GetBoolConfigValue("IsPatient");
        ///// <summary>
        ///// 是否启用病历直接保存，提交审核时验证必填项(True:启用；False:不启用)。
        ///// </summary>
        //public static readonly bool IsDirectSave = GetBoolConfigValue("IsDirectSave");
        ///// <summary>
        ///// 病历打印的读取路径
        ///// </summary>
        //public static readonly string Patient_RDLC_Path = GetStringConfigValue("Patient_RDLC_Path");

        ///// <summary>
        ///// 是否控制审核后才能打印(True:是; False:否)
        ///// </summary>
        //public static readonly bool IsPrint = GetBoolConfigValue("IsPrint");
        ///// <summary>
        ///// 是否无锡病历校验(True:是; False:否)
        ///// </summary>
        //public static readonly bool IsPatientAudit = GetBoolConfigValue("IsPatientAudit");
        /// <summary>
        /// 缓存时间
        /// </summary>
        public static readonly int CacheTimeSeconds = GetInt32ConfigValue("CacheTimeSeconds");
        ///// <summary>
        ///// 是否超时
        ///// </summary>
        //public static readonly int IsTimeout = GetInt32ConfigValue("IsTimeout");
        ///// <summary>
        ///// 病历管理中任务列表筛选时间
        ///// </summary>
        //public static readonly string SelectTime = GetStringConfigValue("SelectTime");
        ///// <summary>
        ///// 是否根据初步诊断来判断【院前急救费】为危重 (否：根据页面传回的选择内容判定)
        ///// </summary>
        //public static readonly bool IsTentativeDiagnose = GetBoolConfigValue("IsTentativeDiagnose");
        ///// <summary>
        ///// 是否根据人员身份不同 ，显示不同病历标题(True:是; False:否)
        ///// </summary>
        //public static readonly bool IsDoctor = GetBoolConfigValue("IsDoctor");
        ///// <summary>
        ///// 病历新增读取的页面
        ///// </summary>
        //public static readonly string AddPatientPage = GetStringConfigValue("AddPatientPage");
        ///// <summary>
        ///// 病历查看读取的页面
        ///// </summary>
        //public static readonly string LookPatientPage = GetStringConfigValue("LookPatientPage");
        ///// <summary>
        ///// 病历抬头
        ///// </summary>
        //public static readonly string PatientTitle = GetStringConfigValue("PatientTitle");

        ///// <summary>
        ///// 长春病历页面打开连接
        ///// </summary>
        //public static readonly string AddPatientRecordCC = GetStringConfigValue("AddPatientRecordCC");
        ///// <summary>
        ///// 长春药品耗材维护页面打开连接
        ///// </summary>
        //public static readonly string TreeDic = GetStringConfigValue("TreeDic");

        ///// <summary>
        ///// 修改编码
        ///// </summary>
        //public static int ModifyCode = GetInt32ConfigValue("ModifyCode");

        ///// <summary>
        ///// 是否允许医生药箱库存负值
        ///// </summary>
        //public static readonly bool IsAllowStoreNegative = GetBoolConfigValue("IsAllowStoreNegative");

        /// <summary>
        /// 上传文件夹所在路径
        /// </summary> 
        public static readonly string Upload = GetStringConfigValue("Upload");

        /// <summary>
        /// 系统版本号
        /// </summary>
        public static readonly string Version = GetStringConfigValue("Version");

        /// <summary>
        /// 不同网段处理
        /// </summary>
        public static readonly bool NetworkSegment = GetBoolConfigValue("NetworkSegment");


        /// <summary>
        /// CoreServiceUrl
        /// </summary>
        public static readonly string CoreServiceUrl = GetStringConfigValue("CoreServiceUrl");

        /// <summary>
        /// 是否显示待办工作
        /// </summary>
        public static readonly bool IsShowWork = GetBoolConfigValue("IsShowWork");

        /// <summary>
        /// 是否显示未读知会
        /// </summary>
        public static readonly bool IsShowNotify = GetBoolConfigValue("IsShowNotify");

        /// <summary>
        /// 是否显示未读邮件
        /// </summary>
        public static readonly bool IsShowMail = GetBoolConfigValue("IsShowMail");

        /// <summary>
        /// 录音回放页面
        /// </summary>
        public static readonly string RecordingPlayPage = GetStringConfigValue("RecordingPlayPage");

        #endregion

        #region Excel报表服务参数
        /// <summary>
        /// 报表服务IP
        /// </summary>
        public static string ReportServerIP = GetStringConfigValue("ReportServerIP");
        /// <summary>
        /// 报表服务端口
        /// </summary>
        public static int ReportServerPort = GetInt32ConfigValue("ReportServerPort");
        #endregion

        #region 格式化各类型 Config
        /// <summary>
        /// 获取字符串型Config值
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string GetStringConfigValue(string keyName)
        {
            try
            {
                return Config.GetKeyValue(keyName);
            }
            catch
            {
                Log4Net.LogError("GlobalData/GetStringConfigValue()", "Config配置有误,KeyName:" + keyName);
                return "";
            }
        }

        /// <summary>
        /// 获取bool型Config值
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static bool GetBoolConfigValue(string keyName)
        {
            try
            {
                return Convert.ToBoolean(Config.GetKeyValue(keyName));
            }
            catch
            {
                Log4Net.LogError("GlobalData/GetBoolConfigValue()", "Config配置有误,KeyName:" + keyName);
                return false;
            }
        }

        /// <summary>
        /// 获取数字型Config值
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static Int32 GetInt32ConfigValue(string keyName)
        {
            Regex regex = new Regex("^\\d*$");
            string tempStr;
            try
            {
                tempStr = Config.GetKeyValue(keyName);
                if (regex.IsMatch(tempStr))
                {
                    return Convert.ToInt32(tempStr);
                }

                return 0;
            }
            catch
            {
                Log4Net.LogError("GlobalData/GetInt32ConfigValue()", "Config配置有误,KeyName:" + keyName);
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// 返回上传文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string GetUpload()
        {
            if (string.IsNullOrEmpty(Upload)) //是否存在配置路径
            {
                string defautltPath = System.Web.HttpContext.Current.Server.MapPath("/Upload/");

                if (!Directory.Exists(defautltPath))
                {
                    Directory.CreateDirectory(defautltPath);
                }

                return defautltPath;
            }
            else
            {
                if (!Directory.Exists(Upload))
                {
                    Directory.CreateDirectory(Upload);
                }

                return Upload;
            }
        }
    }

    /// <summary>
    /// 日志记录
    /// </summary>
    public class Log4Net
    {
        //private static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Log4Net));
        private static log4net.ILog Log;
        private Log4Net() { }
        static Log4Net()
        {
            Log = log4net.LogManager.GetLogger(typeof(Log4Net));
            string logFileName = AppDomain.CurrentDomain.BaseDirectory + "bin\\log4net.xml";
            //string logFileName = AppDomain.CurrentDomain.BaseDirectory + AppConfig.LogConfigFile;
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(logFileName));
        }

        #region 记录各种类型 log

        /// <summary>
        /// 记录log信息
        /// </summary>
        /// <param name="Info">消息</param>
        public static void LogInfo(string Info)
        {
            Log.Info("[" + Info + "]");
        }

        /// <summary>
        /// 记录错误日志到文件
        /// </summary>
        /// <param name="ErrorPlace">错误出处</param>
        /// <param name="ErrorMsg">错误内容</param>
        public static void LogError(string ErrorPlace, string ErrorMsg)
        {
            Log.Error("[" + ErrorPlace + "]" + ErrorMsg);
        }

        /// <summary>
        /// 记录调试日志到文件
        /// </summary>
        /// <param name="BugPlace">记录出处</param>
        /// <param name="BugMsg">记录内容</param>
        public static void LogBug(string BugPlace, string BugMsg)
        {
            Log.Debug("[" + BugPlace + "]" + BugMsg);
        }

        /// <summary>
        /// 记录警告日志到文件
        /// </summary>
        /// <param name="BugPlace">记录出处</param>
        /// <param name="BugMsg">记录内容</param>
        public static void LogWarn(string WarnPlace, string WarnMsg)
        {
            Log.Warn("[" + WarnPlace + "]" + WarnMsg);
        }

        #endregion
    }
}


