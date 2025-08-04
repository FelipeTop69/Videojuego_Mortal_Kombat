using Business.AutoMapper;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// AutoMapper
builder.Services.AddAutoMapper(typeof(GeneralMapper));

// Swagger - Extension
builder.Services.AddSwaggerWithJwtSupport();

// CORS - Extension
builder.Services.AddCustomCors(builder.Configuration);

//DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();
app.Run();