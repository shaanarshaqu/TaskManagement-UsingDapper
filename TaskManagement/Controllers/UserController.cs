using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data.DTO;
using TaskManagement.Manager.Interface;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersManager usersManager;
        public UserController(IUsersManager usersManager) 
        {
            this.usersManager = usersManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserLoginDto userLogin)
        {
            try
            {
                if(!ModelState.IsValid)return BadRequest(ModelState);
                var token = await usersManager.LoginUser(userLogin);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UsersDto userDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                bool isRegistered = await usersManager.Register(userDto);
                return isRegistered ? Ok(isRegistered) : BadRequest(isRegistered);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
