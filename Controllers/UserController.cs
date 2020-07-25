using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_v3._1.Data;
using Shop_v3._1.Models;
using Shop_v3._1.Services;

namespace Shop_v3._1.Controllers
{
    [Route("v1/users")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Post(
            [FromServices] DataContext context,
            [FromBody] User model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();
                model.Password = "*******";
                return model;
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar o usuário." });
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authentication(
            [FromServices] DataContext context,
            [FromBody]User model )
        {
            var user = await context.Users
                .AsNoTracking()
                .Where(x => x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user == null) return NotFound(new { Message = "Usuário ou Senha Inválidos." });

            var token = TokenService.GenerateToken(user);
            user.Password = "*********";
            return new
            {
                user = user,
                token = token
            };
        }

    }
}
