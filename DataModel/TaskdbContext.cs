using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataModel;

public partial class TaskdbContext : DbContext
{
    public TaskdbContext()
    {
    }

    public TaskdbContext(DbContextOptions<TaskdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=taskdb;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
