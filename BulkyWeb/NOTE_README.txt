
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


19. Scaffold Identity 
	- Scaffold Identity là một tính năng cho phép bạn tạo các tệp mã nguồn mặc định liên quan đến Identity (hệ thống quản lý xác thực và phân
	quyền) để tùy chỉnh và mở rộng tính năng đăng nhập, đăng ký, quản lý tài khoản người dùng, .... 
	
	- Identity trong ASP.NET Core là một framework quản lý các chức năng xác thực (authentication) và phân quyền (authorization) cho ứng dụng
	web. Bằng cách "scaffold", bạn có thể tạo mã nguồn mà ASP.NET Core Identity sử dụng và điều chỉnh nó theo nhu cầu của mình.

	- Trong ASP.NET Core, scaffolding là tính năng tự động tạo các tệp mã nguồn cần thiết cho một phần chức năng cụ thể, như:
		+ Controllers: Tự động tạo Controller để xử lý các yêu cầu HTTP.
		+ Views: Tạo các trang giao diện dựa trên model (ví dụ như Create, Edit, Delete, Details).
		+ Models: Tạo các lớp mô hình dữ liệu để tương tác với cơ sở dữ liệu.
		+ Identity: Tạo các trang và logic liên quan đến quản lý người dùng (đăng nhập, đăng ký, đổi mật khẩu).

	- public class ApplicationDbContext : IdentityDbContext chuyển từ kế thừa DbContext sang IdentityDbContext 
	
	1. chọn Scaffold ở bulkyWeb rồi chọn vào mục Identity 
	2. chọn ApplicationDbContext ở mục DbContext class và chọn Override all files
	3. Sau khi thêm, bấm chạy project thì sẽ xuất hiện 1 lỗi là "Some services are not able to
	be constructed (Error while validating thẻ service ...)...." 
		- Bạn hãy thêm Generic type <IdentityUser> vào IdentityDbContext
		- Xóa file BulkyWeb\Areas\Identity\Data\ApplicationDbContext.cs


	- Luôn phải Xác thực(Authentication) rồi mới Ủy quyền(Authorization)
		+ Thêm app.UseAuthentication(); vào program.cs 

**-Authentication-*********************************************************************************************************************************************************
		
	- Add Identity Tables(Bảng danh tính của người dùng)
		+ Để dùng được các trang Razor thì nên đăng ký thêm builder.Services.AddRazorPages() và app.MapRazorPages();
		+ add-migration addIdentityTables để tạo bảng lưu trữ dữ liệu người dùng(AspNetUsers) và các bảng liên quan 
	
	- Khi tạo tự động sẽ có các thuộc tính không cần thiết được tạo và chưa đúng với yêu cầu của mình nên ta cần sửa đổi lại
		+ Tạo thêm class ApplicationUser ở Bulky.Models
		+ add-migration ExtendIdentityUser - Ghi vào bảng AspNetUsers các thuộc tính mà ta yêu cầu đối với User
		+ update-database :>>>>>>>
		+ Trong bảng AspNetUsers sẽ có thuộc tính "Discriminator": nó được sử dụng để phân biệt các loại thực thể (entities)
		khác nhau trong một bảng duy nhất.
		+ Thuộc tính "Discriminator" có giá trị mặc định là IdentityUser, nhưng ở đây ta đã cho ApplicationUser kế thừa IdentityUser
		và trong Register.cshtml.cs có 1 hàm CreateUser với kiểu trả về là IdentityUser. Ta nên đổi kiểu trả về lại thành ApplicationUser

	- Khi tạo mới 1 user thì user đó vẫn chỉ là 1 user bình thường, chưa được phân quyền. Vì vậy gán quyền hoặc vai trò (roles) cho 
	người dùng mới ngay sau khi họ đăng ký tài khoản. 

	- Ta cần tạo các Role ở trong file Bulky.Unitily\SD.cs
	- Khi đăng ký tài khoản thì ta cần gán role cho user đó. Sau khi đăng ký xong thì ta cần EmailSender để gửi email xác nhận đăng ký cho
	user đó, nhưng ở đây chưa có EmailSender nên ta phải tạo thêm 1 class EmailSender impelement IEmailSender 
		+ Sau đó ta cần đăng ký EmailSender vào Service Container 
		+ Ngoài ra thì ta cần add thêm AddDefaultTokenProviders() bởi vì ta muốn Xác nhận Email (Email Confirmation). Nếu bạn muốn yêu cầu 
		người dùng xác nhận email sau khi đăng ký, bạn cần token để tạo liên kết xác nhận. Điều này yêu cầu cấu hình Token Provider mặc định
	
**-Authorization-*********************************************************************************************************************************************************
	- Với user là customer thì giao diện không có phần quản lý Category, Product ... Nên ta cần điều kiện hiển thị dropdown trong _Layout
		+ Đảm bảo chỉ có admin mới có quyền thấy giao diện quản lý
		+ Thêm [Authorize(Roles = SD.Role_Admin)] vào Controller của admin
		+ @if(User.IsInRole(SD.Role_Admin)) 

	- Bên cạnh đó, ta cần thêm ConfigureApplicationCookie() ở trang program
		+ CHỈ CÓ THỂ THÊM COOLIE SAU KHI THÊM DANH TÍNH
		+ Cookie này lưu trữ thông tin phiên đăng nhập và các thiết lập liên quan đến hành vi đăng nhập, đăng xuất, và quyền truy cập


20. Company 
	- Tạo tác chức năng CRUD cho company
	- Khi người dùng đăng ký, ta phải chỉ định người dùng đó thuộc về 1 công ty(company) nào đó. Để làm được điều đó, ta cần thêm 1 khóa ngoại
	cho người dùng của mình
	- Thêm CompanyId cho ApplicationUser
	- Hiển thị Company Dropdown 
		+ Thêm IUnitOfWork vào trang register.cshtml.cs
		+ Thêm thuộc tính vào class InputModel
		+ Tạo CompanyList trong OnGetAsync()
		+ Thêm <select></select> trong register.cshtml
		+ Ta nên hiển thị ra dropdown Company khi Role == "Company" nên cần:
			. Set display: none cho thẻ select
			. Thêm phần js để set lại giá trị cho display


21. Shopping Cart - Giỏ hàng 
	- Tạo class ShoppingCart trong Model
	- Thêm DbSet cho ApplicationDbContext
	- add-migration AddShoppingCartToDb
	- Thêm ShoppingCart vào Repository, UnitOfWork

	- Tạo thêm IApplicationUser trong IRepository

	- Model for Details Page(BulkyWeb\Areas\Customer\Views\Home\Details.cshtml)
	- Khi user click vào AddToCart(Tức là mua hàng) thì phải lấy UserId gán cho ApplicationUserId của shoppingCart
		+ Có trường hợp cùng 1 người, mua cùng 1 món hàng nhiều lần. Lúc này ta cần phải cộng dồn biến Count lại với nhau 
		+ Khi bỏ lệnh _unitOfWork.ShoppingCart.Update(cartFromDb); mà record trong database vẫn được cập nhật, điều này 
		thường xảy ra do Entity Framework (EF) Change Tracker tự động theo dõi các thay đổi trên thực thể (Entity) được 
		lấy từ database. --> Đọc thêm về Tracker 

	- Tạo view cho cart
		+ Nhớ thêm Area("customer")
		+ Thêm hành động cho các nút tăng giảm số lượng hàng và xóa order
		+ Thêm View cho Summary 


22. Order Confrimation(Xác nhận đơn hàng)
	- Tạo class OrderHeader và OrderDetails và add chúng vào Repository 
	- Summary GET Action Method: lấy data để truyền vào View Summary

	- Thêm Order Status managent vào SD.cs
	- SummaryPOST thêm httpPost cho Summary()


23. Order Manager 
	- Tạo Order Controller ở Admin Controller
	- Tạo OrderVM trong Bulky.Models.ViewModels
	- Tạo order.js 
	- Tạo view Index 
		+ asp-route-status: 
	- Tạo các Action cho từng status ở View Index
	- Thêm filter dựa trên status ở func GetAll() trong OrderController
		+ Tức là ứng với mỗi status thì sẽ hiển thị ra Order có trạng thái tương ứng 
	- Sửa lại order.js
		+ Gọi func getall và status
		+ thêm tham số status vào func loadDataTable() 

	- Tạo Get Action cho Order details
		+ Hiển thị thông tin lên các thẻ input 
		+ Phương thức submit có nhiều hành động khác nhau do ở Details.cshtml có nhiều nút.
		Ứng với mỗi nút(input submit) ta sẽ có 1 asp-action="" khác nhau 
			. VD: <input type="submit" asp-action="UpdateOrderDetail" value="Update Order Details" />

	- Chỉ có Admin hoặc Employee mới có quyền thấy tất cả các Order còn mỗi User họ chỉ nhìn thấy order
	của họ thôi. Vì vậy ta cần kiểm tra Role của người dùng trước khi lấy ra danh sách các orderHeader
	trong func GetAll

	- Thực hiện các action ứng với mỗi button trong Details.cshtml
		+ UpdateOrderDetail
		+ StartProcessing
		+ ShipOrder


24. Advance concepts
	- Authorization: thêm quyền cho các Controller
	- Session: Lưu trữ thông tin trên server 
		+ Đầu tiên phải thêm bộ nhớ Cach phân tán ở Program.cs
		+ Sau đó mới thêm phiên(Session)
		+ Bất cứ khi nào thêm mặt hàng mới vào giỏ hàng, ta sẽ thêm giá trị đó vào Session 
		+ Thêm @using Microsoft.AspNetCore.Http
			@inject IHttpContextAccessor HttpContextAccessor vào _Layout.cshtml
		+ Gán giá trị cho SD.SessionCart mỗi khi thêm hàng vào giỏ hàng hoặc khi đăng nhập 

		+ Sau khi LOGOUT thì ta cần phải xóa Session hay xóa giỏ hàng đi 
			. Thêm HttpContext.Session.Clear(); vào Logout.cshtml.cs

	- Tạo View Component
		+ Tạo class ShoppingCartViewComponent 
			. Trong func InvokeAsync() ta sẽ ra Id người dùng hiện tại. Nếu không có thì xóa 
			Session còn trong LOGOUT sẽ không còn dòng code "HttpContext.Session.Clear();"
		+ Phải tạo folder Components trong Views/Share 
			. Tạo folder ShoppingCart rồi tạo Default.cshtml 
			. Trong _layout.cshtml ta sẽ gọi @await Component.InvokeAsync("ShoppingCart") --> Nó sẽ
			trả ra View chính là Default.cshtml

	- Đăng nhập bằng Facebook(hoặc GG hay LinkedIn)
		+ Đầu tiên ta cần tạo 1 ứng dụng mới trên Facebook Developer(https://developers.facebook.com/)
		(https://developers.facebook.com/apps/579749481720878/add/)
		+ Thêm Facebook App ID và Facebook App Secret vào appsettings.json)

		+ Cài đặt Authentication.Facebook ở NuGet Package 
		+ Thêm lệnh builder.Services.AddAuthentication().AddFacebook ở Program.cs
		

25. Deployment and Email 
	- Tạo folder DbInitializer rồi tạo DbInitializer.cs dùng để khởi tạo userAdmin khi deploy dự án  
	    + Thêm hàm SeedDatabase() vào program.cs

	- Tạo tài khoản trên SendGrid https://sendgrid.com/en-us
	- Lấy khóa API Key và thêm vào Biến môi trường (hoặc appsettings.json)
	- Thêm EmailSender vào BulkyBook.Utility





































