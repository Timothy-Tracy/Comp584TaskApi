using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataModel;

[Table("TaskObject")]
public partial class TaskObject
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("body")]
    [StringLength(50)]
    [Unicode(false)]
    public string Body { get; set; } = null!;

    [Column("complete")]
    public bool Complete { get; set; }

    [Column("userId")]
    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TaskObjects")]
    public virtual AppUser User { get; set; } = null!;
}
