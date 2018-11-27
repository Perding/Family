using DapperData;
using Entity;
using Microsoft.Extensions.Configuration;
using Repository.IReposotiry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Reposotiry
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private DapperFactory _factory;
        private IDapper _dapper;
        public UserInfoRepository(IConfiguration configuration) 
        {
            _factory = DapperFactory.GetInstance(configuration);
            _dapper = _factory.GetDapper();
        }

        public async Task<IEnumerable<UserInfo>> GetList()
        {
            return await _dapper.GetList<UserInfo>("select u_userid,u_isEnable, u_account, u_realName, isnull(U_IP, '')U_IP, isnull(u_phone, '')u_phone, isnull(convert(varchar(20), U_CreatorTime, 120), '')U_CreatorTimes from UserInfo where U_IsAdmin=0 order by u_isEnable desc ");
        }

        public async Task<bool> Insert(UserInfo userInfo)
        {
            StringBuilder str = new StringBuilder();
            str.Append("insert into UserInfo(u_userid,u_account,u_realName,u_password,U_IP,u_isEnable,U_IsAdmin,u_phone,U_CreatorTime,U_CreatorUserId)");
            str.Append("values(@u_userid,@u_account,@u_realName,@u_password,@U_IP,@u_isEnable,@U_IsAdmin,@u_phone,@U_CreatorTime,@U_CreatorUserId)");
            return await _dapper.Insert(str.ToString(),userInfo);
        }

        public async Task<bool> Delete(string id)
        {
            return await _dapper.Delete("delete UserInfo where u_userid=@u_userid",new { u_userid =id});
        }

        public async Task<bool> Update(UserInfo userInfo)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update UserInfo set u_account=@u_account,u_realName=@u_realName,U_IP=@U_IP,u_isEnable=@u_isEnable,u_phone=@u_phone where u_userid=@u_userid");
            return await _dapper.Update(str.ToString(),userInfo);
        }

        public async Task<UserInfo> GetModel(string id)
        {
            return await _dapper.GetSinger<UserInfo>("select * from UserInfo where u_userid=@u_userid",new { u_userid =id});
        }
        public async Task<bool> GetListSql(Dictionary<object, string> dic)
        {
            return await _dapper.GetListSql(dic);
        }
        public Task<UserInfo> GetModel(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public  Task<UserInfo> GetUserName(UserInfo userInfo)
        {
            return _dapper.Istrue<UserInfo>("select * from UserInfo where u_account=@u_account and replace(u_password,' ','')=@u_password", userInfo);
        }
		public async Task<bool> UpdatePasswork(UserInfo userInfo)
		{
			return await _dapper.Update("update UserInfo set u_password=@u_password where u_userid=@u_userid", userInfo);
		}


	}
}
