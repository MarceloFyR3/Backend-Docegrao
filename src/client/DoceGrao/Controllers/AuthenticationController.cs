using System;
using System.Linq;
using DoceGrao.Api.Domain.Models.ValueObjects;
using DoceGrao.Api.Domain.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace DoceGrao.Api.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        //[AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(Credential credencial)
        {
            try
            {

                var credencialValidate = new CredentialValidator();
                var result = credencialValidate.Validate(credencial);
                if (!result.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        mensage = result.Errors.Select(e => e.ErrorMessage).ToList()
                    });
                }
                
                var user = _userService.Authenticate(credencial);

                if (!user.Sucesso)
                    return BadRequest(user);

                return Ok(new
                {
                    user.Data.Id,
                    Username = user.Data.Name,
                    user.Data.Credential.Login,
                    user.Data.Email.Address
                });
            }
            catch (ArgumentException e)
            {
                return Ok(new
                {
                    sucesso = false,
                    message = e.Message
                });
            }
        }
    }
}
