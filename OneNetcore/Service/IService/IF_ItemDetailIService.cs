using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IF_ItemDetailIService
    {
        Task<F_ItemDetail> IsTrue(F_ItemDetail F_ItemCode);

        Task<F_ItemDetail> IsTrue(string id);

        Task<bool> InserModel(F_ItemDetail model);

        Task<IEnumerable<F_ItemDetail>> GetList(string id);
        Task<IEnumerable<F_ItemDetail>> GetList();
        Task<bool> UpdateModel(F_ItemDetail model);

        Task<bool> DeleteModel(F_ItemDetail model);

    }
}
