using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Spring.Context;
using Spring.Context.Support;
using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;
using Anchor.FA.Utility;

namespace Anchor.FA.BLL.Organize
{
    public class Worker : IBLL.IWorker
    {
        public B_WORKER Login(string loginName, string passWord)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            IEncrypt encrypt = ctx["Encrypt"] as IEncrypt;

            HashEncrypt he = new HashEncrypt();

            //解密前台DES加密的密码
            //loginName = he.DESDecrypt(loginName, "", "");
            //passWord = he.DESDecrypt(passWord, "", "");

            B_WORKER entity = DAL.Organize.Worker.Login(loginName);

            if (entity == null)
            {
                return null;
            }

            //根据后台配置加密密码
            string temp = encrypt.Encrypt(entity.ID, passWord);
            if (entity.PassWord == temp)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 调度台或者回访程序登录
        /// </summary>
        /// <param name="loginName">TPerson编码</param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public B_WORKER DeskLogin(string loginName, string passWord)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            IEncrypt encrypt = ctx["Encrypt"] as IEncrypt;

            B_WORKER entity = DAL.Organize.Worker.DeskLogin(loginName);

            if (entity == null)
            {
                return null;
            }
            string temp = encrypt.Encrypt(entity.ID, passWord);
            if (entity.PassWord == temp)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        public void LoginLog(B_LOGIN_LOG log)
        {
            DAL.Organize.Worker.LoginLog(log);
        }

        public string LoginCheck(string loginName)
        {
            List<B_LOGIN_LOG> listLog = DAL.Organize.Worker.LoginCheck(loginName);

            if (listLog.Count == 1)
            {
                return "您是第一次登陆，请修改初始密码";
            }
            if (listLog.Count == 2)
            {
                if (listLog[0].IP != listLog[1].IP)
                {
                    return " 您上次登录的地点和本次不一致,请注意登录安全";
                }
            }

            return string.Empty;

        }

        public B_WORKER GetWorkerById(int? userId)
        {
            //IApplicationContext ctx = ContextRegistry.GetContext();
            //IPrimaryKeyCreater prikey = ctx["PrimaryKeyCreater"] as IPrimaryKeyCreater;

            B_WORKER entity = DAL.Organize.Worker.GetWorkerById(userId);

            //if (entity.ID == 0)
            //{
            //    entity.ID = prikey.getIntPrimaryKey("B_WORKER");
            //}
            return entity;
        }
        public C_WorkerDetail GetWorkerDetailById(int userId)
        {
            C_WorkerDetail entity = DAL.Organize.Worker.GetWorkerDetailById(userId);
            return entity;
        }

        public List<B_WORKER> GetAllWorker()
        {
            return DAL.Organize.Worker.GetAllWorker();
        }

        public bool UpdatePassword(int workerId, string newPassword)
        {
            return DAL.Organize.Worker.UpdatePassword(workerId, newPassword);
        }

        public bool UpdatePassword(string empNo, string newPassword)
        {
            B_WORKER_ROLE wr = DataAccess<B_WORKER_ROLE>.FirstOrDefault(t => t.EmpNo == empNo);

            if (wr != null)
            {
                return DAL.Organize.Worker.UpdatePassword(wr.WorkerID, newPassword);
            }
            else
            {
                return false;
            }
        }

        public List<B_WORKER> GetWorkerByOrg(int orgId)
        {
            return DAL.Organize.Worker.GetWorkerByOrg(orgId);
        }

        public List<B_WORKER> GetWorkerByRole(int roleId)
        {
            return DAL.Organize.Worker.GetWorkerByRole(roleId);
        }

        //public List<C_ORGANIZE_TREE> GetTree(int? id)
        //{

        //    List<C_ORGANIZE_TREE> listUnit = new Organize().GetTree(id);

            //if (id != null)
            //{
            //    List<C_ORGANIZE_TREE> listWorker = DAL.Organize.Worker.GetWorkerTree((int)id);

            //    //遍历组织树添加人员
            //    new Organize().AddNode(listUnit, "Org", listWorker, "icon tu0825");
            //}

        //    return listUnit;
        //}

        public object LoadAllWorkerByPage(int page, int rows, string order, string sort, string name, string orgId, int? roleId, bool mustHavePhone, string empNo)
        {
            return DAL.Organize.Worker.LoadAllWorkerByPage(page, rows, order, sort, name, orgId, roleId, mustHavePhone, empNo);
        }

        public DataSet LoadAllWorkerByPageUpdo(string name, int? orgId, int? roleId, string empNo)
        {
            return DAL.Organize.Worker.LoadAllWorkerByPageUpdo(name, orgId, roleId, empNo);
        }


        public bool Delete(IList<string> idList)
        {
            return DAL.Organize.Worker.Delete(idList);
        }

        public int Save(B_WORKER entity)
        {
            return DAL.Organize.Worker.Save(entity);
        }

        public object LoadWorkerRole(int workerId)
        {
            //List<int> list = new List<int>();

            //foreach (B_WORKER_ROLE w in DAL.Organize.Worker.LoadWorkerRole(workerId))
            //{
            //    list.Add(w.RoleID);
            //}

            return DAL.Organize.Worker.LoadWorkerRole(workerId);
        }

        public bool SaveWorkerRole(B_WORKER_ROLE workerRole)
        {
            return DAL.Organize.Worker.SaveWorkerRole(workerRole);
        }

        public bool DelWorkerRole(int id)
        {
            return DAL.Organize.Worker.DelWorkerRole(id);
        }

        public B_WORKER_ORGANIZATION GetWorkerDefaultUnit(int workerId)
        {
            return DAL.Organize.Worker.GetWorkerDefaultUnit(workerId);
        }

        /// <summary>
        /// 当前登陆者所在机构列表
        /// </summary>
        /// <param name="workerId"></param>
        /// <returns></returns>
        public object GetUnitList(int workerId)
        {
            return DAL.Organize.Worker.GetUnitList(workerId);
        }

        /// <summary>
        /// 加载人员兼职信息
        /// </summary>
        /// <returns></returns>
        public object GetWorkerSideline(int workerId)
        {
            return DAL.Organize.Worker.GetWorkerSideline(workerId);
        }
        public bool SaveSideline(B_WORKER_ORGANIZATION entity)
        {
            return DAL.Organize.Worker.SaveSideline(entity);
        }
        public bool DeleteSideline(int id)
        {
            return DAL.Organize.Worker.DeleteSideline(id);
        }

        public C_Worker_Level GetLevelByOrg(int workerId, int orgId)
        {
            return DAL.Organize.Worker.GetLevelByOrg(workerId, orgId);
        }

        public C_Worker_Level GetDefaultLevel(int workerId)
        {
            return DAL.Organize.Worker.GetDefaultLevel(workerId);
        }

        //获取所有工号
        public bool IsExistEmpNo(string empNo)
        {
            return DAL.Organize.Worker.IsExistEmpNo(empNo);
        }

        public TPerson GetTPersonByEmpNo(string empNo)
        {
            return DAL.Organize.Worker.GetTPerson(empNo);
        }
        /// <summary>
        /// loginName可能是工号也可能是TPerson编码
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public TPerson GetTPerson(int workerId, string loginName)
        {
            B_WORKER_ROLE wr = DataAccess<B_WORKER_ROLE>.FirstOrDefault(t => t.EmpNo == loginName && t.WorkerID == workerId);

            if (wr == null)
            {
                wr = DataAccess<B_WORKER_ROLE>.FirstOrDefault(t => t.TPerson编码 == loginName && t.WorkerID == workerId);
            }

            return DataAccess<TPerson>.FirstOrDefault(AppConfig.ConnectionStringDispatch, t => t.编码 == wr.TPerson编码);
        }


        public List<B_WORKER_ROLE> GetWorkerRole(int workerId)
        {
            return DAL.Organize.Worker.GetWorkerRole(workerId);
        }

        //public void InitPassWord()
        //{
        //    IApplicationContext ctx = ContextRegistry.GetContext();
        //    IEncrypt encrypt = ctx["Encrypt"] as IEncrypt;

        //    List<B_WORKER_ROLE> listWR = DataAccess<B_WORKER_ROLE>.ToList();

        //    foreach (B_WORKER_ROLE d in listWR)
        //    {
        //        if (!string.IsNullOrEmpty(d.EmpNo))
        //        {
        //            Func<B_WORKER, bool> c = s => s.ID == d.WorkerID;

        //            B_WORKER worker = DataAccess<B_WORKER>.SingleOrDefault(c);

        //            if (worker != null)
        //            {
        //                worker.PassWord = encrypt.Encrypt(worker.ID, d.EmpNo);

        //                DataAccess<B_WORKER>.Update(worker);
        //            }
        //        }
        //    }

        //    Func<G_CONFIG, bool> g = s => s.Key == "InitPassWord";
        //    G_CONFIG config = DataAccess<G_CONFIG>.SingleOrDefault(g);
        //    config.Value = "True";
        //    DataAccess<G_CONFIG>.Update(config);
        //}


    }

}
