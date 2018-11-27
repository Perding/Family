using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ARefundes
    {
        public string Getdate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
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
        private string _studentid;
        public string StudentId
        {
            get { return _studentid; }
            set { _studentid = value; }
        }
        /// <summary>
        /// 退款类别（根据缴费科目读取，增加退学费）
        /// </summary>		
        private string _category;
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }
        /// <summary>
        /// 推荐金额
        /// </summary>		
        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public decimal xa { get; set; }

        public decimal za { get; set; }

        public decimal wa { get; set; }
        public decimal tx { get; set; }
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

        public string str { get; set; }

        public string ArefundId { get; set; }

        public string PayDate { get; set; }

    }
}
