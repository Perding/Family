using Entity;
using Repository.IReposotiry;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Service.Service
{
    public class UserInfoService : IUserInfoService
    {
        private IUserInfoRepository _repository;
        private IM_menuIService _m_MenuIService;

        public UserInfoService(IUserInfoRepository repository, IM_menuIService m_MenuIService)
        {
            _m_MenuIService = m_MenuIService;
            _repository = repository;
        }
        public async Task<IEnumerable<UserInfo>> GetList()
        {
            return await _repository.GetList();
        }

        public async Task<bool> Insert(UserInfo userInfo, string MidList)
        {
            Dictionary<object, string> dic = new Dictionary<object, string>();
            userInfo.U_IsAdmin = 0;
            if (!string.IsNullOrEmpty(userInfo.u_userid))
            {
                Usermenu u = new Usermenu();
                u.u_id = userInfo.u_userid;
                StringBuilder str = new StringBuilder();
                str.Append("update UserInfo set u_account=@u_account,u_realName=@u_realName,U_IP=@U_IP,u_isEnable=@u_isEnable,u_phone=@u_phone where u_userid=@u_userid");
                dic.Add(userInfo, str.ToString());
                dic.Add(u, "delete Usermenu where u_id=@u_id");
              
            }
            else
            {
                userInfo.u_userid= Guid.NewGuid().ToString();
                StringBuilder str = new StringBuilder();
                str.Append("insert into UserInfo(u_userid,u_account,u_realName,u_password,U_IP,u_isEnable,U_IsAdmin,u_phone,U_CreatorTime,U_CreatorUserId)");
                str.Append("values(@u_userid,@u_account,@u_realName,@u_password,@U_IP,@u_isEnable,@U_IsAdmin,@u_phone,@U_CreatorTime,@U_CreatorUserId)");
                dic.Add(userInfo,str.ToString());

            }
            string[] arr = MidList.Split(',');
            List<Usermenu> usermenus = new List<Usermenu>();
            IList<M_menu> data = await _m_MenuIService.Getlist() as IList<M_menu>;
            if (arr.Length > 0)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    usermenus.Add(new Usermenu());
                }
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] != "")
                    {
                        usermenus[i] = new Usermenu();
                        usermenus[i].F_ID = Guid.NewGuid().ToString();
                        usermenus[i].u_id = userInfo.u_userid;
                        usermenus[i].m_id = arr[i];
                        var M_PartentID = data.Where(t => t.M_ID == arr[i]).Select(t => new { t.M_PartentID }).FirstOrDefault();
                        usermenus[i].m_partentID = M_PartentID.M_PartentID;
                        StringBuilder str4 = new StringBuilder();
                        str4.Append("insert into Usermenu(f_id,u_id,m_id,m_partentID)values(@f_id,@u_id,@m_id,@m_partentID)");
                        dic.Add(usermenus[i], str4.ToString());
                    }
                }
            }
            return await _repository.GetListSql(dic);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }

        public async Task<UserInfo> GetUserName(UserInfo userInfo)
        {
            return await _repository.GetUserName(userInfo);
        }

        public async Task<UserInfo> GetModel(string id)
        {
            return await _repository.GetModel(id);
        }

		public async Task<bool> UpdatePasswork(UserInfo userInfo)
		{
			return await _repository.UpdatePasswork(userInfo);
		}
	}
}
