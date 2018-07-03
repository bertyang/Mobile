using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;


namespace Anchor.FA.Utility
{
    public class Config
    {
        public static string GetKeyValue(string key)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                G_CONFIG config = dbContext.G_CONFIG.FirstOrDefault(t => t.Key == key);

                if (config != null)
                {
                    return config.Value;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
