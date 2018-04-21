using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;
using System.Data.Linq;
using System.Linq.Expressions;
using Anchor.FA.Utility;
using Anchor.FA.BLL.IBLL;
using Spring.Context;
using Spring.Context.Support;
using System.Collections;

namespace Anchor.FA.DAL.BasicInfo
{
    public class Person
    {
        public static TPerson GetOnePerson(string personid)
        {
            TPerson personInfo = new TPerson();
            try
            {
                using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
                {
                    return dbContext.TPerson.First(p => p.编码 == personid);
                }
            }
            catch (Exception ex)
            {
                Log4Net.LogError("PersonDAL/GetOnePerson()", ex.ToString());
                personInfo = null;
            }
            return personInfo;
        }

        /// <summary>
        /// 根据人员类型(-1全部)和分站编码(-1或""或"--请选择--"时取全部)和是否有效(-1全部、0无效、1有效)获取人员编码、姓名列表
        /// </summary>
        /// <param name="personType">人员类型(-1全部)</param>
        /// <param name="stationCode">分站编码(-1或""或"--请选择--"时取全部)</param>
        /// <param name="isValid">是否有效(-1全部、0无效、1有效)</param>
        /// <returns></returns>
        public static object GetPersonList(int personType, string stationCode, int isValid,ButtonPower p, C_WorkerDetail userDetail)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                bool BisValid = Convert.ToBoolean(isValid);

                switch (p.GetGroupRangePower("searchBound"))
                {
                    case "SearchAll"://查找所属分中心
                        return (from t in dbContext.TPerson
                                where (personType == -1 ? true : t.类型编码 == personType)
                             // && (stationCode == "-1" || stationCode == "" || stationCode == "--请选择--" || t.分站编码 == stationCode)
                             && (isValid == -1 ? true : t.是否有效 == BisValid)
                                select new C_Worker
                                {
                                    EmpNo = t.编码,
                                    Name = t.姓名 + "(" + t.工号 + ")",
                                }).ToList();
                    case "SearchCenter"://查找所属分中心
                        int CenterCode = (from t in dbContext.TStation
                                          where t.编码 == stationCode
                                          select t.中心编码).First();
                        return (from t in dbContext.TPerson
                                join s in dbContext.TStation on t.分站编码 equals s.编码
                                where (personType == -1 ? true : t.类型编码 == personType)
                             // && (stationCode == "-1" || stationCode == "" || stationCode == "--请选择--" || t.分站编码 == stationCode)
                              && (isValid == -1 ? true : t.是否有效 == BisValid)
                              && s.中心编码 == CenterCode//userDetail.CenterCode
                                select new C_Worker
                                {
                                    EmpNo = t.编码,
                                    Name = t.姓名 + "(" + t.工号 + ")",
                                }).ToList();
                    case "SearchOrganization"://查找分站
                        return (from t in dbContext.TPerson
                                where (personType == -1 ? true : t.类型编码 == personType)
                                && t.分站编码 == stationCode
                              //&& (stationCode == "-1" || stationCode == "" || stationCode == "--请选择--" || t.分站编码 == stationCode)
                              && (isValid == -1 ? true : t.是否有效 == BisValid)
                              //&& userDetail.Sta.Contains(t.分站编码)
                                select new C_Worker
                                {
                                    EmpNo = t.编码,
                                    Name = t.姓名 + "(" + t.工号 + ")",
                                }).ToList();

                    default://没有设置查询权限
                        return null;
                }
            }
        } 
       
        /// <summary>
        /// 根据人员类型(-1全部)和分站编码(-1或""或"--请选择--"时取全部)和是否有效(-1全部、0无效、1有效)获取人员编码、姓名列表
        /// </summary>
        /// <param name="personType">人员类型(-1全部)</param>
        /// <param name="stationCode">分站编码(-1或""或"--请选择--"时取全部)</param>
        /// <param name="isValid">是否有效</param>
        /// <returns></returns>
        public static
            //IEnumerable
            List<TPerson> 
            GetPersons(int[] personType, string[] stationCode, string ambCode, string branchID, bool? isValid)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                //SqlParameterTool sqllistAc = new SqlParameterTool();
//                sqllistAc.commandText.Append(@"
//and exists(select * from TAcceptEvent tac where tae.事件编码=tac.事件编码");

                var Templist = from tp in dbContext.TPerson
                               select tp;
                               //join ts in dbContext.TStation on tp.分站编码 equals ts.编码 into ts_join
                               //from ts in ts_join.DefaultIfEmpty()

                               //join tr in dbContext.TRole on tp.类型编码 equals tr.编码 into tr_join
                               //from tr in tr_join.DefaultIfEmpty()

                               //join tzb in dbContext.TZBranch on tp.单位编码 equals Convert.ToString(tzb.编码) into tzb_join
                               //from tzb in tzb_join.DefaultIfEmpty()

                               //select new TPerson
                               //{
                               //    编码=tp.编码,
                               //    姓名=tp.姓名,
                               //    工号=tp.工号,
                               //    分站编码=tp.分站编码,
                               //    通话号码=tp.通话号码,
                               //    短信号码=tp.短信号码,
                               //    类型编码=tp.类型编码,
                               //    绑定车辆编码=tp.绑定车辆编码,
                               //    登录台号=tp.登录台号,
                               //    上次操作时间=tp.上次操作时间,
                               //    顺序号=tp.顺序号,
                               //    是否有效=tp.是否有效,
                               //    单位编码=tp.单位编码,
                               //    人员类型 = tr.名称,
                               //    所属分站 = ts.名称,
                               //    部门名称 = tzb.名称
                               //};
                if (isValid.HasValue)
                {
                    Templist = Templist.Where(p => p.是否有效 == isValid.Value);
                }

                if (personType.Length>0)
                {
                    Templist = Templist.Where(p =>personType.Contains(p.类型编码));
                }
                if (stationCode.Length > 0)
                {
                    Templist = Templist.Where(p => stationCode.Contains(p.分站编码));
                }

                if (!string.IsNullOrEmpty(ambCode))
                {
                    Templist = Templist.Where(p => p.绑定车辆编码 == ambCode);
                }
                else
                {
                    Templist = Templist.Where(p => p.绑定车辆编码 == null);
                }

                if (!string.IsNullOrEmpty(branchID))
                {
                    Templist = Templist.Where(p => p.单位编码 == branchID);
                }
                Templist = Templist.OrderBy(p => p.顺序号);
                //Templist = Templist.Select(p => Tuple.Create();

                //return Templist.AsEnumerable().Select(new );

                return Templist.ToList();
                //return dbContext.ExecuteQuery<TPerson>(Templist);
                //using (dbContext.Connection)
                //{
                //    return dbContext.Translate<TPerson>(Templist);
                //}
            }
        }
    }
}
