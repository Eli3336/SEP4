using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using EfcDataAccess;
using WebAPI.IoTGate;
using WebAPI.IoTGate.Background;
using WebAPI.IoTGate.Interface;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();