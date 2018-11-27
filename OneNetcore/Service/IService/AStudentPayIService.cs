using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Service.IService
{
    public interface AStudentPayIService
    {
        Task<IEnumerable<AStudentPay>> GetList(string stuid,int type);
        Task<Tuple<List<Tone>, List<Ttwo>>> GetProcePage(string year);
        Task<IEnumerable<AStudentPay>> GetListYear();

		/// <summary>
		/// 获取 AStudentPay 表实体
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<AStudentPay> GetModel(string id);

	}
}
