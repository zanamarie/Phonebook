using FluentValidation;
using Phonebook.ViewModels;

namespace Phonebook.Models
{
    public class PhoneViewModelValidation : AbstractValidator<PhoneViewModel>
    {
        public PhoneViewModelValidation()
        {
            RuleFor(phone => phone.PhoneNumber).Length(0, 50).WithMessage("Phone number too long, max is 50 characters.");
        }
    }
}
