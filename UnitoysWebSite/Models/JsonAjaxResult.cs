using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitoysWebSite.Models
{
    public class JsonAjaxResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }
    }
}