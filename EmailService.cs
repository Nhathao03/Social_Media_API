using MimeKit;
using MailKit.Net.Smtp;

namespace Social_Media
{
    public class EmailService
    {
        private readonly string _from = "nhathaoha11@gmail.com";
        private readonly string _appPassword = "bppq mawm exkc tuho"; 

        public async Task SendOtpAsync(string toEmail, string otp)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Social Media Nila", _from));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "OTP Xác nhận quên mật khẩu";

            message.Body = new TextPart("plain")
            {
                Text = $"Mã xác nhận của bạn là: {otp}. Mã này sẽ hết hạn sau 5 phút."
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_from, _appPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
