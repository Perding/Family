using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Entity;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Service.IService;
using System.Diagnostics;
using System.IO;
using AutoMapper;
using Entity.Viewmodel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCore.Controllers
{
    public class SerachController : BaseControll
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private AStudentIService _aStudentIService;
        private AStudentPayIService _aStudentPayIService;
        private ISys_ItemIService _ItemIService;
        private ARefundesIService _aRefundesIService;
        private ILogIService _LogService;
        private IF_ItemDetailIService _ItemDetailIService;
        public SerachController(IHostingEnvironment hostingEnvironment,AStudentIService aStudentIService, AStudentPayIService aStudentPayIService, ISys_ItemIService sys_ItemIService, ARefundesIService aRefundesIService, ILogIService logIService, IF_ItemDetailIService f_ItemDetailIService)
        {
            _hostingEnvironment = hostingEnvironment;
            _ItemDetailIService = f_ItemDetailIService;
            _LogService = logIService;
            _ItemIService = sys_ItemIService;
            _aStudentIService = aStudentIService;
            _aStudentPayIService = aStudentPayIService;
            _aRefundesIService = aRefundesIService;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            ViewBag.zs = await _ItemDetailIService.GetList("0CF05878-8AA2-40C1-BAC6-32B81E3F31E5");
            ViewBag.nj = await _ItemDetailIService.GetList("B3FF1DDC-1BBC-4911-ACDC-19BABD4EEF49");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IActionResult> Serachluru(AStudent model)
        {
            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            

            Tuple<int,List<AStudent>> list = await _aStudentIService.GetProcePage(model,1);
            foreach (var item in list.Item2)
            {
                item.GetAStudentPay = await _aStudentPayIService.GetList(item.ID,1);
                item.GetARefundes = await _aRefundesIService.Getlist(item.ID,1);
            }
            stopwatch.Stop();
            TimeSpan timespan = stopwatch.Elapsed;
            string seconds = timespan.TotalSeconds.ToString("#0.00000000 ");
            LogHelp.Monitor("查询时间(单位秒)=" + seconds);
            var totalPage = int.Parse(Math.Ceiling((decimal)list.Item1 /20).ToString());
            return Json(new { totalPage = totalPage, recordCount = list.Item1, list = list.Item2 });
        }
        public FileStreamResult download(string path)
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string fileName = "学生缴费记录.xls";//客户端保存的文件名
          
            string filePath = contentRootPath + "/Temp/" + path;
            var stream = System.IO.File.OpenRead(filePath);
            return File(stream, "application/vnd.ms-excel", fileName);
        }
        public void DownloadFile(string path)
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string fullname = contentRootPath + "/Temp/" + path;

            if (!System.IO.File.Exists(fullname))
            {
               // LogHelper.Instance.Error("用户名为‘" + Session["UName"] + "’下载文件" + fullname + "，该文件不存在服务器");
                return;
            }
            else
            {
               // S_OPInfoImpl.AddFileOP(6, Session["UID"].ToString(), Session["UCode"].ToString() + "," + Session["UName"].ToString(), id, Session["RID"].ToString());
                //Response.ContentType = "application/octet-stream";  //二进制流
                //Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fname, System.Text.Encoding.UTF8));
                //Response.TransmitFile(fullname); //将指定文件写入 HTTP 响应输出流
            }
        }
        public async Task<IActionResult> Seradaoru(AStudent model)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Tuple<int, List<AStudent>> list = await _aStudentIService.GetProcePage(model, 2);
            foreach (var item in list.Item2)
            {
                item.GetAStudentPay = await _aStudentPayIService.GetList(item.ID,2);
                item.GetARefundes = await _aRefundesIService.Getlist(item.ID,2);
            }
            var filename = "学生缴费记录.xls";
            //Mapper.Initialize(x => x.CreateMap<AStudent, AstudentView>());
            //var dto = Mapper.Map<List<AstudentView>>(list.Item2);
            var book = _aStudentIService.BuildWorkbook(list.Item2, contentRootPath);
            //System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //book.Write(ms);
            //ms.Seek(0, SeekOrigin.Begin);
            TimeSpan timespan = stopwatch.Elapsed;
            string seconds = timespan.TotalSeconds.ToString("#0.00000000 ");
            LogHelp.Monitor("查询时间(单位秒)=" + seconds);
            stopwatch.Stop();
            return Json(new { status = "ok", message = book });
            // return File(ms, "application/vnd.ms-excel", filename);
            //  var totalPage = int.Parse(Math.Ceiling((decimal)list.Item1 / 20).ToString());
            //  return Json(new { totalPage = totalPage, recordCount = list.Item1, list = list.Item2 });

        }
        public async Task<IActionResult> GetYear(string year)
        {
            Tuple<List<Tone>, List<Ttwo>> tuple = await _aStudentPayIService.GetProcePage(string.IsNullOrEmpty(year)?DateTime.Now.Year.ToString():year);
            var data = new
            {
                list1=tuple.Item1,
                list2 = tuple.Item2
            };
            return Json(data);
        }
        public IActionResult YearState()
        {
            return View();
        }

        public async Task<IActionResult> YearStateAll()
        {
            ViewBag.year = await _aStudentPayIService.GetListYear();
            return View();
        }
    }
}
