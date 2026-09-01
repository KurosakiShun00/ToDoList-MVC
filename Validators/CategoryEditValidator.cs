using FluentValidation;
using ToDoList_MVC.ViewModels.Category;

namespace ToDoList_MVC.Validators;

public class CategoryEditValidator : AbstractValidator<CategoryEditViewModel>
{
    public CategoryEditValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Campo Richiesto")
            .MaximumLength(50).WithMessage("Massimo 50 caratteri");

    }
}