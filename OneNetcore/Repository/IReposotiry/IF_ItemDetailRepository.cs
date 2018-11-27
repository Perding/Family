using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IReposotiry
{
    public interface IF_ItemDetailRepository
    {
        Task<F_ItemDetail> IsTrue(F_ItemDetail F_ItemCode);

        Task<F_ItemDetail> IsTrue(string id);

        Task<IEnumerable<F_ItemDetail>> GetList(string id);
        Task<IEnumerable<F_ItemDetail>> GetList();

        Task<bool> InserModel(F_ItemDetail model);

        Task<bool> UpdateModel(F_ItemDetail model);

        Task<bool> DeleteModel(F_ItemDetail model);

		Task<F_ItemDetail> Serch(string name);

		
	}
}
