using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Usermenu
    {
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
        /// u_id
        /// </summary>		
        private string _u_id;
        public string u_id
        {
            get { return _u_id; }
            set { _u_id = value; }
        }
        /// <summary>
        /// m_id
        /// </summary>		
        private string _m_id;
        public string m_id
        {
            get { return _m_id; }
            set { _m_id = value; }
        }
        /// <summary>
        /// m_partentID
        /// </summary>		
        private string _m_partentid;
        public string m_partentID
        {
            get { return _m_partentid; }
            set { _m_partentid = value; }
        }
        /// <summary>
        /// createpeople
        /// </summary>		
        private string _createpeople;
        public string createpeople
        {
            get { return _createpeople; }
            set { _createpeople = value; }
        }
        /// <summary>
        /// createdate
        /// </summary>		
        private DateTime _createdate;
        public DateTime createdate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }

    }
}
