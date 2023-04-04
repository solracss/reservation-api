﻿using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Domain;
using ReservationAPI.Dtos;
using ReservationAPI.Services;

namespace ReservationAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            await accountService.RegisterUserAsync(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            string token = await accountService.GenerateToken(dto);
            return Ok(token);
        }
    }
}
