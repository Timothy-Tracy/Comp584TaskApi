using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    public string? UserId { get; set; }

    [Column("categoryId")]
    public int? CategoryId { get; set; }
}
