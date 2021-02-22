using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using account.data;
using account.data.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace account.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UsersContext usersContext;

        public AccountController(UsersContext usersContext)
        {
            this.usersContext = usersContext;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser([FromQuery] string email)
        {
            User user = await usersContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return Ok(user);
        }

        [HttpPost("user")]
        public async Task<IActionResult> AddUser([FromBody] User model)
        {
            usersContext.Users.Add(model);
            await usersContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("user")]
        public async Task<IActionResult> UpdateUser([FromBody] User model)
        {
            User user = await usersContext.Users.FirstOrDefaultAsync(x => x.Id == model.Id);
            
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, User>();
            });
            Mapper mapper = new Mapper(config);
            mapper.Map(model, user);

            await usersContext.SaveChangesAsync();
            return Ok();
        }
    }
}
