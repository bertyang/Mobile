using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.BasicInfo
{
    public class StatisticsBLL
    {
        #region 打印事件详情
        /// <summary>
        /// 事件信息
        /// </summary>
        /// <param name="almCode"></param>
        /// <returns></returns>
        public static List<AttemperAlarmInfo> GetAlarmInfo(string almCode)
        {
            return Anchor.FA.DAL.BasicInfo.StatisticsDAL.GetAlarmInfo(almCode);
        }
        /// <summary>
        /// 受理信息
        /// </summary>
        /// <param name="almCode"></param>
        /// <returns></returns>
        public static List<AttemperAcceptInfo> GetAcceptInfo(string almCode)
        {
            return Anchor.FA.DAL.BasicInfo.StatisticsDAL.GetAcceptInfo(almCode);
        }
        /// <summary>
        /// 出车信息
        /// </summary>
        /// <param name="almCode"></param>
        /// <returns></returns>
        public static List<AttemperTaskInfo> GetTaskInfo(string almCode)
        {
            return Anchor.FA.DAL.BasicInfo.StatisticsDAL.GetTaskInfo(almCode);
        }
        /// <summary>
        /// 电话信息
        /// </summary>
        /// <param name="almCode"></param>
        /// <returns></returns>
        public static List<AttemperTelInfo> GetTelInfo(string almCode)
        { return Anchor.FA.DAL.BasicInfo.StatisticsDAL.GetTelInfo(almCode); }
        #endregion
        #region 打印命令单
        /// <summary>
        /// 打印命令单
        /// </summary>
        /// <param name="taskCode"></param>
        /// <returns></returns>
        public static StationCommandInfo PrintCommand(string taskCode)
        { return Anchor.FA.DAL.BasicInfo.StatisticsDAL.PrintCommand(taskCode); }
        #endregion

    }
}
