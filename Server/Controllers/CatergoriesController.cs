using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;

namespace Server.Controllers
{
    //routing for the api to display the url
    [Route("api/[Controller]")]
    //To tell the program it is an api controller which uses http reltated requests
    [ApiController]

    public class CategoriesController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;

        public CategoriesController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            List<Category> categories = await _appDBContext.Categories.ToListAsync();

            return Ok(categories);
        }
    }
}
