using System.Net;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoBlogApi.Extensions;
using ProjetoBlogApi.ViewModel;

namespace ProjetoBlogApi.Controller
{
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet("v1/post")]
        public async Task<IActionResult> GetPostAsync([FromServices]BlogDataContext context)
        {
            
            try
            {                
                var post = await context.Posts.ToListAsync();

                if(post is null || post.IsNullOrEmpty())
                    return NotFound("Conteúdo não encontrado");
                    
                return Ok(new ResultViewModel<List<Post>>(post));                
                
            }

            catch(WebException)
            {
                return NotFound("Conteúdo não encontrado");
            }

            catch
            {                
                return StatusCode(500, new ResultViewModel<List<Category>>("05X04 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/post/{id:int}")]
        public async Task<IActionResult> GetPostByIdAsync([FromRoute] int id, [FromServices]BlogDataContext context)
        {
            try
            {
                var post = await context.Posts.FirstOrDefaultAsync(x=> x.Id == id);

                if(post == null)
                    return NotFound(new ResultViewModel<Post>("Conteúdo não encontrado"));
                
                return Ok(new ResultViewModel<Post>(post));
            }
            catch
            {
                
                return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
            }
        }

        [HttpPost("v1/post")]
        public async Task<IActionResult> PostAsync([FromBody] EditorPostViewModel model,
        [FromServices] BlogDataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Post>(ModelState.GetErrors()));

            try
            {
                var post = new Post
                {
                    Id = 0,
                    Title = model.Title.ToUpper(),
                    Summary = model.Summary,
                    Body = model.Body,
                    Slug = model.Slug,
                    CreateDate = DateTime.Now.ToUniversalTime(),
                    LastUpdateDate = DateTime.Now.ToUniversalTime()
                };

                await context.Posts.AddAsync(post);
                await context.SaveChangesAsync();

                return Created($"v1/post{post.Id}", new ResultViewModel<Post>(post));

            }
            catch(DbUpdateException)
            {
                
                return StatusCode(500, new ResultViewModel<Category>("05XE9 - Não foi possível incluir o post"));
            }
        }

        [HttpPut("v1/post/{id:int}")]
        public async Task<IActionResult> PostPutAsync([FromRoute]int id, [FromBody]EditorPostViewModel model, [FromServices]BlogDataContext context)
        {
            try
            {
                var post = await context.Posts
                    .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.Id == id);

                if(post == null)
                    return NotFound(new ResultViewModel<Post>("Conteúdo não encontrado"));
                
                post.Title = model.Title;
                
                context.Posts.Update(post);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Post>(post));
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

        [HttpDelete("v1/post/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id, 
        [FromBody]EditorPostViewModel model, [FromServices]BlogDataContext context)
        {
            try
            {
                var post = await context.Posts
                    .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.Id == id);

                if(post is null)
                    return NotFound(new ResultViewModel<Post>("Post não encontrado"));
                
                context.Posts.Remove(post);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Post>(post));
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

    

}