using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            var user = await _userService.LoginUser(request.Username, request.Password);

            if (user != null)
            {
                // Đăng nhập thành công, trả về thông tin người dùng hoặc token JWT
                return Ok(user);
            }
            else
            {
                // Đăng nhập không thành công, trả về mã lỗi hoặc thông báo lỗi
                return BadRequest("Tên đăng nhập hoặc mật khẩu không chính xác");
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
                return BadRequest("Người dùng không tồn tại");
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
