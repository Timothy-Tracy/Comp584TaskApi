using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace comp584webapi.DTO
{
    public class TaskDTO
    {

        public int Id { get; set; }
        public string Body { get; set; } = null!;
        public bool Complete { get; set; }
       

    }
}