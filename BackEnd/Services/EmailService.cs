using MimeKit;
using MailKit.Net.Smtp;

namespace Atak2.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuracao;

        private string assunto = "Gerador de dados Atak Teste - Dados Gerados";
        private string corpo = "Este é um projeto de geração de dados fictícios. Confira o código no repositório: https://github.com/seu-repositorio";
        private string destinatario = "christianryuji@hotmail.com";
        private string nomeArquivo = "clientes.xlsx";

        public EmailService(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public async Task EnviarEmailComAnexoAsync(byte[] arquivo)
        {
            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("[Nome do Projeto]", _configuracao["Email:De"]));
            mensagem.To.Add(new MailboxAddress("dest",destinatario));
            mensagem.Subject = assunto;

            // Corpo do e-mail (Texto)
            var corpoDoTexto = new BodyBuilder
            {
                TextBody = corpo
            };

            // Anexar o arquivo Excel
            using (var stream = new MemoryStream(arquivo))
            {
                corpoDoTexto.Attachments.Add(nomeArquivo, stream.ToArray(), new ContentType("application", "vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            }

            mensagem.Body = corpoDoTexto.ToMessageBody();

            // Configura o cliente SMTP (utilizando MailKit)
            using (var cliente = new SmtpClient())
            {
                try
                {
                    var smtpHost = _configuracao["Email:SmtpHost"];
                    var smtpPort = int.Parse(_configuracao["Email:SmtpPort"]);
                    var smtpUser = _configuracao["Email:SmtpUser"];
                    var smtpPass = _configuracao["Email:SmtpPass"];

                    await cliente.ConnectAsync(smtpHost, smtpPort, true); // Conectar ao servidor SMTP
                    await cliente.AuthenticateAsync(smtpUser, smtpPass);  // Autenticação
                    await cliente.SendAsync(mensagem);                    // Enviar e-mail
                    await cliente.DisconnectAsync(true);                  // Desconectar
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
