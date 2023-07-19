using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;

namespace Server.Controllers
{
    //routing for the api to display the url
    [Route("api/[controller]")]
    //To tell the program it is an api controller which uses http reltated requests
    [ApiController]

    public class CategoriesController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;

        public CategoriesController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        #region CRUD Operations

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            List<Category> categories = await _appDBContext.Categories.ToListAsync();

            return Ok(categories);
        }

        //website.com/api/categories/withposts

        [HttpGet("withposts")]
         public async Task<IActionResult> GetWithPosts()
        {
            List<Category> categories = await _appDBContext.Categories
                .Include(category => category.Posts)
                .ToListAsync();

            return Ok(categories);
        }

        //website.com/api/categories/id (1,2,3,4 etc)
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Category category = await GetCategoryByCategoryId(id, false);

            return Ok(category);
        }

        //website.com/api/categories/id (1,2,3,4 etc)
        [HttpGet("withposts/{id}")]
        public async Task<IActionResult> GetWithPosts(int id)
        {
            Category category = await GetCategoryByCategoryId(id, true);

            return Ok(category);
        }
        #endregion

        //Utility Methods
        #region Utility methods

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> PersistChangesToDatabase()
        {
            int amountOfChanges = await _appDBContext.SaveChangesAsync();

            return amountOfChanges > 0;
        }
        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Category> GetCategoryByCategoryId(int categoryId, bool withPosts)
        {
            Category categoryToGet = null;

            if (withPosts == true)
            {
                categoryToGet = await _appDBContext.Categories
                    .Include(category => category.Posts)
                    .FirstAsync(category => category.CategoryId == categoryId);
            }
            else
            {
                categoryToGet = await _appDBContext.Categories
                    .FirstAsync(category => category.CategoryId == categoryId);
            }

            return categoryToGet;

            

        }

        #endregion
    }
}
