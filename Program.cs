using Microsoft.EntityFrameworkCore;
using ToDoList_MVC.Data;
using ToDoList_MVC.Repositories;
using ToDoList_MVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDB>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IToDoRepositories, ToDoRepository>();
builder.Services.AddScoped<IToDoServices, ToDoService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();