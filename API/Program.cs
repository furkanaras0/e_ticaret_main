using API.Data;
using API.Entity;
using Microsoft.EntityFrameworkCore;
using API.Middlewares;
using Api.Data;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("defaultConnection");

    options.UseSqlite(connectionString);
});

builder.Services.AddCors();

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<DataContext>();
builder.Services.Configure<Microsoft.AspNetCore.Identity.IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = "rsf.com",//token üretirken verdiğimiz issuer ile buradaki issuer aynı olmalı
                ValidateAudience = false,
                ValidAudience = "abc", //token üretirken verdiğimiz audience ile buradaki audience aynı olmalı
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                    builder.Configuration["JWTSecurity:SecretKey"]!)),
                ValidateLifetime = true
            };
        });


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<TokenServices>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandling>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Demo API");
    });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await SeedDatabase.Initialize(app);

app.Run();
