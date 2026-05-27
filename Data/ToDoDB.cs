using Microsoft.EntityFrameworkCore;
using ToDoList_MVC.Models;

namespace ToDoList_MVC.Data
{
    public class ToDoDB : DbContext
    {
        public ToDoDB(DbContextOptions<ToDoDB> options) : base(options) { }

        public DbSet<ToDo>  ToDos => Set<ToDo>();
        public DbSet<ToDoList> ToDosLists => Set<ToDoList>();
    }
}
