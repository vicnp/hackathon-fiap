using Hackathon.Fiap.Application.Medicos.Servicos;
using Hackathon.Fiap.Domain.Medicos.Servicos;
using Hackathon.Fiap.Domain.Utils.Middleware;
using Hackathon.Fiap.Infra.Medicos;
using Hackathon.Fiap.Infra.Utils.DBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.UseHttpClientMetrics();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<DapperContext>();

builder.Services.Scan(scan => scan.FromAssemblyOf<MedicosAppServico>().AddClasses().AsImplementedInterfaces().WithScopedLifetime());
builder.Services.Scan(scan => scan.FromAssemblyOf<MedicosRepositorio>().AddClasses().AsImplementedInterfaces().WithScopedLifetime());
builder.Services.Scan(scan => scan.FromAssemblyOf<MedicosServico>().AddClasses().AsImplementedInterfaces().WithScopedLifetime());
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
string? secretsValue = configuration.GetValue<string>("SecretJWT") ?? throw new NullReferenceException("Null SecretJWT");
var chaveCriptografia = Encoding.ASCII.GetBytes(secretsValue);


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.RequireHttpsMetadata = false;
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(chaveCriptografia),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSwaggerGen(b =>
{
    b.SwaggerDoc("v1", new OpenApiInfo { Title = "Hackathon.Fiap" });
    b.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization Header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    b.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id ="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI();
app.UseMetricServer();
app.UseHttpMetrics();
app.UseMiddleware<ExceptionHandlingMiddleware>();


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

public partial class Program { }