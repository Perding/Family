using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public  class ARefund
    {
        public string ID { get; set; }
        public IEnumerable<ARefundes> ARefundes { get; set; }
        public string StudID { get; set; }

        public string Node { get; set; }

        public DateTime? tDatetime { get; set; }
    }
}
