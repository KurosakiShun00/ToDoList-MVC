using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList_MVC.Models;

namespace ToDoList_MVC.Data
{
    public class ToDoDB : IdentityDbContext

    {
    public ToDoDB(DbContextOptions<ToDoDB> options) : base(options)
    {
    }

    public DbSet<ToDo> ToDos => Set<ToDo>();
    public DbSet<ToDoList> ToDosLists => Set<ToDoList>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ToDoList>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    }
}
