using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiXiu.Model;

namespace AiXiu.IDAL
{
    public interface IUserService
    {
        bool IsSameMobileNo(string mobileNo);

        bool IsSameUserName(string userName);

        bool AddUser(TBLogins tBLogins);


        //登录  通过username获取该用户名的用户信息
        TBLogins GetLogins(string userName);
        TBLogins GetMobile(string mobileNumber);


        TBUsers GetTBUsers(int id);
        TBUsers EditWithoutAvatar(TBUsers tBUsers);
    }
}
