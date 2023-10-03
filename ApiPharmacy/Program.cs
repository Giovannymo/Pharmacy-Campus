using System.Reflection;
using ApiPharmacy.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => 
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
})/* .AddXmlSerializerFormatters() */;  

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); });
builder.Services.ConfigureCors(); // aplica el cors de serviceExtension
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly()); // aplica automapper
builder.Services.AddJwt(builder.Configuration); //Aplicacion de JWT
builder.Services.AddAplicacionServices(); // para que aplique el archivo de extensions
builder.Services.AddDbContext<PharmacyContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("ConexMysql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using(var scope= app.Services.CreateScope()){
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try{
    var context = services.GetRequiredService<PharmacyContext>();
    await context.Database.MigrateAsync();
    }
    catch(Exception ex){
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex,"Ocurrió un error durante la migración");
    }
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
