namespace ToDoList_MVC.Models;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Color { get; set; } = "#C8C6C4";
    public string UserId { get; set; }
}