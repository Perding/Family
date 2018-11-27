using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IReposotiry
{
    public interface ISys_ItemRepository
    {
        Task<IEnumerable<Sys_Item>> GetList();
        Task<IEnumerable<Sys_Item>> GetList(string id);
        Task<bool> InserModel(Sys_Item model);

        Task<Sys_Item> IsTrue(Sys_Item F_ItemCode);

        Task<bool> UpdateModel(Sys_Item model);

        Task<bool> DeleteModel(Sys_Item model);

		Task<Sys_Item> Daoru(decimal pay);
	}
}
