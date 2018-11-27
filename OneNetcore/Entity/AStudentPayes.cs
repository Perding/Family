using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class AStudentPayes
    {

        public string StuID { get; set; }
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
        /// 缴费详情编号
        /// </summary>		
        private string _studentpayid;
        public string StudentPayID
        {
            get { return _studentpayid; }
            set { _studentpayid = value; }
        }
        /// <summary>
        /// 缴费科目编号（去基础参数查询）
        /// </summary>		
        private string _collectioninfo;
        public string CollectionInfo
        {
            get { return _collectioninfo; }
            set { _collectioninfo = value; }
        }
        /// <summary>
        /// 科目类别 key1 学费 key2:住宿费 key3:文化费
        /// </summary>		
        private string _collentpartenid;
        public string CollentPartenID
        {
            get { return _collentpartenid; }
            set { _collentpartenid = value; }
        }
        /// <summary>
        /// 应缴金额
        /// </summary>		
        private decimal _shouldpay;
        public decimal ShouldPay
        {
            get { return _shouldpay; }
            set { _shouldpay = value; }
        }
        /// <summary>
        /// 优惠金额
        /// </summary>		
        private decimal _discount;
        public decimal Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }
        /// <summary>
        /// 实收金额
        /// </summary>		
        private decimal _paid;
        public decimal Paid
        {
            get { return _paid; }
            set { _paid = value; }
        }
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
