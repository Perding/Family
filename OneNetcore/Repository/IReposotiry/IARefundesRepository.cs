using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Repository.IReposotiry
{
   public  interface IARefundesRepository
    {
        Task<bool> GetListSql(Dictionary<object, string> dic);

        Task<IEnumerable<ARefundes>> Getlist(string stuid,int type);

        Task<IEnumerable<ARefundes>> GetPayList(string stuid);
        Task<ARefund> GetModel(string id);
    }
}
