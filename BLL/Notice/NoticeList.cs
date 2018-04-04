using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Anchor.FA.Model;

using Anchor.FA.BLL.IBLL;

namespace Anchor.FA.BLL.Notice
{
    internal class NoticeList
    {
        public IList<TZNoticeType> getNoticeType()
        {
            return DAL.Notice.NoticeList.getNoticeType();
        }
       // public IList<TStation> GetStation()
       //{
       //    return DAL.Notice.NoticeList.GetStation();

       // }
      public object LoadRemindByPage(int page, int rows, string order, string sort)
      {
          return DAL.Notice.NoticeList.LoadRemindByPage(page ,rows, order,sort);
      }
      public object GetNoticeList(int page, int rows, string order, string sort, DateTime startTime, DateTime endTime, 
          int sendType, string station, string vehicle,Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail UserInfo)
      {
          return DAL.Notice.NoticeList.GetNoticeList(page, rows, order, sort, startTime, endTime,
              sendType, station, vehicle, bp, UserInfo);
      }

      public object GetBack(int code)
      {
          return DAL.Notice.NoticeList.GetBack(code);
      }
      public bool Save(B_Remind entity)
      {
          return DAL.Notice.NoticeList.Save(entity);
      }
      public object Edit(int? id)
      {
          return DAL.Notice.NoticeList.Edit(id);
      }
      public string GetTelByWorkerID(int id)
      {
          return DAL.Notice.NoticeList.GetTelByWorkerID(id);
      }
      public string GetEmpNoByWorkerID(int id)
      {
          return DAL.Notice.NoticeList.GetEmpNoByWorkerID(id);
      }

      public bool Delete(IList<int> idList)
      {
          return DAL.Notice.NoticeList.Delete(idList);
      }
      public IList<B_WORKER> GetAllTel()
      {
          return DAL.Notice.NoticeList.GetAllTel();
      }
         
      public List<TAmbulance> GetAmbulanceByStationID(string id,Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail UserInfo)
      {
          return DAL.Notice.NoticeList.GetAmbulanceByStationID(id, bp, UserInfo);
      }

      public List<TDesk> GetAllDesk(Anchor.FA.Utility.ButtonPower bp, C_WorkerDetail UserInfo)
      {
          return DAL.Notice.NoticeList.GetAllDesk(bp, UserInfo);
      }

      public List<C_TongJi_TREE> GetTree(int? exceptUnitId)
      {
          List<B_ACTION> list = GetAllUnit(exceptUnitId);

          //list.Find(t => t.ParentID == exceptUnitId);

          List<C_TongJi_TREE> mtm = new List<C_TongJi_TREE>();

          foreach (B_ACTION r in list)
          {
              mtm.Add(new C_TongJi_TREE
              {
                  id = r.Url,// url
                  text = r.Remark,//名称
                  ParentID = r.ParentID.ToString(),
                  iconCls = "icon tu1718"
              });
          }
          return mtm;
          //if (list.Count() == 0)
          //{
          //    return null;
          //}
          //else
          //{
          //    return GetUnitTree(mtm, "0");
          //}
      }
      private List<C_TongJi_TREE> GetUnitTree(List<C_TongJi_TREE> mtmList, string Pid)
      {
          List<C_TongJi_TREE> listTree = new List<C_TongJi_TREE>();

          List<C_TongJi_TREE> listParent = mtmList.Where(item => item.ParentID == Pid).ToList();

          foreach (C_TongJi_TREE t in listParent)
          {
              C_TongJi_TREE tm = new C_TongJi_TREE();

              tm.id = t.id;
              tm.text = t.text;
              tm.ParentID = t.ParentID;
              tm.Type = "Org";

              if (int.Parse(t.ParentID) != 0)
              {
                  tm.iconCls = "icon tu1911";

              }
              else
              {
                  tm.iconCls = t.iconCls;
              }


              tm.children = GetUnitTree(mtmList, t.id);


              listTree.Add(tm);
          }

          return listTree;
      }
      public List<B_ACTION> GetAllUnit(int? exceptUnitId)
        {
            return DAL.Notice.NoticeList.GetAllUnit( exceptUnitId);
        }
      
    }
}
