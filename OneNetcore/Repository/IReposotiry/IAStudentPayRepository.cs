using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IReposotiry
{
    public interface IAStudentPayRepository
    {
        Task<IEnumerable<AStudentPay>> GetList(string stuid,int type);
        Task<Tuple<List<Tone>, List<Ttwo>>> GetProcePage(string year);
        Task<IEnumerable<AStudentPay>> GetListYear();

		Task<AStudentPay> GetModel(string id);
    }
}
