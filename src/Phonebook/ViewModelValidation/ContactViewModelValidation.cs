using FluentValidation;
using Phonebook.ViewModels;
using System.Linq;

namespace Phonebook.Models
{
    public class ContactViewModelValidation : AbstractValidator<ContactViewModel>
    {
        public ContactViewModelValidation()
        {
            RuleFor(contact => contact.ContactName).NotEmpty().WithMessage("Contact name is a required field");
            RuleFor(contact => contact.ContactName).Length(0,100).WithMessage("Contact name too long, max is 100 characters.");
            RuleFor(contact => contact.Address).Length(0, 100).WithMessage("Address name too long, max is 100 characters.");
            RuleFor(contact => contact.City).Length(0, 100).WithMessage("City name too long, max is 100 characters.");
            RuleFor(contact => contact.ZipCode).Length(0, 20).WithMessage("Zip code too long, max is 20 characters.");
            RuleFor(contact => contact.AdditionalInfo).Length(0, 500).WithMessage("Additional info is too long, max is 100 characters.");
            RuleFor(customer => customer.PhoneNumbers).SetCollectionValidator(new PhoneViewModelValidation());
            RuleFor(customer => customer.Tags).SetCollectionValidator(new TagViewModelValidation());
            RuleFor(customer => customer.Emails).SetCollectionValidator(new EmailViewModelValidation());


            RuleFor(contact => contact.PhoneNumbers)
                .Must(phoneViewModel => !phoneViewModel.GroupBy(phone => phone.PhoneNumber).Any(x => x.Count() > 1))
                .WithMessage("One or more phone items are duplicates");

            RuleFor(contact => contact.Tags)
                .Must(tagViewModel => !tagViewModel.GroupBy(tag => tag.TagName).Any(x => x.Count() > 1))
                .WithMessage("One or more tag items are duplicates");

            RuleFor(contact => contact.Emails)
                .Must(emailViewModel => !emailViewModel.GroupBy(email => email.EmailAddress).Any(x => x.Count() > 1))
                .WithMessage("One or more email items are duplicates");
        }
    }
}