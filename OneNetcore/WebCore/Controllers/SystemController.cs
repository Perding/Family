using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Entity;
using Common;
using Microsoft.AspNetCore.Http;
namespace WebCore.Controllers
{
    public class SystemController : BaseControll
    {
        private IF_ItemDetailIService _ItemDetailIService;
        private ISys_ItemIService _ItemIService;
        private IUserInfoService _userInfoService;
        private IUsermenuIService _usermenuIService;
        private IM_menuIService _MenuIService;
		private ILogIService _logIService;
		public SystemController(ILogIService logIService, ISys_ItemIService itemIService, IF_ItemDetailIService itemDetailIService, IUserInfoService userInfoService, IUsermenuIService usermenuIService, IM_menuIService m_MenuIService)
        {
			_logIService = logIService;
			_usermenuIService = usermenuIService;
            _MenuIService = m_MenuIService;
            _userInfoService = userInfoService;
            _ItemDetailIService = itemDetailIService;
            _ItemIService = itemIService;
        }

		public async Task<IActionResult> Loginfo()
		{
			return View();
		}
		/// <summary>
		/// 修改密码
		/// </summary>
		/// <param name="userInfo"></param>
		/// <returns></returns>
		public async Task<IActionResult> UpdatePass(UserInfo userInfo)
		{
			userInfo.u_userid = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString();
			UserInfo sinfo = await _userInfoService.GetModel(userInfo.u_userid);
			var pass = SecretTools.Md5Encrypt(userInfo.u_password);
			if (sinfo.u_password.Trim() == pass.Trim())
			{
				if (userInfo.newpassword!=userInfo.setnewpassword)
				{
					return Json(new { status = "false", message = "两次密码输入不一致" });
				}
				else
				{
					userInfo.u_password = SecretTools.Md5Encrypt(userInfo.setnewpassword);
					bool result = await _userInfoService.UpdatePasswork(userInfo);
					if (result)
					{
						return Json(new { status = "ok", message = "修改成功" });
					}
					else
					{
						return Json(new { status = "false", message = "修改失败" });
					}
				}
			
			}
			else
			{
				return Json(new { status ="false",message="初始密码输入不正确"});
			}
		}
		public IActionResult Index()
        {
            return View();
        }
        public IActionResult User()
        {
            return View();
        }
        public  async Task<IActionResult> AddUser(string uid)
        {
            UserInfo m = new UserInfo();
            m.u_isEnable = 1;
            if (!string.IsNullOrEmpty(uid))
            {
                m =await _userInfoService.GetModel(uid);
            }
            ViewBag.uid =string.IsNullOrEmpty(uid)?"":uid;

            return View(m);
        }
        public async Task<IActionResult> GetListUser()
        {
            IEnumerable<UserInfo> list = await _userInfoService.GetList();
            var data = new
            {
                code = 0,
                msg = "",
                count = list.ToList().Count,
                data = list
            };
            return Json(data);
        }
        public async Task<IActionResult> GetMenuTree(string uid)
        {
            var treeList = new List<TreeViewModel>();
            IEnumerable<Usermenu> menu =await _usermenuIService.Getlist(uid);
            IEnumerable<M_menu> data =await _MenuIService.Getlist();
            foreach (M_menu item in data.ToList())
            {
                TreeViewModel tree = new TreeViewModel();
                bool haschildren = data.Count(t=>t.M_PartentID==item.M_ID)==0?false:true;
                tree.id = item.M_ID;
                tree.text = item.M_Name;
                tree.parentId = item.M_PartentID;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.sortid = item.M_sortid;
                if (menu==null)
                {
                    tree.checkstate = 0;
                }
                else
                {
                    tree.checkstate = menu.Count(t=>t.m_id==item.M_ID);
                }
                tree.hasChildren = haschildren;
                tree.img = "";
                treeList.Add(tree);

            }
            var list = treeList.TreeViewJson();
            return Content(list);
        }
        public async Task<IActionResult> InsertUser(UserInfo model, string MidList)
        {
          
            model.u_password= SecretTools.Md5Encrypt(model.u_password);
            model.U_CreatorUserId = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString();
            model.U_CreatorTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            bool result = await _userInfoService.Insert(model, MidList);
            if (result)
            {
                return Json(new { status = "ok", message = "保存成功" });
            }
            else
            {
                return Json(new { status = "error", message = "保存失败" });
            }
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            bool result = await _userInfoService.Delete(id);
            if (result)
            {
                return Json(new { status = "ok", message = "删除成功" });
            }
            else
            {
                return Json(new { status = "error", message = "删除失败" });
            }

        }

        public async Task<IActionResult> Addkey1(string id,string fid)
        {
            Sys_Item mdoel = new Sys_Item();
            mdoel.F_ParentID = id == null ? "" : id ;
            mdoel.F_EnableMark = 1;
            if (!string.IsNullOrEmpty(fid))
            {
                mdoel.F_ID = fid;
                mdoel = await _ItemIService.IsTrue(mdoel);
            }
           
            return View(mdoel);
        }
        public async Task<IActionResult> Deletekey1(Sys_Item id)
        {
            bool result =await _ItemIService.DeleteModel(id);
            if (result)
            {
                return Json(new { status = "ok", message = "" });
            }
            else
            {
                return Json(new { status = "error", message = "删除失败" });
            }
           
        }

        public async Task<IActionResult> GetTableKey(string id, string key)
        {
            IEnumerable<Sys_Item> list = await _ItemIService.GetList(id);
            var data = new
            {
                code = 0,
                msg = "",
                count = list.ToList().Count,
                data = list
            };
            return Json(data);
        }

        public async Task<IActionResult> GetTableGuid(string id)
        {
            IEnumerable<F_ItemDetail> list = await _ItemDetailIService.GetList(id);
            var data = new
            {
                code = 0,
                msg = "",
                count = list.ToList().Count,
                data = list
            };
            return Json(data);
        }

        public async Task<IActionResult> AddGuid(string id,string fid)
        {
            F_ItemDetail mdoel = new F_ItemDetail();
            mdoel.F_itemID = id == null ? "" : id;
            mdoel.f_enablemark = 1;
            if (!string.IsNullOrEmpty(fid))
            {
                mdoel.F_ID = fid;
                mdoel = await _ItemDetailIService.IsTrue(fid);
            }
           ;
            return View(mdoel);
        }

        public async Task<IActionResult> GetItem_Detal(F_ItemDetail model)
        {
            if (ModelState.IsValid)
            {

               
                bool row = false;
                if (!string.IsNullOrEmpty(model.F_ID))
                {
                    row = await _ItemDetailIService.UpdateModel(model);
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.F_ItemCode))
                    {
                        F_ItemDetail sm = await _ItemDetailIService.IsTrue(model);
                        if (sm != null)
                        {
                            return Json(new { status = "error", message = "编号已经存在" });
                        }
                    }
                    model.F_ItemCode = string.IsNullOrEmpty(model.F_ItemCode) ? "" : model.F_ItemCode;
                    model.F_ID = Guid.NewGuid().ToString();
                    model.F_layer = 3;
                    model.f_sortcode = 1;
                    model.f_enablemark = 1;
                    model.F_CreatorTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    model.F_CreatorUserId = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString();
                    row = await _ItemDetailIService.InserModel(model);
                }
               
                if (row)
                {
                    return Json(new { status = "ok" });
                }
                else
                {
                    return Json(new { status = "error", message = "保存失败" });
                }
               
            }
            return Json(new { status = "error", message = "" });
        }

        public async Task<IActionResult> DeleteGuid(F_ItemDetail id)
        {
            bool result = await _ItemDetailIService.DeleteModel(id);
            if (result)
            {
                return Json(new { status = "ok", message = "" });
            }
            else
            {
                return Json(new { status = "error", message = "删除失败" });
            }

        }

        [HttpPost]
        public IActionResult AddkeyModel(Sys_Item model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.F_ID))
                {
                    _ItemIService.UpdateModel(model);
                }
                else
                {
                    model.F_ID = Guid.NewGuid().ToString();
                    model.F_CreatorUserId = ByteConvertHelper.Bytes2Object(HttpContext.Session.Get("UID")).ToString();
                    model.F_CreatorTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    model.F_Layer = 3;
                    model.f_sortcode = 1;
                    model.F_Encode = model.F_ID;
                    model.f_deleteMark = 1;
                    var result = _ItemIService.InserModel(model);
                }
                return Json(new { status = "ok" });
            }
            return Json(new { status = "error", message = "" });
           
        }
        public async Task<IActionResult> GetTree(string id)
        {
            List<Node> nodes = null;
            IEnumerable<Sys_Item> list = null;
            if (!string.IsNullOrEmpty(id))
            {
                list = await _ItemIService.GetList(id);
                nodes = await _ItemIService.GetListNode(list.ToList());
            }
            else
            {
                list = await _ItemIService.GetList();
                nodes = await _ItemIService.GetListNode(list.ToList());
            }
            return Json(nodes);
        }

		public async Task<IActionResult> GetLogInfoList(int page, int limit, string start,string end)
		{
			Loginfo loginfo = new Loginfo();
			loginfo.limit = page;
			loginfo.PageSize = limit;
			loginfo.start = start;
			loginfo.end = end;
			Tuple<int, List<Loginfo>> list = await _logIService.GetList(loginfo);
			var data = new
			{
				code = 0,
				msg = "",
				count = list.Item1,
				data = list.Item2
			};
			return Json(data);
			
		}

      

    }
}