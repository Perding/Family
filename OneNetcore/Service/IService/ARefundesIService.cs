using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ARefundesIService
    {
        Task<bool> Insert(ARefundes aRefundes);
        Task<IEnumerable<ARefundes>> Getlist(string stuid,int type);

        Task<ARefund> GetModel(string id);

        Task<IEnumerable<ARefundes>> GetPayList(string id);
    }
}
