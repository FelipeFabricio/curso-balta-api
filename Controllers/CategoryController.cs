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
    [Route("v1/categories")]
    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();

            if (!categories.Any())
                return Ok(new { message = "Não existem categorias cadastradas até o momento." });

            return categories;
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var categorie = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if (categorie == null)
                return Ok(new { message = "Categoria não encontrada." });

            return categorie;
        }

        [HttpPost]
        [Authorize(Roles = "employee")]
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]  *Retirar uma action Específica do Cache
        public async Task<ActionResult<Category>> Post([FromBody] Category model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                _context.Add(model);
                await _context.SaveChangesAsync();

                return model;
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar a categoria solicitada." });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Category>> Put(int id, [FromBody] Category model)
        {
            if (id != model.Id) return NotFound(new { message = "Categoria não localizada. Favor tentar novamente."});

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                //Avisa ao EF que o estado do modelo está como modificado. 
                //Não precisa verificar propriedade por propriedade.
                _context.Entry(model).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return model;
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Esse registro já foi editado." });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível editar a categoria solicitada." });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

                if (category == null)
                    return NotFound(new { message = "Categoria não encontrada!" });

                _context.Remove(category);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Categoria deletada com sucesso!" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível deletar a categoria solicitada." });
            }


        }
    }
}
