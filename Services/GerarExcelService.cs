using Atak2.Models;
using OfficeOpenXml;

namespace Atak2.Services
{
    public class GerarExcelService
    {
        public async Task<byte[]> GerarArquivoExcelAsync(int quantidade)
        {
            var clientes = GerarClientesFicticios(quantidade);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var pacote = new ExcelPackage())
            {
                var planilha = pacote.Workbook.Worksheets.Add("Clientes");

                planilha.Cells[1, 1].Value = "Nome";
                planilha.Cells[1, 2].Value = "Email";
                planilha.Cells[1, 3].Value = "Telefone";
                planilha.Cells[1, 4].Value = "Data de Nascimento";

                for (int i = 0; i < clientes.Count; i++)
                {
                    planilha.Cells[i + 2, 1].Value = clientes[i].Nome;
                    planilha.Cells[i + 2, 2].Value = clientes[i].Email;
                    planilha.Cells[i + 2, 3].Value = clientes[i].Telefone;
                    planilha.Cells[i + 2, 4].Value = clientes[i].DataNascimento.ToShortDateString();
                }

                return await pacote.GetAsByteArrayAsync();
            }
        }

        private List<ClienteModel> GerarClientesFicticios(int quantidade)
        {
            var clientes = new List<ClienteModel>();
            var rand = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                clientes.Add(new ClienteModel
                {
                    Nome = $"Cliente {i + 1}",
                    Email = $"cliente{i + 1}@exemplo.com",
                    Telefone = $"(44) 9{rand.Next(1000, 9999)}-{rand.Next(1000, 9999)}",
                    DataNascimento = new DateTime(rand.Next(1950, 2000), rand.Next(1, 12), rand.Next(1, 28))
                });
            }

            return clientes;
        }
    }
}
