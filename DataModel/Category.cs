using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataModel;

[Table("Category")]
public partial class Category
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(50)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("userId")]
    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Categories")]
    public virtual AppUser User { get; set; } = null!;
}
