using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using System.Data;
using Anchor.FA.Utility;

namespace Anchor.FA.BLL.BasicInfo
{
    public class TelBook
    {
        public object LoadAllTelBookByPage(int? page, int? rows, string order, string sort, int? OwnerID, string Name, string Type)
        {
            return DAL.BasicInfo.TelBook.LoadAllTelBookByPage(page, rows, order, sort, OwnerID, Name, Type);
        }
        public object Edit(int? id)
        {
            return DAL.BasicInfo.TelBook.Edit(id);
        }
        public bool Save(TTelBook entity)
        {
            return DAL.BasicInfo.TelBook.Save(entity);
        }
        public bool Delete(IList<int> idList)
        {
            return DAL.BasicInfo.TelBook.Delete(idList);
        }

        //public List<TZTelType> GetAllType()
        //{
        //    return DAL.BasicInfo.TelBook.GetAllType();
        //}


        #region 获取树形列表

        public List<C_TELTYPE_TREE> GetTree(int? exceptUnitId, int? OwnerID)
        {
            List<TZTelType> list = new List<TZTelType>();
            if (OwnerID.GetValueOrDefault() == 0)
            {
                list = GetType(null);
            }
            else
            {
                list = GetType((int)OwnerID);    
            }
            list.Remove(list.Find(t => t.编码 == exceptUnitId));

            List<C_TELTYPE_TREE> mtm = new List<C_TELTYPE_TREE>();

            foreach (TZTelType r in list)
            {
                mtm.Add(new C_TELTYPE_TREE
                {
                    id = r.编码.ToString(),
                    text = r.名称,
                    //iconCls="icon tu1501",
                    ParentID = r.上级编码.ToString()
                });
            }
            if (list.Count() == 0)
            {
                return null;
            }
            else
            {
                return GetTypeTree(mtm, "0");
            }
        }

        public List<TZTelType> GetType(int? OwnerID) 
        {
            return DAL.BasicInfo.TelBook.GetType(OwnerID);
        }

        private List<C_TELTYPE_TREE> GetTypeTree(List<C_TELTYPE_TREE> mtmList, string Pid)
        {
            List<C_TELTYPE_TREE> listTree = new List<C_TELTYPE_TREE>();

            List<C_TELTYPE_TREE> listParent = mtmList.Where(item => item.ParentID == Pid).ToList();

            foreach (C_TELTYPE_TREE t in listParent)
            {
                C_TELTYPE_TREE tm = new C_TELTYPE_TREE();

                tm.id = t.id;
                tm.text = t.text;
                tm.ParentID = t.ParentID;
                tm.iconCls = t.iconCls;

                tm.children = GetTypeTree(mtmList, t.id);


                listTree.Add(tm);
            }

            return listTree;
        }

        #endregion


        public object LoadAllTelTypeByPage(int page, int rows, string order, string sort, int OwnerID)
        {
            return DAL.BasicInfo.TelBook.LoadAllTelTypeByPage(page, rows, order, sort,OwnerID);
        }
        public bool SaveTelType(C_TelType entity)
        {
            return DAL.BasicInfo.TelBook.SaveTelType(entity);
        }

        public bool DeleteTelType(int id)
        {
            return DAL.BasicInfo.TelBook.DeleteTelType(id);
        }

        #region 导入
        /// <summary>
        /// 检查导入的Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string CheckUnsaleData(DataTable dt)
        {
            if (!dt.Columns.Contains("名称"))
            {
                return "Excel没有\"名称\"栏位";
            }

            if (!dt.Columns.Contains("电话分类"))
            {
                return "Excel没有\"电话分类\"栏位";
            }

            if (!dt.Columns.Contains("联系电话一"))
            {
                return "Excel没有\"联系电话一\"栏位";
            }

            if (!dt.Columns.Contains("分机一"))
            {
                return "Excel没有\"分机一\"栏位";
            }

            if (!dt.Columns.Contains("联系电话二"))
            {
                return "Excel没有\"联系电话二\"栏位";
            }

            if (!dt.Columns.Contains("分机二"))
            {
                return "Excel没有\"分机二\"栏位";
            }
            if (!dt.Columns.Contains("备注"))
            {
                return "Excel没有\"备注\"栏位";
            }
            if (!dt.Columns.Contains("顺序号"))
            {
                return "Excel没有\"顺序号\"栏位";
            }
            if (dt.Rows.Count == 0)
            {
                return "Excel有些行数据为空";
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Excel.FormatString(dt.Rows[i]["名称"].ToString()) == "")
                {
                    return "Excel中\"名称\"为必填项！";
                }
                if (Excel.FormatString(dt.Rows[i]["电话分类"].ToString()) == "")
                {
                    return "Excel中\"电话分类\"为必填项！";
                }
                if (Excel.FormatString(dt.Rows[i]["顺序号"].ToString()) == "")
                {
                    return "Excel中\"顺序号\"为必填项！";
                }
            }
            return string.Empty;
        }

        public bool InsertUnsale(DataTable dt)
        {
            return DAL.BasicInfo.TelBook.InsertUnsale(dt);
        }

        #endregion
    }
}
