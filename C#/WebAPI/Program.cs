using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using EfcDataAccess;
using WebAPI.Gateway;


var builder = WebApplication.CreateBuilder(args);
LoriotClient client = LoriotClient.Instance;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HospitalContext>();
builder.Services.AddScoped<ISensorDao, SensorEfcDao>();
builder.Services.AddScoped<ISensorLogic, SensorLogic>();

builder.Services.AddScoped<IPatientLogic, PatientLogic>();

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