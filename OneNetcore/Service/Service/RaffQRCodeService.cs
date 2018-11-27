using QRCoder;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Service.Service
{
	public class RaffQRCodeService : IQRCodeIService
	{
		public Bitmap GetQRCode(string url, int pixel)
		{
			QRCodeGenerator generator = new QRCodeGenerator();

			QRCodeData codeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);

			QRCoder.QRCode qrcode = new QRCoder.QRCode(codeData);

			Bitmap qrImage = qrcode.GetGraphic(pixel, Color.Black, Color.White, true);

			return qrImage;

		}
	}
}
