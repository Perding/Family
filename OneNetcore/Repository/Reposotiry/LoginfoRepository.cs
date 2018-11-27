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
    public class LoginfoRepository : ILoginfoRepository
    {
        private DapperFactory _factory;
        private IDapper _dapper;
        public LoginfoRepository(IConfiguration configuration) 
        {
            _factory = DapperFactory.GetInstance(configuration);
            _dapper = _factory.GetDapper();
        }
        public async Task<bool> InsertModel(Loginfo LogInfo)
        {
            return await _dapper.Insert("insert into Loginfo(L_id,u_id,u_account,l_content,l_date,L_ip) VALUES(@L_id,@u_id,@u_account,@l_content,@l_date,@L_ip)", LogInfo);
        }

		public async Task<Tuple<int, List<Loginfo>>> GetList(Loginfo loginfo)
		{
			string sqlcount = "select count(1) from [Loginfo] where 1=1";
			string sql = "  select * from ( select ROW_NUMBER() over (order by l_date desc)number,u_account,l_content,convert(varchar(20),l_date,120)l_dates,l_date,l_ip from [Loginfo] )ta where 1=1";
			if (!string.IsNullOrEmpty(loginfo.start))
			{
				sqlcount += " and  l_date>='" + loginfo.start + "'";
				sql += " and  l_date>='"+loginfo.start+"'";
			}
			if (!string.IsNullOrEmpty(loginfo.end))
			{
				sqlcount += " and  l_date<='"+ loginfo.end + "'";
				sql += " and  l_date<='" + loginfo.end + "'";
			}
			sql += " and   number between str(("+loginfo.limit+"-1)*"+loginfo.PageSize+"+1) AND str("+loginfo.limit+"* "+loginfo.PageSize+")";

			sqlcount += "  "+sql;
			return await _dapper.GetProcePage<Loginfo>(sqlcount);
		}

	}
}
