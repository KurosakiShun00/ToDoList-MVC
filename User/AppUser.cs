using Microsoft.AspNetCore.Identity;

namespace ToDoList_MVC.User;

public class AppUser : IdentityUser
{
    public string? Nome { get; set; } = string.Empty;
    public string? Cognome { get; set; } = string.Empty;
    public DateTime? DataDiNascita { get; set; } = new DateTime();
    public int? Sesso { get; set; } = 2; //0 maschio, 1 femmina, 2 Non Specificato 
    public string? NickName { get; set; } = string.Empty;
    
    
}