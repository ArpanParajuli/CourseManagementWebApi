
using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.Repositories;
using CourseManagement.Services;
using CourseManagement.UnitOfWork;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();


builder.Services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
builder.Services.AddScoped<IGenericRepository<Course>, GenericRepository<Course>>();

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
