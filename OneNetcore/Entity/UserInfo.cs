using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class UserInfo
    {
        public int U_IsAdmin { get; set; }

        public string U_IP { get; set; }

		public string states { get; set; }
		/// <summary>
		/// u_userid
		/// </summary>		
		private string _u_userid;
        public string u_userid
        {
            get { return _u_userid; }
            set { _u_userid = value; }
        }
        /// <summary>
        /// 登陆账户
        /// </summary>		
        private string _u_account;
        public string u_account
        {
            get { return _u_account; }
            set { _u_account = value; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>		
        private string _u_realname;
        public string u_realName
        {
            get { return _u_realname; }
            set { _u_realname = value; }
        }
        /// <summary>
        /// 登陆密码
        /// </summary>		
        private string _u_password;
        public string u_password
        {
            get { return _u_password; }
            set { _u_password = value; }
        }

		public string newpassword { get; set; }
		public string setnewpassword { get; set; }
		/// <summary>
		/// 是否启用
		/// </summary>		
		private int _u_isenable;
        public int u_isEnable
        {
            get { return _u_isenable; }
            set { _u_isenable = value; }
        }
        public string U_CreatorTimes { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>		
        private int _u_phone;
        public int u_phone
        {
            get { return _u_phone; }
            set { _u_phone = value; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>		
        private int _u_deletemark;
        public int u_deleteMark
        {
            get { return _u_deletemark; }
            set { _u_deletemark = value; }
        }
        /// <summary>
        /// U_CreatorTime
        /// </summary>		
        private DateTime _u_creatortime;
        public DateTime U_CreatorTime
        {
            get { return _u_creatortime; }
            set { _u_creatortime = value; }
        }
        /// <summary>
        /// U_CreatorUserId
        /// </summary>		
        private string _u_creatoruserid;
        public string U_CreatorUserId
        {
            get { return _u_creatoruserid; }
            set { _u_creatoruserid = value; }
        }
        /// <summary>
        /// U_LastModifyTime
        /// </summary>		
        private DateTime _u_lastmodifytime;
        public DateTime U_LastModifyTime
        {
            get { return _u_lastmodifytime; }
            set { _u_lastmodifytime = value; }
        }
        /// <summary>
        /// U_lastModifyUserId
        /// </summary>		
        private string _u_lastmodifyuserid;
        public string U_lastModifyUserId
        {
            get { return _u_lastmodifyuserid; }
            set { _u_lastmodifyuserid = value; }
        }
        /// <summary>
        /// U_deleteTime
        /// </summary>		
        private DateTime _u_deletetime;
        public DateTime U_deleteTime
        {
            get { return _u_deletetime; }
            set { _u_deletetime = value; }
        }
        /// <summary>
        /// U_DeleteUserID
        /// </summary>		
        private string _u_deleteuserid;
        public string U_DeleteUserID
        {
            get { return _u_deleteuserid; }
            set { _u_deleteuserid = value; }
        }

    }
}
