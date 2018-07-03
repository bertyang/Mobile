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

namespace Anchor.FA.DAL.BasicInfo
{
    public class Ambulance
    {
        public static IApplicationContext ctx = ContextRegistry.GetContext();

        /// <summary>
        /// 获取所有电话信息
        /// </summary>
        public static object LoadAllAmbulanceByPage(int page, int rows, string order, string sort)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from p in dbContext.TAmbulance
                            join o in dbContext.TZAmbulanceLevel on p.车辆等级编码 equals o.编码
                            join o1 in dbContext.TZAmbulanceState on p.工作状态编码 equals o1.编码
                            join o2 in dbContext.TZAmbulanceType on p.车辆类型编码 equals o2.编码
                            join o3 in dbContext.TStation on p.分站编码 equals o3.编码
                            select new
                            {
                                ID = p.车辆编码,
                                Station = o3.名称,
                                CardNum = p.车牌号码,
                                RealCode = p.实际标识,
                                State = o1.名称,
                                Type = o2.名称,
                                Level = o.名称,
                                Tel=p.随车电话,
                            });
                long total = list.LongCount();
                list = list.OrderBy(p => p.ID);
                list = list.Skip((page - 1) * rows).Take(rows);

                var result = new { total = total, rows = list.ToList() };

                return result;
            }

        }

        #region 绑定字典表



        /// <summary>
        /// 车辆状态相关信息
        /// </summary>
        /// <returns></returns>
        public static List<TZAmbulanceState> LoadAmbulanceStateInfo()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                List<TZAmbulanceState> l = dbContext.TZAmbulanceState.Where(t => t.是否有效 == true).OrderBy(t => t.顺序号).ToList();
                return l;
            }
        }

        public static List<TStation> LoadAllStations(Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail UserInfo)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = dbContext.TStation.Where(s => s.是否有效 == true).OrderBy(s => s.顺序号).ToList();

                string Search = bp.GetGroupRangePower("searchBound");

                switch (bp.GetGroupRangePower("searchBound"))
                {
                    case "SearchAll":
                        break;
                    case "SearchCenter"://查找所属分中心
                        list = list.Where(p => UserInfo.CenterCode == p.中心编码).ToList();
                        break;
                    case "SearchOrganization"://查找所属分站
                        list = list.Where(p => UserInfo.Sta.Contains(p.编码)).ToList();
                        break;
                    default://没有设置查询权限
                        return null;
                    //break;
                }

                return list;
            }
        }

        public static List<TStation> LoadAllStations()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = dbContext.TStation.Where(s => s.是否有效 == true).OrderBy(s => s.顺序号).ToList();

                return list;
            }
        }
        public static List<TZAmbulanceLevel> LoadAllLevels()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = dbContext.TZAmbulanceLevel.ToList();
                return list;
            }
        }
        public static List<TZAmbulanceType> LoadAllTypes()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = dbContext.TZAmbulanceType.ToList();
                return list;
            }
        }

        public static List<TZAmbulanceGroup> LoadAllGroups()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = dbContext.TZAmbulanceGroup.ToList();
                return list;
            }
        }

        #endregion

        #region 修改/编辑

        public static object Edit(string id)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                TAmbulance entity = null;
                if (id != null)
                {
                    entity = dbContext.TAmbulance.FirstOrDefault(t => t.车辆编码 == id);
                }
                entity = entity ?? new TAmbulance
                {
                    车辆编码 = "0",
                    实际标识 = "",
                    分站编码 = "001",
                    车辆类型编码 = 101,
                    车辆等级编码 = 101,
                    随车电话 = "13000000000",
                };
                return entity;
            }
        }
        #endregion

        #region 保存

        public static bool Save(TAmbulance entity)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                if (entity.车辆编码 == "0")  //添加
                {
                    entity.车辆编码 = PrimaryKeyCreater.getIntPrimaryKey("TAmbulance").ToString("00000");
                    entity.分站编码 = entity.分站编码.PadLeft(3, '0');
                    entity.命令单发送去向编码 = 5;
                    entity.任务编码 = "";
                    entity.任务流水号 = 0;
                    entity.用户流水号 = "";
                    entity.工作状态编码 = 8;
                    entity.当班执行任务数 = 0;
                    entity.司机 = "";
                    entity.医生 = "";
                    entity.护士 = "";
                    entity.担架工 = "";
                    entity.抢救员 = "";
                    entity.是否在线 = false;
                    entity.是否有效 = true;
                    entity.顺序号 = 999;

                    dbContext.TAmbulance.InsertOnSubmit(entity);
                    dbContext.SubmitChanges();
                    return true;

                }
                else  //修改
                {
                    var model = dbContext.TAmbulance.FirstOrDefault(t => t.车辆编码 == entity.车辆编码);
                    model.分站编码 = entity.分站编码 == "0" ? "000" : entity.分站编码;
                    model.随车电话 = entity.随车电话;
                    model.实际标识 = entity.实际标识;
                    model.车牌号码 = entity.车牌号码;
                    model.车辆等级编码 = entity.车辆等级编码;
                    model.车辆类型编码 = entity.车辆类型编码;
                    dbContext.SubmitChanges();
                    return true;
                }
            }
        }

        #endregion

        #region 删除

        public static bool Delete(IList<string> idList)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                foreach (string g in idList)
                {
                    var model = dbContext.TAmbulance.Single(t => t.车辆编码 == g);
                    //dbContext.TAmbulance.Load();
                    dbContext.TAmbulance.DeleteOnSubmit(model);
                }
                dbContext.SubmitChanges();
                return true;
            }

        }
        #endregion

        #region 查询

        public static object AmbulanceListSearch(string RealCode, string AmbNum, string Station, string AmbType, string AmbGroup
            , int page, int rows, string order, string sort, bool? IsActive, Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail UserInfo)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from p in dbContext.TAmbulance

                            join o in dbContext.TZAmbulanceLevel on p.车辆等级编码 equals o.编码 into o_join
                            from o in o_join.DefaultIfEmpty()

                            join o1 in dbContext.TZAmbulanceState on p.工作状态编码 equals o1.编码 into o1_join
                            from o1 in o1_join.DefaultIfEmpty()

                            join o2 in dbContext.TZAmbulanceType on p.车辆类型编码 equals o2.编码 into o2_join
                            from o2 in o2_join.DefaultIfEmpty()

                            join o3 in dbContext.TStation on p.分站编码 equals o3.编码 into o3_join
                            from o3 in o3_join.DefaultIfEmpty()

                            join o4 in dbContext.TTask on p.任务编码 equals o4.任务编码 into o4_join
                            from o4 in o4_join.DefaultIfEmpty()

                            select new
                            {
                                ID = p.车辆编码,
                                StationCode = p.分站编码,
                                CenterCode= o3.中心编码,
                                AmbTypeCode = p.车辆类型编码,
                                CardNum = p.车牌号码,
                                RealSign = p.实际标识,
                                CommandAspectId = p.命令单发送去向编码,

                                TaskCode = p.任务编码,
                                UserOrder = p.用户流水号,
                                WorkStateId = p.工作状态编码,
                                OndutyTaskCount = p.当班执行任务数,
                                Driver = p.司机,
                                Doctor = p.医生,
                                Nurse = p.护士,
                                Litter = p.担架工,
                                Salver = p.抢救员,
                                KeyPressTime = p.按键时刻,
                                FollowTel = p.随车电话,
                                GroupNumber = p.分组编码,
                                LevelId = p.车辆等级编码,
                                IsOnline = p.是否在线,
                                IsActive = p.是否有效,

                                //Station = o3 == null ? null : o3.名称,
                                //Type = o2 == null ? null : o2.名称,
                                //Level = o == null ? null : o.名称,
                                //State = o1 == null ? null : o1.名称,
                                //Color = o1 == null ? (Nullable<int>)null : o1.颜色,
                                Station = o3.名称,
                                Type = o2.名称,
                                Level = o.名称,
                                State = o1.名称,
                                Color = (Nullable<int>)o1.颜色,
                                Tel = p.随车电话,
                                AlarmEventCode = o4.事件编码,
                            });
                if (!string.IsNullOrEmpty(RealCode))//实际标识
                {
                    list = list.Where(p => p.RealSign.Contains(RealCode));
                }
                if (!string.IsNullOrEmpty(AmbNum))//车牌号码
                {
                    list = list.Where(p => p.CardNum.Contains(AmbNum));
                }
                if (!string.IsNullOrEmpty(AmbType))//车辆类型编码
                {
                    list = list.Where(p => p.AmbTypeCode == Convert.ToInt32(AmbType));
                }
                if (!string.IsNullOrEmpty(AmbGroup))//分组编码
                {
                    list = list.Where(p => p.GroupNumber == Convert.ToInt32(AmbGroup));
                }

                if (!string.IsNullOrEmpty(Station))//分站编码
                {
                    list = list.Where(p => p.StationCode == Station);
                }
                if (IsActive.HasValue)//是否有效
                {
                    list = list.Where(p => p.IsActive == IsActive);
                }
                string Search = bp.GetGroupRangePower("searchBound");

                switch (bp.GetGroupRangePower("searchBound"))
                {
                    case "SearchAll":
                        break;
                    case "SearchCenter"://查找分站
                        list = list.Where(p => UserInfo.CenterCode==p.CenterCode);
                        break;
                    case "SearchOrganization"://查找分站
                        list = list.Where(p => UserInfo.Sta.Contains(p.StationCode));
                        break;
                    case "SearchMe"://查询车辆本人
                        list = list.Where(p => UserInfo.W.Name.Contains(p.Driver)
                            || UserInfo.W.Name.Contains(p.Doctor)
                            || UserInfo.W.Name.Contains(p.Nurse)
                            || UserInfo.W.Name.Contains(p.Litter)
                            || UserInfo.W.Name.Contains(p.Salver)
                            );
                        break;
                    default://没有设置查询权限
                        return null;
                    //break;
                }



                long total = list.LongCount();
                list = list.Skip((page - 1) * rows).Take(rows);

                var result = new { total = total, rows = list.ToList() };

                return result;
            }
        }
        public static object AmbulanceSearch(string RealCode, string AmbNum, string Station, string AmbType, string AmbGroup
            , int page, int rows, string order, string sort, bool? IsActive)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {                
                var list = (from p in dbContext.TAmbulance

                            join o in dbContext.TZAmbulanceLevel on p.车辆等级编码 equals o.编码 into o_join
                            from o in o_join.DefaultIfEmpty()

                            join o1 in dbContext.TZAmbulanceState on p.工作状态编码 equals o1.编码 into o1_join
                            from o1 in o1_join.DefaultIfEmpty()

                            join o2 in dbContext.TZAmbulanceType on p.车辆类型编码 equals o2.编码 into o2_join
                            from o2 in o2_join.DefaultIfEmpty()

                            join o3 in dbContext.TStation on p.分站编码 equals o3.编码 into o3_join
                            from o3 in o3_join.DefaultIfEmpty()

                            select new
                            {
                                ID = p.车辆编码,
                                StationCode=p.分站编码,
                                AmbTypeCode = p.车辆类型编码,
                                CardNum = p.车牌号码,
                                RealSign = p.实际标识,
                                CommandAspectId = p.命令单发送去向编码,

                                TaskCode = p.任务编码,
                                UserOrder=p.用户流水号,
                                WorkStateId=p.工作状态编码,
                                OndutyTaskCount=p.当班执行任务数,
                                Driver=p.司机,
                                Doctor=p.医生,
                                Nurse=p.护士,
                                Litter=p.担架工,
                                Salver=p.抢救员,
                                KeyPressTime=p.按键时刻,
                                FollowTel=p.随车电话,
                                GroupNumber=p.分组编码,
                                LevelId=p.车辆等级编码,
                                IsOnline=p.是否在线,
                                IsActive=p.是否有效,

                                Station = o3.名称,
                                Type = o2.名称,
                                Level = o.名称,
                                State = o1.名称,
                                Color=o1.颜色,
                                Tel = p.随车电话,
                            });
                if (!string.IsNullOrEmpty(RealCode))
                {
                    list = list.Where(p => p.RealSign.Contains(RealCode));
                }
                if (!string.IsNullOrEmpty(AmbNum))
                {
                    list = list.Where(p => p.CardNum.Contains(AmbNum));
                }
                if (!string.IsNullOrEmpty(AmbType))
                {
                    list = list.Where(p => p.AmbTypeCode == Convert.ToInt32(AmbType));
                }
                if (!string.IsNullOrEmpty(AmbGroup))
                {
                    list = list.Where(p => p.GroupNumber == Convert.ToInt32(AmbGroup));
                }

                if (!string.IsNullOrEmpty(Station))
                {
                    list = list.Where(p => p.StationCode == Station);
                }
                if (IsActive.HasValue)
                {
                    list = list.Where(p => p.IsActive == IsActive);
                }

                long total = list.LongCount();
                //list = list.OrderBy(p => p.ID);
                list = list.Take(rows);

                var result = new { total = total, rows = list.ToList() };

                return result;               
            }
        }

        /// <summary>
        /// 车辆查询
        /// </summary>
        /// <returns></returns>
        public static TAmbulance GetAmbulanceInfo(string Code)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TAmbulance.First(a => a.车辆编码 == Code);
            }
        }
        /// <summary>
        /// 车辆查询
        /// </summary>
        /// <returns></returns>
        public static object GetAmbulance(string Code)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from TA in dbContext.TAmbulance
                            join t in dbContext.TStation on TA.分站编码 equals t.编码 into Leftjoin1
                            from TS in Leftjoin1.DefaultIfEmpty()
                            join t in dbContext.TZAmbulanceType on TA.车辆类型编码 equals t.编码 into Leftjoin2
                            from TZT in Leftjoin2.DefaultIfEmpty()
                            join t in dbContext.TZAmbulanceLevel on TA.车辆等级编码 equals t.编码 into Leftjoin3
                            from TZL in Leftjoin3.DefaultIfEmpty()
                            join t in dbContext.TZAmbulanceState on TA.工作状态编码 equals t.编码 into Leftjoin4
                            from TZS in Leftjoin4.DefaultIfEmpty()
                            join t in dbContext.TZCommandAspect on TA.命令单发送去向编码 equals t.编码 into Leftjoin5
                            from TZC in Leftjoin5.DefaultIfEmpty()
                            join t in dbContext.TZAmbulanceGroup on TA.分组编码 equals t.编码 into Leftjoin6
                            from TZG in Leftjoin6.DefaultIfEmpty()
                            where TA.是否有效 == true && TA.车辆编码 == Code
                            select new
                            {
                                TA.车辆编码,
                                TA.车辆类型编码,
                                TA.车牌号码,
                                TA.实际标识
                                ,
                                TA.用户流水号,
                                TA.当班执行任务数,
                                TA.司机,
                                TA.医生,
                                TA.护士,
                                TA.担架工,
                                TA.抢救员,
                                TA.随车电话
                                ,
                                //TA.分站编码,
                                //所属分站 = TS == null ? null : TS.名称
                                //,
                                //车辆类型 = TZT == null ? null : TZT.名称
                                //,
                                //TA.车辆等级编码,
                                //车辆等级 = TZL == null ? null : TZL.名称
                                //,
                                //TA.工作状态编码,
                                //工作状态 = TZS == null ? null : TZS.名称
                                //,
                                //TA.命令单发送去向编码,
                                //命令单发送去向 = TZC == null ? null : TZC.名称
                                //,
                                //TA.分组编码,
                                //分组 = TZG == null ? null : TZG.名称
                                TA.分站编码,
                                所属分站 =  TS.名称
                                ,
                                车辆类型 =  TZT.名称
                                ,
                                TA.车辆等级编码,
                                车辆等级 =  TZL.名称
                                ,
                                TA.工作状态编码,
                                工作状态 = TZS.名称
                                ,
                                TA.命令单发送去向编码,
                                命令单发送去向 =  TZC.名称
                                ,
                                TA.分组编码,
                                分组 =  TZG.名称
                                ,
                                TA.任务编码
                            });

                return list.First();
            }
        }
        #endregion

        public static object BindNotWorkPerson(int[] personTypes, string[] stationCode)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var result = from tp in Person.GetPersons(personTypes, stationCode, null, null, true)

                             join tr in dbContext.TRole on tp.类型编码 equals tr.编码 into tr_join
                             from tr in tr_join.DefaultIfEmpty()

                             orderby tp.类型编码, tp.姓名
                             select new
                             {
                                 编码 = tp.编码,
                                 姓名 = tp.姓名,
                                 工号 = tp.工号,
                                 分站编码 = tp.分站编码,
                                 通话号码 = tp.通话号码,
                                 短信号码 = tp.短信号码,
                                 类型编码 = tp.类型编码,
                                 绑定车辆编码 = tp.绑定车辆编码,
                                 登录台号 = tp.登录台号,
                                 上次操作时间 = tp.上次操作时间,
                                 顺序号 = tp.顺序号,
                                 是否有效 = tp.是否有效,
                                 单位编码 = tp.单位编码,
                                 人员类型 = tr.名称
                             };
                return result.ToList();
            }
        }

        public static object BindWorkPerson(int[] personTypes,string AmbCode)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var result = from tp in Person.GetPersons(personTypes, new string[] { }, AmbCode, null, true)

                             join tr in dbContext.TRole on tp.类型编码 equals tr.编码 into tr_join
                             from tr in tr_join.DefaultIfEmpty()

                             orderby tp.类型编码, tp.姓名
                             select new
                             {
                                 编码 = tp.编码,
                                 姓名 = tp.姓名,
                                 工号 = tp.工号,
                                 分站编码 = tp.分站编码,
                                 通话号码 = tp.通话号码,
                                 短信号码 = tp.短信号码,
                                 类型编码 = tp.类型编码,
                                 绑定车辆编码 = tp.绑定车辆编码,
                                 登录台号 = tp.登录台号,
                                 上次操作时间 = tp.上次操作时间,
                                 顺序号 = tp.顺序号,
                                 是否有效 = tp.是否有效,
                                 单位编码 = tp.单位编码,
                                 人员类型 = tr.名称
                             };
                return result.ToList();
            }
        }
    }
}
