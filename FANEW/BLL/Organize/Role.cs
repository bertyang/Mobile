using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.Organize
{
    public class Role
    {
        public object LoadAllRoleByPage(int page, int rows, string order, string sort)
        {
            return DAL.Organize.Role.LoadAllRoleByPage(page, rows, order, sort);
        }
        public object LoadAllRoleByPage()
        {
            return DAL.Organize.Role.LoadAllRoleByPage();
        }
        public object Edit(int? id)
        {
            return DAL.Organize.Role.Edit(id);
        }
        public bool Save(B_ROLE entity)
        {
            return DAL.Organize.Role.Save(entity);
        }
        public bool Delete(IList<int> idList)
        {
            return DAL.Organize.Role.Delete(idList);
        }
        public List<int> LoadRoleAction(int roleId)
        {
            return DAL.Organize.Role.LoadRoleAction(roleId);
        }
        public static List<string> LoadRoleActionRange(int roleId)
        {
            return DAL.Organize.Role.LoadRoleActionRange(roleId);
        }
        public bool SaveRoleAction(int roleId, IList<int> listActionId)
        {
            return DAL.Organize.Role.SaveRoleAction(roleId, listActionId);
        }
        public static bool SaveRoleAction(int roleId, IList<int> listActionId, IList<string> listRangeId)
        {
            return DAL.Organize.Role.SaveRoleAction(roleId, listActionId, listRangeId);
        }

        public IList<B_ROLE> LoadAllRole()
        {
            return DAL.Organize.Role.LoadAllRole();
        }
        //public object EditRoleAction(int id)
        //{
        //    return DAL.Organize.Role.EditRoleAction(id);
        //}
        //public bool DeleteRoleAction(IList<string> idList)
        //{
        //    return DAL.Organize.Role.DeleteRoleAction(idList);
        //}
    }
}
