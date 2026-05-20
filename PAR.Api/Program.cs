var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string cadenaSQL = builder.Configuration.GetConnectionString("ParSystem")
    ?? throw new InvalidOperationException("Cadena de conexión no encontrada.");

builder.Services.AddSingleton<PAR.Core.Services.Interfaces.IBusinessLogicService, PAR.Core.Services.BusinessLogicService>();

builder.Services.AddSingleton(proveedor =>
{
    var logica = proveedor.GetRequiredService<PAR.Core.Services.Interfaces.IBusinessLogicService>();
    return new PAR.Api.Services.CatalogoDataService(cadenaSQL, logica);
});
builder.Services.AddSingleton(proveedor =>
{
    return new PAR.Api.Services.AuthDataService(cadenaSQL);
});
builder.Services.AddSingleton(proveedor =>
{
    return new PAR.Api.Services.ReportesService(cadenaSQL);
});

var app = builder.Build();

// --- 4. TUBERÍA DE LA APP ---
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();