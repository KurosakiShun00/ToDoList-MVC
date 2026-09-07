using System.Drawing;

namespace ToDoList_MVC.ViewModels.Category;

public class CategoryDetailsViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Color { get; set; } = "#C8C6C4";
    public int ToDoCompleted { get; set; }
    public int ToDoNotCompleted { get; set; }
    
    private Color _color => System.Drawing.ColorTranslator.FromHtml(Color);
    
    private float brightness => _color.GetBrightness();
    
    public bool isDark  => brightness > 0.5 ? false : true;     
    
    public int ToDoTotal => ToDoCompleted + ToDoNotCompleted;
}