using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.ViewModels
{
    // VM là view model 
    public class ProductVM
    {
        public Product Product { get; set; }

        [ValidateNever]
        // [ValidateNever] là một Data Annotation Attribute
        // dùng để chỉ định rằng một thuộc tính cụ thể sẽ không được
        // kiểm tra (validate) trong quá trình xác thực đầu vào của mô hình
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
