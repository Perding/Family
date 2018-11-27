using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Sys_Item
    {
        public bool IsEnable { get; set; }
        /// <summary>
        /// F_ID
        /// </summary>		
        private string _f_id;


        public string F_ID
        {
            get { return _f_id; }
            set { _f_id = value; }
        }
        /// <summary>
        /// F_ParentID
        /// </summary>		
        private string _f_parentid;
        public string F_ParentID
        {
            get { return _f_parentid; }
            set { _f_parentid = value; }
        }
        /// <summary>
        /// F_Encode
        /// </summary>		
        private string _f_encode;
        public string F_Encode
        {
            get { return _f_encode; }
            set { _f_encode = value; }
        }
        /// <summary>
        /// F_FullName
        /// </summary>		
        private string _f_fullname;
        public string F_FullName
        {
            get { return _f_fullname; }
            set { _f_fullname = value; }
        }
        /// <summary>
        /// F_Layer
        /// </summary>		
        private int _f_layer;
        public int F_Layer
        {
            get { return _f_layer; }
            set { _f_layer = value; }
        }
        /// <summary>
        /// f_sortcode
        /// </summary>		
        private int _f_sortcode;
        public int f_sortcode
        {
            get { return _f_sortcode; }
            set { _f_sortcode = value; }
        }
        /// <summary>
        /// f_deleteMark
        /// </summary>		
        private int _f_deletemark;
        public int f_deleteMark
        {
            get { return _f_deletemark; }
            set { _f_deletemark = value; }
        }
        /// <summary>
        /// F_EnableMark
        /// </summary>		
        private int _f_enablemark;
        public int F_EnableMark
        {
            get { return _f_enablemark; }
            set { _f_enablemark = value; }
        }
        /// <summary>
        /// F_Description
        /// </summary>		
        private string _f_description;
        public string F_Description
        {
            get { return _f_description; }
            set { _f_description = value; }
        }
        /// <summary>
        /// F_Pay
        /// </summary>		
        private decimal _f_pay;
        public decimal F_Pay
        {
            get { return _f_pay; }
            set { _f_pay = value; }
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
        private DateTime? _f_deletetime;
        public DateTime? F_deleteTime
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
