using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Net;
using System.Text;
using System.IO;

namespace UnitoysWebSite.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            CultureInfo info = Thread.CurrentThread.CurrentCulture;
            if (info.Name.Equals("zh-CN"))
            {
                return Redirect("/cn");
            }
            else
            {
                return Redirect("/en");
            }
        }

        
        [HttpPost]
        public ActionResult AddFeedBack(FeedBackModel model)
        {


            var postDataStr = String.Format("Info={0}&Terminal={1}&Mail={2}", model.Content, "WebSite", model.Email);
            var result = HttpPost("https://api.unitoys.com/api/public/addfeedback", postDataStr);

            ResultJson resultJson = JsonConvert.DeserializeObject<ResultJson>(result);
            if (resultJson.status == 1)
            {
                return Json(resultJson.status);
            }
            else
            {
                return Json(resultJson.msg);
            }
        }
        private static string HttpPost(string Url, string postDataStr)
        {
            string expires = ConvertDateTimeInt(DateTime.Now).ToString();
            string partner = "2006808";
            string key = "BAS123!@#FD1A56K";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.Headers.Add("expires", expires);
            request.Headers.Add("partner", partner);
            request.Headers.Add("sign", MD5(partner + expires + key));

            byte[] btBodys = Encoding.UTF8.GetBytes(postDataStr);
            request.ContentLength = btBodys.Length;
            request.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
            request.Abort();
            httpWebResponse.Close();

            return responseContent;

        }
        public static string GetIP(HttpRequestBase request)
        {
            string ip = string.Empty;

            if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                ip = request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(ip))
                ip = "127.0.0.1";
            return ip;
        }
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// MD5散列
        /// </summary>
        public static string MD5(string inputStr)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashByte = md5.ComputeHash(Encoding.UTF8.GetBytes(inputStr));
            StringBuilder sb = new StringBuilder();
            foreach (byte item in hashByte)
                sb.Append(item.ToString("x").PadLeft(2, '0'));
            return sb.ToString();
        }
    }
    public class FeedBackModel
    {
        public string Content { get; set; }
        public string Email { get; set; }
    }
    public class ResultJson
    {
        public int status { get; set; }

        public string msg { get; set; }
        public string data { get; set; }
    }
}