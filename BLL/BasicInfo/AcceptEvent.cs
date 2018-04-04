using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.BasicInfo
{
    public class AcceptEvent
    //: IBLL.IAcceptEvent 看来 接口的方法不能是静态的
    {
        public static List<TZFolk> LoadFolks()
        {
            return DAL.BasicInfo.AcceptEvent.LoadFolks();
        }
        public static List<TZNational> LoadNationals()
        {
            return DAL.BasicInfo.AcceptEvent.LoadNationals();
        }
        public static List<TZAge> LoadAges()
        {
            return DAL.BasicInfo.AcceptEvent.LoadAges();
        }
        public static List<TZIllState> LoadIllStates()
        {
            return DAL.BasicInfo.AcceptEvent.LoadIllStates();
        }

        public static List<TZLocalAddrType> LoadLocalAddrTypes()
        {
            return DAL.BasicInfo.AcceptEvent.LoadLocalAddrTypes();
        }
        public static List<TZSendAddrType> LoadSendAddrTypes()
        {
            return DAL.BasicInfo.AcceptEvent.LoadSendAddrTypes();
        }
        public static List<TZSpecialRequest> LoadSpecialRequests()
        {
            return DAL.BasicInfo.AcceptEvent.LoadSpecialRequests();
        }
        public static List<TZBackupOne> LoadBackupOnes()
        {
            return DAL.BasicInfo.AcceptEvent.LoadBackupOnes();
        }
        public static List<TZBackupTwo> LoadBackupTwos()
        {
            return DAL.BasicInfo.AcceptEvent.LoadBackupTwos();
        }

        public static string Update(TAcceptEvent entity)
        {
            return DAL.BasicInfo.AcceptEvent.Update(entity);
        }
        public static C_AccInfo GetC_AccInfo(string eventCode, int orderNumber)
        {
            return DAL.BasicInfo.AcceptEvent.GetC_AccInfo(eventCode, orderNumber);
        }

        public static List<C_ORGANIZE_TREE> GetAlarmReasonsTree(string Code)
        {
            List<C_ORGANIZE_TREE> mtmList = (from p in DAL.BasicInfo.AcceptEvent.LoadAlarmReasons()
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

    }
}
