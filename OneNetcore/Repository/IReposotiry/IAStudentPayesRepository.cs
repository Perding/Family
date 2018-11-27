using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Repository.IReposotiry
{
    public interface IAStudentPayesRepository
    {
        Task<IEnumerable<AStudentPayes>> GetList(string id);

		Task<AStudentPayes> GetModel(string id);
	}
}
