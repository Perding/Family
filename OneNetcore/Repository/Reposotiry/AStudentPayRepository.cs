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
	public class AStudentPayRepository : IAStudentPayRepository
	{
		private DapperFactory _factory;
		private IDapper _dapper;
		public AStudentPayRepository(IConfiguration configuration)
		{
			_factory = DapperFactory.GetInstance(configuration);
			_dapper = _factory.GetDapper();
		}
		public async Task<IEnumerable<AStudentPay>> GetList(string stuid, int type)
		{
			return await _dapper.GetProce<AStudentPay>("proc_Tetail", new { stuid = stuid, type = type });
		}

		public async Task<Tuple<List<Tone>, List<Ttwo>>> GetProcePage(string year)
		{
			return await _dapper.GetProcePageS<Tone, Ttwo>(" exec proc_Tonji '" + year + "'  exec proc_Year '" + year + "'");
		}

		public async Task<IEnumerable<AStudentPay>> GetListYear()
		{
			return await _dapper.GetList<AStudentPay>("select distinct DateName(year,paydate)years from dbo.AStudentPay order by DateName(year,paydate) desc");
		}

		public async Task<AStudentPay> GetModel(string id)
		{
			return await _dapper.Istrue<AStudentPay>("select * from  AStudentPay where id=@id",new { id=id});
		}
	}
}
