using System;
using SOLID_Principles.Domain;
using SOLID_Principles.Services.DTOs.Users;

namespace SOLID_Principles.Services.UserService;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers(CancellationToken token = default);
    Task<User?> GetUser(int id, CancellationToken token = default);
    Task<User?> CreateUserAsync(PostUserDTO model, CancellationToken token = default);
    Task<User?> UpdateUserAsync(int id, PostUserDTO model, CancellationToken token = default);
    Task<bool> DeleteUserAsync(int id, CancellationToken token = default);
}
