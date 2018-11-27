using Entity;
using Repository.IReposotiry;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Service.Service
{
   public  class AStudentPayesService: AStudentPayesIService
	{
		private IAStudentPayesRepository _aStudentPayesRepository;
		public AStudentPayesService(IAStudentPayesRepository aStudentPayesRepository)
		{
			_aStudentPayesRepository = aStudentPayesRepository;
		}

		public async Task<AStudentPayes> GetModel(string id)
		{
			return await _aStudentPayesRepository.GetModel(id);
		}
	}
}
