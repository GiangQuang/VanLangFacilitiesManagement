using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        public UsersController(IUserService userService, IJwtService jwtService) 
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            var user = await _userService.LoginUser(request.Username, request.Password);

            if (user != null)
            {
                var token = _jwtService.GenerateJwtToken(request.Username);
                return Ok(new {Token = token});
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var userDetailsList = await _userService.GetAllUsers();
            if (userDetailsList == null)
            {
                return NotFound();
            }
            return Ok(userDetailsList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            var userDetails = await _userService.GetUserById(Id);

            if (userDetails != null)
            {
                return Ok(userDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDetails userDetails)
        {
            var isUserCreated = await _userService.CreateUser(userDetails);

            if (isUserCreated)
            {
                return Ok(isUserCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UserDetails userDetails)
        {
            if (userDetails != null)
            {
                var isUserCreated = await _userService.UpdateUser(userDetails);
                if (isUserCreated)
                {
                    return Ok(isUserCreated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var isUserCreated = await _userService.DeleteUser(Id);

            if (isUserCreated)
            {
                return Ok(isUserCreated);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
