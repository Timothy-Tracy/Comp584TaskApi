using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<TaskObject> TaskObjects { get; set; } = new List<TaskObject>();
    }
}
