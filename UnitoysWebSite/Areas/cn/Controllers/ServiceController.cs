using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Unitoys.Core;
using UnitoysWebSite.Areas.cn.Models;
using UnitoysWebSite.Models;

namespace UnitoysWebSite.Areas.cn.Controllers
{
    public class ServiceController : Controller
    {
        //
        // GET: /cn/Service/
        public ActionResult Index()
        {
            return ReturnListIndexView("Index");
        }
        public ActionResult Channel()
        {
            return ReturnListIndexView("Channel");
        }
        public ActionResult Question()
        {
            return ReturnListIndexView("Question");
        }
        private ActionResult ReturnListIndexView(string type)
        {
            ViewBag.Type = type;
            List<UT_GlobalContent> list = new List<UT_GlobalContent>(){
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.AfterSalesPolicy),
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.Aftermarket),
                GlobalContentService.GetHtmlDecodeContentFirstOrDefault(GlobalContentType.CommonProblem)
            };
            return View("Index", list);
        }


        public ActionResult Repair()
        {
            return View();
        }
        public ActionResult ApplyRepair()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ApplyRepair(ModelAfterSales model)
        {

            JsonAjaxResult result = new JsonAjaxResult();

            if (ModelState.IsValid)
            {
                //处理图片上传
                //保存与赋值
                int picNum = 0;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var image = Request.Files[i];

                    //HttpPostedFileBase image = Request.Files[i];
                    if (image.ContentLength <= 0)
                    {
                        continue;
                    }
                    var resultAsync = await WebUtil.UploadImgAsync(image);
                    switch (resultAsync)
                    {
                        case "-1":
                            result.Success = false;
                            result.Msg = "图片上传失败，请重试！";
                            return Json(result);
                            break;
                        case "-2":
                            result.Success = false;
                            result.Msg = "图片格式不正确！";
                            return Json(result);
                            break;
                        case "-3":
                            result.Success = false;
                            result.Msg = "超过限制大小！";
                            return Json(result);
                            break;
                        default:
                            picNum++;
                            break;
                    }

                    switch (picNum)
                    {
                        case 1:
                            model.Pic1 = resultAsync;
                            break;
                        case 2:
                            model.Pic2 = resultAsync;
                            break;
                        case 3:
                            model.Pic3 = resultAsync;
                            break;
                        case 4:
                            model.Pic4 = resultAsync;
                            break;
                        case 5:
                            model.Pic5 = resultAsync;
                            break;
                        default:
                            break;
                    }
                }
                if (picNum == 0)
                {
                    result.Success = false;
                    result.Msg = "故障图片不能为空！";
                    return Json(result);
                }


                using (UnitoysEntities db = new UnitoysEntities())
                {

                    UT_AfterSales entity = new UT_AfterSales();
                    entity.ID = Guid.NewGuid();
                    entity.AfterSalesNum = String.Format("8022{0}", DateTime.Now.ToString("yyMMddHHmmssffff"));
                    entity.Contact = model.Contact;
                    entity.MobilePhone = model.MobilePhone;
                    entity.Address = model.Address;
                    entity.BuyDate = model.BuyDate;
                    entity.ProblemDescr = model.ProblemDescr;
                    entity.ProductModel = model.ProductModel;
                    entity.Pic1 = model.Pic1;
                    entity.Pic2 = model.Pic2;
                    entity.Pic3 = model.Pic3;
                    entity.Pic4 = model.Pic4;
                    entity.Pic5 = model.Pic5;
                    entity.CreateDate = CommonHelper.GetDateTimeInt();



                    db.UT_AfterSales.Add(entity);

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
            else
            {
                var error = ModelState.Values.Where(x => x.Errors.Count > 0).FirstOrDefault().Errors.FirstOrDefault().ErrorMessage.Replace("字段", "").Replace("是必需的", "不能为空");

                result.Success = false;
                result.Msg = error;
            }
            return Json(result);
        }

        public ActionResult SearchRepair()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SearchRepair(string MobilePhone, string Code)
        {
            JsonAjaxResult result = new JsonAjaxResult();

            if (Session["ValidateCode"].ToString() != Code)
            {
                result.Success = false;
                result.Msg = "验证码错误！";
            }
            else
            {
                result.Success = true;
                result.Msg = "验证成功！";

                Session["RepairTel"] = MobilePhone;
            }

            return Json(result);
        }
        public ActionResult SearchRepairResult()
        {
            if (Session["RepairTel"] == null)
            {
                return RedirectToAction("SearchRepair");
            }

            using (UnitoysEntities db = new UnitoysEntities())
            {
                string mobilePhone = Session["RepairTel"].ToString();
                var list = db.UT_AfterSales
                    .OrderByDescending(x => x.CreateDate)
                    .Where(x => x.MobilePhone == mobilePhone)
                    .ToList()
                    .Select(x => new ModelRepairResult()
                {
                    ID = x.ID,
                    CreateDateDesc = Unitoys.Core.CommonHelper.GetTime(x.CreateDate.ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                    ProductModelDesc = x.ProductModel == 0 ? "手环" : "双待王",
                    Contact = x.Contact,
                    MobilePhone = x.MobilePhone,
                    StatusDesc = GetStatusForDesc(x.Status),
                }).ToList();

                return View(list);
            }
        }
        private string GetStatusForDesc(int status)
        {
            AfterSalesStatus enumStatus = (AfterSalesStatus)status;
            return enumStatus.GetDescription();
        }

        [HttpPost]
        public async Task<JsonResult> CancelRepair(Guid ID)
        {
            JsonAjaxResult result = new JsonAjaxResult();

            if (Session["RepairTel"] == null)
            {
                result.Success = false;
                result.Msg = "请重新查询！";
            }
            else
            {
                using (UnitoysEntities db = new UnitoysEntities())
                {
                    var entity = await db.UT_AfterSales.FindAsync(ID);
                    if (entity.MobilePhone != Session["RepairTel"].ToString())
                    {
                        result.Success = false;
                        result.Msg = "无取消权限！";
                    }
                    else if (entity.Status != (int)AfterSalesStatus.Pending)
                    {
                        result.Success = false;
                        result.Msg = "当前状态不允许取消！";
                    }
                    else
                    {
                        entity.Status = (int)AfterSalesStatus.Cancel;

                        if (await db.SaveChangesAsync() > 0)
                        {
                            result.Success = true;
                            result.Msg = "取消成功！";
                        }
                        else
                        {
                            result.Success = true;
                            result.Msg = "取消失败！";
                        }
                    }
                }
            }

            return Json(result);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public FileResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
    }
    public class ModelAfterSales
    {
        //public string AfterSalesNum { get; set; }
        [Required]
        [Display(Name = "联系人")]
        public string Contact { get; set; }
        [Required]
        [Display(Name = "手机号码")]
        public string MobilePhone { get; set; }
        [Required]
        [Display(Name = "联系地址")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "故障描述")]
        public string ProblemDescr { get; set; }
        //public int Status { get; set; }
        //public string TrackingNO { get; set; }
        public int ProductModel { get; set; }
        //public string ExpressCompany { get; set; }
        //public int CreateDate { get; set; }

        public string Pic1 { get; set; }
        public string Pic2 { get; set; }
        public string Pic3 { get; set; }
        public string Pic4 { get; set; }
        public string Pic5 { get; set; }
        //public string AuditRemark { get; set; }
        //[Required]
        [Display(Name = "购买时间")]
        public System.DateTime? BuyDate { get; set; }
    }
    public class ModelRepairResult
    {
        public Guid ID { get; set; }
        public string CreateDateDesc { get; set; }
        public string ProductModelDesc { get; set; }
        public string Contact { get; set; }
        public string MobilePhone { get; set; }
        public string StatusDesc { get; set; }
    }

    public enum AfterSalesStatus
    {
        //提交申请
        [Description("提交申请")]
        Pending = 0,
        //审核
        [Description("审核通过")]
        Pass = 1,
        //审核不通过
        [Description("审核不通过")]
        NotPass = 2,
        //已取消
        [Description("已取消")]
        Cancel = 3,
        //收到快递
        [Description("收到快递")]
        ReceivedCourier = 4,
        //邮寄
        [Description("回寄中")]
        ReturnTo = 5,
        //完成
        [Description("完成")]
        Done = 6,
        //邮寄 = 2,
        //处理 = 3,
        //完成 = 4,
    }
}