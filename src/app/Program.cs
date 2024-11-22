using Microsoft.EntityFrameworkCore;
using SOLID_Principles.Infrastructure;
using SOLID_Principles.Services.ExpenseService;
using SOLID_Principles.Services.Profiles.Mapping;
using SOLID_Principles.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//Configure Db Context
builder.Services.AddDbContext<AppDbContext>(opt => 
{        
    // This is merely a sandbox project, never going to be used in a production environment,
    // hence why the following are enabled & why the conn string is exposed..
    opt.EnableSensitiveDataLogging();
    opt.EnableDetailedErrors();

    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Configure Profiles
builder.Services.AddAutoMapper(typeof(PostUserDTOMappingProfiles).Assembly);

//Dependency Injections
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
