using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace Entity.Viewmodel
{
    public class AstudentView
    {
        /// <summary>
        /// 学生编号
        /// </summary>		
        private int _stunum;
        [DisplayName("学号")]
        public int StuNum
        {
            get { return _stunum; }
            set { _stunum = value; }
        }
        /// <summary>
        /// 学生姓名
        /// </summary>		
        [DisplayName("姓名")]
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 学生身份证
        /// </summary>		
        private string _cards;
        [DisplayName("身份证")]
        public string Cards
        {
            get { return _cards; }
            set { _cards = value; }
        }
        /// <summary>
        /// 所在年级
        /// </summary>		
        private string _gradeid;
        [DisplayName("年级")]
        public string GradeID
        {
            get { return _gradeid; }
            set { _gradeid = value; }
        }
        ///// <summary>
        ///// 所在学校
        ///// </summary>		
        //private string _schoolid;
        //[DisplayName("学校")]
        //public string SchoolID
        //{
        //    get { return _schoolid; }
        //    set { _schoolid = value; }
        //}
        /// <summary>
        /// 招生老师
        /// </summary>		
        private string _teacherid;
        [DisplayName("招生老师")]
        public string TeacherID
        {
            get { return _teacherid; }
            set { _teacherid = value; }
        }
        /// <summary>
        /// 联系方式
        /// </summary>		
        private string _phonenum;
        [DisplayName("联系方式")]
        public string PhoneNum
        {
            get { return _phonenum; }
            set { _phonenum = value; }
        }
       
        [DisplayName("应缴金额")]
        public decimal CollecMoney { get; set; }
        [DisplayName("优惠金额")]
        public decimal Youhui { get; set; }
        [DisplayName("实缴金额")]
        public decimal PayMoeny { get; set; }
        [DisplayName("欠费金额")]
        public decimal QinfeiMoeny { get; set; }
        public IEnumerable<AStudentPayView> GetAStudentPay { get; set; }
        public IEnumerable<ARefunView> GetARefundes { get; set; }
    }
}
