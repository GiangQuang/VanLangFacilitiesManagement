using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Core.Response;
using VLFM.Services;
using VLFM.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VLFM.Controllers
{
    [Route("api/user")]
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

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            var user = await _userService.LoginUser(request.Username, request.Password);

            if (user != null)
            {
                var token = _jwtService.GenerateJwtToken(request.Username);
                return Ok(new {
                    username = user.Username,
                    password = user.Password,
                    token = token,
                    status = "ok"
                });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("currentUser")]
        public async Task<IActionResult> GetCurrentUsers()
        {
            var current = new CurrentUserResponse
            {
                name = "admin",
                avatar = "https://gw.alipayobjects.com/zos/rmsportal/KDpgvguMpGfqaHPjicRK.svg",
                email = "quang@gmail.com"
            };

            return Ok(await Task.FromResult(current));
        }


        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var userDetailsList = await _userService.GetAllUsers();
            if (userDetailsList == null)
            {
                return NotFound();
            }
            string EmployeeID = HttpContext.Request.Query.ContainsKey("EmployeeID") ? HttpContext.Request.Query["EmployeeID"].ToString() : null;
            string Username = HttpContext.Request.Query.ContainsKey("Username") ? HttpContext.Request.Query["Username"].ToString() : null;
            string Status = HttpContext.Request.Query.ContainsKey("Status") ? HttpContext.Request.Query["Status"].ToString() : null;

            if (!string.IsNullOrEmpty(EmployeeID))
            {
                userDetailsList = userDetailsList.Where(u => u.EmployeeID.ToString().Contains(EmployeeID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Username))
            {
                userDetailsList = userDetailsList.Where(u => u.Username.Contains(Username, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Status))
            {
                int parsedStatus;
                if (int.TryParse(Status, out parsedStatus))
                {
                    userDetailsList = userDetailsList.Where(u => u.Status == parsedStatus).ToList();
                }
            }
            var responseData = new
            {
                data = userDetailsList,
            };

            return Ok(responseData);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            var userDetails = await _userService.GetUserById(Id);

            if (userDetails != null)
            {
                var responseData = new { data = userDetails };
                return Ok(responseData);
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

        [HttpPost("{Id}")]
        public async Task<IActionResult> UpdateUser(UserDetails userDetails)
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

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(List<UserResponse> users)
        {
            var isUserCreated = await _userService.DeleteUser(users);

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
