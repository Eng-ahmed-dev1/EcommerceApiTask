using Ecomerec.BLL.Services.CartItemServices;
using Ecomerec.BLL.Services.CartServices;
using Ecomerec.BLL.Services.ProductServices;
using Ecomerec.DAL;
using Ecomerec.DAL.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICartRepository,CartRepository>();
builder.Services.AddScoped<ICartService,CartService>();
builder.Services.AddScoped<ICartitemservice,Cartitemservice>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>().AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = "default";
    o.DefaultChallengeScheme = "default";

}).AddJwtBearer("default",o =>
{
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AhmedAlaaAhmedAlistrongpasswordandstrongsercretkey")),
        ValidateIssuer = false,
        ValidateLifetime = true,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
