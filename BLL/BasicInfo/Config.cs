using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.BasicInfo
{
    public class Config
    {
        public object LoadAllActionByPage(int page, int rows, string order, string sort)
        {
            return DAL.BasicInfo.Config.LoadAllActionByPage(page, rows, order, sort);
        }
        public object Edit(string key)
        {
            return DAL.BasicInfo.Config.Edit(key);
        }
        public bool Save(G_CONFIG entity)
        {
            return DAL.BasicInfo.Config.Save(entity);
        }

    }
}
