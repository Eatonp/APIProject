using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApi.Mappers;
using TodoAPI.Data.Database;
using TodoAPI.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string sqliteConnectionString = $"Data Source={System.IO.Directory.GetParent(Directory.GetCurrentDirectory())}\\TodoAPI.Data\\Database.db";

builder.Services.AddControllers();
builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseSqlite(sqliteConnectionString));
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
builder.Services.AddScoped<TodoDbContext>();

var app = builder.Build(); // Past this point we can use the built app to grab any services / control the built application 

// Creates the database and runs all the required migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<TodoDbContext>();
    if(dbContext != null)
        dbContext.Database.Migrate();
}

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
