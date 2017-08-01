using FluentValidation;
using Phonebook.ViewModels;

namespace Phonebook.Models
{
    public class TagViewModelValidation : AbstractValidator<TagViewModel>
    {
        public TagViewModelValidation()
        {
            RuleFor(tag => tag.TagName).Length(0, 100).WithMessage("Tag name too long, max is 100 characters.");
        }
    }
}
