using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGrao.Api.Domain.Services.User;
using DoceGrao.Api.Domain.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoceGrao.Api.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _userService.GetById(id));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UserUpdateViewModel form)
        {
            try
            {
                var validator = new UserUpdateViewModelValidator();
                var result = await validator.ValidateAsync(form);
                if (!result.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        mensage = result.Errors.Select(e => e.ErrorMessage).ToList()
                    });
                }

                return Ok(await _userService.Update(form));
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
