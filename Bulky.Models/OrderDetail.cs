using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    // Class này được dùng để lưu trữ thông tin chi tiết của từng sản phẩm trong một đơn hàng.
    // Mỗi đơn hàng có thể bao gồm nhiều sản phẩm, và mỗi sản phẩm sẽ có một bản ghi trong OrderDetail.
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }


        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }


        public int Count { get; set; }
        public double Price { get; set; }
    }
}
