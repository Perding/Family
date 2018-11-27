using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IReposotiry
{
    public interface IAStudentRepository
    {
        Task<AStudent> IsCard(string card);
        Task<int> CardNum();

        Task<bool> GetListSql(Dictionary<object,string> dic);
        Task<IEnumerable<AStudent>> GetStoreProc(string name="");

        Task<bool> UpdateDrop(string id);

        Task<AStudent> GetSql(string id);

		Task<AStudent> GetModel(string stuid);

		Task<Tuple<int, List<AStudent>>> GetProcePage(Page model);

		Task<Tuple<List<AStudent>, List<AStudentPay>>> GetDayin(string id);
	}
}
