using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Service.IService
{
   public  interface IQRCodeIService
    {
		Bitmap GetQRCode(string url, int pixel);
	}
}
