using InvoicerAPI.DTOs.UserDTOs;

namespace InvoicerAPI.Services;

public interface IUserService
{
    Task<UserDTO> CreateUser(CreateUserDTO userDto);
    Task<UserDTO> GetUserById(Guid id);
    Task<IEnumerable<UserDTO>> GetAllUsers();
}
