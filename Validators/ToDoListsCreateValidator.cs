using FluentValidation;
using ToDoList_MVC.ViewModels.ToDoList;

namespace ToDoList_MVC.Validators;

public class ToDoListsCreateValidator : AbstractValidator<ToDoListsCreateViewModel>
{
    public ToDoListsCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome Richiesto").MaximumLength(30)
            .WithMessage("Massimo 30 caratteri");
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Massimo 500 caratteri");
    }
}