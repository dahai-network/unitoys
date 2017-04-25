using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitoysWebSite.Models
{
    /// <summary>
    /// 内容类型
    /// </summary>
    public enum GlobalContentType
    {
        /// <summary>
        /// 招聘
        /// </summary>
        Recruitment = 0,
        /// <summary>
        /// 售后政策
        /// </summary>
        AfterSalesPolicy = 1,
        /// <summary>
        /// 售后渠道
        /// </summary>
        Aftermarket = 2,
        /// <summary>
        /// 常见问题
        /// </summary>
        CommonProblem = 3,
        /// <summary>
        /// 企业简介
        /// </summary>
        CompanyInfo = 4,
        /// <summary>
        /// 商务合作
        /// </summary>
        BusinessCooperation = 5,
        /// <summary>
        /// 企业文化
        /// </summary>
        CompanyCulture = 6
    }
}