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
    public class TaskPersonLink
    {
        /// <summary>
        /// 暂不进行数据库操作(只修改对象) 分块修改对象 更灵活 提高效率
        /// </summary>
        public static TTaskPersonLink InsertTaskPerson(string TaskCode, string PersonCode)
        {
            TTaskPersonLink info = new TTaskPersonLink();
            TPerson perInfo = Person.GetOnePerson(PersonCode);
            info.任务编码 = TaskCode;
            info.人员编码 = perInfo.编码;
            info.姓名 = perInfo.姓名;
            info.分站编码 = perInfo.分站编码;
            info.是否有效 = perInfo.是否有效;
            info.人员类型编码 = perInfo.类型编码;
            return info;
            //return m_DAL.InsertTaskPerson(info);
        }


        public static List<TTaskPersonLink> GetTaskPersons(string TaskCode)
        {
            using (MainDataContext dbContext = new MainDataContext(AppConfig.ConnectionStringDispatch))
            {
                return dbContext.TTaskPersonLink.Where(p => p.任务编码 == TaskCode).ToList();
            }
        }
    }
}
