using System.Reflection;
using System.Text;
using Catalogo_Escolar_API.Helpers;
using Catalogo_Escolar_API.Services.AuthService;
using Catalogo_Escolar_API.Services.BackupService;
using Catalogo_Escolar_API.Services.EmailService;
using Catalogo_Escolar_API.Services.StudentService;
using Catalogo_Escolar_API.Services.TeacherService;
using Catalogo_Escolar_API.Services.UniqueService;
using Catalogo_Escolar_API.Settings.AuthSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Cors policy to allow all origins, methods, and headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add settings
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection(nameof(AuthSettings)));
builder.Services.AddSingleton<IAuthSettings>(sp => sp.GetRequiredService<IOptions<AuthSettings>>().Value);

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));
builder.Services.AddSingleton<IEmailSettings>(sp => sp.GetRequiredService<IOptions<EmailSettings>>().Value);

// Add services to the container.
builder.Services.AddScoped<IUniqueService, UniqueService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<JWTGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add hosted services to the container
builder.Services.AddHostedService<BackupService>();

// Add controllers to the container
builder.Services.AddControllers();

//Add authorization
var authSettings = builder.Configuration.GetSection(nameof(AuthSettings)).Get<AuthSettings>() ?? throw new Exception("AuthSettings section is missing in appsettings.json");

var key = Encoding.ASCII.GetBytes(authSettings.SecretKey);
builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Teacher", policy => policy.RequireRole("teacher"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //Add custom controller summaries
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    //Add security definition to swagger (JWT)
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter your JWT token in this field",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});
  
builder.Services.AddDbContext<SchoolContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.LogTo(Console.WriteLine, LogLevel.Critical);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
