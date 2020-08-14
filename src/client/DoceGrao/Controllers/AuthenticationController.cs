using System;
using System.Linq;
using System.Threading.Tasks;
using DoceGrao.Api.Domain.Models.ValueObjects;
using DoceGrao.Api.Domain.Services.User;
using DoceGrao.Api.Domain.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
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
                        mensage = result.Errors.SelectMany(e => e.ErrorMessage)
                    });
                }
                
                var user = _userService.Authenticate(credencial);

                if (!user.Sucesso)
                    return BadRequest(user.Message);

                return Ok(user);
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

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserCreateViewModel form)
        {
            try
            {
                var validator = new UserCreateViewModelValidator();
                var result = await validator.ValidateAsync(form);
                if (!result.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        mensage = result.Errors.Select(e => e.ErrorMessage).ToList()
                    });
                }

                return Ok(await _userService.Register(form));
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
