using Application.Interfaces;
using Contracts.Dto;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ReservationAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/account")]
    [ApiVersion("1.0")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IValidator<RegisterUserDto> registerValidator;
        private readonly IValidator<LoginDto> loginValidator;

        public AccountController(IAccountService accountService, IValidator<RegisterUserDto> registerValidator, IValidator<LoginDto> loginValidator)
        {
            this.accountService = accountService;
            this.registerValidator = registerValidator;
            this.loginValidator = loginValidator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            var validationResult = await registerValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Errors = errorList });
            }
            await accountService.RegisterUserAsync(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            var validationResult = loginValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }
            string token = await accountService.LoginAsync(dto);
            return Ok(token);
        }
    }
}
