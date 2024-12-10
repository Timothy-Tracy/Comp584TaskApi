using Comp584TaskApi.DTO;
using comp584webapi.DTO;
using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Comp584TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(TaskdbContext context) : ControllerBase
    {
        private readonly TaskdbContext _context = context;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<CategoryDTO>>> GetCategories()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            { // If the user ID is not found,
                return Unauthorized("User ID not found.");
            }
            IQueryable<CategoryDTO> x = _context.Categories.Where(x => x.UserId == userId).Select(t => new CategoryDTO
            {
                Id = t.Id,
                Title = t.Title,
                
            }).Take(100);
            return await x.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            { // If the user ID is not found,
                return Unauthorized("User ID not found.");
            }
            Category? category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            if (category.UserId != userId)
            {
                return Unauthorized();
            }

            return category;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoryPost>> UpdateTask(int id, CategoryPost req)
        {
            // Retrieve the user ID of the currently authenticated user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            { // If the user ID is not found,
                return Unauthorized("User ID not found.");
            }

            Category? category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            if (category.UserId != userId)
            {
                return Unauthorized();
            }
            category.Title = req.Title;


            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTask", new { id = category.Id }, category);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            { // If the user ID is not found,
                return Unauthorized("User ID not found.");
            }
            Category? category = await _context.Categories.FindAsync(id);


            if (category == null)
            {
                return NotFound();
            }
            if (category.UserId != userId)
            {
                return Unauthorized();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent(); ;
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryPost>> PostCategory(CategoryPost category)
        {
            // Retrieve the user ID of the currently authenticated user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            { // If the user ID is not found,
                return Unauthorized("User ID not found.");
            }
            // Create a new TaskObject and assign the user ID
            var newCategory = new Category
            {
               
                Title = category.Title,
                AppUserId = userId,
                UserId = userId,
                // Add other properties as necessary
            };
            // Add the new TaskObject to the context
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCategory", new { id = newCategory.Id }, newCategory);
        }
    }
}
