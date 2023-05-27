using System.Text;
using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Domain.Auth;
using EfcDataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShopApplication.LogicInterfaces;
using WebAPI.IoTGate;
using WebAPI.IoTGate.Background;
using WebAPI.IoTGate.Interface;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HospitalContext>();
builder.Services.AddScoped<ISensorDao, SensorEfcDao>();
builder.Services.AddScoped<ISensorLogic, SensorLogic>();

builder.Services.AddScoped<IWebClient, LoriotClient>();
builder.Services.AddHostedService<WebClientBackgroundService>();

builder.Services.AddScoped<IPatientLogic, PatientLogic>();
builder.Services.AddScoped<IPatientDao, PatientEfcDao>();

builder.Services.AddScoped<IDoctorLogic, DoctorLogic>();
builder.Services.AddScoped<IDoctorDao, DoctorEfcDao>();

builder.Services.AddScoped<IReceptionistLogic, ReceptionistLogic>();
builder.Services.AddScoped<IReceptionistDao, ReceptionistEfcDao>();

builder.Services.AddScoped<IRoomDao, RoomEfcDao>();
builder.Services.AddScoped<IRoomLogic, RoomLogic>();

builder.Services.AddScoped<IRequestDao, RequestEfcDao>();
builder.Services.AddScoped<IRequestLogic, RequestLogic>();
builder.Services.AddScoped<IUserDao, UserEfcDao>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

AuthorizationPolicies.AddPolicies(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();


app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseAuthorization();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();