using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList_MVC.Models
{
    public class ToDoList
    { 
        public int Id { get; set; }
        public string? Name { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser? User { get; set; }
        
        public List<ToDo> ToDos { get; set; } = new();
    }
}
