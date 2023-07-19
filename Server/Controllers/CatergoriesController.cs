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
