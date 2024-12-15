
1. Khi Find, Get thì nên kiểm tra xem có bằng null không? 


+----------LUÔN LUÔN NHỚ------------------------------------ Bất cứ khi nào cần cập nhật thứ gì vào DB ta đều cần thêm 1 migration - "add-migration ..." ---------------+
+----------------------------------------------------------- Sau khi thêm thì cần phải cập nhật lại database - "update-database" ---------------------------------------+


1. Tạo Database - Program.cs
	- Vào tools
	- Chọn Nuget Package Manager
	- Chọn Package Manager Console 
	- Hiện ra cửa sổ Package Manager Console 
	- Nhập update-database 
	- Hiển thị thông báo "No migrations were applied. The database is already up to date" --> Đã tạo DB thành công 


2. Create Category Table - ApplicationDbContext.cs
	- Class Category bắt buộc phải có khóa chính 
	- Vào Package Manager Console 
	- Nhập add-migration AddCategoryTableToDb 
	- Hiển thị thông báo "Build succeeded. To undo this action, use Remove-Migration." 
	- Vào Database kiểm tra xem đã tạo được bảng chưa? (Ở đây là SqlServer) 
	- Nếu chưa có thì nhập "update-database" vào Package Manager Console --> Thúc đẩy quá trình migrations vào cơ sở dữ liệu
	- Rồi vào kiểm tra lại 


3. Add Category Controller 
	- Tạo file CategoryController.cs trong folder Controllers 
	- Phải tạo thêm folder Category trong folder Views 
	- Dựa vào action để tạo thêm các file tương ứng 

	- VD: https://localhost:7169/category/index 
		+ Controller: category 
		+ Action: index


4. Seed Category Table - ApplicationDbContext.cs 
	- Thêm records vào trong bảng Categories ở Database
    - Để thêm thì cần phải thêm 1 migration 
	- Nhập add-migration SeedCategoryTable vào Package Manager Console 
    - Bất cứ khi nào cần cập nhật thứ gì vào DB ta đều cần thêm 1 migration
    - Sau khi thêm thì cần phải cập nhật lại database 
	- Nhập update-database vào Package Manager Console 


5. Tạo project mới 
	- Chuột phải vào Solution 
	- Chọn Add rồi chọn Class Library 
	- Project BulkyBook.DataAccess dùng để xử lý mọi thứ liên quan đến database 
	- Projetc BulkyBook.Ulitily dùng để lưu các tiện ích mà ta sẽ thêm 
		+ Chức năng email 
		+ Các hằng số 
		+ ... 


6. Trong project BulkyBook.DataAccess 
	- Ta cần các gói NuGet liên quan đến Entity Fw Core


7. Chuyển data sang project BulkyBook.DataAccess 
	- Khi chuyển data từ BulkyBookWeb project sang BulkyBook.DataAccess project thì dự án mặc định phải cập nhật lên project 
	BulkyBook.DataAccess. Còn project khởi động mặc định(Startup project) là BulkyBookWeb 
	- Chạy lệnh "add-migration AddCategoryToDbAndSeedTable" ở Package Manager Console để thêm migration 
	- Sau đó chạy lệnh "update-database" 

	(*) Trong trường hợp migration bị hỏng thì có thể xóa table ở trong database, xóa folder migration rồi chạy lệnh "add-migration ... " 


8. Dependency Injection Service Lifetimes 


9. Unit Of Work 
	- Trong UnitOfWork chúng ta có quyền truy cập tất cả các repositories mà ta muốn
	- Nhưng nó cx có nhược điểm: 
		+ Giả sử có thêm repository của Order, hay Product thì tại CategoryController ta cũng có thể truy cập được vào các repository ấy

	- Lớp IUnitOfWork là interface bao gồm các repository, bằng cách này thay vì phải inject từng repository vào nơi cần sử dụng thì chỉ 
	cần inject lớp UnitOfWork này và mỗi khi cần được sử dụng thì nó mới khởi tạo instance cho repository đó.
	- Trong IUnitOfWork có method Save(), khi gọi method này thì tất cả những thay đổi từ các repository sẽ đều được lưu lại mà không cần
	phải save lại nhiều lần khi mỗi thay đổi xảy ra 


10. Areas in .NET 
	- Bạn muốn trang web có 2 giao diện dành cho customer và cho admin
	- Chuột phải vào BulkyBookWeb
	- Chọn Add 
	- Chọn new scaffolded item
	- Chọn MVC Areas 
	- Nhập "Admin" 

	- Sau đó ta phải thêm areas vào routing
		+ Copy "{area:exists}" trong file ScaffoldingReadMe.txt 
		+ Vào file Program.cs và paste -> Sau đó sửa thành    mặc định    là {area=Customer}    (Sửa dấu : thành dấu = )
			app.MapControllerRoute(
                Title: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

	- Tiếp theo Click chuột phải vào folder Areas rồi tạo Customers Areas tương tự như Admin 
	- Xóa bỏ folder Data và Models ở 2 folder vừa tạo 

	- Di chuyển file CategoryController.cs vào Admin/Controllers 
	- Di chuyển file HomeController.cs vào Customer/Controllers 

	- Để controller biết controller này thuộc về khu vực cụ thể nào thì thêm [Area("Admin")] vào CategoryController.cs
	- Tượng tự thêm [Area("Customer")] vào HomeController.cs

	- Di chuyển folder BulkyBookWeb/Views/Category vào Admin/Views 
	- Di chuyển folder BulkyBookWeb/Views/Hole vào Customer/Views 

	- Copy 2 file _ViewImports.cshtml, _ViewStart.cshtml và paste vào Admin/Views và Customer/Views 

	- Vào file _Layout.cshtml và thêm asp-area="Customer" hoặc asp-area="Admin" vào các thẻ a


11. Thêm khóa ngoại trong Entity Framwork Core 
	- Có một thuộc tính điều hướng tới bảng danh mục và tôi sẽ gọi danh mục đó.
	
	- VD: Thuộc tính Category này được sử dụng để điều hướng khóa ngoại cho categoryId. Sử dụng chú thích dữ liệu 

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

	- Vào file "Bulky.DataAccess\Data\ApplicationDbContext.cs" rồi thêm thuộc tính "CategoryId = 1" vào các new object
	- Sau khi viết thêm thuộc tính thì chạy lệnh "add-migration addForeignKeyForCategoryProductRelation" ở Package Manager Console

	- Nếu bị lỗi conflic thì thử đổi defaultValue: 0 thành 1 trong file "Bulky.DataAccess\Migrations\20241002152828_addForeignKeyForCategoryProductRelation.cs"
			migrationBuilder.AddColumn<int>(
						name: "CategoryId",
						table: "Products",
						type: "int",
						nullable: false,
						defaultValue: 1);	
			+ Rồi chạy "update-database"


12. Thêm cột URL Image 
	- Ta thêm thuộc tính ImageUrl vào Product 
	- VD: 
		public string ImageUrl { get; set; }

	- Sau đó thêm giá trị ImageUrl = "" vào file "Bulky.DataAccess\Data\ApplicationDbContext.cs" 
	- Chạy lệnh "add-migration addImageUrlToProduct"
	- Rồi chạy "update-database"


13. Sử dụng ViewBag, ViewData để tạo dropdown - SelectListItem 
	- 
	- 


14. File Upload 
	- Trong form phải có enctype="multipart/form-data" 
	- Khi upload file dữ liệu cần được mã hóa thành nhiều phần để server có thể hiểu và xử lý chúng đúng cách.
	Nếu không có thì "form chỉ mã hóa dữ liệu dưới dạng văn bản"

	
15. Kết hợp trang Create và Update Product lại với nhau 
	- Chỉnh sửa trong file ProductController.cs
	- Xoá bỏ func Update
	- Đổi tên func Create -> Upsert 
	- Thay đổi ở file _layout.cshtml 
	- Thay đổi ở file BulkyWeb\Areas\Admin\Views\Product\Index.cshtml
	- Thêm "IFormFile? file" vào param cho phương thức Upsert
		+ Nó được sử dụng để nhận một tệp (file) được tải lên từ phía người dùng thông qua form.

16. Tải Ảnh 
	- Tạo folder images ở wwwroot 
	- Tạo folder product là con của images  
	- Tạo 1 biến IWebHostEnvironment _webHostEnvironment dùng để cung cấp thông tin về môi trường lưu trữ ứng dụng web (hosting environment)
		+ WebRootPath: Đường dẫn đến thư mục wwwroot của ứng dụng, nơi chứa các tài nguyên tĩnh (CSS, JavaScript, hình ảnh, ...)


17. Display/Handle image on Update 
	

18. Sử dụng navigation property
	- Khi truy vấn dữ liệu từ một bảng, ta có thể sử dụng các thuộc tính này để dễ dàng truy cập dữ liệu từ các bảng liên quan mà không cần 
	phải thực hiện truy vấn thủ công (join).


