using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AiXiu.Common;
using AiXiu.Model;
using Newtonsoft.Json;
using System.Text;
using AiXiu.BLL;
using AiXiu.IBLL;

namespace AiXiu.WebSite
{
    public partial class PersonalEdit : System.Web.UI.Page
    {
        private District district;
        private ListItem ddlTips = new ListItem("-请选择-", "");
        public PersonalEdit()
        {
            string path = Server.MapPath("~/App_Data/district.json");
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string json = sr.ReadToEnd();
                    district = JsonConvert.DeserializeObject<District>(json);
                }

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //读取用户资料
                TBUsers tBUsers = IdentityManager.ReadUser();
                if (tBUsers != null)
                {
                    txtNickName.Text = tBUsers.NickName;
                    ddlSex.SelectedValue = tBUsers.Sex.ToString();


                    txtBirthday.Text = tBUsers.Birthday.HasValue ? tBUsers.Birthday.Value.ToString("M") : "";
                    if (!string.IsNullOrWhiteSpace(tBUsers.Hobby))
                    {
                        string[] hobbyList = tBUsers.Hobby.Split(' ');//篮球 看电影 网络游戏   string[] hobbyList ={ "篮球","看电影","网络游戏"}

                        foreach (ListItem item in cblHobby.Items)
                        {
                            if (hobbyList.Contains(item.Value))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    
                    
                    
                    if (!string.IsNullOrWhiteSpace(tBUsers.ADDress))
                    {
                        string[] addressList = tBUsers.ADDress.Split(' ');
                        //省份城市 默认项 将数据库的地址展示在页面
                        if (addressList.Length > 1)
                        {
                            string proName = addressList[0];
                            string cityName = addressList[1];
                            BindProvince(proName);
                            BindCity(proName, cityName);
                        }
                        else
                        {
                            BindProvince();
                        }
                    }
                    else
                    {
                        BindProvince();
                    }

                }
                else
                {
                    Response.Redirect("/Login.aspx");
                }

                //页面首次加载


            }
        }
        protected void BindProvince(string provinceName = null)
        {
            //3.1、绑定某个省份的下拉框列表，下拉框设置数据源等属性，选中传入省份
            ddlProvince.DataSource = district.Provinces;
            ddlProvince.DataTextField = "ProvinceName";
            ddlProvince.DataValueField = "ProvinceName";
            ddlProvince.DataBind();
            ddlProvince.Items.Insert(0, ddlTips);

            if (!string.IsNullOrWhiteSpace(provinceName))
            {
                ddlProvince.SelectedValue = provinceName;
            }
        }
        protected void btnProfile_Click(object sender, EventArgs e)
        {
            TBUsers tBUsers = IdentityManager.ReadUser();
            tBUsers.ADDress = $"{ddlProvince.SelectedValue} {ddlCity.SelectedValue}";
            tBUsers.NickName = txtNickName.Text;
            if (!string.IsNullOrWhiteSpace(txtBirthday.Text))
            {
                tBUsers.Birthday = DateTime.Parse(txtBirthday.Text);
            }
            tBUsers.Sex = int.Parse(ddlSex.SelectedValue);
            List<string> hobbyList = new List<string>();

            foreach (ListItem item in cblHobby.Items)
            {
                if (item.Selected)
                {
                    hobbyList.Add(item.Value);
                }
            }
            if (hobbyList.Count > 0)
            {
                string hobbyStr = string.Join(" ", hobbyList);
                tBUsers.Hobby = hobbyStr;
            }
            IUserManager userManager = new UserManager();
            OperResult<TBUsers> operResult = userManager.EditWithoutAvatar(tBUsers);
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
        /// <summary>
        /// 选择省份时的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取选中的省份名称
            string provinceName = ddlProvince.SelectedValue;
            //根据省份名称获取城市列表
            BindCity(provinceName);
        }
        protected void BindCity(string provinceName, string cityName = null)
        {
            if (string.IsNullOrWhiteSpace(provinceName))
            {
                ddlCity.Items.Clear();
                ddlCity.Items.Add(ddlTips);
                return;
            }
            Province province = district.Provinces.SingleOrDefault(p => p.ProvinceName == provinceName);
            if (province == null)
            {
                this.ddlCity.Items.Clear();
                this.ddlCity.Items.Add(ddlTips);
                return;
            }
            //绑定省份下的城市列表
            this.ddlCity.DataSource = province.Citys;
            this.ddlCity.DataTextField = "CityName";
            this.ddlCity.DataValueField = "CityName";
            this.ddlCity.DataBind();
            this.ddlCity.Items.Insert(0, ddlTips);
            //设置城市默认选中
            if (string.IsNullOrWhiteSpace(cityName))
            {
                //ddlCity.SelectedValue = cityName;
                this.ddlCity.SelectedIndex = 0;
            }
            else
            {
                this.ddlCity.SelectedValue = cityName;
            }
        }
    }
}