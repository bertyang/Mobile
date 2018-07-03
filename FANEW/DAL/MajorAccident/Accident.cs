using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using Anchor.FA.Utility;
using System.Configuration;

namespace Anchor.FA.DAL.MajorAccident
{
    public class Accident
    {
        #region 查询
        /// <summary>
        /// 获取所有满足条件的重大事故
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="order">排序顺序</param>
        /// <param name="sort">排序字段</param>
        /// <param name="startTime">起始时刻</param>
        /// <param name="endTime">中止时刻</param>
        /// <param name="accidentName">事故名称</param>
        /// <param name="place">区域</param>
        /// <param name="type">事故类型</param>
        /// <param name="level">事故等级</param>
        /// <returns></returns>
        public static object GetAccident(int page, int rows, string order, string sort,
            DateTime startTime, DateTime endTime,
            string accidentName, string place, int? type, int? level,
            Anchor.FA.Utility.ButtonPower b, C_WorkerDetail userDetail)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                //高级版病例
                if (AppConfig.GetStringConfigValue("PatientRecordTable") == "M_PatientRecord")
                {
                    var list = from tae in dbContext.TAlarmEvent
                               join tzt in dbContext.TZAccidentType on tae.事故类型编码 equals tzt.编码 into left_tzt
                               from tzt in left_tzt.DefaultIfEmpty()
                               join tzl in dbContext.TZAccidentLevel on tae.事故等级编码 equals tzl.编码 into left_tzl
                               from tzl in left_tzl.DefaultIfEmpty()
                               join tp in dbContext.TPerson on tae.首次调度员编码 equals tp.编码 into left_tp
                               from tp in left_tp.DefaultIfEmpty()
                               where tae.首次受理时刻 >= startTime && tae.首次受理时刻 <= endTime
                                     && tae.事件编码 == tae.所属事故编码 && tae.事故类型编码 != -1
                               select new
                               {
                                   关联事件数 = (from t in dbContext.TAlarmEvent where t.所属事故编码 == tae.事件编码 select t).Count(),
                                   事故编码 = tae.事件编码,
                                   事故名称 = tae.事件名称,
                                   调度员 = tp.姓名,
                                   事发时间 = tae.首次受理时刻,
                                   事发区域 = tae.区域,
                                   事故类型 = tzt.名称 == null ? "未找到" : tzt.名称,
                                   事故等级 = tzl.名称 == null ? "未找到" : tzl.名称,
                                   伤亡总人数一 = (from p in dbContext.TAccidentPatient
                                             where (from t in dbContext.TAlarmEvent where t.所属事故编码 == tae.事件编码 select t.事件编码).Contains(p.事故编码)
                                             select p).Count(),
                                   伤亡总人数二 = (from tpr in dbContext.M_PatientRecord
                                             where (from t in dbContext.TAlarmEvent where t.所属事故编码 == tae.事件编码 select t.事件编码).Contains(tpr.TaskCode.Substring(1, 16))
                                             select tpr).Count(),
                                   伤亡总人数三 = (from tar in dbContext.TAccidentReport
                                             where (from t in dbContext.TAlarmEvent where t.所属事故编码 == tae.事件编码 select t.事件编码).Contains(tar.事故编码)
                                             select tar.受伤人数).Count()
                                                   + (from tar in dbContext.TAccidentReport
                                                      where (from t in dbContext.TAlarmEvent where t.所属事故编码 == tae.事件编码 select t.事件编码).Contains(tar.事故编码)
                                                      select tar.死亡人数).Count(),
                                   事故类型编码 = tae.事故类型编码,
                                   事故等级编码 = tae.事故等级编码,
                                   中心编码 = tae.中心编码
                               };
                    //动态Where
                    if (!string.IsNullOrEmpty(accidentName))
                    {
                        list = list.Where(t => t.事故名称.Contains(accidentName));
                    }
                    if (!string.IsNullOrEmpty(place) && place != "--请选择--")
                    {
                        list = list.Where(t => t.事发区域.Contains(place));
                    }

                    if (type != null)
                    {
                        list = list.Where(t => t.事故类型编码 == type);
                    }

                    if (level != null)
                    {
                        list = list.Where(t => t.事故等级编码 == level);
                    }

                    switch (b.GetGroupRangePower("searchBound"))
                    {
                        case "SearchAll"://查找所有
                            break;
                        case "SearchCenter"://查找所属分中心
                            list = list.Where(t => t.中心编码 == userDetail.CenterCode);
                            break;
                        default://没有设置查询权限
                            return null;
                    }

                    long total = list.LongCount();
                    list = list.OrderByDescending(t => t.事发时间);
                    list = list.Skip((page - 1) * rows).Take(rows);

                    if (Config.GetKeyValue("WoundedMode") == "0")
                    {
                        var listAccident = list.ToList().Select(o => new
                        {
                            关联事件数 = o.关联事件数,
                            事故编码 = o.事故编码,
                            关联 = o.事故编码,
                            打印 = o.事故编码,
                            事故名称 = o.事故名称,
                            调度员 = o.调度员,
                            事发时间 = o.事发时间.ToString("yyyy-MM-dd HH:mm:ss"),
                            事发区域 = o.事发区域,
                            事故类型 = o.事故类型,
                            事故等级 = o.事故等级,
                            伤亡总人数 = o.伤亡总人数一,
                        });
                        var result = new { total = total, rows = listAccident.ToList() };
                        return result;
                    }
                    else if (Config.GetKeyValue("WoundedMode") == "1")
                    {
                        var listAccident = list.ToList().Select(o => new
                        {
                            关联事件数 = o.关联事件数,
                            事故编码 = o.事故编码,
                            关联 = o.事故编码,
                            打印 = o.事故编码,
                            事故名称 = o.事故名称,
                            调度员 = o.调度员,
                            事发时间 = o.事发时间.ToString("yyyy-MM-dd HH:mm:ss"),
                            事发区域 = o.事发区域,
                            事故类型 = o.事故类型,
                            事故等级 = o.事故等级,
                            伤亡总人数 = o.伤亡总人数二,
                        });
                        var result = new { total = total, rows = listAccident.ToList() };
                        return result;
                    }
                    else if (Config.GetKeyValue("WoundedMode") == "2")
                    {
                        var listAccident = list.ToList().Select(o => new
                        {
                            关联事件数 = o.关联事件数,
                            事故编码 = o.事故编码,
                            关联 = o.事故编码,
                            打印 = o.事故编码,
                            事故名称 = o.事故名称,
                            调度员 = o.调度员,
                            事发时间 = o.事发时间.ToString("yyyy-MM-dd HH:mm:ss"),
                            事发区域 = o.事发区域,
                            事故类型 = o.事故类型,
                            事故等级 = o.事故等级,
                            伤亡总人数 = o.伤亡总人数三,
                        });
                        var result = new { total = total, rows = listAccident.ToList() };
                        return result;
                    }
                    return null;
                }
                else //标准版病例
                {
                    var list = from tae in dbContext.TAlarmEvent
                               join tzt in dbContext.TZAccidentType on tae.事故类型编码 equals tzt.编码 into left_tzt
                               from tzt in left_tzt.DefaultIfEmpty()
                               join tzl in dbContext.TZAccidentLevel on tae.事故等级编码 equals tzl.编码 into left_tzl
                               from tzl in left_tzl.DefaultIfEmpty()
                               join tp in dbContext.TPerson on tae.首次调度员编码 equals tp.编码 into left_tp
                               from tp in left_tp.DefaultIfEmpty()
                               where tae.首次受理时刻 >= startTime && tae.首次受理时刻 <= endTime
                                     && tae.事件编码 == tae.所属事故编码 && tae.事故类型编码 != -1
                               select new
                               {
                                   关联事件数 = (from t in dbContext.TAlarmEvent where t.所属事故编码 == tae.事件编码 select t).Count(),
                                   事故编码 = tae.事件编码,
                                   事故名称 = tae.事件名称,
                                   调度员 = tp.姓名,
                                   事发时间 = tae.首次受理时刻,
                                   事发区域 = tae.区域,
                                   事故类型 = tzt.名称 == null ? "未找到" : tzt.名称,
                                   事故等级 = tzl.名称 == null ? "未找到" : tzl.名称,
                                   伤亡总人数一 = (from p in dbContext.TAccidentPatient
                                             where (from t in dbContext.TAlarmEvent where t.所属事故编码 == tae.事件编码 select t.事件编码).Contains(p.事故编码)
                                             select p).Count(),
                                   伤亡总人数二 = (from tpr in dbContext.TPatientRecord
                                             where (from t in dbContext.TAlarmEvent where t.所属事故编码 == tae.事件编码 select t.事件编码).Contains(tpr.任务编码.Substring(1, 16))
                                             select tpr).Count(),
                                   伤亡总人数三 = (from tar in dbContext.TAccidentReport
                                             where (from t in dbContext.TAlarmEvent where t.所属事故编码 == tae.事件编码 select t.事件编码).Contains(tar.事故编码)
                                             select tar.受伤人数).Count()
                                                   + (from tar in dbContext.TAccidentReport
                                                      where (from t in dbContext.TAlarmEvent where t.所属事故编码 == tae.事件编码 select t.事件编码).Contains(tar.事故编码)
                                                      select tar.死亡人数).Count(),
                                   事故类型编码 = tae.事故类型编码,
                                   事故等级编码 = tae.事故等级编码,
                                   中心编码 = tae.中心编码
                               };
                    //动态Where
                    if (!string.IsNullOrEmpty(accidentName))
                    {
                        list = list.Where(t => t.事故名称.Contains(accidentName));
                    }
                    if (!string.IsNullOrEmpty(place) && place != "--请选择--")
                    {
                        list = list.Where(t => t.事发区域.Contains(place));
                    }

                    if (type != null)
                    {
                        list = list.Where(t => t.事故类型编码 == type);
                    }

                    if (level != null)
                    {
                        list = list.Where(t => t.事故等级编码 == level);
                    }

                    switch (b.GetGroupRangePower("searchBound"))
                    {
                        case "SearchAll"://查找所有
                            break;
                        case "SearchCenter"://查找所属分中心
                            list = list.Where(t => t.中心编码 == userDetail.CenterCode);
                            break;
                        default://没有设置查询权限
                            return null;
                    }

                    long total = list.LongCount();
                    list = list.OrderByDescending(t => t.事发时间);
                    list = list.Skip((page - 1) * rows).Take(rows);

                    if (Config.GetKeyValue("WoundedMode") == "0")
                    {
                        var listAccident = list.ToList().Select(o => new
                        {
                            关联事件数 = o.关联事件数,
                            事故编码 = o.事故编码,
                            关联 = o.事故编码,
                            打印 = o.事故编码,
                            事故名称 = o.事故名称,
                            调度员 = o.调度员,
                            事发时间 = o.事发时间.ToString("yyyy-MM-dd HH:mm:ss"),
                            事发区域 = o.事发区域,
                            事故类型 = o.事故类型,
                            事故等级 = o.事故等级,
                            伤亡总人数 = o.伤亡总人数一,
                        });
                        var result = new { total = total, rows = listAccident.ToList() };
                        return result;
                    }
                    else if (Config.GetKeyValue("WoundedMode") == "1")
                    {
                        var listAccident = list.ToList().Select(o => new
                        {
                            关联事件数 = o.关联事件数,
                            事故编码 = o.事故编码,
                            关联 = o.事故编码,
                            打印 = o.事故编码,
                            事故名称 = o.事故名称,
                            调度员 = o.调度员,
                            事发时间 = o.事发时间.ToString("yyyy-MM-dd HH:mm:ss"),
                            事发区域 = o.事发区域,
                            事故类型 = o.事故类型,
                            事故等级 = o.事故等级,
                            伤亡总人数 = o.伤亡总人数二,
                        });
                        var result = new { total = total, rows = listAccident.ToList() };
                        return result;
                    }
                    else if (Config.GetKeyValue("WoundedMode") == "2")
                    {
                        var listAccident = list.ToList().Select(o => new
                        {
                            关联事件数 = o.关联事件数,
                            事故编码 = o.事故编码,
                            关联 = o.事故编码,
                            打印 = o.事故编码,
                            事故名称 = o.事故名称,
                            调度员 = o.调度员,
                            事发时间 = o.事发时间.ToString("yyyy-MM-dd HH:mm:ss"),
                            事发区域 = o.事发区域,
                            事故类型 = o.事故类型,
                            事故等级 = o.事故等级,
                            伤亡总人数 = o.伤亡总人数三,
                        });
                        var result = new { total = total, rows = listAccident.ToList() };
                        return result;
                    }
                    return null;

                }
            }
        }

        /// <summary>
        /// 获取所有事故区域
        /// </summary>
        /// <returns></returns>
        public static object GetAccidentAddress()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from t in dbContext.TZArea
                            select new
                            {
                                ID = t.编码,
                                Name = t.名称,
                            }).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取所有事故等级
        /// </summary>
        /// <returns></returns>
        public static object GetAccidentLevel()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from t in dbContext.TZAccidentLevel
                            select new
                            {
                                ID = t.编码,
                                Name = t.名称,
                            }).ToList();
                return list;
            }
        }

        /// <summary>
        /// 根据事件编码返回事件信息
        /// </summary>
        /// <param name="accidentId"></param>
        /// <returns></returns>
        public static object GetAccidentInfoById(string accidentId)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = from tae in dbContext.TAlarmEvent
                           join tp in dbContext.TPerson on tae.首次调度员编码 equals tp.编码 into left_tp
                           from tp in left_tp.DefaultIfEmpty()
                           where tae.所属事故编码 == accidentId
                           select new
                           {
                               呼救电话 = tae.首次呼救电话,
                               事件名称 = tae.事件名称,
                               受理时刻 = tae.首次受理时刻,
                               受理调度员 = tp.姓名,
                               区域 = tae.区域,
                               所属事故编码 = tae.所属事故编码,
                               事件编码 = tae.事件编码,
                               伤亡人数 = (from tap in dbContext.TAccidentPatient where tap.事故编码 == tae.事件编码 select tap).Count(),
                           };
                var listInfo = list.ToList().Select(o => new
                {
                    呼救电话 = o.呼救电话,
                    事件名称 = o.事件名称,
                    受理时刻 = o.受理时刻.ToString("yyyy-MM-dd HH:mm:ss"),
                    受理调度员 = o.受理调度员,
                    区域 = o.区域,
                    所属事故编码 = o.所属事故编码,
                    事件编码 = o.事件编码,
                    伤亡人数 = o.伤亡人数,
                }).ToList();

                return listInfo;
            }
        }

        /// <summary>
        /// 根据事件编码返回伤病员信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static object GetPatient(int page, int rows, string order, string sort, string eventId)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = from tap in dbContext.TAccidentPatient
                           join tt in dbContext.TTask on tap.任务编码 equals tt.任务编码 into left_tt
                           from tt in left_tt.DefaultIfEmpty()
                           join tzs in dbContext.TZIllState on tap.病情编码 equals tzs.编码 into left_tzs
                           from tzs in left_tzs.DefaultIfEmpty()
                           join ta in dbContext.TAmbulance on tap.车辆编码 equals ta.车辆编码 into left_ta
                           from ta in left_ta.DefaultIfEmpty()
                           where tap.事故编码 == eventId
                           orderby sort
                           select new
                           {
                               事故编码 = tap.事故编码,
                               序号 = tap.序号,
                               任务编码 = tap.任务编码,
                               车辆编码 = tap.车辆编码,
                               姓名 = tap.姓名,
                               性别 = tap.性别,
                               年龄 = tap.年龄,
                               年龄单位 = tap.年龄单位,
                               职业 = tap.职业,
                               单位 = tap.单位,
                               住址 = tap.住址,
                               初步诊断 = tap.初步诊断,
                               收治医院 = tap.收治医院,
                               治疗方式 = tap.治疗方式,
                               转归 = tap.转归,
                               相关车辆 = "车号：" + ta.实际标识 + " 司机：" + tt.司机 + " 医护：" + tt.医生 + "/" + tt.护士,
                               相关车辆编号 = tt.任务编码 + "|" + tt.车辆编码,
                               病情 = tzs.名称,
                               病情编码 = tap.病情编码,
                           };
                long total = list.LongCount();
                list = list.Skip((page - 1) * rows).Take(rows);

                var listPatient = list.ToList().Select(o => new {
                        事故编码 = o.事故编码,
                        序号 = o.序号,
                        任务编码 = o.任务编码,
                        车辆编码 = o.车辆编码,
                        姓名 = o.姓名,
                        性别 = o.性别,
                        年龄 = o.年龄.ToString() + o.年龄单位,
                        年龄单位 = o.年龄单位,
                        职业 = o.职业,
                        单位 = o.单位,
                        住址 = o.住址,
                        初步诊断 = o.初步诊断,
                        收治医院 = o.收治医院,
                        治疗方式 = o.治疗方式,
                        转归 = o.转归,
                        相关车辆 = o.相关车辆,
                        相关车辆编号 = o.相关车辆编号,
                        病情 = o.病情,
                        病情编码  = o.病情编码,
                });
                var result = new { total = total, rows = listPatient.ToList() };
                return result;
            }
        }

        /// <summary>
        /// 获取所有病情
        /// </summary>
        /// <returns></returns>
        public static object GetPatientIllState()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = (from t in dbContext.TZIllState
                            select new
                            {
                                ID = t.编码,
                                Name = t.名称,
                            }).ToList();
                return list;
            }
        }

        /// <summary>
        /// 根据事件编码返回相关车辆
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static object GetAmbulance(string eventId)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = from tt in dbContext.TTask
                           join tae in dbContext.TAlarmEvent on tt.事件编码 equals tae.事件编码 into left_tae
                           from tae in left_tae.DefaultIfEmpty()
                           join ta in dbContext.TAmbulance on tt.车辆编码 equals ta.车辆编码 into left_ta
                           from ta in left_ta.DefaultIfEmpty()
                           where tae.事件编码 == eventId
                           select new
                           {
                               生成任务时刻 = tt.生成任务时刻,
                               任务编码 = tt.任务编码,
                               车辆编码 = tt.车辆编码,
                               车牌号码 = ta.车牌号码,
                               实际标识 = ta.实际标识,
                               随车人员 = " 司机: " + tt.司机 + " 医护：" + tt.医生 + "/" + tt.护士,
                           };
                var listAmb = list.ToList().Select(o => new
                {
                    ID = o.任务编码 + "|" + o.车辆编码,
                    Name = o.生成任务时刻.ToString("yyyy-MM-dd HH:mm") + " 车号：" + o.实际标识 + "[" + o.车牌号码 + "]" + o.随车人员,
                });
                return listAmb.ToList();
            }
        }

        /// <summary>
        /// 获取所有事故类型
        /// </summary>
        /// <returns></returns>
        public static List<TZAccidentType> GetAccidentType()
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TZAccidentType.ToList();
            }
        } 
        #endregion

        #region 保存
        /// <summary>
        /// 保存伤病员信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool SavePatient(TAccidentPatient entity)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                if (entity.序号 == 0)
                {
                    //新增的保存
                    var total = dbContext.TAccidentPatient.Where(t => t.事故编码 == entity.事故编码).Count();
                    if (total == 0)
                    {
                        entity.序号 = 1;
                    }
                    else
                    {
                        entity.序号 = total + 1;
                    }

                    dbContext.TAccidentPatient.InsertOnSubmit(entity);
                    dbContext.SubmitChanges();
                    return true;
                }
                else
                {
                    //修改的保存
                    var model = dbContext.TAccidentPatient.FirstOrDefault(t => t.序号 == entity.序号 && t.事故编码 == entity.事故编码);
                    model.任务编码 = entity.任务编码;
                    model.姓名 = entity.姓名;
                    model.性别 = entity.性别;
                    model.年龄 = entity.年龄;
                    model.年龄单位 = entity.年龄单位;
                    model.单位 = entity.单位;
                    model.收治医院 = entity.收治医院;
                    model.病情编码 = entity.病情编码;
                    model.车辆编码 = entity.车辆编码;
                    model.初步诊断 = entity.初步诊断;
                    model.职业 = entity.职业;
                    model.治疗方式 = entity.治疗方式;
                    model.住址 = entity.住址;
                    model.转归 = entity.转归;

                    dbContext.SubmitChanges();
                    return true;
                }
            }
        }
        #endregion

        #region 删除
        #region 删除伤病员

        /// <summary>
        /// 删除伤病员
        /// </summary>
        /// <param name="eventId">事件编码</param>
        /// <param name="idList">伤病员序号列表</param>
        /// <returns></returns>
        public static bool DeletePatient(string eventId, int number)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
               
                ////弹出框datagrid做删除时总是保留上次已经删除的伤病员序号，在这里排除
                //var list = from t in dbContext.TAccidentPatient 
                //           where t.事故编码 == eventId && numList.Contains(t.序号) 
                //           select t;

                var entity = dbContext.TAccidentPatient.SingleOrDefault(t => t.事故编码 == eventId && t.序号 == number);
                //dbContext.TAccidentPatient.Load();
                dbContext.TAccidentPatient.DeleteOnSubmit(entity);
                dbContext.SubmitChanges();
                return true;
            }

        }
        
        #endregion

        #region 删除事件
        public static bool DeleteEvent(string eventId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                //删除事件
                var eventModel = dbContext.TAlarmEvent.Single(t => t.事件编码 == eventId);
                //dbContext.TAlarmEvent.Load();
                dbContext.TAlarmEvent.DeleteOnSubmit(eventModel);

                //删除该事件下的伤病员
                var patientModels = dbContext.TAccidentPatient.Where(t => t.事故编码 == eventId);


                foreach (TAccidentPatient g in patientModels)
                {
                    dbContext.TAccidentPatient.DeleteOnSubmit(g);
                }
                dbContext.SubmitChanges();
                return true;
            }
        }
        #endregion

        #endregion

        #region 重大事件相关
        #region 根据事件List返回关联事件信息
        public static object AccidentRelation(List<string> accidentList)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = from tae in dbContext.TAlarmEvent
                           join tzt in dbContext.TZAccidentType on tae.事故类型编码 equals tzt.编码 into left_tzt
                           from tzt in left_tzt.DefaultIfEmpty()
                           join tzl in dbContext.TZAccidentLevel on tae.事故等级编码 equals tzl.编码 into left_tzl
                           from tzl in left_tzl.DefaultIfEmpty()
                           join tp in dbContext.TPerson on tae.首次调度员编码 equals tp.编码 into left_tp
                           from tp in left_tp.DefaultIfEmpty()
                           where accidentList.Contains(tae.所属事故编码)
                           select new
                           {
                               事件编码 = tae.事件编码,
                               所属事故编码 = tae.所属事故编码,
                               事故名称 = tae.事件名称,
                               调度员 = tp.姓名,
                               受理时间 = tae.首次受理时刻,
                               区域 = tae.区域,
                               事故类型 = tzt.名称 == null ? "未找到" : tzt.名称,
                               事故等级 = tzl.名称 == null ? "未找到" : tzl.名称,
                           };
                var listInfo = list.ToList().Select(o => new
                {
                    事件编码 = o.事件编码,
                    所属事故编码 = o.所属事故编码,
                    事故名称 = o.事故名称,
                    调度员 = o.调度员,
                    受理时间 = o.受理时间.ToString("yyyy-MM-dd HH:mm:ss"),
                    区域 = o.区域,
                    事故类型 = o.事故类型,
                    事故等级 = o.事故等级,
                });

                return listInfo.ToList();
            }
        }
        #endregion

        #region 合并重大事故
        /// <summary>
        /// 重大事故合并
        /// </summary>
        /// <param name="accidentId">事故编码</param>
        /// <param name="accidentList">事故编码列表</param>
        /// <returns></returns>
        public static bool CombineAccident(string accidentId, List<string> accidentList) 
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                foreach (string g in accidentList)
                {
                    var model = dbContext.TAlarmEvent.Single(t => t.事件编码 == g );
                    model.所属事故编码 = accidentId;
                }
                dbContext.SubmitChanges();
                return true;
            }
        }
        #endregion

        #region 解除事故关联

        public static bool ReleaseAccident(string accidentId)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var model = dbContext.TAlarmEvent.Single(t => t.事件编码 == accidentId);
                model.所属事故编码 = accidentId;
                dbContext.SubmitChanges();
                return true;
            }
        }
        #endregion

        #endregion
    }
}
