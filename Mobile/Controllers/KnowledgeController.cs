using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;
using System.Web.UI.WebControls;


namespace Anchor.FA.Mobile.Controllers
{
    public class KnowledgeController : BaseController
    {
        #region 化学常识
        /// <summary>
        ///左侧列表
        /// </summary>
        /// <param name="chineseName">中文名称</param>
        /// <param name="englishName">英文名称</param>
        /// <returns></returns>
        public ActionResult Danger(string chineseName, string englishName)
        {
            BLL.Knowledge.Danger danger = new BLL.Knowledge.Danger();

            //radio框的值
            string type = Request.QueryString["type"];
            this.ViewData["type"] = type == null?"1":type;

            IList<TDanger> cot = danger.GetDanger(chineseName,englishName);
            this.ViewData["danger"] = cot;

            //给详细内容传递的id
            if (cot.Count != 0)
            {
                this.ViewData["id"] = cot[0].序号;
            }
            else
            {
                this.ViewData["id"] = "1";
            }
            
            return View();
        }

        /// <summary>
        /// 右侧详细内容
        /// </summary>
        /// <param name="id">化学危险品序号</param>
        /// <returns></returns>
        public ActionResult DangerDetail(string id)
        {
            BLL.Knowledge.Danger danger = new BLL.Knowledge.Danger();

            if (id != null)
            {
                TDanger cot = danger.GetDangerById(id);

                this.ViewData["danger"] = cot;
                this.ViewData["Character"] = cot.性状;
                this.ViewData["Taboo"] = cot.禁忌;
                this.ViewData["Synonym"] = cot.同义词;
                this.ViewData["Fatalness"] = cot.危险性;
                this.ViewData["FirstAidMeasure"] = cot.急救措施;
                this.ViewData["PreventMeasure"] = cot.防护措施;
                this.ViewData["Store"] = cot.储存;
                this.ViewData["LeakOutDisposal"] = cot.泄露处理;
                this.ViewData["TransportDemand"] = cot.运输要求;
                this.ViewData["Postscript"] = cot.附注;
                this.ViewData["LimitAndMensurateInAir"] = cot.空气中的允许极限及测定;
                this.ViewData["LimitAndMensurateInWater"] = cot.水中的允许极限及测定;

            }
            return View();
        }
        #endregion

        #region 毒品常识
        /// <summary>
        /// 左侧列表
        /// </summary>
        /// <param name="chineseName">中文名称</param>
        /// <param name="englishName">英文名称</param>
        /// <returns></returns>
        public ActionResult Poison(string chineseName, string englishName) 
        {
            BLL.Knowledge.Poison poison= new BLL.Knowledge.Poison();

            //radio框的值
            string type = Request.QueryString["type"];
            this.ViewData["type"] = type == null ? "1" : type;

            IList<TPoison> cot = poison.GetPoison(chineseName, englishName);
            this.ViewData["poison"] = cot;

            for (int i = 0; i < cot.Count; i++)
            {
                cot[i].英文名称 = cot[i].英文名称 == null ? cot[i].序号 : cot[i].英文名称;
            }

            //给详细内容传递的id
            if (cot.Count != 0)
            {
                this.ViewData["id"] = cot[0].序号;
            }
            else
            {
                this.ViewData["id"] = "02100600";
            }

            return View();
        }

        /// <summary>
        /// 右侧详细内容
        /// </summary>
        /// <param name="id">毒品序号</param>
        /// <returns></returns>
        public ActionResult PoisonDetail(string id)  
        {
            BLL.Knowledge.Poison poison = new BLL.Knowledge.Poison();

            TPoison cot = poison.GetPoisonById(id);

            this.ViewData["poison"] = cot;

            this.ViewData["Character"] = cot.性质;
            this.ViewData["Fatalness"] = cot.毒性;
            this.ViewData["Characteristic"] = cot.特点;
            this.ViewData["ToxicEffect"] = cot.毒理作用;
            this.ViewData["PoisoningManifestation"] = cot.中毒表现;
            this.ViewData["DiagnoseMainPoint"] = cot.诊断要点;
            this.ViewData["CureMainPoint"] = cot.救治要点;
            this.ViewData["Toxicology"] = cot.毒理;
            this.ViewData["Pharmacokinetic"] = cot.药动学;
            this.ViewData["LabExamine"] = cot.实验室检查;
            this.ViewData["Pharmacology"] = cot.药理;
            this.ViewData["Etiology"] = cot.病原学;
            this.ViewData["Epidemiology"] = cot.流行病学;
            this.ViewData["Remark"] = cot.备注;
            return View();
        }
        #endregion

        #region 应急处理
        /// <summary>
        /// 左侧列表
        /// </summary>
        /// <param name="Name">疾病名称</param>
        /// <returns></returns>
        public ActionResult CureRule(string Name) 
        {
            BLL.Knowledge.CureRule cure = new BLL.Knowledge.CureRule();
            IList<TCureRule> cot = cure.GetCureRule(Name);
            this.ViewData["cureRule"] = cot;

            //给详细内容传递的id
            if (cot.Count != 0)
            {
                this.ViewData["id"] = cot[0].编码;
            }
            else
            {
                this.ViewData["id"] = "0001";
            }

            return View();
        }

        /// <summary>
        /// 右侧详细内容
        /// </summary>
        /// <param name="id">疾病编码</param>
        /// <returns></returns>
        public ActionResult CureRuleDetail(string id)   
        {
            BLL.Knowledge.CureRule cure = new BLL.Knowledge.CureRule();

            TCureRule cot = cure.GetCureRuleById(id);

            if (cot != null)
            {
                string description = "疾病名称：\n";
                description += cot.疾病名称 + "\n\n";

                description += "病症描述：\n";
                description += cot.病症描述 + "\n\n";

                description += "诊断要点：\n";
                description += cot.诊断要点 + "\n\n";

                description += "处理方法：\n";
                description += cot.即刻处理 + "\n\n";

                description += "转运条件：\n";
                description += cot.转运条件 + "\n\n";

                this.ViewData["Description"] = description;
            }

            return View();
        }
        #endregion

        #region 应急预案
        public ActionResult Scheme() 
        {
            return View();
        }
        #endregion
    }
}
