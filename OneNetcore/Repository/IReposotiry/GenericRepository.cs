using DapperData;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IReposotiry
{
   public abstract class GenericRepository
  {
   //     private DapperFactory _factory;
   //     private IDapper _dapper;
   //     public GenericRepository(IConfiguration configuration)
   //     {
   //         _factory = DapperFactory.GetInstance(configuration);
   //         _dapper = _factory.GetDapper();
   //     }
   //     public Task<bool> Delete(string sql, T id)
   //     {
   //         return _dapper.Delete(sql, id);
   //     }
   //     public async Task<IEnumerable<T>> GetList(string sql)
   //     {
   //         return await _dapper.GetList<T>(sql, null);
   //     }
   //     public async Task<T> GetStudent(string sql, T model)
   //     {
   //         return await _dapper.GetSinger(sql, model);
   //     }

   //     public async Task<bool> Insert(string sql, T model)
   //     {
   //         return await _dapper.Insert(sql, model);
   //     }

   //     public async Task<bool> Update(string sql, T model)
   //     {
   //         return await _dapper.Update(sql, model);
   //     }
    }
}
