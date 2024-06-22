using TC_IOC.DBContext;
using TC_Application.RequisicoesConteudo.Servicos;
using TC_Domain.RequisicoesConteudo.Repositorios;
using System.Text.Json.Serialization;
using TC_Domain.Usuarios.Servicos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<DapperContext>();


builder.Services.Scan(scan => scan.FromAssemblyOf<RequisicoesConteudoAppServico>().AddClasses().AsImplementedInterfaces().WithScopedLifetime());

builder.Services.Scan(scan => scan.FromAssemblyOf<RequisicoesConteudoRepositorio>().AddClasses().AsImplementedInterfaces().WithScopedLifetime());

builder.Services.Scan(scan => scan.FromAssemblyOf<UsuariosServico>().AddClasses().AsImplementedInterfaces().WithScopedLifetime());


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin(); 
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
