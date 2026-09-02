using FluentValidation;
using NuGet.Packaging.Signing;
using ToDoList_MVC.ViewModels.ToDo;

namespace ToDoList_MVC.Validators;

public class ToDoCreateValidator : AbstractValidator<ToDoCreateViewModel>
{
    public ToDoCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Campo Richiesto")
            .MaximumLength(50).WithMessage("Massimo 50 caratteri");

        RuleFor(x => x.Deadline).GreaterThan(DateTime.Now).WithMessage("Inserire una data futura");
    }
}