using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoBlogApi.Extensions;
using ProjetoBlogApi.ViewModel;

namespace ProjetoBlogApi.Controller;

[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("v1/user")]
    public async Task<IActionResult> GetUserAsync([FromServices] BlogDataContext context)
    {
        try
        {
            var user = await context.Users.ToListAsync();

            if(user.IsNullOrEmpty())                            
                return NotFound("Conteúdo não encontrado");
            
            return Ok(new ResultViewModel<List<User>>(user));

        }
        catch
        {            
            return StatusCode(500, new ResultViewModel<Post>("05X04 - Falha interna no servidor"));
        }
    }

    [HttpGet("v1/user/{id:int}")]
    public async Task<IActionResult> GetUserById([FromRoute]int id, [FromServices]BlogDataContext context)
    {
        try
        {
            var user = await context.Categories.FirstOrDefaultAsync(x=> x.Id == id);

            if(user is null)
                return NotFound(new ResultViewModel<Category>("Contéudo não encontrado"));

            return Ok(new ResultViewModel<Category>(user));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
        }
    }

    [HttpPost("v1/user")]
    public async Task<IActionResult> PostUserAsync([FromBody] EditorUserViewModel model, [FromServices] BlogDataContext context)
    {
        if(!ModelState.IsValid)
            return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));
        
        try
        {
            var user = new User
            {
                Id = 0,
                Name = model.Name,
                Email = model.Email,
                PasswordHash = model.PasswordHash
            };

            await context.AddAsync(user);
            await context.SaveChangesAsync();

            return Created($"v1/user/{user.Id}", new ResultViewModel<User>(user));
        }
        catch (DbUpdateException)
        {
            
            return StatusCode(500, new ResultViewModel<User>("05XE9 - Não foi possível incluir um novo usuário"));
        }

        catch(Exception)
        {
            return StatusCode(500, new ResultViewModel<User>("05X20 - Erro interno no servidor"));
        }
    }

    [HttpPut("v1/user/{id:int}")]
    public async Task<IActionResult>PutUserAsync([FromRoute] int id, [FromServices]BlogDataContext context)
    {
        try
        {
            var user = await context.Users
                .AsNoTracking()
            .FirstOrDefaultAsync(x=> x.Id == id);

            if(user is null)
                return NotFound(new ResultViewModel<User>("Conteúdo não encontrado"));
            
            context.Users.Update(user);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<User>(user));

        }
        catch (DbUpdateException)
        {

            return StatusCode(500, new ResultViewModel<User>("0845XY - Não foi possível a deleção do usuário"));
        }

        catch (Exception)
        {
            
            return StatusCode(500, new ResultViewModel<User>("500XYZ - Erro interno no servidor"));
        }

    }

    [HttpDelete("v1/user/{id:int}")]
    public async Task<IActionResult> DeleteUserAsync([FromRoute]int id, [FromServices]BlogDataContext context)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x=> x.Id == id);

            if(user is null)
                return NotFound(new ResultViewModel<User>("Conteúdo não encontrado"));

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<User>(user));
        }
        catch (DbUpdateException)
        {
            
            return StatusCode(500, new ResultViewModel<User>("0845XY - Não foi possível a deleção do usuário"));
        }

        catch (Exception)
        {
            
            return StatusCode(500, new ResultViewModel<User>("500XYZ - Erro interno no servidor"));
        }
    }

}