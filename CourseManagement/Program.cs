
using CourseManagement.Repositories;
using CourseManagement.Data;
using Microsoft.EntityFrameworkCore;
using CourseManagement.UnitOfWork;
using CourseManagement.Services;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();


// repositories dependency injection
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();


// Unit of Work dependency injection
builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();



// Service layer dependency

builder.Services.AddScoped<IStudentService ,StudentService>();
builder.Services.AddScoped<ICourseService , CourseService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()                
              .AllowAnyMethod();                  
    });
});


var app = builder.Build();


app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
