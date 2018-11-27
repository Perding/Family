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
   public  class ARefundesRepository : IARefundesRepository
    {
        private DapperFactory _factory;
        private IDapper _dapper;
        public ARefundesRepository(IConfiguration configuration) 
        {
            _factory = DapperFactory.GetInstance(configuration);
            _dapper = _factory.GetDapper();
        }


        public async Task<bool> GetListSql(Dictionary<object, string> dic)
        {
            return await _dapper.GetListSql(dic);
        }

        public async Task<IEnumerable<ARefundes>> Getlist(string stuid,int type)
        {
            return await _dapper.GetProce<ARefundes>("proc_Refund ", new { stuid = stuid,type=type });
        }
        public async Task<ARefund> GetModel(string id)
        {
            return await _dapper.GetSinger<ARefund>("select * from ARefund where id=@id ",new { id=id});
        }
        public async Task<IEnumerable<ARefundes>> GetPayList(string id)
        {
            return await _dapper.GetList<ARefundes>("select * from ARefundes where ArefundId=@ArefundId ", new { ArefundId = id });
        }
    }
}
