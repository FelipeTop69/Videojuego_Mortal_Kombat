using Business.AutoMapper;
using Business.JWTService;
using Business.JWTService.Interfaces;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// =============== [ SERVICES ] ===============
builder.Services.AddPersistence(builder.Configuration); 
builder.Services.AddControllers();

// AutoMapper
builder.Services.AddAutoMapper(typeof(GeneralMapper));

// Swagger - Extension
builder.Services.AddSwaggerWithJwtSupport();

// JWT - Extension 
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();


// CORS - Extension
builder.Services.AddCustomCors(builder.Configuration);
    
// =============== [ REGISTER DbContext ] ===============
builder.Services.AddEntitiesServices(); 

var app = builder.Build();

// =============== [ MIDDLEWARE ] ===============
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // no estorba aunque no uses login aún
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
