using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Repository.IReposotiry
{
    public interface IUsermenuRepository
    {
        Task<IEnumerable<Usermenu>> GetUsermenu(string uid);
    }
}
