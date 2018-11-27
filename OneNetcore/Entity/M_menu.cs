using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class M_menu
    {

        /// <summary>
        /// M_ID
        /// </summary>		
        private string _m_id;
        public string M_ID
        {
            get { return _m_id; }
            set { _m_id = value; }
        }
        /// <summary>
        /// M_PartentID
        /// </summary>		
        private string _m_partentid;
        public string M_PartentID
        {
            get { return _m_partentid; }
            set { _m_partentid = value; }
        }
        /// <summary>
        /// M_Layer
        /// </summary>		
        private int _m_layer;
        public int M_Layer
        {
            get { return _m_layer; }
            set { _m_layer = value; }
        }
        /// <summary>
        /// M_Name
        /// </summary>		
        private string _m_name;
        public string M_Name
        {
            get { return _m_name; }
            set { _m_name = value; }
        }
        /// <summary>
        /// M_icon
        /// </summary>		
        private string _m_icon;
        public string M_icon
        {
            get { return _m_icon; }
            set { _m_icon = value; }
        }
        /// <summary>
        /// M_Url
        /// </summary>		
        private string _m_url;
        public string M_Url
        {
            get { return _m_url; }
            set { _m_url = value; }
        }
        /// <summary>
        /// 是否常用菜单 1 是 0 否
        /// </summary>		
        private int _m_type;
        public int M_type
        {
            get { return _m_type; }
            set { _m_type = value; }
        }
        /// <summary>
        /// M_IsEnable
        /// </summary>		
        private int _m_isenable;
        public int M_IsEnable
        {
            get { return _m_isenable; }
            set { _m_isenable = value; }
        }
        /// <summary>
        /// M_sortid
        /// </summary>		
        private int _m_sortid;
        public int M_sortid
        {
            get { return _m_sortid; }
            set { _m_sortid = value; }
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
