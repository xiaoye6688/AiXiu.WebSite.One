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
                using (StreamReader sr = new StreamReader(fileStream,Encoding.UTF8))
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
                //页面首次加载
                BindProvince();
                
            }
        }
        protected void BindProvince() {
            //3.1、绑定某个省份的下拉框列表，下拉框设置数据源等属性，选中传入省份
            ddlProvince.DataSource = district.Provinces;
            ddlProvince.DataTextField = "ProvinceName";
            ddlProvince.DataValueField = "ProvinceName";
            ddlProvince.DataBind();
            ddlProvince.Items.Insert(0, ddlTips);
        }
        protected void btnProfile_Click(object sender, EventArgs e)
        {

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
                this.ddlCity.SelectedIndex = 0;
            }
            else
            {
                this.ddlCity.SelectedValue = cityName;
            }
        }
    }
}