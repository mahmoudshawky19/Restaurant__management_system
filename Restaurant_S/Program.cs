using Microsoft.EntityFrameworkCore;
 using Microsoft.AspNetCore.Identity;
using Models;
using DataAccess;
using DataAccess.Repository.IRepository;
using DataAccess.Repository;
using Stripe;
using Utility;

namespace Restaurant_S
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages(); 
            builder.Services.AddDbContext<ApplicationDbContext>(
               option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
               );
            builder.Services.AddScoped<ISubmitReviewRepository, SubmitReviewRepository>();


            builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            builder.Services.AddScoped<IOrderMenuItemRepository, OrderMenuItemRepository>();
            builder.Services.AddScoped<IAssignedOrdersRepository, AssignedOrdersRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
            builder.Services.AddScoped<IOrderTableRepository, OrderTableRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            builder.Services.AddIdentity<IdentityUser, IdentityRole>
                (options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

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
            app.MapRazorPages(); 

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
//using DataAccess;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;
//using Model;
//using DataAccess.Data;
//using DataAccess.Repository.IRepository;
//using DataAccess.IRepository;
//using DataAccess.Repository;

//namespace Hotel_Track
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);



//            // Add services to the container.
//            builder.Services.AddControllersWithViews();
//            builder.Services.AddDbContext<ApplicationDbContext>(
//                option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//                );

//            builder.Services.AddIdentity<ApplicationUser, IdentityRole>
//                (options => options.SignIn.RequireConfirmedAccount = true)
//                .AddEntityFrameworkStores<ApplicationDbContext>()
//                .AddDefaultUI()
//                .AddDefaultTokenProviders();
//            builder.Services.AddScoped<ICartRepository, CartRepository>();
//            builder.Services.AddScoped<IHotelRepository, HotelRepository>();
//            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
//            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
//            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
//            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
//            builder.Services.AddRazorPages();


//            var app = builder.Build();



//            // Configure the HTTP request pipeline.
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }
//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();
//            app.MapRazorPages();

//            app.UseAuthorization();

//            app.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}");

//            app.Run();
//        }
//    }
//}