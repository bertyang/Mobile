using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using Anchor.FA.Utility;

namespace Anchor.FA.BLL.BasicInfo
{
    public class Task
    //        : IBLL.ITask看来 接口的方法不能是静态的
    {
        public static string Update(TTask newEntity, string Driver, string Doctor, string Nurse, string Litter, string Salver, string AmbulanceStateTime1, string AmbulanceStateTime2, string AmbulanceStateTime3, string AmbulanceStateTime4, string AmbulanceStateTime5, string AmbulanceStateTime6, string AmbulanceStateTime7
            , ButtonPower p, C_WorkerDetail UserInfo)
        {
            return DAL.BasicInfo.Task.Update(newEntity, Driver, Doctor, Nurse,Salver, Litter, AmbulanceStateTime1, AmbulanceStateTime2, AmbulanceStateTime3, AmbulanceStateTime4, AmbulanceStateTime5, AmbulanceStateTime6, AmbulanceStateTime7
                , p, UserInfo);
        }

        public static C_TaskInfoDetail GetC_TaskInfoDetail(string Code)
        {
            return DAL.BasicInfo.Task.GetC_TaskInfoDetail(Code);
        }

        public static TTask GetTaskInfo(string Code)
        {
            return DAL.BasicInfo.Task.GetTaskInfo(Code);
        }
        public static List<C_AmbulanceStateTimeInfo> GetAmbulanceStateTimeLs(string Code)
        {
            return DAL.BasicInfo.Task.GetAmbulanceStateTimeLs(Code);
        }

        public static List<C_ORGANIZE_TREE> GetTaskAbendReasonTree(string Code)
        {
            List<C_ORGANIZE_TREE> mtmList = (from p in DAL.BasicInfo.Task.LoadTaskAbendReasons()
                                             //where p.上级编码==0
                                             select new C_ORGANIZE_TREE
                                             {
                                                 id = p.编码.ToString(),
                                                 text = p.名称,
                                                 ParentID = p.上级编码.ToString()
                                             }).ToList();
            Tree tr = new Tree();
            return tr.GetUnitTree(mtmList, Code);
        }
        /// <summary>
        /// 根据分站编码（-1全部）获取车辆下拉菜单
        /// </summary>
        /// <param name="stationCode">分站编码（-1全部）</param>
        //public static List<TAmbulance> GetAmbulanceByStationCode(string stationCode)
        //{
        //    if (stationCode == "-1")
        //    {
        //        return Anchor.FA.DAL.BasicInfo.AlarmEvent.LoadAlums();
        //    }
        //    else
        //    {
        //        return Anchor.FA.DAL.BasicInfo.AlarmEvent.LoadAlums().Where(t => t.分站编码 == stationCode).ToList();
        //    }
        //}








    }
}
