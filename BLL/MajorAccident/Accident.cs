using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.MajorAccident
{
    internal class Accident
    {
        public object GetAccident(int page, int rows, string order, string sort, 
            DateTime startTime, DateTime endTime,
            string accidentName, string address, int? type, int? level, 
            Anchor.FA.Utility.ButtonPower p, C_WorkerDetail userDetail)
        {
            return DAL.MajorAccident.Accident.GetAccident(page, rows, order, sort, startTime, endTime, 
                accidentName, address, type, level, p, userDetail);
        }

        public object GetAccidentAddress()
        {
            return DAL.MajorAccident.Accident.GetAccidentAddress();
        }

        public object GetAccidentLevel()
        {
            return DAL.MajorAccident.Accident.GetAccidentLevel();
        }

        public List<TZAccidentType> GetAccidentType() 
        {
            return DAL.MajorAccident.Accident.GetAccidentType();
        }

        #region 重大事故类型树
        public List<C_ACCIDENTTYPE_TREE> GetTree(int? exceptTypeId)
        {
            List<TZAccidentType> list = DAL.MajorAccident.Accident.GetAccidentType();

            list.Remove(list.Find(t => t.编码 == exceptTypeId));

            List<C_ACCIDENTTYPE_TREE> mtm = new List<C_ACCIDENTTYPE_TREE>();

            foreach (TZAccidentType r in list)
            {
                mtm.Add(new C_ACCIDENTTYPE_TREE
                {
                    id = r.编码.ToString(),
                    text = r.名称,
                    ParentID = r.上级编码.ToString(),
                    iconCls = "icon tu0122"
                });
            }

            mtm.Remove(mtm.Find(t => t.id == "-1"));

            if (list.Count() == 0)
            {
                return null;
            }
            else
            {
                return GetTypeTree(mtm, "0");
            }
        }

        private List<C_ACCIDENTTYPE_TREE> GetTypeTree(List<C_ACCIDENTTYPE_TREE> mtmList, string Pid)
        {
            List<C_ACCIDENTTYPE_TREE> listTree = new List<C_ACCIDENTTYPE_TREE>();

            List<C_ACCIDENTTYPE_TREE> listParent = mtmList.Where(item => item.ParentID == Pid).ToList();

            foreach (C_ACCIDENTTYPE_TREE t in listParent)
            {
                C_ACCIDENTTYPE_TREE tm = new C_ACCIDENTTYPE_TREE();

                tm.id = t.id;
                tm.text = t.text;
                tm.ParentID = t.ParentID;

                if (int.Parse(t.ParentID) != 0)
                {
                    tm.iconCls = "icon tu1603";

                }
                else
                {
                    tm.iconCls = t.iconCls;
                }


                tm.children = GetTypeTree(mtmList, t.id);


                listTree.Add(tm);
            }

            return listTree;
        }

        #endregion

        public object GetAccidentInfoById(string accidentId)
        {
            return DAL.MajorAccident.Accident.GetAccidentInfoById(accidentId);
        }

        public object GetAccidentPatient(int page, int rows, string order, string sort, string eventId)
        {
            return DAL.MajorAccident.Accident.GetPatient(page, rows, order, sort,eventId);
        }

        public object GetPatientIllState()
        {
            return DAL.MajorAccident.Accident.GetPatientIllState();
        }

        public object GetAmbulance(string eventId)
        {
            return DAL.MajorAccident.Accident.GetAmbulance(eventId);
        }

        public bool SavePatient(TAccidentPatient entity)
        {
            return DAL.MajorAccident.Accident.SavePatient(entity);
        }

        public bool DeletePatient(string eventId, int number)
        {
            return DAL.MajorAccident.Accident.DeletePatient(eventId,number);
        }

        public bool DeleteEvent(string eventId)
        {
            return DAL.MajorAccident.Accident.DeleteEvent(eventId);
        }

        public object AccidentRelation(List<string> accidentList)
        {
            return DAL.MajorAccident.Accident.AccidentRelation(accidentList);
        }

        public bool CombineAccident(string accidentId, List<string> accidentList)
        {
            return DAL.MajorAccident.Accident.CombineAccident(accidentId,accidentList);
        }

        public bool ReleaseAccident(string accidentId)
        {
            return DAL.MajorAccident.Accident.ReleaseAccident(accidentId);
        }
    }
}
