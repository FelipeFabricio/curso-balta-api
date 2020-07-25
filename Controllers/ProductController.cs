using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_v3._1.Data;
using Shop_v3._1.Models;

namespace Shop_v3._1.Controllers                    
{
    [Route("v1/products")]
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        [ResponseCache(VaryByHeader ="User-Agent", Location=ResponseCacheLocation.Any, Duration = 30)]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _context
                .Products
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();

            if (!products.Any())
                return Ok(new { message = "Não existem produtos cadastrados até o momento." });

            return products;
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategories(int id)
        {
            var products = await _context
                .Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.Category.Id == id)
                .ToListAsync();

            if (!products.Any())
                return Ok(new { message = "Categoria não encontrada." });

            return products;
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Não foi possível cadastrar o produto :(" });

            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível cadastrar o produto." });
            }
        }
    }
}
