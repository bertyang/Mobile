using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;


namespace Anchor.FA.BLL.Organize
{
    /// <summary>
    /// 功能项业务类
    /// </summary>
    public class Action
    {
        #region 贾明

        #region 查询方法

        #region 获取功能项菜单
        /// <summary>
        /// 获取功能项菜单
        /// </summary>
        /// <param name="superiorID">功能项ID</param>
        /// <param name="userId">人员ID</param>
        /// <returns>List</returns>
        public List<C_MENU_TREE> GetMenu(string superiorID, string userId)
        {
            List<B_ACTION> list = DAL.Organize.Action.GetMenu(superiorID,userId);
            if (list.Count() == 0)
            {
                return null;
            }

            List<C_MENU_TREE> mtm = (from r in list
                                    select
                                    new C_MENU_TREE
                                    {
                                        id = r.ID.ToString(),
                                        text = r.Remark,
                                        url = r.Url,
                                        ParentID = r.ParentID.ToString(),
                                        iconCls = r.Icon
                                    }).ToList();
         
            return GetMenuTree(mtm, "0");
        }

        public List<C_MENU_TREE> GetMenu(string superiorID)
        {
            List<B_ACTION> list = DAL.Organize.Action.GetMenu(superiorID);

            List<C_MENU_TREE> mtm = new List<C_MENU_TREE>();

            foreach (B_ACTION r in list)
            {
                mtm.Add(new C_MENU_TREE
                {
                    id = r.ID.ToString(),
                    text = r.Remark,
                    url = r.Url,
                    ParentID = r.ParentID.ToString(),
                    //iconCls = r.Icon
                });
            }

            if (list.Count() == 0)
            {
                return null;
            }
            else
            {
                return GetMenuTree(mtm, "0");
            }

        }

        private List<C_MENU_TREE> GetMenuTree(List<C_MENU_TREE> mtmList, string Pid)
        {
            List<C_MENU_TREE> listTree = new List<C_MENU_TREE>();

            List<C_MENU_TREE> listParent = mtmList.Where(item => item.ParentID == Pid).ToList();

            foreach (C_MENU_TREE t in listParent)
            {
                C_MENU_TREE tm = new C_MENU_TREE();
                tm.id = t.id;
                tm.text = t.text;
                tm.iconCls = t.iconCls;
                tm.ParentID = t.ParentID;
                tm.url = t.url;

                if (mtmList.Where(item => item.ParentID == t.id).Any())
                {
                    tm.children = GetMenuTree(mtmList, t.id);
                }
                else
                {
                    tm.children = new List<C_MENU_TREE>();
                }
                listTree.Add(tm);
            }
            return listTree;
        }



        public static List<C_MENU_TREE> GetMenuRange(string superiorID)
        {
            List<C_MENU_TREE> list = DAL.Organize.Action.GetMenuRange(superiorID);

            //List<C_MENU_TREE> mtm = new List<C_MENU_TREE>();

            //foreach (B_ACTION r in list)
            //{
            //    mtm.Add(new C_MENU_TREE
            //    {
            //        id = r.ID.ToString(),
            //        text = r.Remark,
            //        url = r.Url,
            //        ParentID = r.ParentID.ToString(),
            //        //iconCls = r.Icon
            //    });
            //}

            if (list.Count() == 0)
            {
                return null;
            }
            else
            {
                return GetMenuRangeTree(list, "a-0");
            }

        }

        private static List<C_MENU_TREE> GetMenuRangeTree(List<C_MENU_TREE> mtmList, string Pid)
        {
            List<C_MENU_TREE> listTree = new List<C_MENU_TREE>();

            List<C_MENU_TREE> listParent = mtmList.Where(item => item.ParentID == Pid).ToList();
            if (listParent.Any())//如果是按钮模块权限自动添加 一个默认的 页基本权限
            {
                C_MENU_TREE c =listParent.First();
                if (!string.IsNullOrEmpty(c.id) && c.id.Substring(0,2) == "r-")
                {
                    C_MENU_TREE tm = new C_MENU_TREE();
                    tm.id = "nullid";
                    tm.text = "页基本权限";
                    //tm.iconCls = t.iconCls;
                    tm.ParentID = Pid;
                    tm.children = new List<C_MENU_TREE>();
                    listTree.Add(tm);
                }
            }


            foreach (C_MENU_TREE t in listParent)
            {
                C_MENU_TREE tm = new C_MENU_TREE();

                tm.id = t.id;
                tm.text = t.text;
                //tm.iconCls = t.iconCls;
                tm.ParentID = t.ParentID;
                //tm.url = t.url;

                if (mtmList.Where(item => item.ParentID == t.id).Any())
                {
                    tm.children = GetMenuRangeTree(mtmList, t.id);
                }
                else
                {
                    tm.children = new List<C_MENU_TREE>();
                }
                listTree.Add(tm);
            }
            return listTree;
        }




        #endregion

        #endregion

        #endregion
        public object LoadAllActionByPage(int page, int rows, string order, string sort)
        {
             return DAL.Organize.Action.LoadAllActionByPage(page, rows, order, sort);
        }
        public object Edit(int? id)
        {
            return DAL.Organize.Action.Edit(id);
        }
        public bool Save(B_ACTION entity)
        {
            return DAL.Organize.Action.Save(entity);
        }
        public bool Delete(IList<int> idList)
        {
            return DAL.Organize.Action.Delete(idList);
        }
        public List<B_ACTION> GetAllAction()
        {
            return DAL.Organize.Action.GetAllAction();
        }
    }
}
