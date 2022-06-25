using AiXiu.BLL;
using AiXiu.Common;
using AiXiu.IBLL;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AiXiu.WebSite
{
    public partial class AvatarEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAvatar_Click(object sender, EventArgs e)
        {
            TBUsers tBUsers = IdentityManager.ReadUser();
            IUserManager userManager = new UserManager();
            tBUsers.Avatar = hfAvatar.Value;
            OperResult<TBUsers> operResult = userManager.EditAvatar(tBUsers);
            if (operResult.StatusCode == StatusCode.Succeed)
            {
                IdentityManager.SaveUser(operResult.Result);
                PageExtensions.AlertAndRedirect(this, "editPerSuc", operResult.Message, "Personal.aspx");
            }
            else
            {
                PageExtensions.Alert(this, "editPerError", operResult.Message);
            }

        }
       
    }
}