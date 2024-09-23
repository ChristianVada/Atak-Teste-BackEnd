using MimeKit;
using MailKit.Net.Smtp;
using Atak2.Models;

namespace Atak2.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuracao;

        public EmailService(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public async Task EnviarEmailComAnexoAsync(byte[] arquivo, DadosEmailModel dadosEmail)
        {
            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("Atak Teste Gerador", _configuracao["Email:De"]));
            mensagem.To.Add(new MailboxAddress("", dadosEmail.Destinatario));
            mensagem.Subject = dadosEmail.Assunto;

            var corpoDoTexto = new BodyBuilder
            {
                TextBody = dadosEmail.CorpoEmail
            };

            using (var stream = new MemoryStream(arquivo))
            {
                corpoDoTexto.Attachments.Add("clientes-gerados.xlsx", stream.ToArray(), new ContentType("application", "vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            }

            mensagem.Body = corpoDoTexto.ToMessageBody();

            using (var cliente = new SmtpClient())
            {
                try
                {
                    var smtpHost = _configuracao["Email:SmtpHost"];
                    var smtpPort = int.Parse(_configuracao["Email:SmtpPort"]);
                    var smtpUser = _configuracao["Email:SmtpUser"];
                    var smtpPass = _configuracao["Email:SmtpPass"];

                    await cliente.ConnectAsync(smtpHost, smtpPort, true);
                    await cliente.AuthenticateAsync(smtpUser, smtpPass);
                    await cliente.SendAsync(mensagem);
                    await cliente.DisconnectAsync(true);
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
