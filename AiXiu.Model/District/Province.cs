using System.Collections.Generic;

namespace AiXiu.Model
{
    /// <summary>
    /// 省份类
    /// </summary>
    public class Province
    {
        /// <summary>
        /// 省份名
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市列表
        /// </summary>
        public List<City> Citys { get; set; }
    }
}