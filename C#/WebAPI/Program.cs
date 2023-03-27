using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Csharp_server;
using FileData;
using FileData.DAOs;
using WebSocketSharp.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<ISensorDao, SensorFileDao>();
builder.Services.AddScoped<ISensorLogic, SensorLogic>();

builder.Services.AddScoped<IRoomDao, RoomFileDao>();
builder.Services.AddScoped<IRoomLogic, RoomLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:7890");

wssv.AddWebSocketService<Echo>("/Echo");
wssv.AddWebSocketService<EchoAll>("/EchoAll");

wssv.Start();
Console.WriteLine("WS server started on ws://127.0.0.1:7890/Echo");
Console.WriteLine("WS server started on ws://127.0.0.1:7890/EchoAll");

app.Run();