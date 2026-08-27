using ToDoList_MVC.ViewModels.Category;

namespace ToDoList_MVC.Models;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Color { get; set; } = "#C8C6C4";
    public string UserId { get; set; } = string.Empty;

    public Category(){}
    public Category(CategoryCreateViewModel viewModel)
    {
        Name = viewModel.Name;
        Color = viewModel.Color;
    }   
    
    public Category(CategoryEditViewModel viewModel)
    {
        Name = viewModel.Name;
        Color = viewModel.Color;
    }
}