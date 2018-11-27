using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Service.IService
{
    public interface IUsermenuIService
    {
        Task<IEnumerable<Usermenu>> Getlist(string uid);
    }
}
