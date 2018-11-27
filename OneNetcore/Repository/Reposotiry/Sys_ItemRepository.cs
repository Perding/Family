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
    public class Sys_ItemRepository : ISys_ItemRepository
    {
        private DapperFactory _factory;
        private IDapper _dapper;
        public Sys_ItemRepository(IConfiguration configuration)
        {
            _factory = DapperFactory.GetInstance(configuration);
            _dapper = _factory.GetDapper();
        }
        public async Task<IEnumerable<Sys_Item>> GetList()
        {
            return await _dapper.GetList<Sys_Item>("select * from dbo.Sys_Item   order by f_sortcode");
        }
        public async Task<IEnumerable<Sys_Item>> GetList(string id)
        {
            return await _dapper.GetList<Sys_Item>("select * from dbo.Sys_Item where F_ParentID=@F_ParentID  order by f_sortcode", new { F_ParentID=id });
        }
        public async Task<bool> InserModel(Sys_Item model)
        {
            StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO [Financial].[dbo].[Sys_Item]");
            str.Append("([F_ID],[F_ParentID],[F_Encode],[F_FullName],[F_Layer],[f_sortcode],[f_deleteMark],[F_EnableMark] ,[F_Pay],[F_CreatorTime],[F_CreatorUserId] )");
            str.Append("values(@F_ID,@F_ParentID,@F_Encode,@F_FullName,@F_Layer,@f_sortcode,@f_deleteMark,@F_EnableMark,@F_Pay,@F_CreatorTime,@F_CreatorUserId)");
            return await _dapper.Insert(str.ToString(),model);
        }

        public async Task<Sys_Item> IsTrue(Sys_Item F_ItemCode)
        {
            return await _dapper.Istrue<Sys_Item>("select * from Sys_Item where F_ID=@F_ID  ", F_ItemCode);
        }

        public async Task<bool> UpdateModel(Sys_Item model)
        {
            string sql = "update Sys_Item set F_FullName=@F_FullName,F_Pay=@F_Pay,F_EnableMark=@F_EnableMark where  F_ID=@F_ID";
            return await _dapper.Update(sql,model);
        }

        public async Task<bool> DeleteModel(Sys_Item model)
        {
            string sql = "delete Sys_Item where  F_ID=@F_ID";
            return await _dapper.Delete(sql, model);
        }

		public async Task<Sys_Item> Daoru(decimal pay)
		{
			return await _dapper.Istrue<Sys_Item>("select * from Sys_Item where F_pay=@F_pay",new { F_pay=pay });
		}

	}
}
