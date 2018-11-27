using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IReposotiry
{
    public interface IUserInfoRepository
    {
        Task<IEnumerable<UserInfo>> GetList();
        Task<bool> GetListSql(Dictionary<object, string> dic);
        Task<bool> Insert(UserInfo userInfo);
        Task<bool> Update(UserInfo userInfo);
        Task<UserInfo> GetModel(string id);
        Task<bool> Delete(string id);
        Task<UserInfo> GetModel(UserInfo userInfo);
        Task<UserInfo> GetUserName(UserInfo userInfo);
		Task<bool> UpdatePasswork(UserInfo userInfo);
	}
}
