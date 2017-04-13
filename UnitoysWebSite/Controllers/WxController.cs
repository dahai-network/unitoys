using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UnitoysWebSite.Controllers
{
    public class WxController : Controller
    {
        private string APPID = "wxe114537f747732cf";
        private string SECRET = "b62e263406117f9d20f33b1f36c2305b";
        public ActionResult BindAuthorize()
        {
            return Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxe114537f747732cf&redirect_uri=http://www.unitoys.com/Wx/BindUser&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect");
        }
        public async Task<ActionResult> BindUser(string code, string state)
        {
            HttpClient hc = new HttpClient();
            var result = await hc.GetStringAsync("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + APPID + "&secret=" + SECRET + "&code=" + code + "&grant_type=authorization_code");

             oauthresult resultJson = JsonConvert.DeserializeObject<oauthresult>(result);
             var userinfo = await hc.GetStringAsync("https://api.weixin.qq.com/sns/userinfo?access_token=" + resultJson.access_token + "&openid=" + resultJson.openid + "&lang=zh_CN ");

             return Content(userinfo);
        }
    }
    public class oauthresult
    {
        public string access_token { get; set; }

        public string openid { get; set; }
    }
}