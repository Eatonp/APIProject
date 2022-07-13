using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApi.Mappers;
using TodoAPI.Data.Database;
using TodoAPI.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var sqlConnectionString = builder.Configuration.GetConnectionString("sqlite");
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt => opt.UseSqlite(sqlConnectionString));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TodoApi", Version = "v1" });
});

// Setup automapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

// Here we can do dependency injection for all the services we need
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddSingleton(mapperConfig.CreateMapper());
builder.Services.AddTransient<TodoContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
