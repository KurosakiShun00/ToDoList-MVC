using FluentValidation;
using ToDoList_MVC.ViewModels;

namespace ToDoList_MVC.Validators;

public class ToDoListsCreateValidator : AbstractValidator<ToDoListsCreateViewModel>
{
    public  ToDoListsCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome Richiesto").MaximumLength(20)
            .WithMessage("Massimo 20 caratteri");
    }
}