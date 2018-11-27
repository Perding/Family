using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Loginfo
    {
		
		public int limit { get; set; }
		public int PageSize { get; set; }

		public string start { get; set; }

		public string end { get; set; }

		public string l_dates { get; set; }
        /// <summary>
        /// L_id
        /// </summary>		
        private string _l_id;
        public string L_id
        {
            get { return _l_id; }
            set { _l_id = value; }
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
        /// u_account
        /// </summary>		
        private string _u_account;
        public string u_account
        {
            get { return _u_account; }
            set { _u_account = value; }
        }
        /// <summary>
        /// l_content
        /// </summary>		
        private string _l_content;
        public string l_content
        {
            get { return _l_content; }
            set { _l_content = value; }
        }
        /// <summary>
        /// l_date
        /// </summary>		
        private DateTime _l_date;
        public DateTime l_date
        {
            get { return _l_date; }
            set { _l_date = value; }
        }
        public string L_ip { get; set; }

    }
}
