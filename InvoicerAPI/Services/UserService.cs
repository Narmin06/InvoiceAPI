using InvoicerAPI.Data;
using InvoicerAPI.DTOs.UserDTOs;
using InvoicerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoicerAPI.Services;

public class UserService : IUserService
{
    private readonly InvoicerContext _context;

    public UserService(InvoicerContext context)
    {
        _context = context;
    }
    public async Task<UserDTO> CreateUser(CreateUserDTO userDto)
    {
        var new_user = new User()
        {
            Name = userDto.Name,
            Address = userDto.Address,
            Email = userDto.Email,
            Password = userDto.Password,
            PhoneNumber = userDto.PhoneNumber,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };
        _context.Users.Add(new_user);
        await _context.SaveChangesAsync();
        return ConvertUserDTO(new_user);
    }

    public async Task<UserDTO> GetUserById(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;
        return ConvertUserDTO(user);
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users.Select(ConvertUserDTO);
    }

    private static UserDTO ConvertUserDTO(User user)
    {
        UserDTO userDTO = new()
        {
            Id = user.Id,
            Name = user.Name,
            Address = user.Address,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };
        return userDTO;
    }
}
