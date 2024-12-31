using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.ViewModels
{
    public class ShoppingCartVM
    {
        // Danh sách các giỏ hàng
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

        // Tổng số đơn đặt hàng
        public double OrderTotal { get; set; }
    }
}
