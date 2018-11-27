using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ILogIService
    {
        Task<bool> InsertModel(Loginfo userInfo);

		Task<Tuple<int, List<Loginfo>>> GetList(Loginfo loginfo);

	}
}
