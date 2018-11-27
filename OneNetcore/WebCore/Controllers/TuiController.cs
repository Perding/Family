using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Service.IService;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Hosting;

namespace WebCore.Controllers
{
    public class TuiController : Controller
    {
		private AStudentIService _aStudentIService;
		public TuiController(AStudentIService aStudentIService)
		{
			_aStudentIService = aStudentIService;
		}
        public IActionResult Index(string code)
        {
			ViewBag.code = code;
            return View();
        }
    }
}