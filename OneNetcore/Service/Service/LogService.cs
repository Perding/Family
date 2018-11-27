using Entity;
using Repository.IReposotiry;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class LogService:ILogIService
    {
        private ILoginfoRepository _repository;

        public LogService(ILoginfoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> InsertModel(Loginfo userInfo)
        {
            return await _repository.InsertModel(userInfo);
        }


		public async Task<Tuple<int, List<Loginfo>>> GetList(Loginfo loginfo)
		{
			return await _repository.GetList(loginfo);
		}

	}
}
