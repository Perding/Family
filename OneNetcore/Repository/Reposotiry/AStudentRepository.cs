using DapperData;
using Entity;
using Microsoft.Extensions.Configuration;
using Repository.IReposotiry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Reposotiry
{
    public class AStudentRepository:IAStudentRepository
    {
        private DapperFactory _factory;
        private IDapper _dapper;
       
        public AStudentRepository(IConfiguration configuration) 
        {
           
            _factory = DapperFactory.GetInstance(configuration);
            _dapper = _factory.GetDapper();
        }
        public async Task<Tuple<int, List<AStudent>>> GetProcePage(Page model)
        {
            return await _dapper.GetProcePage<AStudent>("declare @RecordCount int exec proc_Serach "+model.pagecur+","+model.pagesaze+",N'"+model.where+"',"+model.type+" ,@RecordCount output");
        }
        public async Task<AStudent> IsCard(string card)
        {
            return await _dapper.Istrue<AStudent>("select *  from AStudent where Cards=@Cards", new { Cards = card });
        }

        /// <summary>
        /// 返回学号
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public async Task<int> CardNum()
        {
            int num = 1000;
            AStudent model=await _dapper.Istrue<AStudent>("select top 1 *  from AStudent order by StuNum desc");
            if (model!=null)
            {
                num = model.StuNum;
            }
            return await Task.Run(()=>num);
        }
        public  Task<bool> GetListSql(Dictionary<object,string> dic)
        {
            return  _dapper.GetListSql(dic);
        }

        public async Task<IEnumerable<AStudent>> GetStoreProc(string name = "")
        {
            return await _dapper.GetProce<AStudent>("proc_MonthDetail", new { name = name });
        }
        /// <summary>
        /// 修改学生状态未退学
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UpdateDrop(string id)
        {
            return await _dapper.Update("update AStudent set IsDrop=0 where ID=@ID",new { ID =id});
        }

        public async Task<AStudent> GetSql(string id)
        {
            AStudent m = null;
           var  obj= await _dapper.GetSql("select a.StuNum,a.Name,a.Cards,a.GradeID,a.SchoolID,a.TeacherID,a.PhoneNum,b.ID,b.StudID,b.PaymentID,b.CollectionID,b.PayDate from dbo.AStudent a inner join  AStudentPay b on a.ID=b.StudID where b.ID='" + id + "'");
            List<AStudent> list=obj as List<AStudent>;
            if (list.Count>0)
            {
                m = list[0];
               
            }
            return m;
        }


		public async Task<AStudent> GetModel(string stuid)
		{
			return await _dapper.Istrue<AStudent>("select * from [AStudent] where id=@id ",new { id=stuid});
		}

		/// <summary>
		/// 打印收据
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<Tuple<List<AStudent>, List<AStudentPay>>> GetDayin(string id)
		{
			return await _dapper.GetProcePageS<AStudent, AStudentPay>("exec proc_Dayin '"+id+"'");
		}
	}
}
