using EBookSpace;
using EBookSpace.Services.Implementation;
using EBookSpace.Services.Interfaces;
using EBookSpace.Shared;
using EBookSpace.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;

    options.SignIn.RequireConfirmedAccount = false; // better for dev
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options => { 
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
             System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
             )

    };


});
    



builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer(); // Required for Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book API", Version = "v1" });
});

builder.Services.AddHttpContextAccessor();
//builder.Services.AddHttpClient();
builder.Services.AddTransient<IHomeRepository, HomeRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<IUserOrderRepository, UserOrderRepository>();
builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddTransient<IStockService, StockService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddRazorPages();





var app = builder.Build();
//using(var scope = app.Services.CreateScope())
//{
//    await DbSeeder.SeedDefaultData(scope.ServiceProvider);
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>       // This comes from Swashbuckle
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookShoppingCart API V1");
        c.RoutePrefix = string.Empty; // Optional: open Swagger at root
    });

    app.UseMigrationsEndPoint();
}
else
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

app.MapStaticAssets();
app.MapControllers();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();





app.Run();

// bootswatch.com
//getbootstrap.com

//UPDATE Book
//SET Image = CASE Id
//    WHEN 2 THEN 'Odeyssey.jpg'
//    WHEN 3 THEN 'intothewild.png'
//    WHEN 8 THEN 'treasureisland.png'
//	WHEN 10 THEN 'aroundtheworldineightydays.png'
//	WHEN 17 THEN 'intothinair.png'
//	WHEN 18 THEN 'frankeinstein.jpg'
//END
//WHERE Id IN (2, 3,8,10,17,18);

// username: saipriya.k50@gmail.com pwd: Saipriya@123
// links used Bootstrap 5 Modal

