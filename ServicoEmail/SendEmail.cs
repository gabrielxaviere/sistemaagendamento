using System.Net;
using System.Net.Mail;

namespace ServicoEmail
{
    public class SendEmail
    {
        public void SendEmailAsync()
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("teste@gmail.com", "teste"),
                    EnableSsl = true,
                };

                var mensagem = new MailMessage
                {
                    From = new MailAddress("seu_email@gmail.com"),
                    Subject = "Assunto do E-mail",
                    Body = "Body do email",
                    IsBodyHtml = false,
                };

                mensagem.CC.Add("destinatario2@example.com");


                smtpClient.Send(mensagem);

                Console.WriteLine("E-mail enviado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar o e-mail: {ex.Message}");
            }
        }
    }
}
