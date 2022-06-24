using AiXiu.IBLL;
using AiXiu.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AiXiu.Model;
using AiXiu.Common;

namespace AiXiu.WebSite
{
    public partial class Reg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Session["yanzhengma"] != null)
                {
                    if (Session["yanzhengma"].ToString() == txtCaptcha.Text.Trim())
                    {
                        IUserManager userManager = new UserManager();
                        TBLogins tBLogins = new TBLogins()
                        {
                            MobileNumber = txtMobileNumber.Text,
                            Password = txtPassword.Text,
                            UserName = txtUserName.Text
                        };
                        OperResult operResult = userManager.RegUser(tBLogins);
                        if (operResult.StatusCode == StatusCode.Succeed)
                        {
                            PageExtensions.AlertAndRedirect(this, "regSuc", operResult.Message, "Login.aspx");
                        }
                        else
                        {
                            PageExtensions.Alert(this, "regSuc", operResult.Message);
                        }
                    }
                    else
                    {
                        PageExtensions.Alert(this, "regSuc", "验证码不正确");
                    }
                }
                else
                {
                    PageExtensions.Alert(this, "regSuc", "验证码不存在，请刷新验证");
                }
            }
        }
    }
}