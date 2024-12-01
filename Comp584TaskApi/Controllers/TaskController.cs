using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using DataModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using comp584webapi.DTO;
using Microsoft.AspNetCore.Authorization;
using Comp584TaskApi.DTO;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

namespace Comp584TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(TaskdbContext context) : ControllerBase
    {
        private readonly TaskdbContext _context = context;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<TaskDTO>>> GetTasks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            { // If the user ID is not found,
                return Unauthorized("User ID not found.");
            }
            IQueryable<TaskDTO> x = _context.TaskObjects.Where(x => x.UserId == userId).Select(t => new TaskDTO
            {
                Id = t.Id,
                Complete = t.Complete,
                Body = t.Body 
            }) .Take(100);
            return await x.ToListAsync();
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<DataModel.TaskObject>> GetTask(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            { // If the user ID is not found,
                return Unauthorized("User ID not found.");
            }
            DataModel.TaskObject? task = await _context.TaskObjects.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            if (task.UserId != userId)
            {
                return Unauthorized();
            }

            return task;
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<DataModel.TaskObject>> DeleteTask(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            { // If the user ID is not found,
                return Unauthorized("User ID not found.");
            }
            DataModel.TaskObject? task = await _context.TaskObjects.FindAsync(id);
            

            if (task == null)
            {
                return NotFound();
            }
            if (task.UserId != userId)
            {
                return Unauthorized();
            }

            _context.TaskObjects.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent(); ;
        }






        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TaskRequest>> PostTask(TaskRequest task)
        {
            // Retrieve the user ID of the currently authenticated user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
        
            if (userId == null)
            { // If the user ID is not found,
              return Unauthorized("User ID not found."); 
            }
              // Create a new TaskObject and assign the user ID
                var taskObject = new TaskObject { UserId = userId, Body = task.Body, 
            // Add other properties as necessary
            }; 
            // Add the new TaskObject to the context
            _context.TaskObjects.Add(taskObject); 
            await _context.SaveChangesAsync(); 
            return CreatedAtAction("GetTask", new { id = taskObject.Id }, taskObject);
        }
        [Route("/status")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetStatus()
        {
            // Retrieve the user ID of the currently authenticated user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(userId);
            Console.WriteLine("Hello World");
            Log.Information("Hello World");
            Log.Information(User.Identity.GetUserId());
            //Console.WriteLine(userId);
            return Ok();


        }

        
        
        [HttpGet("/toggle/{id}")]
        [Authorize]
        public async Task<ActionResult<DataModel.TaskObject>> ToggleTaskComplete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DataModel.TaskObject? task = await _context.TaskObjects.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            if (task.UserId != userId)
            {
                return Unauthorized();
            }
            task.Complete = !task.Complete;
            _context.SaveChanges();
            return task;
        }



    }
}
