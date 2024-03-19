using App.Entities;
using App.Extensions;
using App.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddAplicationServices(builder.Configuration);
//builder.Services.AddIdentityServices(builder.Configuration);

// p'q funcione Identity
builder.Services.AddScoped<IUserStore<AppUser>, AppUserStore>();
builder.Services.AddIdentityCore<AppUser>(opciones =>
{
    opciones.Password.RequireDigit = false;
    opciones.Password.RequireLowercase = false;
    opciones.Password.RequireUppercase = false;
    opciones.Password.RequireNonAlphanumeric = false;
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));






app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
