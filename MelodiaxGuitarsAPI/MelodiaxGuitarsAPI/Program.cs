using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Extensions;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Brands;
using MelodiaxGuitarsAPI.Repositories.CartItems;
using MelodiaxGuitarsAPI.Repositories.Categories;
using MelodiaxGuitarsAPI.Repositories.OrderProducts;
using MelodiaxGuitarsAPI.Repositories.Orders;
using MelodiaxGuitarsAPI.Repositories.Products;
using MelodiaxGuitarsAPI.Repositories.ShoppingCarts;
using MelodiaxGuitarsAPI.Repositories.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    { 
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Token"]))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
} 
else
{
    app.UseHsts();
}

app.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

/*using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var seedRoles = new SeedRoles();
await seedRoles.SeedTheRoles(roleManager);*/

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var seedRoles = new SeedRoles();
    await seedRoles.SeedTheRoles(roleManager);
}


app.Run();
