using AutoMapper;
using SOLID_Principles.Domain;
using SOLID_Principles.Infrastructure;
using SOLID_Principles.Services.DTOs.Users;
using SOLID_Principles.Services.Profiles.Mapping;
using SOLID_Principles.Services.UserService;

using (var ctx = new AppDbContext())
{
    var userService = await SetupUserService(ctx);

    var dto = new PostUserDTO
    {
        Name = "New User",
        Email = "new_user@gmail.com"
    };

    var createdUser = await userService.CreateUserAsync(dto);

    Console.WriteLine("Fetching all users from the context now...");

    var dbUsers = await userService.GetUsers();
    foreach (User user in dbUsers)
        Console.WriteLine($"User {user.Name} has id {user.Id} and email {user.Email}");
}

#region methods
async Task<IUserService> SetupUserService(AppDbContext ctx)
{
    //Remove existing users
    ctx.Set<User>().RemoveRange(ctx.Set<User>());
    await ctx.SaveChangesAsync();

    //Setup mapping and validation profiles etc.
    var config = new MapperConfiguration(cfg => cfg.AddProfile(new PostUserDTOMappingProfiles()));
    var mapper = config.CreateMapper();

    var userService = new UserService(ctx, mapper);
    return userService;
}
#endregion