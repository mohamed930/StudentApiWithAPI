using Microsoft.Extensions.Options;
using MongoDB.Driver;
using test;
using test.Connection;
using test.Api.StudentRout;
using test.Api.Studentroute;
using test.Api.Courseroute;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StudentConnectionSetting>(
    builder.Configuration.GetSection(nameof(StudentConnectionSetting)));

builder.Services.AddSingleton<StudentConnection>(sp =>
    sp.GetRequiredService<IOptions<StudentConnectionSetting>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string> ("StudentConnectionSetting:ConnectionString")));

builder.Services.AddScoped<StudentProtocol, StudentRoute>();

builder.Services.Configure<CoursesConnectionSetting>(
    builder.Configuration.GetSection(nameof(CoursesConnectionSetting)));

builder.Services.AddSingleton<CourseConnection>(sp =>
    sp.GetRequiredService<IOptions<CoursesConnectionSetting>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("CoursesConnectionSetting:ConnectionString")));

builder.Services.AddScoped<CoursesProtocol, CourseRoute>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();

