using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Bulky.Utility
{
    // Lớp EmailSender trong đoạn mã trên được sử dụng để gửi email trong ứng dụng của bạn. Nó triển khai giao
    // diện IEmailSender từ thư viện Microsoft.AspNetCore.Identity.UI.Services. Giao diện này định nghĩa phương
    // thức SendEmailAsync để gửi email không đồng bộ.
    //
    // Hiện tại, phương thức SendEmailAsync chỉ trả về một Task.CompletedTask, nghĩa là nó không thực hiện bất
    // kỳ hành động nào.Để lớp này thực sự gửi email, bạn cần triển khai logic gửi email trong phương thức
    // SendEmailAsync.
    public class EmailSender : IEmailSender
    {
        public string SendGridSecret { get; set; } 

        public EmailSender(IConfiguration _config) 
        {
            // Không lưu các API KEY ở appsettings.json
            //SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");

            SendGridSecret = Environment.GetEnvironmentVariable("SendGridApiKey");
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Logic gửi email
            var client = new SendGridClient(SendGridSecret);
            
            var from = new EmailAddress("hello@buicongson.com", "Bulky Book");
            var to = new EmailAddress(email);
            var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
            
            return client.SendEmailAsync(message);
        }
    }
}
