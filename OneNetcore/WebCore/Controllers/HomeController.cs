using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entity;
using Microsoft.Extensions.Options;
using Service;

using log4net.Core;
using Common;
using Microsoft.AspNetCore.Http;
using Service.IService;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using System.Data;
using Newtonsoft.Json.Linq;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCore.Controllers
{

	public class HomeController : BaseControll
	{
		private readonly IHostingEnvironment _hostingEnvironment;
		private ILogIService _LogService;
		private IF_ItemDetailIService _ItemDetailIService;
		private ISys_ItemIService _ItemIService;
		private AStudentIService _aStudentIService;
		private IM_menuIService _MenuIService;
		public HomeController(IHostingEnvironment hostingEnvironment, IF_ItemDetailIService itemDetailIService, ISys_ItemIService itemIService, AStudentIService aStudentIService, ILogIService logIService, IM_menuIService m_MenuIService)
		{
			_hostingEnvironment = hostingEnvironment;
			_MenuIService = m_MenuIService;
			_LogService = logIService;
			_aStudentIService = aStudentIService;
			_ItemDetailIService = itemDetailIService;
			_ItemIService = itemIService;
		}
		public async Task<IActionResult> Card(string card)
		{
			AStudent model = await _aStudentIService.IsCard(card);
			if (model == null)
			{
				return Json(new { state = "false", message = "" });
			}
			else
			{
				return Json(new { state = "ok", message = model });
			}


		}
		public IActionResult UpdatePass()
		{
			return View();
		}

		public async Task<IActionResult> Index()
		{
			ViewBag.Acount = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("Acount"));
			ViewBag.RealName = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("RealName"));
			var uid = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString();
			int? Is = HttpContext.Session.GetInt32("IsAdmin");
			if (Is == 1)
			{
				uid = "";
			}
			ViewBag.list = await _MenuIService.Getlist(uid);
			return View();
		}


		public async Task<IActionResult> UploadFiles()
		{
			string webRootPath = _hostingEnvironment.WebRootPath;
			var file = Request.Form.Files;
			foreach (var formFile in file)
			{
				if (formFile.Length > 0)
				{
					var fileName = ContentDispositionHeaderValue
							   .Parse(formFile.ContentDisposition)
							   .FileName
							   .Trim('"');
					var time = DateTime.Now.ToString("yyyyMMddHHmmss") + fileName;
					fileName = webRootPath + "/upload/" + time;
					LogHelp.Info(fileName);
					using (var fileStream = new FileStream(fileName, FileMode.Create))
					{
						formFile.CopyTo(fileStream);
						fileStream.Flush();

					}
					JArray lst = new JArray();
					Tuple<List<AStudent>, List<AStudentPay>> list = OfficeHelper.ReadExcelToDataTable(fileName);
					bool reslt = await _aStudentIService.DaoExcel(list.Item1, list.Item2, ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString());
					if (reslt)
					{
						return Json(new { state = "ok", message = "导入成功" });
					}
				}
			}

			return Json(new { state = "error", message = "数据格式不正确" });
		}

		public IActionResult Load()
		{
			return View();
		}

		public async Task<IActionResult> Default()
		{
			var uid = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString();
			int? Is = HttpContext.Session.GetInt32("IsAdmin");
			if (Is == 1)
			{
				uid = "";
			}
			ViewBag.list = await _MenuIService.GetChildrelist(uid);
			return View();
		}
		public async Task<IActionResult> AddPay(string id)
		{
			AStudent m = await _aStudentIService.GetSql(id);
			ViewBag.zs = await _ItemDetailIService.GetList("0CF05878-8AA2-40C1-BAC6-32B81E3F31E5");
			ViewBag.nj = await _ItemDetailIService.GetList("B3FF1DDC-1BBC-4911-ACDC-19BABD4EEF49");
			ViewBag.xx = await _ItemDetailIService.GetList("026C846D-2CAC-44F7-B046-A272517D86F4");
			ViewBag.fk = await _ItemDetailIService.GetList("2DAE3BFA-7602-44BD-A62C-E967779CFB10");
			ViewBag.sk = await _ItemDetailIService.GetList("E46B22C5-24AD-447B-8C49-84734F453EF9");
			ViewBag.xf = await _ItemIService.GetList("A88107CA-F816-44EC-AD05-0166A42B32EE");
			ViewBag.all = await _ItemIService.GetList();
			return View(m);
		}
		public async Task<IActionResult> GetF_item(string id)
		{
			IEnumerable<F_ItemDetail> list = await _ItemDetailIService.GetList(id);
			return Json(list.ToList());
		}
		public async Task<IActionResult> GetSysItem(string id)
		{
			IEnumerable<Sys_Item> list = await _ItemIService.GetList(id);
			return Json(list.ToList());
		}

		public async Task<IActionResult> GetSysItemModel(string id)
		{
			Sys_Item model = new Sys_Item();
			model.F_ID = id;
			model = await _ItemIService.IsTrue(model);
			return Json(model);
		}


	}
}
