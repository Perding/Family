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
    public class F_ItemDetailRepository : IF_ItemDetailRepository
    {
        private DapperFactory _factory;
        private IDapper _dapper;
        public F_ItemDetailRepository(IConfiguration configuration) 
        {
            _factory = DapperFactory.GetInstance(configuration);
            _dapper = _factory.GetDapper();
        }

        public async Task<IEnumerable<F_ItemDetail>> GetList(string id)
        {
            return await _dapper.GetList<F_ItemDetail>("select * from dbo.F_ItemDetail where F_itemID=@F_itemID  order by f_sortcode", new { F_itemID = id });
        }
        public async Task<IEnumerable<F_ItemDetail>> GetList()
        {
            return await _dapper.GetList<F_ItemDetail>("select * from dbo.F_ItemDetail");
        }
        public async Task<F_ItemDetail> IsTrue(F_ItemDetail F_ItemCode)
        {
            return await _dapper.Istrue<F_ItemDetail>("select F_ID from dbo.F_ItemDetail where F_itemID=@F_itemID and F_ItemCode=@F_ItemCode", F_ItemCode);
        }
        public async Task<F_ItemDetail> IsTrue(string id)
        {
            return await _dapper.Istrue<F_ItemDetail>("select * from dbo.F_ItemDetail where F_ID=@F_ID", new { F_ID =id});
        }

        public async Task<bool> InserModel(F_ItemDetail model)
        {
            StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO [Financial].[dbo].[F_ItemDetail]");
            str.Append("           ([F_ID] ,[F_itemID] ,[F_ItemCode],[F_ItemName] ,[F_layer] ,[f_sortcode],[f_deletemark] ,[f_enablemark],[F_CreatorTime],[F_CreatorUserId])");
            str.Append("values(@F_ID,@F_itemID,@F_ItemCode,@F_ItemName,@F_layer,@f_sortcode,@f_deletemark,@f_enablemark,@F_CreatorTime,@F_CreatorUserId)");
            return await _dapper.Insert(str.ToString(), model);
        }

        public async Task<bool> UpdateModel(F_ItemDetail model)
        {
            string sql = "update F_ItemDetail set F_ItemName=@F_ItemName,F_ItemCode=@F_ItemCode,f_enablemark=@f_enablemark where  F_ID=@F_ID";
            return await _dapper.Update(sql, model);
        }

        public async Task<bool> DeleteModel(F_ItemDetail model)
        {
            return await _dapper.Delete("delete F_ItemDetail where F_ID=@F_ID",model);
        }

		public async Task<F_ItemDetail> Serch(string name)
		{
			return await _dapper.Istrue<F_ItemDetail>("select  * from [dbo].[F_ItemDetail] where(f_itemname=@f_itemname or f_itemcode=@f_itemcode)",new { f_itemname=name, f_itemcode= name });
		}

	}
}
