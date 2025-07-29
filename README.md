# 🎓 Sistema de Gestão de Cartões de Estudante com QR Code

Este projeto é uma aplicação Windows Forms (.NET Framework 4.7.2) para cadastro de alunos, geração de cartões de estudante personalizados com **Código QR** e reimpressão, integrando funcionalidades visuais e armazenamento em banco de dados.

---

## 📌 Funcionalidades

- Cadastro de alunos (Nome, Matrícula).
- Geração automática de QR Code com dados do aluno.
- QR Code salvo localmente com **logo da escola no centro**.
- Personalização do QR Code (cor, margem, escala).
- Destaque visual da logo com fundo branco circular/quadrado.
- QR exibido diretamente no formulário via `PictureBox`.
- Armazenamento do caminho do QR no banco de dados.
- Verificação automática para evitar duplicação de QR Codes.
- Preparado para integração com impressão de cartões.

---

## 🛠️ Tecnologias Utilizadas

- 🎯 **.NET Framework 4.7.2**
- 💻 **Windows Forms**
- 🗃️ **SQL Server**
- 📦 **QRCoder** (via NuGet)
- 📁 Arquitetura separada por camadas:
  - `Model`: Classe Aluno
  - `DAL`: Acesso ao banco de dados
  - `Service`: Geração de QR Code

---

## 🧱 Estrutura da Tabela SQL

```sql
CREATE TABLE Alunos (
    Id INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(100),
    Matricula NVARCHAR(50),
    CaminhoQRCode NVARCHAR(255)
);
