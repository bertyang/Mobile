using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Anchor.FA.Web
{
    public partial class DeskLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = Request.QueryString["Name"];//登录名（工号）
            string pwd = Request.QueryString["passWord"];//密码

            Response.Redirect(string.Format("/Account/DeskLogin/?Name={0}&PassWord={1}", Request.QueryString["Name"], Request.QueryString["PassWord"]));
 
        }
    }
}