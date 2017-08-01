using FluentValidation;
using Phonebook.ViewModels;

namespace Phonebook.Models
{
    public class EmailViewModelValidation : AbstractValidator<EmailViewModel>
    {
        public EmailViewModelValidation()
        {
            RuleFor(email => email.EmailAddress).Length(0, 254).WithMessage("Email too long, max is 254 characters.");
            RuleFor(email => email.EmailAddress).EmailAddress().WithMessage("Email format is invalid.").Unless(email => string.IsNullOrEmpty(email.EmailAddress));
        }
    }
}
