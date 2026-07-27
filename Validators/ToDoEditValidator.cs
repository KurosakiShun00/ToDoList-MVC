using FluentValidation;
using ToDoList_MVC.ViewModels.ToDo;

namespace ToDoList_MVC.Validators;

public class ToDoEditValidator : AbstractValidator<ToDoEditViewModel>
{
    public ToDoEditValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Campo Richiesto")
            .MaximumLength(50).WithMessage("Massimo 50 caratteri");

    }
}