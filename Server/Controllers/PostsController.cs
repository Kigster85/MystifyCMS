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

    public class PostsController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostsController(AppDBContext appDBContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDBContext = appDBContext;
            _webHostEnvironment = webHostEnvironment;
        }

        #region CRUD Operations

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            List<Post> posts = await _appDBContext.Posts.ToListAsync();

            return Ok(posts);
        }

        //website.com/api/posts/withposts

        [HttpGet("withposts")]
        public async Task<IActionResult> GetWithPosts()
        {
            List<Post> posts = await _appDBContext.Posts
                .Include(post => post.Posts)
                .ToListAsync();

            return Ok(posts);
        }

        //website.com/api/posts/id (1,2,3,4 etc)
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Post post = await GetPostByPostId(id, false);

            return Ok(post);
        }

        //website.com/api/posts/id (1,2,3,4 etc)
        [HttpGet("withposts/{id}")]
        public async Task<IActionResult> GetWithPosts(int id)
        {
            Post post = await GetPostByPostId(id, true);

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Post postToCreate)
        {
            try
            {
                if (postToCreate == null)
                {
                    return BadRequest(ModelState);
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                await _appDBContext.Posts.AddAsync(postToCreate);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our side. Please Contact the Administrator.");
                }
                else
                {
                    return Created("Create", postToCreate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on our side. Please contact the administrator. Error message: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Post updatedPost)
        {
            try
            {
                if (id < 1 || updatedPost == null || id != updatedPost.PostId)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDBContext.Posts.AnyAsync(post => post.PostId == id);

                if (exists == false)
                {
                    return NotFound();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                _appDBContext.Posts.Update(updatedPost);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our side. Please Contact the Administrator.");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on our side. Please contact the administrator. Error message: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDBContext.Posts.AnyAsync(post => post.PostId == id);

                if (exists == false)
                {
                    return NotFound();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                Post postToDelete = await GetPostByPostId(id, false);

                if (postToDelete.ThumbnailImagePath != "uploads/placeholder.jpg")
                {

                    string filename = postToDelete.ThumbnailImagePath.Split('/').Last();

                    System.IO.File.Delete($"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{filename}");

                }
                _appDBContext.Posts.Remove(postToDelete);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our side. Please Contact the Administrator.");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on our side. Please contact the administrator. Error message: {e.Message}");
            }
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
        private async Task<Post> GetPostByPostId(int postId, bool withPosts)
        {
            Post postToGet = null;

            if (withPosts == true)
            {
                postToGet = await _appDBContext.Posts
                    .Include(post => post.Posts)
                    .FirstAsync(post => post.PostId == postId);
            }
            else
            {
                postToGet = await _appDBContext.Posts
                    .FirstAsync(post => post.PostId == postId);
            }

            return postToGet;



        }

        #endregion
    }
}
