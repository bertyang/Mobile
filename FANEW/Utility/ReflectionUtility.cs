using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Anchor.FA.Utility
{
    public class ReflectionUtility
    {

        public static void CopyObjectProperty<T>(T tSource, T tDestination) where T : class
        {
            //获得所有property的信息
            PropertyInfo[] properties = tSource.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                p.SetValue(tDestination, p.GetValue(tSource, null), null);//设置tDestination的属性值              
            }
        }

        /// <summary>
        /// 将tSource中的所有属性赋值到tDestination有对应类型属性的值里
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tSource"></param>
        /// <param name="tDestination"></param>
        public static void CopyObjectPropertyToObjectProperty<T, U>(T tSource, U tDestination)
            where T : class
            where U : class
        {
            //获得所有property的信息
            PropertyInfo[] poSource = tSource.GetType().GetProperties();
            //List<PropertyInfo> poSource = tSource.GetType().GetProperties().ToList();
            PropertyInfo[] poDestination = tDestination.GetType().GetProperties();
            //PropertyInfo[] poSource = tSource.GetType().GetProperties();
            //PropertyInfo[] poDestination = tSource.GetType().GetProperties();

            //List<PropertyInfo> DestinationLs = tDestination.GetType().GetProperties().ToList();


            foreach (PropertyInfo s in poSource)
            {
                foreach (PropertyInfo d in poDestination)
                {
                    if (s.Name == d.Name && s.PropertyType == d.PropertyType)
                    {
                        d.SetValue(tDestination, s.GetValue(tSource, null), null);//设置tDestination的属性值 
                        //poDestination.Remove(d);
                        break;
                    }
                }
            }
            //foreach (PropertyInfo d in poDestination)
            //{
            //    foreach (PropertyInfo s in poSource)
            //    {
            //        if (s.Name == d.Name && s.PropertyType == d.PropertyType)
            //            d.SetValue(tDestination, s.GetValue(tSource, null), null);//设置tDestination的属性值 
            //        poSource.Remove(s);
            //        break;
            //    }
            //}
        }
    }
}
