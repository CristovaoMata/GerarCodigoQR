using QRCoder;
using System.Drawing;
using System.IO;

namespace QRCodeAluno.Service
{
    public class QRCodeService
    {
        private string pasta = @"C:\CartoesEstudante\QRCodes\";

        public string GerarQRCode(string texto, string nomeArquivo)
        {
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(data);
            using (Bitmap bmp = qrCode.GetGraphic(10))
            {
                string caminho = Path.Combine(pasta, nomeArquivo + ".png");
                bmp.Save(caminho, System.Drawing.Imaging.ImageFormat.Png);
                return caminho;
            }
        }
    }
}
