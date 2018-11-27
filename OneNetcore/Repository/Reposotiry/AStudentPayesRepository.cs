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
   public  class AStudentPayesRepository : IAStudentPayesRepository
    {
        private DapperFactory _factory;
        private IDapper _dapper;
        public AStudentPayesRepository(IConfiguration configuration)
        {
            _factory = DapperFactory.GetInstance(configuration);
            _dapper = _factory.GetDapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AStudentPayes>> GetList(string id)
        {
            return await _dapper.GetList<AStudentPayes>("select * from AStudentPayes where StudentPayID=@StudentPayID",new { StudentPayID =id});
        }


		public async Task<AStudentPayes> GetModel(string id)
		{
			return await _dapper.Istrue<AStudentPayes>("select * from AStudentPayes where id=@id",new { id=id});
		}
	}
}
