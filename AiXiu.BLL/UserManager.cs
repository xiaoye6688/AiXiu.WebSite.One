using AiXiu.Common;
using AiXiu.IBLL;
using AiXiu.DAL;
using AiXiu.IDAL;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXiu.BLL
{
    public class UserManager: IUserManager
    {
        public OperResult<TBUsers> Login(string userName, string pwd)
        {
            TBUsers tBUsers = new TBUsers();
            if (string.IsNullOrWhiteSpace(userName))
            {
                //为空   所以直接返回 错误信息
                return OperResult<TBUsers>.Failed("用户名不能为空");
            }
            if (string.IsNullOrWhiteSpace(pwd))
            {
                //为空   所以直接返回 错误信息
                return OperResult<TBUsers>.Failed("密码不能为空");
            }
            IUserService userService = new UserService();
            TBLogins tBLogins = userService.GetLogins(userName);
            if (tBLogins != null)
            {
                //加密处理
                SHAEncryption sHAEncryption = new SHAEncryption();
                pwd = sHAEncryption.SHA1Encrypt(pwd);
                if (pwd.Equals(tBLogins.Password))
                {
                    tBUsers = userService.GetTBUsers(tBLogins.Id);
                    return OperResult<TBUsers>.Succeed(tBUsers);
                }
                else
                {
                    return OperResult<TBUsers>.Failed("密码不正确");
                }
            }
            else
            {
                return OperResult<TBUsers>.Failed("该用户不存在");
            }
        }

        public OperResult LoginResult(string userName, string pwd)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                //为空   所以直接返回 错误信息
                return OperResult.Failed("用户名不能为空");
            }
            if (string.IsNullOrWhiteSpace(pwd))
            {
                //为空   所以直接返回 错误信息
                return OperResult.Failed("密码不能为空");
            }
            IUserService userService = new UserService();
            TBLogins tBLogins = userService.GetLogins(userName);
            if (tBLogins != null)
            {
                //加密处理
                SHAEncryption sHAEncryption = new SHAEncryption();
                pwd = sHAEncryption.SHA1Encrypt(pwd);
                if (pwd.Equals(tBLogins.Password))
                {
                    return OperResult.Succeed();
                }
                else
                {
                    return OperResult.Failed("密码不正确");
                }
            }
            else
            {
                return OperResult.Failed("该用户不存在");
            }
        }

        public OperResult RegUser(TBLogins tBLogins)
        {
            //1、判空
            //2、判重处理
            //3、加密
            //4、添加
            if (string.IsNullOrWhiteSpace(tBLogins.UserName))
            {

                //为空   所以直接返回 错误信息
                return OperResult.Failed("用户名不能为空");
            }
            if (string.IsNullOrWhiteSpace(tBLogins.MobileNumber))
            {
                //为空   所以直接返回 错误信息
                return OperResult.Failed("手机号不能为空");
            }
            if (string.IsNullOrWhiteSpace(tBLogins.Password))
            {
                //为空   所以直接返回 错误信息
                return OperResult.Failed("密码不能为空");
            }
            //判别重复处理
            IUserService userService = new UserService();
            if (userService.IsSameMobileNo(tBLogins.MobileNumber))
            {
                return OperResult.Failed("手机号已注册");
            }
            if (userService.IsSameUserName(tBLogins.UserName))
            {
                return OperResult.Failed("用户名已注册");
            }
            //加密处理
            SHAEncryption sHAEncryption = new SHAEncryption();
            tBLogins.Password = sHAEncryption.SHA1Encrypt(tBLogins.Password);
            //添加操作
            if (userService.AddUser(tBLogins))
            {
                return OperResult.Succeed();
            }
            else
            {
                return OperResult.Failed();
            }
        }
    
}
}
