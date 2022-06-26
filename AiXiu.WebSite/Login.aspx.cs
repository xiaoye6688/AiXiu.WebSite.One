using AiXiu.BLL;
using AiXiu.Common;
using AiXiu.IBLL;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AiXiu.WebSite
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            IUserManager userManager = new UserManager();
            
            OperResult<TBUsers> operResult = userManager.Login(txtUserName.Text, txtPassword.Text);
            if (operResult.StatusCode==StatusCode.Succeed)
            {
                TBUsers tBUsers = operResult.Result;
                IdentityManager.SaveUser(tBUsers);
                string url = "/Default.aspx";
                if (Request.QueryString["ReturnUrl"]==null)
                {
                    url = Request.QueryString["ReturnUrl"];
                }
                PageExtensions.AlertAndRedirect(this, "regSucces", operResult.Message, url);
            }
            //IUserManager userManager = new UserManager();

            //OperResult operResult = userManager.LoginResult(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            //if (operResult.StatusCode == StatusCode.Succeed)
            //{
            //    PageExtensions.AlertAndRedirect(this, "regSucces", operResult.Message, "Index.aspx");
            //}
            else
            {
                PageExtensions.Alert(this, "regSucces", operResult.Message);
            }
        }
    }
}