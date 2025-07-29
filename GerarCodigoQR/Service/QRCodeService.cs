using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace QRCodeAluno.Service
{
    public class QRCodeService
    {
        private readonly string pastaSalvar = @"C:\CartoesEstudante\QRCodes\";
        private readonly string caminhoLogo = @"C:\CartoesEstudante\logo.png"; // Logo da escola

        public Image GerarQRCodeComLogo(string texto, out string caminhoSalvo,
            int margem = 2, int escala = 10, int logoPercent = 30, Color? corQRCode = null)
        {
            if (corQRCode == null)
                corQRCode = Color.Black;

            if (!Directory.Exists(pastaSalvar))
                Directory.CreateDirectory(pastaSalvar);

            QRCodeGenerator qrGerador = new QRCodeGenerator();
            QRCodeData dados = qrGerador.CreateQrCode(texto, QRCodeGenerator.ECCLevel.H);

            Bitmap logo = File.Exists(caminhoLogo) ? (Bitmap)Image.FromFile(caminhoLogo) : null;

            // Cria uma logo com fundo (espaço) para destacar no QR
            if (logo != null)
            {
                int tamanhoFinal = (int)(logo.Width * 1.1); // aumenta 80% para espaço
                logo = CriarLogoComFundo(logo, tamanhoFinal, circular: true); // true = fundo redondo
            }

            using (QRCode qrCode = new QRCode(dados))
            using (Bitmap qrBitmap = qrCode.GetGraphic(
                pixelsPerModule: escala,
                darkColor: corQRCode.Value,
                lightColor: Color.White,
                icon: logo,
                iconSizePercent: logoPercent,
                iconBorderWidth: 0,
                drawQuietZones: true))
            {
                string nomeArquivo = "qr_" + texto.GetHashCode() + ".png";
                caminhoSalvo = Path.Combine(pastaSalvar, nomeArquivo);

                qrBitmap.Save(caminhoSalvo, ImageFormat.Png);
                return new Bitmap(qrBitmap);
            }
        }


        public Bitmap CriarLogoComFundo(Bitmap logoOriginal, int tamanhoFinal, bool circular = true)
        {
            Bitmap imagemFinal = new Bitmap(tamanhoFinal, tamanhoFinal);
            using (Graphics g = Graphics.FromImage(imagemFinal))
            {
                g.Clear(Color.White); // fundo branco (pode mudar)

                if (circular)
                {
                    System.Drawing.Drawing2D.GraphicsPath caminho = new System.Drawing.Drawing2D.GraphicsPath();
                    caminho.AddEllipse(0, 0, tamanhoFinal, tamanhoFinal);
                    g.SetClip(caminho);
                }

                int margem = (tamanhoFinal - logoOriginal.Width) / 2;
                g.DrawImage(logoOriginal, margem, margem, logoOriginal.Width, logoOriginal.Height);
            }

            return imagemFinal;
        }

    }
}
