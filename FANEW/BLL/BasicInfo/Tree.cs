using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.BasicInfo
{
    public class Tree
    {
        public string type,iconCls;
        public Tree()//默认没有样式
        {
            type = null;
            iconCls = null;
        }
        //这里借用下 C_ORGANIZE_TREE 对象
        public List<C_ORGANIZE_TREE> GetUnitTree(List<C_ORGANIZE_TREE> mtmList, string Pid)
        {
            List<C_ORGANIZE_TREE> listTree = new List<C_ORGANIZE_TREE>();

            List<C_ORGANIZE_TREE> listParent = mtmList.Where(item => item.ParentID == Pid).ToList();
            if (!listParent.Any())
                return null;

            foreach (C_ORGANIZE_TREE t in listParent)
            {
                C_ORGANIZE_TREE tm = new C_ORGANIZE_TREE();

                tm.id = t.id;
                tm.text = t.text;
                tm.ParentID = t.ParentID;
                tm.Type = type;
                tm.iconCls = iconCls;//若需要级别 的图标 上层只好改下图标样式了，这个默认本机到下级都是同一图标
                tm.children = GetUnitTree(mtmList, t.id);
                listTree.Add(tm);
            }

            return listTree;
        }
    }
}
