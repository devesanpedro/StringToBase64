using Microsoft.AspNetCore.Mvc;
using StringToBase64.API;
using StringToBase64.Application;
using StringToBase64.Application.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddCors(setup => setup.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));


//builder.Services.AddCors(options =>
//        {
//            options.AddDefaultPolicy(builder =>
//                builder.SetIsOriginAllowed(_ => true)
//                .AllowAnyMethod()
//                .AllowAnyHeader()
//                .AllowCredentials());
//        });

builder.Services.AddScoped<ICommonService, CommonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<MainHub>("/mainHub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseCors();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.Run();
