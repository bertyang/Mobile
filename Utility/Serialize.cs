using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Anchor.FA.Utility
{
    /// <summary>
    /// 序列化和反序列化公共方法
    /// </summary>
    public class Serialize
    {
        /// <summary>
        /// 序列化object转换为string对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        static public string ScriptSerialize<T>(T t)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(t);
        }

        /// <summary>
        /// 将string对象反序列化为object对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        static public T ScriptDeserialize<T>(string strJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(strJson);
        }

        /// <summary>
        /// 将string对象反序列化为list对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>

        //static public List<T> ScriptDeserializeToList<T>(string strJson)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    List<T> objList = serializer.Deserialize<List<T>>(strJson);
        //    return objList;
        //}
    }
}
