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
	public class EnterController : BaseControll
	{
		private readonly IHostingEnvironment _hostingEnvironment;
		private IUserInfoService _userInfoService;
		private AStudentIService _aStudentIService;
		private AStudentPayIService _aStudentPayIService;
		private ISys_ItemIService _ItemIService;
		private ARefundesIService _aRefundesIService;
		private ILogIService _LogService;
		private IQRCodeIService _iQRCode;


		public EnterController(IHostingEnvironment hostingEnvironment,IQRCodeIService qRCode, IUserInfoService userInfoService, AStudentIService aStudentIService, AStudentPayIService aStudentPayIService, ISys_ItemIService sys_ItemIService, ARefundesIService aRefundesIService, ILogIService logIService)
		{
			_hostingEnvironment = hostingEnvironment;
			_iQRCode = qRCode;
			_userInfoService = userInfoService;
			_LogService = logIService;
			_ItemIService = sys_ItemIService;
			_aStudentIService = aStudentIService;
			_aStudentPayIService = aStudentPayIService;
			_aRefundesIService = aRefundesIService;
		}
		public IActionResult Index()
		{
			return View();
		}

		public string GetQRCode(string url)
		{
			string webRootPath = _hostingEnvironment.WebRootPath;
			int pixel = 4;
			var contentTypeStr = "image/jpeg";
			var bitmap = _iQRCode.GetQRCode(url, pixel);
			MemoryStream ms = new MemoryStream();
			bitmap.Save(ms, ImageFormat.Jpeg);
			var time = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpeg";
			var bytes = ms.GetBuffer();
			using (var fileStream = new FileStream(webRootPath+"/qrcode/"+time, FileMode.Create))
			{
				ms.WriteTo(fileStream);
				fileStream.Flush();
				ms.Close();
				fileStream.Close();
			}
			return time;
		}
		

		public async Task<IActionResult> Dayin(string id)
		{
			
			AStudent aStudent = new AStudent();
			ViewBag.RealName = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("RealName"));
			ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			Tuple<List<AStudent>, List<AStudentPay>> list = await _aStudentIService.GetDayin(id);
			ViewBag.all = list.Item2;
			aStudent = list.Item1[0];
			ViewBag.url = GetQRCode("http://localhost:50895/Tui/Index?code="+ aStudent.StuNum);
			
			return View(aStudent);
		}

		/// <summary>
		/// 退回界面
		/// </summary>
		/// <param name="student"></param>
		/// <returns></returns>
		public async Task<IActionResult> Tuihui(AStudent student)
		{
			ARefund model = null;
			if (!string.IsNullOrEmpty(student.ID))
			{
				model = await _aRefundesIService.GetModel(student.ID);
				model.ARefundes = await _aRefundesIService.GetPayList(student.ID);
			}
			AStudent students = await _aStudentIService.IsCard(student.Cards);
			ViewBag.id = students.ID;
			//AStudent m= await _aStudentIService.GetSql("6ef1fff5-8515-4a45-a95c-2c7524998941");

			ViewBag.name = students.Name;
			ViewBag.collecMoney = student.CollecMoney;
			ViewBag.youhui = student.Youhui;
			ViewBag.payMoeny = student.PayMoeny;
			ViewBag.xf = await _ItemIService.GetList("A88107CA-F816-44EC-AD05-0166A42B32EE");
			return View(model);
		}
		public async Task<IActionResult> Delete(string id, string type)
		{
			Loginfo log = new Loginfo();
			log.L_ip = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("IP")).ToString();
			log.L_id = System.Guid.NewGuid().ToString();
			log.l_date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			log.u_id = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString();
			log.u_account = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("Acount")).ToString();
			if (type == "1")
			{
				AStudent m = await _aStudentIService.GetModel(id);
				log.l_content = "用户" + log.u_account + "删除学生" + m.Name + "的所有信息";
			}
			if (type == "2")
			{
				AStudentPay ap = await _aStudentPayIService.GetModel(id);
				AStudent m1 = await _aStudentIService.GetModel(ap.StudID);
				log.l_content = "用户" + log.u_account + "删除" + m1.Name + "在" + ap.PayDate.ToString("yyyy-MM-dd") + "的缴费记录";
			}
			if (type == "3")
			{
				ARefund aRefund = await _aRefundesIService.GetModel(id);
				AStudent m1 = await _aStudentIService.GetModel(aRefund.StudID);
				log.l_content = "用户" + log.u_account + "删除" + m1.Name + "在" + aRefund.tDatetime + "的退费记录";
			}
			bool result = await _aStudentIService.DeleteStudent(id, type, log);
			if (result)
			{
				await _LogService.InsertModel(log);
				return Json(new { status = "ok", message = "删除成功" });
			}
			return Json(new { status = "error", message = "删除失败" });
		}


		/// <summary>
		/// 退费加制个人缴费详情
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<IActionResult> GetSerach(string id)
		{
			PayModel payModel = new PayModel();
			payModel.GetAStudentPay = await _aStudentPayIService.GetList(id, 1);
			payModel.GetARefundes = await _aRefundesIService.Getlist(id, 1);
			return Json(payModel);
		}

		/// <summary>
		/// 保存退费信息
		/// </summary>
		/// <param name="aRefundes"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> SaveRuPay(ARefundes aRefundes)
		{

			UserInfo sinfo = await _userInfoService.GetModel(ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString());
			Loginfo log = new Loginfo();
			log.L_ip = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("IP")).ToString();
			log.L_id = System.Guid.NewGuid().ToString();
			log.l_date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			log.u_id = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString();
			log.u_account = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("Acount")).ToString();

			aRefundes.F_CreatorUserId = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString();
			aRefundes.F_CreatorTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			bool result = await _aRefundesIService.Insert(aRefundes);
			if (result)
			{
				AStudent m = await _aStudentIService.GetModel(aRefundes.StudentId);
				if (m != null)
				{
					if (!string.IsNullOrEmpty(aRefundes.ID))
					{
						log.l_content = "用户" + sinfo.u_account + "修改学生" + m.Name + "退费信息";
					}
					else
					{
						log.l_content = "用户" + sinfo.u_account + "新增学生" + m.Name + "退费信息";
					}

					await _LogService.InsertModel(log);
				}

				return Json(new { status = "ok" });
			}
			return Json(new { status = "error", message = "保存失败" });
		}
		/// <summary>
		/// 总账管理首页
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public async Task<IActionResult> Serachluru(string name)
		{
			System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			IEnumerable<AStudent> list = await _aStudentIService.GetStoreProc(name == null ? "" : name);
			foreach (var item in list)
			{
				item.GetAStudentPay = await _aStudentPayIService.GetList(item.ID, 1);
				item.GetARefundes = await _aRefundesIService.Getlist(item.ID, 1);
			}
			stopwatch.Stop();
			TimeSpan timespan = stopwatch.Elapsed;
			string seconds = timespan.TotalSeconds.ToString("#0.00000000 ");
			LogHelp.Monitor("首页加制时间(单位秒)=" + seconds);
			return Json(list);
		}
		/// <summary>
		/// 缴费录入
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<IActionResult> GetSaveEnterStudent(AStudent model)
		{
			Loginfo log = new Loginfo();
			log.L_ip = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("IP")).ToString();
			log.L_id = System.Guid.NewGuid().ToString();
			log.l_date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			log.u_id = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString();
			log.u_account = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("Acount")).ToString();

			model.F_lastModifyUserId = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString();
			if (!string.IsNullOrEmpty(model.ID))
			{

				bool result = await _aStudentIService.Update(model);
				if (result)
				{
					log.l_content = "用户" + log.u_account + "给学生" + model.Name + "修改缴费";
					await _LogService.InsertModel(log);
					return Json(new { status = "ok", message = "" });
				}
				else
				{
					return Json(new { status = "error", message = "保存失败" });
				}
			}
			else
			{
				log.l_content = "用户" + log.u_account + "给学生" + model.Name + "新增缴费";
				await _LogService.InsertModel(log);
				Tuple<bool, string> result = await _aStudentIService.IsList(model);
				if (result.Item1)
				{
					return Json(new { status = "ok", message = result.Item2 });
				}
				else
				{
					return Json(new { status = "error", message = "保存失败" });
				}
			}
		}

	}
}