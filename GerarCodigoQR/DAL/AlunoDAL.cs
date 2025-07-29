using GerarCodigoQR.Model;
using System;
using System.Data.SqlClient;

namespace QRCodeAluno.DAL
{
    public class AlunoDAL
    {
        //Cristóvao Mata
        private string connectionString = @"Data Source=MATA-SERVER\SQLEXPRESS;Initial Catalog=QRCODE;Integrated Security=True";

        public int Inserir(Aluno aluno)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Alunos (Nome, Matricula, CaminhoQRCode) OUTPUT INSERTED.ID VALUES (@Nome, @Matricula, @Caminho)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
                cmd.Parameters.AddWithValue("@Matricula", aluno.Matricula);
                cmd.Parameters.AddWithValue("@Caminho", aluno.CaminhoQRCode ?? (object)DBNull.Value);

                conn.Open();
                int idGerado = (int)cmd.ExecuteScalar();
                return idGerado;
            }
        }

        public void AtualizarCaminhoQRCode(int id, string caminhoQR)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Alunos SET CaminhoQRCode = @Caminho WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Caminho", caminhoQR);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
