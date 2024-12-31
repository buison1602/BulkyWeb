using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public int? CompanyId { get; set; } // Dùng làm khóa ngoại kết nối với bảng Company.
        [ForeignKey("CompanyId")] // Xác định CompanyId là khóa ngoại và mối quan hệ giữa
                                  // ApplicationUser và Company
        [ValidateNever] // Bỏ qua quá trình kiểm tra tính hợp lệ (validation) của thuộc tính
                        // Company trong quá trình binding dữ liệu từ client
        public Company? Company { get; set; } // - Đây là thuộc tính điều hướng (navigation
                                              // property) để thiết lập mối quan hệ với thực thể Company 
                                              // - Cho phép truy cập thông tin chi tiết của thực thể Company
                                              // tương ứng với CompanyId mà không cần thực hiện thủ công một truy vấn khác
    }
}
