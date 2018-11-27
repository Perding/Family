using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace Entity.Viewmodel
{
    public class AStudentPayView
    {
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
      
        [DisplayName("缴费时间")]
      
        public string PayDateString { get; set; }
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

        public decimal za { get; set; }
        public decimal zb { get; set; }
        public decimal zc { get; set; }

        public decimal wa { get; set; }
        public decimal wb { get; set; }
        public decimal wc { get; set; }
    }
}
