using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Models;
using SimpleBlog.Repositories;

namespace SimpleBlog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userService.GetUserAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDto newUserDto)
    {
        User createdUser = await _userService.CreateUserAsync(newUserDto);

        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, _mapper.Map<UserDto>(createdUser));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserDto updatedUserDto)
    {
        User updatedUser = _mapper.Map<User>(updatedUserDto);
        try
        {
            await _userService.UpdateUserAsync(id, updatedUser);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}