using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entity;
using Common;
using Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCore.Controllers
{
    public class LoginController : Controller
    {
		private IHttpContextAccessor _accessor;
			 private IMemoryCache _cache;
        private ILogIService _LogService;
        private IUserInfoService _userinfo;
        public LoginController(IUserInfoService student, IHttpContextAccessor accessor, ILogIService logservice, IMemoryCache memoryCache)
        {
			_cache = memoryCache;
			_accessor = accessor;
            _userinfo = student;
            _LogService = logservice;
        }
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult LogOut()
		{
			_cache.Remove("userinfo");
			return Redirect("/Login/Login");
		}
		public IActionResult Login()
        {
			UserInfo userInfo = null;
			if (_cache.TryGetValue("userinfo",out userInfo))
			{
				if (userInfo!=null)
				{
					ViewBag.name = userInfo.u_account;
					ViewBag.pwd = userInfo.u_password;
					ViewBag.states = userInfo.states;
				}
				else
				{
					ViewBag.name = "";
					ViewBag.pwd = "";
					ViewBag.states = "0";
				}
			}
			else
			{
				ViewBag.name = "";
				ViewBag.pwd = "";
				ViewBag.states = "0";
			}
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckLogin(UserInfo model) {

			var doub = GetIPAdress.ConvertToChinese(Convert.ToDecimal(0.00));
			string pwd = model.u_password;
            var httpcontext = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            Loginfo log = new Loginfo();
            log.L_id = System.Guid.NewGuid().ToString();
            log.L_ip = httpcontext;
            log.l_date= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            model.u_account = model.u_account.ToLower();
            model.u_password = SecretTools.Md5Encrypt(model.u_password);

            LogHelp.Info(httpcontext);
            UserInfo sm =await _userinfo.GetUserName(model);
            if (sm!=null)
            {
				if (model.states=="1")
				{
					model.u_password = pwd;
					_cache.Set<UserInfo>("userinfo",model,DateTime.Now.AddYears(1));
				}
				else
				{
					_cache.Remove("userinfo");
				}

				log.u_id = sm.u_userid;
                log.u_account = sm.u_account;
                log.l_content = "用户"+sm.u_account+"登陆成功";
                await _LogService.InsertModel(log);
                HttpContext.Session.Set("UID", ByteConvertHelper.Object2Bytes(sm.u_userid));
                HttpContext.Session.Set("Acount", ByteConvertHelper.Object2Bytes(sm.u_account));
                HttpContext.Session.Set("RealName", ByteConvertHelper.Object2Bytes(sm.u_realName));
                HttpContext.Session.Set("IP", ByteConvertHelper.Object2Bytes(httpcontext));
                HttpContext.Session.SetInt32("IsAdmin", sm.U_IsAdmin);
                return Json(new  { state = 200, message = "" });
            }
            else
            {
                log.u_id = model.u_account;
                log.u_account = model.u_account;
                log.l_content = "用户" + model.u_account + "登陆失败";
                await _LogService.InsertModel(log);
                return Json(new {state=500,message="账号或者密码输入错误" });
            }
        }
    }
}
