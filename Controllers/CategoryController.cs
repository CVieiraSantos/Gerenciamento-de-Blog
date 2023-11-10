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
    public class CategoryController : ControllerBase
    {
        /// <summary>
        /// Busca todas as categorias criadas que já se encontram populadas no banco de dados.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        ///<response code="200">Retorna as categorias criadas</response>        
        ///<response code="500">Retorna erro interno no servidor, podendo ser falta de internet, servidor fora, etc</response>    
        [HttpGet("v1/categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryAsyc([FromServices]BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();

                if(categories.IsNullOrEmpty())
                    return NotFound("Conteúdo não encontrado");

                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {                                
                return StatusCode(500, new ResultViewModel<List<Category>>("05X04 - Falha interna no servidor"));
            }
        }

        /// <summary>
        /// Busca uma categoria que já está criada através do seu Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetCategoryAsyncById([FromRoute]int id,[FromServices]BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.FirstOrDefaultAsync(x=> x.Id == id);

                if(categories is null)
                    return NotFound(new ResultViewModel<Category>("Contéudo não encontrado"));

                return Ok(new ResultViewModel<Category>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
            }
        }

        /// <summary>
        /// Cria uma categoria através da propriedade Name e Slug.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostCategoryAsync([FromBody]EditorCategoryViewModel model, [FromServices]BlogDataContext context)
        {

            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            try
            {
                var categories = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                    CreateDate = model.CreatedDate.ToUniversalTime()                                      
                };

                await context.Categories.AddAsync(categories);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{categories.Id}", new ResultViewModel<Category>(categories));
            }
            catch (DbUpdateException)
            {
                
                return StatusCode(500, new ResultViewModel<Category>("05XE9 - Não foi possível incluir a categoria"));
            }
        }

        /// <summary>
        /// Altera uma categoria através do Id da mesma.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPut("v1/category/{id:int}")]
        public async Task<IActionResult> PutCategoryAsync([FromRoute]int id, [FromBody]EditorCategoryViewModel model, [FromServices]BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories
                    .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.Id == id);

                if(categories is null)
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));
                

                categories.Name = model.Name;
                categories.Slug = model.Slug;

                context.Categories.Update(categories);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(categories));
                
            }
            catch (DbUpdateException)
            {                
                return StatusCode(500, new ResultViewModel<Category>("05XE8 - Não foi possível alterar a categoria"));
            }
             catch (Exception)
            {                
                return StatusCode(500, new ResultViewModel<Category>("05X11 - Falha interna no servidor"));
            }            
        }

        /// <summary>
        /// Deleta uma categoria específica através do Id da mesma.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpDelete("v1/category/{id:int}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute]int id, [FromServices]BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x=> x.Id == id);

                if(category is null)
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));

                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                
                return Ok(new ResultViewModel<Category>(category));

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