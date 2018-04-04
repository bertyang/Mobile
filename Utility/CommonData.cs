using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.Utility
{
    public class CommonData
    {
        public static IList<G_DATA> GetDataByType(string type)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.G_DATA.Where(t => t.Type == type).OrderBy(t => t.Sequence).ToList();

            }
        }
    }
}
