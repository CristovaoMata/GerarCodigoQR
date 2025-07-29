using GerarCodigoQR.Model;
using QRCodeAluno.DAL;
using QRCodeAluno.Service;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GerarCodigoQR.UI
{
    public partial class FrmAluno : Form
    {
        public FrmAluno()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Aluno aluno = new Aluno
            {
                Nome = txtNome.Text,
                Matricula = txtMatricula.Text
            };

            AlunoDAL dal = new AlunoDAL();
            int idGerado = dal.Inserir(aluno);

            QRCodeService qrService = new QRCodeService();
            string textoQR = $"AlunoID:{idGerado}|Nome:{aluno.Nome}|Matricula:{aluno.Matricula}";
            string caminhoQR = qrService.GerarQRCode(textoQR, "qr_" + idGerado);

            dal.AtualizarCaminhoQRCode(idGerado, caminhoQR);

            // Mostrar QR no PictureBox
            picQRCode.Image = Image.FromFile(caminhoQR);

            MessageBox.Show("Aluno cadastrado e QR Code gerado com sucesso.");
        }
    }
}
