using FluentValidation;
using ToDoList_MVC.ViewModels;
using ToDoList_MVC.ViewModels.ToDoList;

namespace ToDoList_MVC.Validators;

public class ToDoListsEditValidator : AbstractValidator<ToDoListsEditViewModel>
{
    public  ToDoListsEditValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome Richiesto").MaximumLength(20)
            .WithMessage("Massimo 20 caratteri");
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Massimo 500 caratteri");

    }
}