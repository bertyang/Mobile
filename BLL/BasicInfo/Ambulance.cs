using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using Anchor.FA.Utility;


namespace Anchor.FA.BLL.BasicInfo
{
    public class Ambulance
    {
        /// <summary>
        /// 车辆人员上班
        /// </summary>
        public string AmbulancePersonCheckIn(string personCode, string ambCode, int operationOrigin, string operatorCode, DateTime operateTime)
        {
            try
            {
                CoreService.AmbulancePersonCheckIn(personCode, ambCode, operationOrigin, operatorCode, operateTime);
                return "";
            }
            catch (Exception ex)
            {
                Log4Net.LogError("AmbulanceBLL/UnlockPerson", ex.Message);
                return ex.Message;
            }
        }
        /// <summary>
        /// 车辆人员下班
        /// </summary>
        public string AmbulancePersonCheckOut(string personCode, string ambCode, int operationOrigin, string operatorCode, DateTime operateTime)
        {
            try
            {
                CoreService.AmbulancePersonCheckOut(personCode, ambCode, operationOrigin, operatorCode, operateTime);
                return "";
            }
            catch (Exception ex)
            {
                Log4Net.LogError("AmbulanceBLL/UnlockPerson", ex.Message);
                return ex.Message;
            }
        }

        public string ModifyState(string ambCode, int ambStateCode, DateTime operateTime, string operatorCode)
        {
            try
            {
                object item = DAL.BasicInfo.Ambulance.GetAmbulance(ambCode);
                Type itemType = item.GetType();

                string taskCode = itemType.GetProperty("任务编码").GetValue(item, null).ToString();
                CoreService.ModifyState(ambCode, ambStateCode,
                             operateTime, 5, operatorCode, taskCode);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public object LoadAllAmbulanceByPage(int page, int rows, string order, string sort)
        {
            return DAL.BasicInfo.Ambulance.LoadAllAmbulanceByPage(page, rows, order, sort);
        }

        public object LoadAllAmbulance()
        {
            return DataAccess<TAmbulance>.ToList();
        }

        public List<TZAmbulanceState> LoadAmbulanceStateInfo()
        {
            return DAL.BasicInfo.Ambulance.LoadAmbulanceStateInfo();
        }
        public List<TStation> LoadAllStations(Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail UserInfo)
        {
            return DAL.BasicInfo.Ambulance.LoadAllStations(bp, UserInfo);
        }

        public List<TStation> LoadAllStations()
        {
            return DAL.BasicInfo.Ambulance.LoadAllStations();
        }
        public List<TZAmbulanceLevel> LoadAllLevels()
        {
            return DAL.BasicInfo.Ambulance.LoadAllLevels();
        }
        public List<TZAmbulanceType> LoadAllTypes()
        {
            return DAL.BasicInfo.Ambulance.LoadAllTypes();
        }
        public List<TZAmbulanceGroup> LoadAllGroups()
        {
            return DAL.BasicInfo.Ambulance.LoadAllGroups();
        }
        public object Edit(string id)
        {
            return DAL.BasicInfo.Ambulance.Edit(id);
        }
        public bool Save(TAmbulance entity)
        {
            return DAL.BasicInfo.Ambulance.Save(entity);
        }
        public bool Delete(IList<string> idList)
        {
            return DAL.BasicInfo.Ambulance.Delete(idList);
        }


        public object AmbulanceListSearch(string RealCode, string AmbNum, string Station, string AmbType, string AmbGroup
      , int page, int rows, string order, string sort, bool? IsActive, Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail UserInfo)
        {
            return DAL.BasicInfo.Ambulance.AmbulanceListSearch(RealCode, AmbNum, Station, AmbType, AmbGroup
      , page, rows, order, sort, IsActive, bp, UserInfo);
        }
        public object AmbulanceSearch(string RealCode, string AmbNum, string Station, string AmbType, string AmbGroup
            , int page, int rows, string order, string sort,bool? IsActive)
        {
            return DAL.BasicInfo.Ambulance.AmbulanceSearch( RealCode,  AmbNum,  Station,  AmbType,  AmbGroup
            ,  page,  rows,  order,  sort, IsActive);
        }

        public static TAmbulance GetAmbulanceInfo(string Code)
        {
            return DAL.BasicInfo.Ambulance.GetAmbulanceInfo(Code);
        }

        public object GetAmbulance(string Code)
        {
            return DAL.BasicInfo.Ambulance.GetAmbulance(Code);
        }

        /// <summary>
        /// 绑定未上班人员
        /// </summary>
        public object BindNotWorkPerson(int[] personTypes, string[] stationCode)
        {
            return DAL.BasicInfo.Ambulance.BindNotWorkPerson(personTypes, stationCode);
        }

        /// <summary>
        /// 绑定上班人员
        /// </summary>
        public object BindWorkPerson(int[] personTypes, string AmbCode)
        {
            return DAL.BasicInfo.Ambulance.BindWorkPerson(personTypes, AmbCode);
        }

    }
}
