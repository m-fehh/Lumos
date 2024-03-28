using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lumos.Data;
using Lumos.Data.Models.Management;
using Lumos.Application.Configurations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configuração da autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:JwtBearer:Issuer"],
        ValidAudience = builder.Configuration["Authentication:JwtBearer:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JwtBearer:SecurityKey"]))
    };
});


// Chamada para registrar as dependências padrão
DependencyInjectionConfig.RegisterDependencies(builder.Services, builder.Configuration);


// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuração do Entity Framework
builder.Services.AddDbContext<LumosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nome da sua API", Version = "v1" });
});

// Configure o AutoMapper antes de construir a aplicação
var mapperConfiguration = new MapperConfiguration(cfg =>
{
    Lumos.Application.Configurations.Mapper.CreateMappings(cfg);
});
var mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

// Configuração do Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LumosTechnology v1");
});
app.Run();
