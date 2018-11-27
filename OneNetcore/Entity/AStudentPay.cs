using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entity
{
    public class AStudentPay
    {

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
        private string _studid;

        public string StudID
        {
            get { return _studid; }
            set { _studid = value; }
        }
        /// <summary>
        /// 付款账户
        /// </summary>		
        private string _paymentid;
        [DisplayName("付款账户")]
        public string PaymentID
        {
            get { return _paymentid; }
            set { _paymentid = value; }
        }
        public string years { get; set; }
        /// <summary>
        /// 收款账户
        /// </summary>		
        private string _collectionid;
        [DisplayName("收款账户")]
        public string CollectionID
        {
            get { return _collectionid; }
            set { _collectionid = value; }
        }
        /// <summary>
        /// 缴费时间
        /// </summary>		
        private DateTime _paydate;
        [DisplayName("缴费时间")]
        public DateTime PayDate
        {
            get { return _paydate; }
            set { _paydate= value; }
        }
        public string PayDateString { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        private string _note;
        [DisplayName("备注")]
        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }

        public decimal xa { get; set; }
        public decimal xb { get; set; }
        public decimal xc { get; set; }
		public decimal xd { get; set; }

		public decimal za { get; set; }
        public decimal zb { get; set; }
        public decimal zc { get; set; }

        public decimal wa { get; set; }
        public decimal wb { get; set; }
        public decimal wc { get; set; }


        /// <summary>
        /// F_DeleteMark
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
