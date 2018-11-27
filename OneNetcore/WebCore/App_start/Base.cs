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
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebCore.App_start
{
	public class Base : Controller
	{
		private IHttpContextAccessor _accessor;
		public Base(IHttpContextAccessor accessor)
		{
			_accessor = accessor;
		}
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var httpcontext = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

			base.OnActionExecuting(filterContext);
		}
	}
}
