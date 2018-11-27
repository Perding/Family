using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using Entity.Viewmodel;

namespace Service.IService
{
    public interface AStudentIService
    {
        Task<Tuple<bool, string>> IsList(AStudent model);
        Task<bool> Update(AStudent model);
        Task<IEnumerable<AStudent>> GetStoreProc(string name = "");
        Task<Tuple<int, List<AStudent>>> GetProcePage(AStudent model,int type);
        Task<AStudent> IsCard(string card);

        Task<bool> DeleteStudent(string id,string type,Loginfo info);

        Task<AStudent> GetSql(string id);

         string BuildWorkbook(List<AStudent> model,string path);

		/// <summary>
		/// 导入EXcel
		/// </summary>
		/// <param name="list1"></param>
		/// <param name="list2"></param>
		/// <param name="uid"></param>
		/// <returns></returns>
		Task<bool> DaoExcel(List<AStudent> list1, List<AStudentPay> list2,string uid);
		/// <summary>
		/// 根据编号获取实体类型
		/// </summary>
		/// <param name="stuid"></param>
		/// <returns></returns>

		Task<AStudent> GetModel(string stuid);
		/// <summary>
		/// 打印收据
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<Tuple<List<AStudent>, List<AStudentPay>>> GetDayin(string id);


	}
}
