using Microsoft.AspNetCore.Identity.UI.Services;

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
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Logic gửi email
            return Task.CompletedTask;
        }
    }
}
