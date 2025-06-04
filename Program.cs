using bankApI.BusinessLayer.Services;
using bankApI.BusinessLayer.Services.ClientServer.IClient;
using bankApI.BusinessLayer.Services.ClientServer.SClient;
using bankApI.BusinessLayer.Services.EmployeeServer.IEmployee;
using bankApI.BusinessLayer.Services.EmployeeServer.SEmployee;
using bankApI.BusinessLayer.Services.SClient.IClient;
using bankApI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject services
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IClientNotifications, TheNotificatinsService>();
builder.Services.AddScoped<ITransactionsHistory, TransactionsHistoryService>();
builder.Services.AddScoped<IClientsManagement, ClientsManagementService>();
builder.Services.AddScoped<IENotifications, ENotificationsService>();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Authentication setup
var key = Encoding.UTF8.GetBytes("thisIsAReallyStrongSecretKey1234567890");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourIssuer",
            ValidAudience = "yourAudience",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        // Allow reading token from cookie
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["token"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

// CORS configuration - allow only your deployed frontend domain
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("https://nova-umber-tau.vercel.app")
                  .AllowCredentials()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Enable HTTPS redirection **only in development** to avoid the warning on Render
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();

    // Enable Swagger only in development
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
