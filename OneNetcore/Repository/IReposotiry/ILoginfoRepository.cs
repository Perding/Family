using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IReposotiry
{
   public interface ILoginfoRepository
    {
        Task<bool> InsertModel(Loginfo userInfo);

		/// <summary>
		/// 日志查询
		/// </summary>
		/// <returns></returns>
		Task<Tuple<int, List<Loginfo>>> GetList(Loginfo loginfo);
    }
}
