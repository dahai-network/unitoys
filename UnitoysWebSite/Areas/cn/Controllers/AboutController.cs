using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Unitoys.Core;
using UnitoysWebSite.Models;

namespace UnitoysWebSite.Areas.cn.Controllers
{
    public class AboutController : Controller
    {
        //
        // GET: /cn/About/
        public ActionResult Index()
        {
            return ReturnListIndexView("Index");
        }
        /// <summary>
        /// 企业文化
        /// </summary>
        /// <returns></returns>
        public ActionResult Culture()
        {
            return ReturnListIndexView("Culture");
        }

        public ActionResult Join()
        {
            return ReturnListIndexView("Join");
        }
        public ActionResult Business()
        {
            return ReturnListIndexView("Business");
        }

        private ActionResult ReturnListIndexView(string type)
        {
            ViewBag.Type = type;
            List<UT_GlobalContent> list = new List<UT_GlobalContent>(){
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.CompanyInfo),
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.CompanyCulture),
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.Recruitment),
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.BusinessCooperation)
            };
            return View("Index", list);
        }

        /// <summary>
        /// 代理商申请
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyAgent()
        {
            return View(new ModelAgent());
        }

        [HttpPost]
        public async Task<JsonResult> ApplyAgent(ModelAgent model)
        {
            JsonAjaxResult result = new JsonAjaxResult();

            if (ModelState.IsValid)
            {
                model.CompanyName = model.CompanyName.Trim();
                using (UnitoysEntities db = new UnitoysEntities())
                {
                    if (db.UT_Agent.Any(x => x.CompanyName == model.CompanyName && x.Status == 0))
                    {
                        result.Success = false;
                        result.Msg = "申请处理中，请勿重复提交！";
                    }
                    else
                    {
                        UT_Agent entity = new UT_Agent();
                        entity.ID = Guid.NewGuid();
                        entity.CompanyName = model.CompanyName;
                        entity.Area = model.Area;
                        entity.RegisteredAddress = model.RegisteredAddress;
                        entity.CorporateRepresentative = model.CorporateRepresentative;
                        entity.CompanyRegistrationTime = model.CompanyRegistrationTime.Value;
                        entity.CorporationNature = (int)model.CorporationNature;
                        entity.MarketPersonnelNum = model.MarketPersonnelNum;
                        entity.SalerPersonnelNum = model.SalerPersonnelNum;
                        entity.AfterSalesPersonalNum = model.AfterSalesPersonalNum;
                        entity.ScopeBusiness = model.ScopeBusiness;
                        entity.SalesArea = model.SalesArea;
                        entity.AnnualSales = model.AnnualSales;
                        entity.MainBusiness = string.Join(",", model.MainBusiness);
                        entity.MainClientCategory = string.Join(",", model.MainClientCategory);
                        entity.Contact = model.Contact;
                        entity.ContactMobilePhone = model.ContactMobilePhone;
                        entity.EMail = model.EMail;
                        entity.QQWeChat = model.QQWeChat;
                        entity.Tel = model.Tel;
                        entity.FAX = model.FAX;
                        entity.CompanyWebSite = model.CompanyWebSite;
                        entity.IsAgentOtherProducts = model.IsAgentOtherProducts.Value;
                        entity.OtherProducts = model.OtherProducts;
                        entity.DevelopMarketAssumptions = model.DevelopMarketAssumptions;
                        entity.CompanyChannelsResources = model.CompanyChannelsResources;
                        entity.CooperationSuggestion = model.CooperationSuggestion;
                        entity.CreateDate = CommonHelper.GetDateTimeInt();

                        db.UT_Agent.Add(entity);

                        if (await db.SaveChangesAsync() > 0)
                        {
                            result.Success = true;
                            result.Msg = "提交成功！";
                        }
                        else
                        {
                            result.Success = false;
                            result.Msg = "提交失败！";
                        }
                    }


                }
            }
            else
            {
                var error = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault().Errors.FirstOrDefault().ErrorMessage.Replace("字段", "").Replace("是必需的", "不能为空");

                result.Success = false;
                result.Msg = error;
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> ContactUs(ModelContactUS model)
        {
            JsonAjaxResult result = new JsonAjaxResult();

            if (ModelState.IsValid)
            {
                model.Name = model.Name.Trim();
                using (UnitoysEntities db = new UnitoysEntities())
                {
                    if (db.UT_ContactUS.Any(x => x.Name == model.Name && x.Status == 0))
                    {
                        result.Success = false;
                        result.Msg = "信息处理中，请勿重复提交！";
                    }
                    else
                    {
                        UT_ContactUS entity = new UT_ContactUS();
                        entity.ID = Guid.NewGuid();
                        entity.Name = model.Name;
                        entity.MailAddress = model.EMail;
                        entity.Subject = model.Subject;
                        entity.Content = model.Message;

                        entity.CreateDate = CommonHelper.GetDateTimeInt();

                        db.UT_ContactUS.Add(entity);

                        if (await db.SaveChangesAsync() > 0)
                        {
                            result.Success = true;
                            result.Msg = "提交成功！";
                        }
                        else
                        {
                            result.Success = false;
                            result.Msg = "提交失败！";
                        }
                    }


                }
            }
            else
            {
                var error = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault().Errors.FirstOrDefault().ErrorMessage.Replace("字段", "").Replace("是必需的", "不能为空");

                result.Success = false;
                result.Msg = error;
            }
            return Json(result);
        }


    }

    /// <summary>
    /// 代理商表
    /// </summary>
    public class ModelAgent
    {
        /// <summary>
        /// 公司名字
        /// </summary>
        [Required]
        [Display(Name = "公司名字")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        [Required]
        [Display(Name = "区域")]
        public string Area { get; set; }
        /// <summary>
        /// 注册地址
        /// </summary>
        [Required]
        [Display(Name = "注册地址")]
        public string RegisteredAddress { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        [Required]
        [Display(Name = "法人代表")]
        public string CorporateRepresentative { get; set; }
        /// <summary>
        /// 公司注册时间
        /// </summary>
        [Required]
        [Display(Name = "公司注册时间")]
        public DateTime? CompanyRegistrationTime { get; set; }
        /// <summary>
        /// 公司性质，枚举
        /// </summary>
        [Required]
        [Display(Name = "公司性质")]
        public AgentCorporationNature CorporationNature { get; set; }

        [Required]
        [Display(Name = "团队规模-市场人员")]
        /// <summary>
        /// 团队规模-市场人员_名
        /// </summary>
        public int MarketPersonnelNum { get; set; }

        [Required]
        [Display(Name = "团队规模-销售人员")]
        /// <summary>
        /// 团队规模-销售人员_名
        /// </summary>
        public int SalerPersonnelNum { get; set; }

        [Required]
        [Display(Name = "团队规模-售后服务")]
        /// <summary>
        /// 团队规模-售后服务_名
        /// </summary>
        public int AfterSalesPersonalNum { get; set; }

        [Required]
        [Display(Name = "经营范围")]
        /// <summary>
        /// 经营范围
        /// </summary>
        public string ScopeBusiness { get; set; }

        [Required]
        [Display(Name = "目前销售区域")]
        /// <summary>
        /// 目前销售区域
        /// </summary>
        public string SalesArea { get; set; }

        [Required]
        [Display(Name = "年销售额")]
        /// <summary>
        /// 年销售额
        /// </summary>
        public string AnnualSales { get; set; }

        [Required]
        [Display(Name = "主营业务")]
        /// <summary>
        /// 主营业务（多选）逗号分隔
        /// </summary>
        public string[] MainBusiness { get; set; }

        [Required]
        [Display(Name = "主要客户类型")]
        /// <summary>
        /// 主要客户类型（多选）逗号分隔
        /// </summary>
        public string[] MainClientCategory { get; set; }

        [Required]
        [Display(Name = "联系人")]
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        [Required]
        [Display(Name = "联系人手机")]
        /// <summary>
        /// 联系人手机
        /// </summary>
        public string ContactMobilePhone { get; set; }
        [Display(Name = "邮箱")]
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }
        [Display(Name = "QQ/微信")]
        /// <summary>
        /// QQ/微信
        /// </summary>
        public string QQWeChat { get; set; }
        [Display(Name = "固话")]
        /// <summary>
        /// 固话
        /// </summary>
        public string Tel { get; set; }
        [Display(Name = "传真")]
        /// <summary>
        /// 传真
        /// </summary>
        public string FAX { get; set; }
        [Display(Name = "公司网站")]
        /// <summary>
        /// 公司网站
        /// </summary>
        public string CompanyWebSite { get; set; }

        [Required]
        [Display(Name = "是否代理其他产品")]
        /// <summary>
        /// 是否代理其他产品
        /// </summary>
        public bool? IsAgentOtherProducts { get; set; }
        [Display(Name = "所代理产品其他产品")]
        /// <summary>
        /// 所代理产品其他产品
        /// </summary>
        public string OtherProducts { get; set; }
        [Display(Name = "开发市场设想")]
        /// <summary>
        /// 开发市场设想
        /// </summary>
        public string DevelopMarketAssumptions { get; set; }
        [Display(Name = "贵司渠道和资源")]
        /// <summary>
        /// 贵司渠道和资源
        /// </summary>
        public string CompanyChannelsResources { get; set; }
        [Display(Name = "合作建议和要求")]
        /// <summary>
        /// 合作建议和要求
        /// </summary>
        public string CooperationSuggestion { get; set; }
        public AgentStatus Status { get; set; }
        public int CreateDate { get; set; }
    }
    public enum AgentStatus
    {
        待审核 = 0,
        审核通过 = 1,
        审核不通过 = 2,
    }
    public enum AgentCorporationNature
    {
        未知 = 0,
        国有 = 1,
        三资 = 2,
        私营 = 3,
        联营 = 4,
        个体 = 5,
    }

    /// <summary>
    /// 代理商表
    /// </summary>
    public class ModelContactUS
    {
        [Required]
        [Display(Name = "姓名")]
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        [Required]
        [Display(Name = "邮箱")]
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }
        [Required]
        [Display(Name = "主题")]
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
        [Required]
        [Display(Name = "信息")]
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }

}