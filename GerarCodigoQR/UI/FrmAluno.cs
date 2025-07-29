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
            int id = dal.Inserir(aluno);

            string textoQR = $"AlunoID:{id}|Nome:{aluno.Nome}|Matricula:{aluno.Matricula}";
            QRCodeService qrService = new QRCodeService();
            string caminhoQR;
            Image imagemQR = qrService.GerarQRCodeComLogo(textoQR, out caminhoQR, margem: 2, corQRCode: Color.FromArgb(0, 132, 176));

            dal.AtualizarCaminhoQRCode(id, caminhoQR);
            picQRCode.Image = imagemQR;

            MessageBox.Show("Aluno cadastrado com QR personalizado.");
        }
    }
}
