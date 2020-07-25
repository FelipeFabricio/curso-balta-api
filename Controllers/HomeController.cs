using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_v3._1.Data;
using Shop_v3._1.Models;

namespace Shop_v3._1.Controllers
{
    [Route("v1")]
    public class HomeController : Controller
    {
        [HttpGet]
        [AllowAnonymous]

        public async Task<ActionResult<dynamic>> Get([FromServices] DataContext context)
        {
            var empregado = new User { Id = 1, Username = "Madruga", Password = "senha123", Role = "employee" };
            var chefe = new User { Id = 2, Username = "Seu Barriga", Password = "senha123", Role = "manager" };
            var categoria = new Category { Id = 1, Title = "Primeira categoria" };
            var produto = new Product
            {
                Id = 1,
                Title = "Produto teste",
                Description = "Novo produto",
                Category = categoria,
                CategoryId = categoria.Id,
                Price = 299
            };

            context.Users.Add(empregado);
            context.Users.Add(chefe);
            context.Categories.Add(categoria);
            context.Products.Add(produto);
            await context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Dados Configurados"
            });
        }
    }
}
