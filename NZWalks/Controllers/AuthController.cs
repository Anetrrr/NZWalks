using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;
using NZWalks.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {

            var user = await _userManager.FindByEmailAsync(registerRequestDTO.Username);
            if (user != null)
            {
                return BadRequest("User already exists");

            }

                var identityUser = new IdentityUser
                {
                    UserName = registerRequestDTO.Username,
                    Email = registerRequestDTO.Username
                };


                var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);

                //add roles

                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {

                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered. Please login.");
                    }

                }
            
            return BadRequest("Something went wrong");
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);

            if (user != null)
            {

                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if (checkPasswordResult)
                {

                    //get roles
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {

                        //create token

                        var jwt = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwt
                        };

                        return Ok(response);
                    }

                }
            }

            return BadRequest("Username or password incorrect");    

               

        }
    }

}
