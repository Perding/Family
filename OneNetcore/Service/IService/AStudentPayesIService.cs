using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface AStudentPayesIService
    {
		Task<AStudentPayes> GetModel(string id);
	}
}
