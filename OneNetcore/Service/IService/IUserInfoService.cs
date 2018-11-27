using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IUserInfoService
    {

        Task<IEnumerable<UserInfo>> GetList();
        Task<bool> Insert(UserInfo userInfo, string MidList);
        Task<bool> Delete(string id);

        Task<UserInfo> GetModel(string id);
        Task<UserInfo> GetUserName(UserInfo userInfo);

		Task<bool> UpdatePasswork(UserInfo userInfo);
	}
}
