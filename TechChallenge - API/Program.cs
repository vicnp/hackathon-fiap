using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TC_Application.Contatos.Servicos;
using TC_Domain.Contatos.Servicos;
using TC_Infra.Contatos;
using TC_Infra.Utils.Repositorios;
using TC_IOC.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<DapperContext>();

builder.Services.Scan(scan => scan.FromAssemblyOf<ContatosAppServico>().AddClasses().AsImplementedInterfaces().WithScopedLifetime());
builder.Services.Scan(scan => scan.FromAssemblyOf<ContatosRepositorio>().AddClasses().AsImplementedInterfaces().WithScopedLifetime());
builder.Services.Scan(scan => scan.FromAssemblyOf<ContatosServico>().AddClasses().AsImplementedInterfaces().WithScopedLifetime());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var chaveCriptografia = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretJWT"));


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
    b.SwaggerDoc("v1", new OpenApiInfo { Title = "TC - API" });
    b.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization Header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
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
