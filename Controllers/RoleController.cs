using System.Threading.RateLimiting;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoBlogApi.Extensions;
using ProjetoBlogApi.ViewModel;

namespace ProjetoBlogApi.Controller;

[ApiController]
public class RoleController : ControllerBase
{
    [HttpGet("v1/role")]    
    public async Task<IActionResult> GetRoleAsync([FromServices] BlogDataContext context)
    {
        try
        {
            var role = await context.Roles.ToListAsync();

            if(role.IsNullOrEmpty())
                return NotFound(new ResultViewModel<Role>("Perfis não encontrados"));
            
            return Ok(new ResultViewModel<List<Role>>(role));

        }
        catch
        {
            
            return StatusCode(500, new ResultViewModel<Role>("0899 - erro interno no servidor"));
        }
    }

    [HttpGet("v1/role/{id:int}")]
    public async Task<IActionResult> GetRoleByIdAsync([FromRoute]int id, [FromServices]BlogDataContext context)
    {
        try
        {
            var role = await context.Roles.FirstOrDefaultAsync(x=> x.Id == id);

            if(role is null)
                return NotFound(new ResultViewModel<Role>("Perfis não encontrados"));

            return Ok(new ResultViewModel<Role>(role));
        }
        catch 
        {
            
            return StatusCode(500, new ResultViewModel<Role>("0899 - erro interno no servidor"));
        }
    }

    [HttpPost("v1/role")]
    public async Task<IActionResult> PostRole([FromBody] EditorRoleViewModel model, [FromServices]BlogDataContext context)
    {
        try
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Role>(ModelState.GetErrors()));  
            
            var role =  new Role
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug                
            };

            await context.AddAsync(role);
            await context.SaveChangesAsync();

            return Created($"v1/role/{role.Id}", new ResultViewModel<Role>(role));
        }
        catch (DbUpdateException)
        {
            
            return StatusCode(500, new ResultViewModel<Role>("0X669 - não foi possível criar um novo usuário"));
        }

        catch(Exception)
        {
            return StatusCode(500, new ResultViewModel<Role>("08WS - erro interno no servidor"));
        }
    }

    [HttpPut("v1/role/{id:int}")]
    public async Task<IActionResult> PutRoleAsync
    ([FromRoute] int id, [FromBody]EditorRoleViewModel model, [FromServices]BlogDataContext context)
    {
        try
        {
            var role = await context.Roles.FirstOrDefaultAsync(x=> x.Id == id);

            if(role is null)
            {
                return NotFound(new ResultViewModel<Role>("Perfis não encontrados"));
            }

            context.Roles.Update(role);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Role>(role));
        }
        catch (DbUpdateException)
        {
            
            return StatusCode(500, new ResultViewModel<Category>("05XE8 - Não foi possível alterar a categoria"));
        }

         catch(Exception)
        {
            return StatusCode(500, new ResultViewModel<Category>("05X11 - Falha interna no servidor"));
        }
    }

    [HttpDelete("v1/role/{id:int}")]    
        public async Task<IActionResult> DeleteAsync([FromRoute]int id, 
        [FromBody]EditorPostViewModel model, [FromServices]BlogDataContext context)
        {
            try
            {
                var role = await context.Posts
                    .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.Id == id);

                if(role is null)
                    return NotFound(new ResultViewModel<Role>("Post não encontrado"));
                
                context.Posts.Remove(role);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Post>(role));
            }
            catch (DbUpdateException)
            {
                
                return StatusCode(500, new ResultViewModel<Category>("05XE7 - Não foi possível excluir a categoria"));
            }

            catch(Exception)
            {
                return StatusCode(500, new ResultViewModel<Category>("05X12 - Falha interna no servidor"));
            }
        }    

}