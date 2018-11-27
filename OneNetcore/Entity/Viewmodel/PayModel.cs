using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class PayModel
    {
        public IEnumerable<AStudentPay> GetAStudentPay { get; set; }
        public IEnumerable<ARefundes> GetARefundes { get; set; }
    }
}
