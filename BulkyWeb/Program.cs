using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Bulky.Utility;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Numerics;

namespace BulkyWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // - Thêm dịch vụ Identity(lớp đại diện cho một người dùng trong ứng dụng) và Role(vai trò) của người dùng vào ứng dụng
            // với cấu hình mặc định.
            //
            // options => options.SignIn.RequireConfirmedAccount = true
            // cấu hình yêu cầu người dùng phải xác minh tài khoản (qua email hoặc một cách khác) trước khi họ có thể đăng nhập
            //
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            // Cấu hình ASP.NET Identity sử dụng Entity Framework Core để lưu trữ dữ liệu người dùng và danh tính vào cơ sở dữ liệu.
            //
            //.AddDefaultTokenProviders()
            //      - Thêm các token providers mặc định cho Identity.
            //      - Token providers được sử dụng trong các tính năng như:
            //          + Xác nhận tài khoản thông qua email hoặc SMS.
            //          + Đặt lại mật khẩu.
            //          + Xác thực đa yếu tố(Two - Factor Authentication).
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => 
                options.SignIn.RequireConfirmedAccount = true
                ).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


            // CHỈ CÓ THỂ THÊM COOKIE SAU KHI THÊM DANH TÍNH
            // - Khi yêu cầu một tài nguyên bảo mật, nếu chưa đăng nhập, ứng dụng sẽ tự động chuyển hướng đến URL /Identity/Account/Login
            // - Người dùng sẽ được chuyển hướng đến URL /Identity/Account/Logout khi họ thực hiện đăng xuất.
            // - Nếu người dùng đã đăng nhập nhưng không có quyền truy cập vào tài nguyên, họ sẽ được chuyển hướng đến URL /Identity/Account/AccessDenied.
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
            builder.Services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "579749481720878";
                options.AppSecret = "01d48774f0a40240b81bcb59f2391cc8";
            });


            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = System.TimeSpan.FromMinutes(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            // Đăng ký dịch vụ Rezor vào Dependency Injection. Vì khi add Scaffolded Item, các file tự động được tạo thêm là RezorPage
            // Mà tại program.cs chỉ hỗ trợ cho MVC nên ta phải đăng ký dịch vụ Razor :>>>>>>>>>> 
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IEmailSender, EmailSender>();

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            // - app.MapRazorPages() ánh xạ các Razor Pages (.cshtml) thành các endpoint HTTP để ứng dụng xử lý
            // được các yêu cầu đến các tệp Razor Page.
            // - sử dụng app.MapRazorPages() nếu ứng dụng của bạn có sử dụng Razor Pages.
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
