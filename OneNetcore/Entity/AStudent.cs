using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entity
{
    public class AStudent
    {
        [DisplayName("应缴金额")]
        public decimal CollecMoney { get; set; }
        [DisplayName("优惠金额")]
        public decimal Youhui { get; set; }
        [DisplayName("实缴金额")]
        public decimal PayMoeny { get; set; }
        [DisplayName("欠费金额")]
        public decimal QinfeiMoeny { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

        public AStudentPay Owers { get; set; }

        public IEnumerable<AStudentPay> GetAStudentPay { get; set; }

        public IEnumerable<AStudentPayes> GetAStudentPayes { get; set; }
        public IEnumerable<ARefundes> GetARefundes { get; set; }


        [DisplayName("备注")]
        public string Note { get; set; }
        public string PaymentID { get; set; }
        public string CollectionID { get; set; }
        public string PayDate { get; set; }
        public string html { get; set; }
       
        /// <summary>
        /// ID
        /// </summary>		
        private string _id;
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
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
        /// <summary>
        /// 所在学校
        /// </summary>		
        private string _schoolid;
        [DisplayName("学校")]
        public string SchoolID
        {
            get { return _schoolid; }
            set { _schoolid = value; }
        }
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
        /// <summary>
        /// 是否退学0退学 1在校
        /// </summary>		
        private int _drop;
        public int IsDrop
        {
            get { return _drop; }
            set { _drop = value; }
        }
        /// <summary>
        /// 删除编辑 0删除
        /// </summary>		
        private int _f_deletemark;
        public int F_DeleteMark
        {
            get { return _f_deletemark; }
            set { _f_deletemark = value; }
        }
        /// <summary>
        /// F_CreatorTime
        /// </summary>		
        private DateTime _f_creatortime;
        public DateTime F_CreatorTime
        {
            get { return _f_creatortime; }
            set { _f_creatortime = value; }
        }
        /// <summary>
        /// F_CreatorUserId
        /// </summary>		
        private string _f_creatoruserid;
        public string F_CreatorUserId
        {
            get { return _f_creatoruserid; }
            set { _f_creatoruserid = value; }
        }
        /// <summary>
        /// F_LastModifyTime
        /// </summary>		
        private DateTime _f_lastmodifytime;
        public DateTime F_LastModifyTime
        {
            get { return _f_lastmodifytime; }
            set { _f_lastmodifytime = value; }
        }
        /// <summary>
        /// F_lastModifyUserId
        /// </summary>		
        private string _f_lastmodifyuserid;
        public string F_lastModifyUserId
        {
            get { return _f_lastmodifyuserid; }
            set { _f_lastmodifyuserid = value; }
        }
        /// <summary>
        /// F_deleteTime
        /// </summary>		
        private DateTime _f_deletetime;
        public DateTime F_deleteTime
        {
            get { return _f_deletetime; }
            set { _f_deletetime = value; }
        }
        /// <summary>
        /// F_DeleteUserID
        /// </summary>		
        private string _f_deleteuserid;
        public string F_DeleteUserID
        {
            get { return _f_deleteuserid; }
            set { _f_deleteuserid = value; }
        }

    }
}
