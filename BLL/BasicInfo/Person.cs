using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using System.Collections;

namespace Anchor.FA.BLL.BasicInfo
{
    public class Person
    {

        /// <summary>
        /// 根据人员类型(-1全部)和分站编码(-1或""或"--请选择--"时取全部)和是否有效(-1全部、0无效、1有效)获取人员编码、姓名列表
        /// </summary>
        /// <param name="personType">人员类型(-1全部)</param>
        /// <param name="stationCode">分站编码(-1或""或"--请选择--"时取全部)</param>
        /// <param name="isValid">是否有效(-1全部、0无效、1有效)</param>
        /// <returns></returns>
        public static object GetPersonList(int personType, string stationCode, 
            int isValid,Anchor.FA.Utility.ButtonPower p, C_WorkerDetail userDetail)
        {
            return Anchor.FA.DAL.BasicInfo.Person.GetPersonList(personType, stationCode, isValid, p, userDetail);
        }
        public static List<TPerson> GetPersons(int[] personType, string[] stationCode, string ambCode, string branchID, bool? isValid)
        {
            return Anchor.FA.DAL.BasicInfo.Person.GetPersons(personType, stationCode, ambCode, branchID, isValid);
        }

    }
}
