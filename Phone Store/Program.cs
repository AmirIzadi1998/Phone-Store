using Application.CQRS.PhoneProductCQRS.Command;
using Application.Repositories.ProductCQRSRepo;
using Application.Repositories.DapperRepo;
using Application.Repositories.ProductRepo;
using Application.Repositories.UnitOfWorkRepo;
using Application.Repositories.UserRepo;
using AutoMapper;
using Core.Context;
using Infrastructure;
using Infrastructure.Utility;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Phone_Store;

var builder = WebApplication.CreateBuilder(args);
//configs from appsetting
builder.Services.AddOptions();
builder.Services.Configure<Configs>(builder.Configuration.GetSection("Configs"));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSwagger();
builder.Services.AddJWT();

//Register Class:
builder.Services.AddSingleton<EncryptionUtility>();
builder.Services.AddSingleton<NewConnectionUtility>();
builder.Services.AddSingleton<QueryUtility>();

//Register Repusitories:
builder.Services.AddScoped<IPhone, Phone>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IProductDapperRepo, ProductDapperRepo>();

//Register MediatR:
builder.Services.AddMediatR(typeof(SaveCommandHandler));

//Register AutoMapper:
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
