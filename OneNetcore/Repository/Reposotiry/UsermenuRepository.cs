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
   public class UsermenuRepository: IUsermenuRepository
    {
        private DapperFactory _factory;
        private IDapper _dapper;
        public UsermenuRepository(IConfiguration configuration)
        {
            _factory = DapperFactory.GetInstance(configuration);
            _dapper = _factory.GetDapper();
        }
        public async Task<IEnumerable<Usermenu>> GetUsermenu(string uid)
        {
            string  sql = "select* from Usermenu where u_id = @u_id";
           
            return await _dapper.GetList<Usermenu>(sql,new { u_id=uid});
        }
    }
}
