namespace ToDoList_MVC.ViewModels.Shared;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public int? StatusCode { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}