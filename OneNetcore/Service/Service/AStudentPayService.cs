using Entity;
using Repository.IReposotiry;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AStudentPayService: AStudentPayIService
    {
        private IAStudentPayRepository _repository;
        public AStudentPayService(IAStudentPayRepository iAStudentPayRepository)
        {
            _repository = iAStudentPayRepository;
        }

        public async Task<IEnumerable<AStudentPay>> GetList(string stuid,int type)
        {
            return await _repository.GetList(stuid,type);
        }

        public async Task<Tuple<List<Tone>, List<Ttwo>>> GetProcePage(string year)
        {
            return await _repository.GetProcePage(year);
        }
        public async Task<IEnumerable<AStudentPay>> GetListYear()
        {
            return await _repository.GetListYear();
        }

		public async Task<AStudentPay> GetModel(string id)
		{
			return await _repository.GetModel(id);
		}
	}
}
