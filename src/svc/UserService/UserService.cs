using System.Data.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SOLID_Principles.Domain;
using SOLID_Principles.Infrastructure;
using SOLID_Principles.Services.DTOs.Users;

namespace SOLID_Principles.Services.UserService;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> GetUsers(CancellationToken token = default)
    {
        var users = await _context.Set<User>().ToListAsync(token);
        return users;
    }

    public async Task<User?> GetUser(int id, CancellationToken token = default)
    {
        var user = await _context.Set<User>().SingleOrDefaultAsync(u => u.Id.Equals(id), token);
        return user;
    }

    public async Task<User?> CreateUserAsync(PostUserDTO model, CancellationToken token = default)
    {
        var user = _mapper.Map<User>(model);
        await _context.AddAsync(user, token);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUserAsync(int id, PostUserDTO model, CancellationToken token = default)
    {
        var src = _mapper.Map<User>(model);

        var dest = await _context.Set<User>().SingleOrDefaultAsync(u => u.Id.Equals(id), token);

        // The user existing should be an invariant, but just in case...
        ArgumentNullException.ThrowIfNull(dest);

        _mapper.Map(src, dest);
        await _context.SaveChangesAsync(token);

        return dest;
    }

    public async Task<bool> DeleteUserAsync(int id, CancellationToken token = default)
    {
        var user = await _context.Set<User>().SingleOrDefaultAsync(u => u.Id.Equals(id), token);

        ArgumentNullException.ThrowIfNull(user);

        try
        {
            _context.Remove(user);
            await _context.SaveChangesAsync(token);

            return true;
        }
        catch (DbException)
        {
            return false;
        }
    }
}
