//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnitoysWebSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UT_News
    {
        public System.Guid ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public System.DateTime NewsDate { get; set; }
        public string Publisher { get; set; }
        public string Content { get; set; }
        public int NewsType { get; set; }
        public int CreateDate { get; set; }
        public bool IsTop { get; set; }
    }
}
