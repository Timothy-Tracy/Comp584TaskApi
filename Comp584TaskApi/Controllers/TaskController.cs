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

namespace Comp584TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(TaskdbContext context) : ControllerBase
    {
        private readonly TaskdbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IList<TaskDTO>>> GetCities()
        {
            IQueryable<TaskDTO> x = _context.Tasks.Select(t => new TaskDTO
            {
                Id = t.Id,
                Body = t.Body,
                Complete = t.Complete
                

            }).Take(100);
            return await x.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DataModel.Task>> GetCity(int id)
        {
            DataModel.Task? task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        [HttpPost]
        public async Task<ActionResult<DataModel.Task>> PostTask(DataModel.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }



    }
}
