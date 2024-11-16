using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SOLID_Principles.Infrastructure;
using SOLID_Principles.Services.Profiles.Mapping;
using SOLID_Principles.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/User", async (IUserService svc, CancellationToken ct) => 
    await svc.GetUsers(ct)
);

app.MapGet("/api/User({id})", async (int id, IUserService svc, CancellationToken ct) => 
{
    var user = await svc.GetUser(id, ct);
    return user == null ? Results.NotFound() : Results.Ok(user);
});

app.Run();
