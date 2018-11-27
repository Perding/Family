using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public  class Page
    {
        public int pagecur { get; set; }
        public int pagesaze { get; set; }
        public string where { get; set; }

        /// <summary>
        /// 1 查询  2导出
        /// </summary>
        public int type { get; set; }
    }
}
