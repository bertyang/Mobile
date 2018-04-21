using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;
using Anchor.FA.Utility;

namespace Anchor.FA.DAL.Notice
{
    public class Back
    {
        public static object GetTelBackCallsV7(int page, int rows, string order, string sort, string start, string end,string type)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(" select ");
                strSQL.Append(" 患者姓名=tac.患者姓名, ");
                strSQL.Append(" 性别=tac.性别, ");
                strSQL.Append(" 年龄=tac.年龄, ");
                strSQL.Append(" 出车分站=ts.名称, ");
                strSQL.Append(" 现场地址=tac.现场地址, ");
                strSQL.Append(" 发送时间=tbcs.发送时间, ");
                strSQL.Append(" 主叫电话=tac.呼救电话, ");
                strSQL.Append(" 联系人=tac.联系人, ");
                strSQL.Append(" 联系电话=tac.联系电话, ");
                strSQL.Append(" 受理类型=tzac.名称, ");
                strSQL.Append(" 出车结果=case when tt.是否正常结束=1 then '正常结束' else tztar.名称 end,  ");
                strSQL.Append(" 任务编码=tt.任务编码, ");
                strSQL.Append(" 事件详情='查看', ");
                strSQL.Append(" 司机=tt.司机,医生=tt.医生,护士=tt.护士,车辆=tab.实际标识,任务号=tt.任务流水号,主诉=tac.主诉,送往地点=tac.送往地点, ");
                strSQL.Append(" 回访人=tbc.回访人, ");
                strSQL.Append(" 回访时刻=case when tbcs.发送时间 is null then tbc.回访保存时间 else tbcs.发送时间 end, ");
                strSQL.Append(" 短信=isnull((select 短信=case when tbcs.任务编码 is null then '未发' else case when tbcs.接收内容 is null then '已发' else tbcs.接收内容 end end from TBackCallSM tbcs where tbcs.任务编码=tt.任务编码 ),'未发'), ");
                strSQL.Append(" 回访单=(select 回访单=case when isnull(count(*),0)=0 then '未访' else tbc.是否有效 end from TBackCall where tbc.任务编码=tt.任务编码 ),");
                strSQL.Append(" 录音=(select 录音=Convert(varchar(20),isnull(count(*),0))+'个' from TBackCallRecordLink tbcl where tbcl.任务编码=tt.任务编码 ), ");
                strSQL.Append(" 编码= tbcs.编码 ");
                strSQL.Append(" from dbo.TTask tt ");
                strSQL.Append(" left join TAlarmEvent tae on tae.事件编码=tt.事件编码 ");
                strSQL.Append(" left join TAcceptEvent tac on tac.事件编码=tt.事件编码 and tt.受理序号=tac.受理序号 ");
                strSQL.Append(" left join TStation ts on ts.编码=tt.分站编码 ");
                strSQL.Append(" left join TZAcceptEventType tzac on tzac.编码=tac.受理类型编码 ");
                strSQL.Append(" left join TZTaskAbendReason tztar on tztar.编码=tt.异常结束原因编码 ");
                strSQL.Append(" left join TAmbulance tab on tab.车辆编码=tt.车辆编码 ");
                strSQL.Append(" left join TBackCallAudit tbca on tbca.任务编码=tt.任务编码 ");
                strSQL.Append(" left join TBackCall tbc on tbc.任务编码=tt.任务编码 ");
                strSQL.Append(" left join dbo.TBackCallSM tbcs on tbcs.任务编码=tt.任务编码 ");
                strSQL.Append(" where tbcs.发送时间>='" + start + "' and tbcs.发送时间<'" + end + "' and (tae.所属事故编码 is null or tae.所属事故编码='') and tt.是否执行中=0 and tbca.任务编码 is null ");

                if(!string.IsNullOrEmpty(type))
                {
                    if (type == "乱码")
                    {
                        strSQL.AppendFormat(" and tbcs.接收内容 not in ('满意','不满意','NULL')"); 
                    }
                    else
                    {
                        strSQL.AppendFormat(" and tbcs.接收内容='{0}'", type);
                    }
                }

                var list1 = dbContext.ExecuteQuery<C_BackCall>(strSQL.ToString());
                var list2 = dbContext.ExecuteQuery<C_BackCall>(strSQL.ToString());
                long total = list1.LongCount();

                list2 = list2.Skip((page - 1) * rows).Take(rows);

                return new { total = total, rows = list2.ToList() };
            }
        }

        public static TBackCallSM GetBackSM(int code)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TBackCallSM.FirstOrDefault(t => t.编码 == code);
            }
        }

        public static void SaveBackSM(TBackCallSM entity)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var model = dbContext.TBackCallSM.FirstOrDefault(t => t.编码 == entity.编码);

                model.接收内容 = entity.接收内容;

                dbContext.SubmitChanges();
            }

        }

        public static TBackCall GetBackCall(string taskCode)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.TBackCall.FirstOrDefault(t => t.任务编码 == taskCode);
            }
        }

        public static void SaveBackCall(TBackCall entity)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var model = dbContext.TBackCall.FirstOrDefault(t => t.任务编码 == entity.任务编码);

                if (model == null)
                {

                    dbContext.TBackCall.InsertOnSubmit(entity);
                }
                else
                {
                    model.护士 = entity.护士;
                    model.司机 = entity.司机;
                    model.医生 = entity.医生;
                    model.调度 = entity.调度;
                    model.担架 = entity.担架;
                    model.备注 = entity.备注;                    
                    model.回访保存时间 = DateTime.Now;
                    model.回访结果 = entity.回访结果;
                }

                dbContext.SubmitChanges();
            }

        }

        public static string GetFirstDispatcher(string taskCode)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                var list = from a in dbContext.TAlarmEvent
                           join b in dbContext.TTask on a.事件编码 equals b.事件编码
                           join c in dbContext.TPerson on a.首次调度员编码 equals c.编码
                           where b.任务编码 == taskCode
                           select c.姓名;

                return list.FirstOrDefault();

            }
        }
    }
}
