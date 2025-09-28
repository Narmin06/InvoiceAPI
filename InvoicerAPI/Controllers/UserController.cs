using InvoicerAPI.DTOs.UserDTOs;
using InvoicerAPI.Models;
using InvoicerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoicerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> Create([FromBody] CreateUserDTO userdto)
    {
        var createdUser = await _service.CreateUser(userdto);
        return Ok(createdUser);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
    {
        var user = await _service.GetUserById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
    {
        var users = await _service.GetAllUsers();
        return Ok(users);
    }
}
