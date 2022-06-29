using AiXiu.Model;
using AiXiu.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXiu.DAL
{
    public class UserService : IUserService
    {
        public bool AddUser(TBLogins tBLogins)
        {
            //1、login   得到id      2、创建user   赋值给uses  id
            //创建数据访问上下文
            AiXiuDB aiXiuModel = new AiXiuDB();
            //将得到的登录实体类型附加到ef容器中
            aiXiuModel.TBLogins.Add(tBLogins);
            //执行保存操作，判断如果添加成功继续添加用户表信息
            if (aiXiuModel.SaveChanges() > 0)
            {
                //创建用户表模型
                TBUsers tBUsersModel = new TBUsers()
                {
                    Id = tBLogins.Id,
                    Sex = 0,
                    CreationTime = DateTime.Now
                };
                //将得到的登录实体类型附加到ef容器中
                aiXiuModel.TBUsers.Add(tBUsersModel);
                //执行保存操作，判断是否成功
                if (aiXiuModel.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public TBUsers EditWithoutAvatar(TBUsers tBUsers)
        {
            AiXiuDB aiXiuDB = new AiXiuDB();
            var tbUserDBModel = aiXiuDB.TBUsers.SingleOrDefault(e => e.Id == tBUsers.Id);
            if (tbUserDBModel != null)
            {
                tbUserDBModel.ADDress = tBUsers.ADDress;
                tbUserDBModel.NickName = tBUsers.NickName;
                tbUserDBModel.Birthday = tBUsers.Birthday;
                tbUserDBModel.Sex = tBUsers.Sex;
                tbUserDBModel.Hobby = tBUsers.Hobby;
                aiXiuDB.SaveChanges();
                return tbUserDBModel;
            }
            return tbUserDBModel;
        }
        public TBUsers EditAvatar(TBUsers tBUsers)
        {
            AiXiuDB aiXiuModel = new AiXiuDB();
            var tbUserDBModel = aiXiuModel.TBUsers.FirstOrDefault(t => t.Id == tBUsers.Id);
            if (tbUserDBModel != null)
            {
                tbUserDBModel.Avatar = tBUsers.Avatar;
                if (aiXiuModel.SaveChanges() > 0)
                {
                    return tbUserDBModel;
                }
                return null;
            }
            return null;
        }

        public TBLogins GetLogins(string userName)
        {
            AiXiuDB aiXiuModel = new AiXiuDB();
            var model = aiXiuModel.TBLogins.SingleOrDefault(e => e.UserName == userName);
            return model;
        }

        public TBLogins GetMobile(string mobileNumber)
        {
            AiXiuDB aiXiuModel = new AiXiuDB();
            var model = aiXiuModel.TBLogins.SingleOrDefault(e => e.MobileNumber == mobileNumber);
            return model;
        }

        public TBUsers GetTBUsers(int id)
        {
            AiXiuDB aiXiuModel = new AiXiuDB();
            var model = aiXiuModel.TBUsers.SingleOrDefault(e => e.Id == id);
            return model;
        }

        /// <summary>
        /// 手机号判断重复
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <returns>true找到重复</returns>
        public bool IsSameMobileNo(string mobileNo)
        {
            AiXiuDB aiXiuModel = new AiXiuDB();
            var model = aiXiuModel.TBLogins.SingleOrDefault(e => e.MobileNumber == mobileNo);
            if (model != null)
            {
                return true;
            }
            return false;
        }

        public bool IsSameUserName(string userName)
        {
            AiXiuDB aiXiuModel = new AiXiuDB();
            var model = aiXiuModel.TBLogins.SingleOrDefault(e => e.UserName == userName);
            if (model != null)
            {
                return true;
            }
            return false;
        }

    }
}
