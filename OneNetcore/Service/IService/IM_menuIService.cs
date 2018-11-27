using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Service.IService
{
   public  interface IM_menuIService
    {
        Task<IEnumerable<M_menu>> Getlist();

        Task<IEnumerable<M_menu>> Getlist(string uid);

        Task<IEnumerable<M_menu>> GetChildrelist(string uid);
    }
}
