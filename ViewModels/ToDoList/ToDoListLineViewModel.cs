using System.Drawing;

namespace ToDoList_MVC.ViewModels.ToDoList;

public class ToDoListLineViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? Deadline { get; set; }
    public string? CategoryName { get; set; }
    public int? CategoryId { get; set; }
    public string? LineColor { get; set; } =  "#C8C6C4";
    
    private Color _color => System.Drawing.ColorTranslator.FromHtml(LineColor ?? "#C8C6C4");
    
    private float brightness => _color.GetBrightness();
    
    public bool isDark  => brightness > 0.5 ? false : true;  
}