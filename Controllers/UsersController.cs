using BookNest.Dtos;
using BookNest.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] InsertUserDto dto)
        {
            try
            {
                await _userService.AddUser(dto);
                return Ok(new { message = "User added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.ValidateUserAsync(dto);

            if (user != null)
            {
                return Ok(new
                {
                    message = "Login successful.",
                    role = user.Role 
                });
            }

            return Unauthorized(new { error = "Invalid credentials." });
        }


        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
        {
            _userService.UpdateUser(id, dto);
            return Ok("User updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            _userService.DeleteUser(id);
            return Ok("User deleted successfully.");
        }
    }
}
