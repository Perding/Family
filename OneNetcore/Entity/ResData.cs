using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ResData<T>
    {
        public string message { get; set; }
        public int state { get; set; }
        public IList<T> Resdata { get; set; }
    }
}
