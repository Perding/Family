using Entity;
using Repository.IReposotiry;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NPOI.HPSF;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using Common;
using Entity.Viewmodel;
using System.IO;

namespace Service.Service
{
    public class AStudentService : AStudentIService
    {
        private IAStudentRepository _repository; 
        private ISys_ItemRepository _Sys_Itempository;
        private IAStudentPayesRepository _aStudentPayesRepository;
		private IF_ItemDetailRepository _f_ItemDetailRepository;
		private ILoginfoRepository _loginfoRepository;
		public AStudentService(IAStudentRepository repository, ISys_ItemRepository sys_ItemRepository, IAStudentPayesRepository aStudentPayesRepository, IF_ItemDetailRepository f_ItemDetailRepository, ILoginfoRepository loginfoRepository)
        {
			_loginfoRepository = loginfoRepository;
            _aStudentPayesRepository = aStudentPayesRepository;
            _Sys_Itempository = sys_ItemRepository;
            _repository = repository;
			_f_ItemDetailRepository = f_ItemDetailRepository;

		}
        /// <summary>
        /// 删除学生所有信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteStudent(string id,string type, Loginfo loginfo)
        {
            Dictionary<object, string> dic = new Dictionary<object, string>();
		

			AStudent m1 = new AStudent();
          
            AStudentPay m2 = new AStudentPay();
         
            AStudentPayes m3 = new AStudentPayes();
          
            try
            {
                string sql1, sql2, sql3;
                if (type=="1")
                {
                    m1.ID = id;
                    m2.StudID = id;
                    m3.StuID = id;
                    sql1 = "delete AStudent where id=@id";
                    sql2 = "delete AStudentPay where studid=@studid";
                    sql3 = "delete AStudentPayes where StuID=@StuID";
                    dic.Add(m1, sql1);
                    dic.Add(m2, sql2);
                    dic.Add(m3, sql3);
                }
                if (type=="2")
                {
                    m2.ID = id;
                    m3.StudentPayID = id;
                    sql2= "delete AStudentPay where id=@id";
                    sql3 = "delete AStudentPayes where StudentPayID=@StudentPayID";
                    dic.Add(m2, sql2);
                    dic.Add(m3, sql3);
                }
                if (type=="3")
                {
                    ARefund m5 = new ARefund();
                    ARefundes m6 = new ARefundes();
                    m5.ID = id;
                    m6.ArefundId = id;
                    sql2 = "delete ARefund where id=@id";
                    sql3 = "delete ARefundes where ArefundId=@ArefundId";
                    dic.Add(m5, sql2);
                    dic.Add(m6, sql3);
                }
            }
            catch (Exception)
            {

                throw;
            }
         return await _repository.GetListSql(dic);

        }
        public async Task<IEnumerable<AStudent>> GetStoreProc(string name = "")
        {
            
            return await _repository.GetStoreProc(name);
        }
        public async Task<Tuple<int, List<AStudent>>> GetProcePage(AStudent model,int type)
        {
            Page m = new Page();
            m.type = type;
            m.pagecur = model.pageIndex;
            m.pagesaze = model.pageSize;
            m.where = "1=1";
            if (!string.IsNullOrEmpty(model.TimeStart))
            {
                m.where += " and b.PayDate>=''"+model.TimeStart+"''";
            }
            if (!string.IsNullOrEmpty(model.TimeEnd))
            {
                m.where += " and b.PayDate<=''" + model.TimeEnd + "''";
            }
            if (!string.IsNullOrEmpty(model.GradeID))
            {
                m.where += " and GradeID=''"+model.GradeID+"''";
            }
            if (!string.IsNullOrEmpty(model.TeacherID))
            {
                m.where += " and TeacherID=''" + model.TeacherID + "''";
            }
            if (!string.IsNullOrEmpty(model.Name))
            {
                m.where += " and  name like  ''%"+model.Name+"%'' ";
            }
          
            return await _repository.GetProcePage(m);
        }

        public async Task<AStudent> IsCard(string card)
        {
           
            return await _repository.IsCard(card);
        }

        public async Task<AStudent> GetSql(string id)
        {
            AStudent m= await _repository.GetSql(id);
            if (m!=null)
            {
                m.GetAStudentPayes = await _aStudentPayesRepository.GetList(m.Owers.ID);
            }
            return m;
        }

		public async Task<Tuple<List<AStudent>, List<AStudentPay>>> GetDayin(string id)
		{
			return await _repository.GetDayin(id);
		}

		public async Task<bool> Update(AStudent model)
        {
            Dictionary<object, string> dic = new Dictionary<object, string>();
            AStudent m1 = model;
            AStudentPay m2 = new AStudentPay();
            
            m2.ID = model.ID;
            AStudent result = await _repository.IsCard(model.Cards);
            if (result == null)
            {
               
                m1.ID = Guid.NewGuid().ToString();
                m1.StuNum = await _repository.CardNum() + 1;
                m1.IsDrop = 1;
                m1.F_CreatorTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                m1.F_CreatorUserId = model.F_lastModifyUserId;
                m2.F_CreatorTime = m1.F_CreatorTime;
                m2.F_CreatorUserId = m1.F_CreatorUserId;
                m1.F_DeleteMark = 1;
                StringBuilder str = new StringBuilder();
                str.Append("insert into AStudent(ID,StuNum,Name,Cards,GradeID,SchoolID,TeacherID,PhoneNum,IsDrop,F_CreatorTime,F_CreatorUserId)");
                str.Append("values(@ID,@StuNum,@Name,@Cards,@GradeID,@SchoolID,@TeacherID,@PhoneNum,@IsDrop,@F_CreatorTime,@F_CreatorUserId)");
                dic.Add(m1, str.ToString());
            }
            else
            {
                m1.ID = result.ID;
                m1.F_LastModifyTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                m1.F_lastModifyUserId = model.F_lastModifyUserId;
                //m2.F_CreatorTime = m1.F_LastModifyTime;
                //m2.F_CreatorUserId = m1.F_lastModifyUserId;
                StringBuilder str = new StringBuilder();
                str.Append("update AStudent set name=@name,cards=@cards,gradeid=@gradeid,schoolid=@schoolid,teacherid=@teacherid,phonenum=@phonenum,F_LastModifyTime=@F_LastModifyTime,F_lastModifyUserId=@F_lastModifyUserId where ID=@id");
                dic.Add(m1, str.ToString());
            }


           

            string[] arr = model.html.Split('|');
            if (arr.Length > 0)
            {
                string[] brr = arr[0].Split(',');
                if (!string.IsNullOrEmpty(brr[2]))
                {
                   
                    m2.StudID = m1.ID;
                    m2.PaymentID = model.PaymentID;
                    m2.CollectionID = model.CollectionID;
                    m2.PayDate = DateTime.Parse(model.PayDate);//
                    m2.Note = model.Note;
                    m2.F_DeleteMark = 1;
                    m2.F_LastModifyTime= m1.F_LastModifyTime;
                    m2.F_lastModifyUserId = m1.F_lastModifyUserId;
                    StringBuilder str1 = new StringBuilder();
                    str1.Append("update AStudentPay set PaymentID=@PaymentID,F_LastModifyTime=@F_LastModifyTime,F_lastModifyUserId=@F_lastModifyUserId ,CollectionID=@CollectionID,PayDate=@PayDate,Note=@Note where id=@id");
                    dic.Add(m2, str1.ToString());
                    StringBuilder str2 = new StringBuilder();
                    AStudentPayes m3= new AStudentPayes();
                    m3.StudentPayID = m2.ID;
                    str2.Append("delete AStudentPayes where StudentPayID=@StudentPayID");
                    dic.Add(m3, str2.ToString());
                    List<AStudentPayes> vs = new List<AStudentPayes>();
                    for (int j = 0; j < arr.Length; j++)
                    {
                        vs.Add(new AStudentPayes());
                    }
                    var s = vs[0];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string[] arrone = arr[i].Split(',');
                        if (!string.IsNullOrEmpty(arrone[0]))
                        {
                            vs[i] = new AStudentPayes();
                            vs[i].ID = Guid.NewGuid().ToString();
                            vs[i].StudentPayID = m2.ID;
                            vs[i].CollectionInfo = arrone[1];
                            Sys_Item item = new Sys_Item();
                            item.F_ID = arrone[0].ToString();
                            item = await _Sys_Itempository.IsTrue(item);
                            vs[i].CollentPartenID = item.F_Encode;
                            if (!string.IsNullOrEmpty(arrone[2]))
                            {
                                vs[i].ShouldPay = decimal.Parse(arrone[2]);
                            }
                            else
                            {
                                vs[i].ShouldPay = 0;
                            }
                            if (!string.IsNullOrEmpty(arrone[3]))
                            {
                                vs[i].Discount = decimal.Parse(arrone[3]);
                            }
                            else
                            {
                                vs[i].Discount = 0;
                            }
                            if (!string.IsNullOrEmpty(arrone[4]))
                            {
                                vs[i].Paid = decimal.Parse(arrone[4]);
                            }
                            else
                            {
                                vs[i].Paid = 0;
                            }
                            vs[i].F_CreatorTime = m2.F_LastModifyTime;
                            vs[i].F_CreatorUserId = m2.F_lastModifyUserId;
                            vs[i].StuID = m1.ID;
                            StringBuilder str4 = new StringBuilder();
                            str4.Append("insert into AStudentPayes(id,StuID,StudentPayID,CollectionInfo,CollentPartenID,ShouldPay,Discount,Paid,F_CreatorTime,F_CreatorUserId)");
                            str4.Append("values(@id,@StuID,@StudentPayID,@CollectionInfo,@CollentPartenID,@ShouldPay,@Discount,@Paid,@F_CreatorTime,@F_CreatorUserId)");
                            dic.Add(vs[i], str4.ToString());
                        }
                    }
                }

            }
            return await _repository.GetListSql(dic);

        }
        public async Task<Tuple<bool,string>> IsList(AStudent model)
        {
            Dictionary<object, string> dic = new Dictionary<object, string>();
            AStudent m1= model;
          
            AStudentPay m2 = new AStudentPay();
            m2.ID = "";
            AStudent result = await _repository.IsCard(model.Cards);
            if (result == null)
            {
			
                m1.ID = Guid.NewGuid().ToString();
                m1.StuNum = await _repository.CardNum() + 1;
                m1.IsDrop = 1;
                m1.F_CreatorTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                m1.F_CreatorUserId = model.F_lastModifyUserId;
                m2.F_CreatorTime = m1.F_CreatorTime;
                m2.F_CreatorUserId = m1.F_CreatorUserId;
                m1.F_DeleteMark = 1;
                StringBuilder str = new StringBuilder();
                str.Append("insert into AStudent(ID,StuNum,Name,Cards,GradeID,SchoolID,TeacherID,PhoneNum,IsDrop,F_CreatorTime,F_CreatorUserId)");
                str.Append("values(@ID,@StuNum,@Name,@Cards,@GradeID,@SchoolID,@TeacherID,@PhoneNum,@IsDrop,@F_CreatorTime,@F_CreatorUserId)");
                dic.Add(m1,str.ToString());
            }
            else
            {
                m1.ID = result.ID;
                m1.F_LastModifyTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                m1.F_lastModifyUserId = model.F_lastModifyUserId;
                m2.F_CreatorTime = m1.F_LastModifyTime;
                m2.F_CreatorUserId = m1.F_lastModifyUserId;
                StringBuilder str = new StringBuilder();
                str.Append("update AStudent set name=@name,cards=@cards,gradeid=@gradeid,schoolid=@schoolid,teacherid=@teacherid,phonenum=@phonenum,F_LastModifyTime=@F_LastModifyTime,F_lastModifyUserId=@F_lastModifyUserId where ID=@id");
                dic.Add(m1,str.ToString());
            }
            string[] arr = model.html.Split('|');
            if (arr.Length > 0)
            {
                string[] brr = arr[0].Split(',');
                if (!string.IsNullOrEmpty(brr[2]))
                {
                    m2.ID = Guid.NewGuid().ToString();
                    m2.StudID = m1.ID;
                    m2.PaymentID = model.PaymentID;
                    m2.CollectionID = model.CollectionID;
                    m2.PayDate = DateTime.Parse(model.PayDate);//
                    m2.Note = model.Note;
                    m2.F_DeleteMark = 1;
                   
                    StringBuilder str1 = new StringBuilder();
                    str1.Append("insert into AStudentPay(id,studID,PaymentID,CollectionID,PayDate,Note,F_DeleteMark,F_CreatorTime,F_CreatorUserId)");
                    str1.Append("values(@id,@studID,@PaymentID,@CollectionID,@PayDate,@Note,@F_DeleteMark,@F_CreatorTime,@F_CreatorUserId)");
                    dic.Add(m2,str1.ToString());
                    List<AStudentPayes> vs = new List<AStudentPayes>();
                    for (int j = 0; j < arr.Length; j++)
                    {
                        vs.Add(new AStudentPayes());
                    }
                    var s = vs[0];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string[] arrone = arr[i].Split(',');
                        if (!string.IsNullOrEmpty(arrone[0]))
                        {
                            vs[i] = new AStudentPayes();
                            vs[i].ID = Guid.NewGuid().ToString();
                            vs[i].StudentPayID = m2.ID;
                            vs[i].CollectionInfo = arrone[1];
                            Sys_Item item = new Sys_Item();
                            item.F_ID = arrone[0].ToString();
                            item = await _Sys_Itempository.IsTrue(item);
                            vs[i].CollentPartenID = item.F_Encode;
                            if (!string.IsNullOrEmpty(arrone[2]))
                            {
                                vs[i].ShouldPay = decimal.Parse(arrone[2]);
                            }
                            else
                            {
                                vs[i].ShouldPay = 0;
                            }
                            if (!string.IsNullOrEmpty(arrone[3]))
                            {
                                vs[i].Discount = decimal.Parse(arrone[3]);
                            }
                            else
                            {
                                vs[i].Discount = 0;
                            }
                            if (!string.IsNullOrEmpty(arrone[4]))
                            {
                                vs[i].Paid = decimal.Parse(arrone[4]);
                            }
                            else
                            {
                                vs[i].Paid = 0;
                            }
                            vs[i].F_CreatorTime = m2.F_CreatorTime;
                            vs[i].F_CreatorUserId = m2.F_CreatorUserId;
                            vs[i].StuID = m1.ID;

                            StringBuilder str4 = new StringBuilder();
                            str4.Append("insert into AStudentPayes(id,StuID,StudentPayID,CollectionInfo,CollentPartenID,ShouldPay,Discount,Paid,F_CreatorTime,F_CreatorUserId)");
                            str4.Append("values(@id,@StuID,@StudentPayID,@CollectionInfo,@CollentPartenID,@ShouldPay,@Discount,@Paid,@F_CreatorTime,@F_CreatorUserId)");

                            dic.Add(vs[i],str4.ToString());
                        }
                    }
                }
            
            }
            bool ret = await _repository.GetListSql(dic);
            return new Tuple<bool,string>(ret,m2.ID);
        }
		public async Task<bool> DaoExcel(List<AStudent> list1, List<AStudentPay> list2,string uid)
		{
			Dictionary<object, string> dic = new Dictionary<object, string>();
			if (list1.Count>0)
			{
				List<AStudent> vs = new List<AStudent>();
				List<AStudentPay> ts = new List<AStudentPay>();
				List<AStudentPayes> b = new List<AStudentPayes>();
				for (int j = 0; j < list1.Count; j++)
				{
					vs.Add(new AStudent());
					ts.Add(new AStudentPay());
					b.Add(new AStudentPayes());
					b.Add(new AStudentPayes());
					b.Add(new AStudentPayes());
				}
				int tu = 0;
				foreach (var item in list1)
				{
					if (list2[tu].xa!=0|| list2[tu].za!=0|| list2[tu].wa!=0)
					{
						try
						{
							vs[tu] = new AStudent();
							AStudent result = await _repository.IsCard(item.Cards);
							if (result == null)
							{
								if (!string.IsNullOrEmpty(item.GradeID))
								{
									F_ItemDetail f_item = await _f_ItemDetailRepository.Serch(item.GradeID);
									if (f_item == null)
									{
										vs[tu].GradeID = "";
									}
									else
									{
										vs[tu].GradeID = f_item.F_ID;
									}
								}
								else
								{
									vs[tu].GradeID = "";
								}
								if (!string.IsNullOrEmpty(item.TeacherID))
								{
									F_ItemDetail f_item1 = await _f_ItemDetailRepository.Serch(item.TeacherID);
									if (f_item1 == null)
									{
										vs[tu].TeacherID = "";
									}
									else
									{
										vs[tu].TeacherID = f_item1.F_ID;
									}

								}
								else
								{
									vs[tu].TeacherID = "";
								}
								if (!string.IsNullOrEmpty(item.SchoolID))
								{
									F_ItemDetail f_item2 = await _f_ItemDetailRepository.Serch(item.SchoolID);
									if (f_item2 == null)
									{
										vs[tu].SchoolID = "";
									}
									else
									{
										vs[tu].SchoolID = f_item2.F_ID;
									}
								}
								else
								{
									vs[tu].SchoolID = "";
								}
								vs[tu].ID = Guid.NewGuid().ToString();
								vs[tu].Cards = item.Cards;
								vs[tu].Name = item.Name;
								if (tu==0)
								{
									vs[tu].StuNum = await _repository.CardNum() + 1;
								}
								else
								{
									vs[tu].StuNum = vs[tu - 1].StuNum + 1;
								}
								
								vs[tu].IsDrop = 1;
								vs[tu].PhoneNum = item.PhoneNum;
								vs[tu].F_CreatorTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
								vs[tu].F_CreatorUserId = uid;
								vs[tu].F_DeleteMark = 1;
								StringBuilder str = new StringBuilder();
								str.Append("insert into AStudent(ID,StuNum,Name,Cards,GradeID,SchoolID,TeacherID,PhoneNum,IsDrop,F_CreatorTime,F_CreatorUserId)");
								str.Append("values(@ID,@StuNum,@Name,@Cards,@GradeID,@SchoolID,@TeacherID,@PhoneNum,@IsDrop,@F_CreatorTime,@F_CreatorUserId)");
								dic.Add(vs[tu], str.ToString());
							}
							else
							{
								F_ItemDetail f_item = await _f_ItemDetailRepository.Serch(item.GradeID);
								if (f_item == null)
								{
									vs[tu].GradeID = "";
								}
								else
								{
									vs[tu].GradeID = f_item.F_ID;
								}
								F_ItemDetail f_item1 = await _f_ItemDetailRepository.Serch(item.TeacherID);
								if (f_item1 == null)
								{
									vs[tu].TeacherID = "";
								}
								else
								{
									vs[tu].TeacherID = f_item1.F_ID;
								}
								F_ItemDetail f_item2 = await _f_ItemDetailRepository.Serch(item.SchoolID);
								if (f_item2 == null)
								{
									vs[tu].SchoolID = "";
								}
								else
								{
									vs[tu].SchoolID = f_item2.F_ID;
								}
								vs[tu].Cards = item.Cards;
								vs[tu].ID = result.ID;
								vs[tu].F_LastModifyTime = vs[tu].F_CreatorTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
								vs[tu].F_lastModifyUserId = vs[tu].F_CreatorUserId = uid;
								vs[tu].PhoneNum = item.PhoneNum;
								vs[tu].Name = item.Name;
								StringBuilder str = new StringBuilder();
								str.Append("update AStudent set name=@name,cards=@cards,gradeid=@gradeid,schoolid=@schoolid,teacherid=@teacherid,phonenum=@phonenum,F_LastModifyTime=@F_LastModifyTime,F_lastModifyUserId=@F_lastModifyUserId where ID=@id");
								dic.Add(vs[tu], str.ToString());
							}
							#region
							ts[tu] = new AStudentPay();
							ts[tu].ID = Guid.NewGuid().ToString();
							ts[tu].StudID = vs[tu].ID;
							if (!string.IsNullOrEmpty(list2[tu].PaymentID))
							{
								F_ItemDetail f_item4 = await _f_ItemDetailRepository.Serch(list2[tu].PaymentID);
								if (f_item4 == null)
								{
									ts[tu].PaymentID = "";
								}
								else
								{
									ts[tu].PaymentID = f_item4.F_ID;
								}
							}
							else
							{
								ts[tu].PaymentID = "";
							}
							if (!string.IsNullOrEmpty(list2[tu].CollectionID))
							{
								F_ItemDetail f_item4 = await _f_ItemDetailRepository.Serch(list2[tu].CollectionID);
								if (f_item4 == null)
								{
									ts[tu].CollectionID = "";
								}
								else
								{
									ts[tu].CollectionID = f_item4.F_ID;
								}
							}
							else
							{
								ts[tu].CollectionID = "";
							}
							ts[tu].PayDate = DateTime.Parse(list2[tu].PayDateString);//
							ts[tu].Note = list2[tu].Note;
							ts[tu].F_DeleteMark = 1;
							ts[tu].F_CreatorTime = vs[tu].F_CreatorTime;
							ts[tu].F_CreatorUserId = vs[tu].F_CreatorUserId;
							StringBuilder str1 = new StringBuilder();
							str1.Append("insert into AStudentPay(id,studID,PaymentID,CollectionID,PayDate,Note,F_CreatorTime,F_CreatorUserId)");
							str1.Append("values(@id,@studID,@PaymentID,@CollectionID,@PayDate,@Note,@F_CreatorTime,@F_CreatorUserId)");
							dic.Add(ts[tu], str1.ToString());
							#endregion
							b[tu] = new AStudentPayes();
							b[tu].ID = Guid.NewGuid().ToString();
							b[tu].StuID = vs[tu].ID;
							b[tu].StudentPayID = ts[tu].ID;
							b[tu].F_CreatorTime = ts[tu].F_CreatorTime;
							b[tu].F_CreatorUserId = ts[tu].F_CreatorUserId;
							if (list2[tu].xa != 0)
							{
								Sys_Item sys_item = await _Sys_Itempository.Daoru(list2[tu].xa);
								if (sys_item != null)
								{
									b[tu].CollectionInfo = sys_item.F_ID;
									b[tu].CollentPartenID = "key1";
									b[tu].ShouldPay = list2[tu].xa;
									b[tu].Discount = list2[tu].xb;
									b[tu].Paid = list2[tu].xc;
									StringBuilder str5 = new StringBuilder();
									str5.Append("insert into AStudentPayes(id,StuID,StudentPayID,CollectionInfo,CollentPartenID,ShouldPay,Discount,Paid,F_CreatorTime,F_CreatorUserId)");
									str5.Append("values(@id,@StuID,@StudentPayID,@CollectionInfo,@CollentPartenID,@ShouldPay,@Discount,@Paid,@F_CreatorTime,@F_CreatorUserId)");
									dic.Add(b[tu], str5.ToString());
								}
							}
							if (list2[tu].za != 0)
							{
								Sys_Item sys_item1 = await _Sys_Itempository.Daoru(list2[tu].za);
								if (sys_item1 != null)
								{
									b[tu + 1] = new AStudentPayes();
									b[tu + 1].ID = Guid.NewGuid().ToString();
									b[tu + 1].StuID = vs[tu].ID;
									b[tu + 1].StudentPayID = ts[tu].ID;
									b[tu + 1].F_CreatorTime = ts[tu].F_CreatorTime;
									b[tu + 1].F_CreatorUserId = ts[tu].F_CreatorUserId;
									b[tu + 1].CollectionInfo = sys_item1.F_ID;
									b[tu + 1].CollentPartenID = "key2";
									b[tu + 1].ShouldPay = list2[tu].za;
									b[tu + 1].Discount = list2[tu].zb;
									b[tu + 1].Paid = list2[tu].zc;
									StringBuilder str6 = new StringBuilder();
									str6.Append("insert into AStudentPayes(id,StuID,StudentPayID,CollectionInfo,CollentPartenID,ShouldPay,Discount,Paid,F_CreatorTime,F_CreatorUserId)");
									str6.Append("values(@id,@StuID,@StudentPayID,@CollectionInfo,@CollentPartenID,@ShouldPay,@Discount,@Paid,@F_CreatorTime,@F_CreatorUserId)");
									dic.Add(b[tu + 1], str6.ToString());
								}
							}
							if (list2[tu].wa != 0)
							{
								Sys_Item sys_item2 = await _Sys_Itempository.Daoru(list2[tu].wa);
								if (sys_item2 != null)
								{
									b[tu + 2] = new AStudentPayes();
									b[tu + 2].ID = Guid.NewGuid().ToString();
									b[tu + 2].StuID = vs[tu].ID;
									b[tu + 2].StudentPayID = ts[tu].ID;
									b[tu + 2].F_CreatorTime = ts[tu].F_CreatorTime;
									b[tu + 2].F_CreatorUserId = ts[tu].F_CreatorUserId;
									b[tu + 2].CollectionInfo = sys_item2.F_ID;
									b[tu + 2].CollentPartenID = "key3";
									b[tu + 2].ShouldPay = list2[tu].wa;
									b[tu + 2].Discount = list2[tu].wb;
									b[tu + 2].Discount = list2[tu].wc;
									StringBuilder str7 = new StringBuilder();
									str7.Append("insert into AStudentPayes(id,StuID,StudentPayID,CollectionInfo,CollentPartenID,ShouldPay,Discount,Paid,F_CreatorTime,F_CreatorUserId)");
									str7.Append("values(@id,@StuID,@StudentPayID,@CollectionInfo,@CollentPartenID,@ShouldPay,@Discount,@Paid,@F_CreatorTime,@F_CreatorUserId)");
									dic.Add(b[tu + 2], str7.ToString());
								}
							}
						}
						catch (Exception ex)
						{
							LogHelp.Error("导入Excel DaoExcel方法" + ex.Message);
						}
					
					}
					
					tu += 1;
				}
			}
			return await _repository.GetListSql(dic);
		}

		public async Task<AStudent> GetModel(string stuid)
		{
			return await _repository.GetModel(stuid);
		}
		public List<string> Gettabname(int type)
        {
            List<string> list = new List<string>();
            if (type==1)
            {
                list.Add("学号");
                list.Add("姓名");
                list.Add("身份证号");
                list.Add("招生老师");
                list.Add("年级");
                list.Add("学校");
                list.Add("联系方式");
                list.Add("应缴金额");
                list.Add("优惠金额");
                list.Add("实缴金额");
                list.Add("欠费金额");
                list.Add("缴费日期");
                list.Add("付款方式");
                list.Add("收款方式");
            }
            if (type==2)
            {
                list.Add("应收");
                list.Add("优惠");
                list.Add("实收");
                list.Add("应收");
                list.Add("优惠");
                list.Add("实收");
                list.Add("应收");
                list.Add("优惠");
                list.Add("实收");

                list.Add("学费");
                list.Add("住宿费");
                list.Add("文化费");
                list.Add("退学");
            }
           
            return list;
        }
		/// <summary>
		/// 导出excel
		/// </summary>
		/// <param name="model"></param>
		/// <param name="path"></param>
		/// <returns></returns>
        public string BuildWorkbook(List<AStudent> model,string path)
        {
            var workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("学生缴费记录");
            var dateStyle = workbook.CreateCellStyle();
            var format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy/mm/dd");
            #region 表头
            var headerRow = sheet.CreateRow(0);
            var headerRow1 = sheet.CreateRow(1);
            headerRow1.Height = 60 * 20;
            ICellStyle headStyle1 = workbook.CreateCellStyle();
            headStyle1.WrapText = true;
            headStyle1.Alignment = HorizontalAlignment.Center;// 左右居中    
            headStyle1.VerticalAlignment = VerticalAlignment.Center;// 上下居中 
            sheet.DefaultRowHeightInPoints = 15;

           
            //定义font
            IFont font1 = workbook.CreateFont();
            font1.FontHeightInPoints = 13;
            font1.Boldweight = 100;
            headStyle1.SetFont(font1);

            for (int i = 0; i < Gettabname(1).Count; i++)
            {
               
                if (i==2)
                {
                    headerRow.CreateCell(i).SetCellValue(Gettabname(1)[i]);
                    SetCellRangeAddress(sheet, 0, 1, i, i);
                    headerRow.GetCell(i).CellStyle = headStyle1;
                    sheet.SetColumnWidth(i, 25 * 256);
                }
                else if (i==5||i==11||i==6)
                {
                    headerRow.CreateCell(i).SetCellValue(Gettabname(1)[i]);
                    SetCellRangeAddress(sheet, 0, 1, i, i);
                    headerRow.GetCell(i).CellStyle = headStyle1;
                    sheet.SetColumnWidth(i, 18 * 256);
                }
                
                else
                {
                    headerRow.CreateCell(i).SetCellValue(Gettabname(1)[i]);
                    SetCellRangeAddress(sheet, 0, 1, i, i);
                    headerRow.GetCell(i).CellStyle = headStyle1;
                    sheet.SetColumnWidth(i, 12 * 256);
                }
            }
            headerRow.CreateCell(14).SetCellValue("学费");
            SetCellRangeAddress(sheet, 0, 0, 14, 16);
            headerRow.GetCell(14).CellStyle = headStyle1;
            sheet.SetColumnWidth(14, 30 * 256);

            headerRow.CreateCell(17).SetCellValue("住宿费");
            SetCellRangeAddress(sheet, 0, 0, 17, 19);
            headerRow.GetCell(17).CellStyle = headStyle1;
            sheet.SetColumnWidth(17, 30 * 256);

            headerRow.CreateCell(20).SetCellValue("文化费");
            SetCellRangeAddress(sheet, 0, 0, 20, 22);
            headerRow.GetCell(20).CellStyle = headStyle1;
            sheet.SetColumnWidth(20, 30 * 256);

            headerRow.CreateCell(23).SetCellValue("备注");
            SetCellRangeAddress(sheet, 0, 1, 23, 23);
            headerRow.GetCell(23).CellStyle = headStyle1;
            sheet.SetColumnWidth(23, 15 * 256);

            headerRow.CreateCell(24).SetCellValue("退费日期");
            SetCellRangeAddress(sheet, 0, 1, 24, 24);
            headerRow.GetCell(24).CellStyle = headStyle1;
            sheet.SetColumnWidth(24, 15 * 256);

            headerRow.CreateCell(25).SetCellValue("退费类别");
            SetCellRangeAddress(sheet, 0, 0, 25, 28);
            headerRow.GetCell(25).CellStyle = headStyle1;
            sheet.SetColumnWidth(25, 30 * 256);

            headerRow.CreateCell(29).SetCellValue("备注");
            SetCellRangeAddress(sheet, 0, 1, 29, 29);
            headerRow.GetCell(29).CellStyle = headStyle1;
            sheet.SetColumnWidth(29, 16 * 256);

            int ct = 14;
            int cp = 25;
            for (int i = 0; i < Gettabname(2).Count; i++)
            {
                if (i<=8)
                {
                    headerRow1.CreateCell(ct).SetCellValue(Gettabname(2)[i]);
                    SetCellRangeAddress(sheet, 1, 1, ct, ct);
                    sheet.SetColumnWidth(ct, 13 * 256);
                    headerRow1.GetCell(ct).CellStyle = headStyle1;
                    ct += 1;
                }
                else
                {
                    headerRow1.CreateCell(cp).SetCellValue(Gettabname(2)[i]);
                    SetCellRangeAddress(sheet, 1, 1, cp, cp);
                    headerRow1.GetCell(cp).CellStyle = headStyle1;
                    sheet.SetColumnWidth(cp, 13 * 256);
                    cp += 1;
                }
              
            }
          


            #endregion colorm=28开始
            ICellStyle headStyle2 = workbook.CreateCellStyle();
            headStyle2.WrapText = true;
            headStyle2.Alignment = HorizontalAlignment.Center;// 左右居中    
            headStyle2.VerticalAlignment = VerticalAlignment.Center;// 上下居中 
            sheet.DefaultRowHeightInPoints = 13;


            //定义font
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 12;
            font.Boldweight = 100;
            headStyle2.SetFont(font);
            IRow rowtemp= sheet.CreateRow(2);
            int index = 2;
            int row = 0;
            for (int i = 0; i < model.Count; i++)
            {
               
                int start = row;
                if (i==0)
                {
                    start = index;
                    row = index;
                    rowtemp = sheet.CreateRow(index);
                }
                else
                {
                    row = row + 1;
                    start = row;
                    rowtemp = sheet.CreateRow(row);
                }
                var con = model[i].GetAStudentPay.Count(); ;
                var pay = model[i].GetARefundes.Count();
                con = con > pay ? con : pay;
                 row = con == 1 ? row : row + con - 1;
                rowtemp.CreateCell(0).SetCellValue(model[i].StuNum);
                SetCellRangeAddress(sheet, start, row, 0, 0);
                rowtemp.GetCell(0).CellStyle = headStyle2;
                rowtemp.CreateCell(1).SetCellValue(model[i].Name);
                SetCellRangeAddress(sheet, start, row, 1, 1);
                rowtemp.GetCell(1).CellStyle = headStyle2;
                rowtemp.CreateCell(2).SetCellValue(model[i].Cards);
                SetCellRangeAddress(sheet, start, row, 2, 2);
                rowtemp.GetCell(2).CellStyle = headStyle2;
                rowtemp.CreateCell(3).SetCellValue(model[i].TeacherID);
                SetCellRangeAddress(sheet, start, row, 3, 3);
                rowtemp.GetCell(3).CellStyle = headStyle2;

                rowtemp.CreateCell(4).SetCellValue(model[i].GradeID);
                SetCellRangeAddress(sheet, start, row, 4, 4);
                rowtemp.GetCell(4).CellStyle = headStyle2;

                rowtemp.CreateCell(5).SetCellValue(model[i].SchoolID);
                SetCellRangeAddress(sheet, start, row, 5, 5);
                rowtemp.GetCell(5).CellStyle = headStyle2;

                rowtemp.CreateCell(6).SetCellValue(model[i].PhoneNum);
                SetCellRangeAddress(sheet, start, row, 6, 6);
                rowtemp.GetCell(6).CellStyle = headStyle2;

                rowtemp.CreateCell(7).SetCellValue(Convert.ToDouble(model[i].CollecMoney));
                SetCellRangeAddress(sheet, start, row, 7, 7);
                rowtemp.GetCell(7).CellStyle = headStyle2;
                rowtemp.CreateCell(8).SetCellValue(Convert.ToDouble(model[i].Youhui));
                SetCellRangeAddress(sheet, start, row, 8, 8);
                rowtemp.GetCell(8).CellStyle = headStyle2;
                rowtemp.CreateCell(9).SetCellValue(Convert.ToDouble(model[i].PayMoeny));
                SetCellRangeAddress(sheet, start, row, 9, 9);
                rowtemp.GetCell(9).CellStyle = headStyle2;
                rowtemp.CreateCell(10).SetCellValue(Convert.ToDouble(model[i].QinfeiMoeny));
                SetCellRangeAddress(sheet, start, row, 10, 10);
                rowtemp.GetCell(10).CellStyle = headStyle2;
                int tp = 11;
                if (model[i].GetAStudentPay.Count()>0)
                {
                    int it = 0;
                    foreach (var item in model[i].GetAStudentPay)
                    {
                        if (it==0)
                        {
                            rowtemp.CreateCell(tp).SetCellValue(item.PayDateString);
                            SetCellRangeAddress(sheet, start, start, tp, tp);
                            rowtemp.GetCell(tp).CellStyle = headStyle2;

                            rowtemp.CreateCell(tp + 1).SetCellValue(item.PaymentID);
                            SetCellRangeAddress(sheet, start, start, tp + 1, tp + 1);
                            rowtemp.GetCell(tp + 1).CellStyle = headStyle2;

                            rowtemp.CreateCell(tp + 2).SetCellValue(item.CollectionID);
                            SetCellRangeAddress(sheet, start, start, tp + 2, tp + 2);
                            rowtemp.GetCell(tp + 2).CellStyle = headStyle2;

                            rowtemp.CreateCell(tp + 3).SetCellValue(Convert.ToDouble(item.xa));
                            SetCellRangeAddress(sheet, start, start, tp + 3, tp + 3);
                            rowtemp.GetCell(tp + 3).CellStyle = headStyle2;

                            rowtemp.CreateCell(tp + 4).SetCellValue(Convert.ToDouble(item.xb));
                            SetCellRangeAddress(sheet, start, start, tp + 4, tp + 4);
                            rowtemp.GetCell(tp + 4).CellStyle = headStyle2;

                            rowtemp.CreateCell(tp + 5).SetCellValue(Convert.ToDouble(item.xc));
                            SetCellRangeAddress(sheet, start, start, tp + 5, tp + 5);
                            rowtemp.GetCell(tp + 5).CellStyle = headStyle2;
                            rowtemp.CreateCell(tp + 6).SetCellValue(Convert.ToDouble(item.za));
                            SetCellRangeAddress(sheet, start, start, tp + 6, tp + 6);
                            rowtemp.GetCell(tp + 6).CellStyle = headStyle2;
                            rowtemp.CreateCell(tp + 7).SetCellValue(Convert.ToDouble(item.zb));
                            SetCellRangeAddress(sheet, start, start, tp + 7, tp + 7);
                            rowtemp.GetCell(tp + 7).CellStyle = headStyle2;
                            rowtemp.CreateCell(tp + 8).SetCellValue(Convert.ToDouble(item.zc));
                            SetCellRangeAddress(sheet, start, start, tp + 8, tp + 8);
                            rowtemp.GetCell(tp + 8).CellStyle = headStyle2;
                            rowtemp.CreateCell(tp + 9).SetCellValue(Convert.ToDouble(item.wa));
                            SetCellRangeAddress(sheet, start, start, tp + 9, tp + 9);
                            rowtemp.GetCell(tp + 9).CellStyle = headStyle2;
                            rowtemp.CreateCell(tp + 10).SetCellValue(Convert.ToDouble(item.wb));
                            SetCellRangeAddress(sheet, start, start, tp + 10, tp + 10);
                            rowtemp.GetCell(tp + 10).CellStyle = headStyle2;
                            rowtemp.CreateCell(tp + 11).SetCellValue(Convert.ToDouble(item.wc));
                            SetCellRangeAddress(sheet, start, start, tp + 11, tp + 11);
                            rowtemp.GetCell(tp + 11).CellStyle = headStyle2;
                            rowtemp.CreateCell(tp + 12).SetCellValue(item.Note);
                            SetCellRangeAddress(sheet, start, start, tp + 12, tp + 12);
                            rowtemp.GetCell(tp + 12).CellStyle = headStyle2;
                        }
                        else
                        {
                            int setu = start+it;
                            var rowtpt = sheet.CreateRow(setu);
                            rowtpt.CreateCell(tp).SetCellValue(item.PayDateString);
                            SetCellRangeAddress(sheet, setu, setu, tp, tp);
                            rowtpt.GetCell(tp).CellStyle = headStyle2;

                            rowtpt.CreateCell(tp+1).SetCellValue(item.PaymentID);
                            SetCellRangeAddress(sheet, setu, setu, tp+1, tp+1);
                            rowtpt.GetCell(tp+1).CellStyle = headStyle2;

                            rowtpt.CreateCell(tp+2).SetCellValue(item.CollectionID);
                            SetCellRangeAddress(sheet, setu, setu, tp + 2, tp + 2);
                            rowtpt.GetCell(tp + 2).CellStyle = headStyle2;

                            rowtpt.CreateCell(tp + 3).SetCellValue(Convert.ToDouble(item.xa));
                            SetCellRangeAddress(sheet, setu, setu, tp + 3, tp + 3);
                            rowtpt.GetCell(tp + 3).CellStyle = headStyle2;

                            rowtpt.CreateCell(tp + 4).SetCellValue(Convert.ToDouble(item.xb));
                            SetCellRangeAddress(sheet, setu, setu, tp + 4, tp + 4);
                            rowtpt.GetCell(tp + 4).CellStyle = headStyle2;

                            rowtpt.CreateCell(tp +5 ).SetCellValue(Convert.ToDouble(item.xc));
                            SetCellRangeAddress(sheet, setu, setu, tp + 5, tp + 5);
                            rowtpt.GetCell(tp + 5).CellStyle = headStyle2;
                            rowtpt.CreateCell(tp + 6).SetCellValue(Convert.ToDouble(item.za));
                            SetCellRangeAddress(sheet, setu, setu, tp + 6, tp + 6);
                            rowtpt.GetCell(tp + 6).CellStyle = headStyle2;
                            rowtpt.CreateCell(tp + 7).SetCellValue(Convert.ToDouble(item.zb));
                            SetCellRangeAddress(sheet, setu, setu, tp + 7, tp + 7);
                            rowtpt.GetCell(tp + 7).CellStyle = headStyle2;
                            rowtpt.CreateCell(tp + 8).SetCellValue(Convert.ToDouble(item.zc));
                            SetCellRangeAddress(sheet, setu, setu, tp + 8, tp + 8);
                            rowtpt.GetCell(tp + 8).CellStyle = headStyle2;
                            rowtpt.CreateCell(tp + 9).SetCellValue(Convert.ToDouble(item.wa));
                            SetCellRangeAddress(sheet, setu, setu, tp + 9, tp + 9);
                            rowtpt.GetCell(tp + 9).CellStyle = headStyle2;
                            rowtpt.CreateCell(tp + 10).SetCellValue(Convert.ToDouble(item.wb));
                            SetCellRangeAddress(sheet, setu, setu, tp + 10, tp + 10);
                            rowtpt.GetCell(tp + 10).CellStyle = headStyle2;
                            rowtpt.CreateCell(tp + 11).SetCellValue(Convert.ToDouble(item.wc));
                            SetCellRangeAddress(sheet, setu, setu, tp + 11, tp + 11);
                            rowtpt.GetCell(tp + 11).CellStyle = headStyle2;
                            rowtpt.CreateCell(tp + 12).SetCellValue(item.Note);
                            SetCellRangeAddress(sheet, setu, setu, tp + 12, tp + 12);
                            rowtpt.GetCell(tp + 12).CellStyle = headStyle2;
                        }
                        it += 1;
                    }
                    
                }
                if (model[i].GetARefundes.Count()>0)
                {
                    int iu = 0;
                    int ti = 24;
                    foreach (var item in model[i].GetARefundes)
                    {
                        if (iu==0)
                        {
                            rowtemp.CreateCell(ti).SetCellValue(item.PayDate);
                            SetCellRangeAddress(sheet, start, start, ti, ti);
                            rowtemp.GetCell(ti).CellStyle = headStyle2;
                            rowtemp.CreateCell(ti + 1).SetCellValue(Convert.ToDouble(item.xa));
                            SetCellRangeAddress(sheet, start, start, ti + 1, ti + 1);
                            rowtemp.GetCell(ti + 1).CellStyle = headStyle2;
                            rowtemp.CreateCell(ti + 2).SetCellValue(Convert.ToDouble(item.za));
                            SetCellRangeAddress(sheet, start, start, ti + 2, ti + 2);
                            rowtemp.GetCell(ti + 2).CellStyle = headStyle2;

                            rowtemp.CreateCell(ti + 3).SetCellValue(Convert.ToDouble(item.wa));
                            SetCellRangeAddress(sheet, start, start, ti + 3, ti + 3);
                            rowtemp.GetCell(ti + 3).CellStyle = headStyle2;

                            rowtemp.CreateCell(ti + 4).SetCellValue(Convert.ToDouble(item.tx));
                            SetCellRangeAddress(sheet, start, start, ti + 4, ti + 4);
                            rowtemp.GetCell(ti + 4).CellStyle = headStyle2;

                            rowtemp.CreateCell(ti + 5).SetCellValue(item.Note);
                            SetCellRangeAddress(sheet, start, start, ti + 5, ti + 5);
                            rowtemp.GetCell(ti + 5).CellStyle = headStyle2;
                        }
                        else
                        {
                            int sect = start  +iu;
                            var rowtp = sheet.CreateRow(sect);
                            rowtp.CreateCell(ti).SetCellValue(item.PayDate);
                            SetCellRangeAddress(sheet, sect, sect, ti, ti);
                            rowtp.GetCell(ti).CellStyle = headStyle2;
                            rowtp.CreateCell(ti+1).SetCellValue(Convert.ToDouble(item.xa));
                            SetCellRangeAddress(sheet, sect, sect, ti+1, ti+1);
                            rowtp.GetCell(ti+1).CellStyle = headStyle2;
                            rowtp.CreateCell(ti+2).SetCellValue(Convert.ToDouble(item.za));
                            SetCellRangeAddress(sheet, sect, sect, ti+2, ti+2);
                            rowtp.GetCell(ti+2).CellStyle = headStyle2;

                            rowtp.CreateCell(ti+3).SetCellValue(Convert.ToDouble(item.wa));
                            SetCellRangeAddress(sheet, sect, sect, ti+3, ti+3);
                            rowtp.GetCell(ti+3).CellStyle = headStyle2;

                            rowtp.CreateCell(ti+4).SetCellValue(Convert.ToDouble(item.tx));
                            SetCellRangeAddress(sheet, sect, sect, ti+4, ti+4);
                            rowtp.GetCell(ti+4).CellStyle = headStyle2;

                            rowtp.CreateCell(ti+5).SetCellValue(item.Note);
                            SetCellRangeAddress(sheet, sect, sect, ti+5, ti+5);
                            rowtp.GetCell(ti+5).CellStyle = headStyle2;
                        }
                        iu += 1;
                    }
                }

            }
            var time = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            string paths = path + "/Temp/" + time;
            FileStream fs = new FileStream(paths,FileMode.Create);
            workbook.Write(fs);
            fs.Close();
            return time;
        }
    
		/// <summary>
		/// Excel单元格合并
		/// </summary>
		/// <param name="sheet"></param>
		/// <param name="rowstart"></param>
		/// <param name="rowend"></param>
		/// <param name="colstart"></param>
		/// <param name="colend"></param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }



    }


      
}
