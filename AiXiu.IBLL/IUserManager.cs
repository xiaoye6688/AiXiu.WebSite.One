using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiXiu.Model;

namespace AiXiu.IBLL
{
    public interface IUserManager
    {
        OperResult RegUser(TBLogins tBLogins);//提示信息   成功是否 


        OperResult<TBUsers> Login(string userName, string pwd);
        OperResult<TBUsers> Mobile(string mobileNumber, string pwd);

        OperResult LoginResult(string userName, string pwd);
        OperResult<TBUsers>EditWithoutAvatar(TBUsers tBUsers);
    }
}
