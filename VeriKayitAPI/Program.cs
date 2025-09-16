using Microsoft.EntityFrameworkCore;
using VeriKayitAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantý dizesi ve servisler
builder.Services.AddDbContext<VeriDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<VeriDbContext>();
        await dbContext.Database.CanConnectAsync();
        app.Logger.LogInformation("Veritabanýna baþarýyla baðlanýldý.");
    }
}
catch (Exception ex)
{
    app.Logger.LogError("Veritabaný baðlantýsý baþarýsýz: " + ex.Message);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
