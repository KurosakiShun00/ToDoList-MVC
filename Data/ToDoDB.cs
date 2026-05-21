using Microsoft.EntityFrameworkCore;
using ToDoList_MVC.Models;

namespace ToDoList_MVC.Data
{
    public class ToDoDB : DbContext
    {
        public ToDoDB(DbContextOptions<ToDoDB> options) : base(options) { }

        public DbSet<ToDo>  toDos => Set<ToDo>();
        public DbSet<ToDoList> toDosLists => Set<ToDoList>();
    }
}
