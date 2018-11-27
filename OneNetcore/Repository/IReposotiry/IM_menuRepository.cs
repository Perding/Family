using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IReposotiry
{
    public interface IM_menuRepository
    {
        Task<IEnumerable<M_menu>> Getlist();
        Task<IEnumerable<M_menu>> Getlist(string uid);

        Task<IEnumerable<M_menu>> GetChildrelist(string uid);
    }
      
}
