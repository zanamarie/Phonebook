using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Phonebook.ViewModels;

namespace Phonebook.Models
{
    public class PhonebookRepository : IPhonebookRepository
    {
        private PhonebookContext _context;

        public PhonebookRepository(PhonebookContext context)
        {
            _context = context;
        }

        public bool AddContact(ContactViewModel newContact)
        {
            RemoveEmptyEmailsFromContact(newContact);
            RemoveEmptyPhonesFromContact(newContact);
            RemoveEmptyTagsFromContact(newContact);

            var newContactModel = Mapper.Map<Contact>(newContact);

            foreach (var tag in newContactModel.ContactTags)
            {
                var currentTag = _context.Tags.Where(t => t.TagName == tag.Tag.TagName).FirstOrDefault();
                if (currentTag != null)
                    tag.Tag = currentTag;
            }

            _context.Add(newContactModel);

            if (_context.SaveChanges() > 0)
            {
                newContact.ContactId = newContactModel.ContactId;
                return true;
            }

            return false;
        }

        private void AddEmailToContact(EmailViewModel newEmail, int contactId)
        {
            var newEmailModel = Mapper.Map<Email>(newEmail);
            newEmailModel.ContactId = contactId;
            _context.Emails.Add(newEmailModel);
        }

        private void AddPhoneToContact(PhoneViewModel newPhone, int contactId)
        {
            var newPhoneModel = Mapper.Map<Phone>(newPhone);
            newPhoneModel.ContactId = contactId;
            _context.Phones.Add(newPhoneModel);
        }

        private void AddTagToContact(TagViewModel newTag, Contact contact)
        {
            var contactTag = Mapper.Map<ContactTag>(newTag);
            contactTag.Contact = contact;
            contact.ContactTags.Add(contactTag);
        }

        public IEnumerable<ContactViewModel> GetContactsBySearchText(string searchText)
        {
            IEnumerable<Contact> searchResults;
            if (String.IsNullOrEmpty(searchText))
            {
                searchResults = _context.Contacts
                .Include(c => c.Emails)
                .Include(c => c.PhoneNumbers)
                .Include(c => c.ContactTags)
                .ThenInclude(ct => ct.Tag)
                .ToArray();
            }
            else
            {
                searchResults = _context.Contacts
                .Include(c => c.Emails)
                .Include(c => c.PhoneNumbers)
                .Include(c => c.ContactTags)
                .ThenInclude(ct => ct.Tag)
                .Where(x => x.ContactName.Contains(searchText) ||
                        x.ContactTags.Any(z => z.Tag.TagName.Contains(searchText)))
                        .ToArray();
            }
            return (Mapper.Map<IEnumerable<ContactViewModel>>(searchResults));
        }

        public ContactViewModel GetContactById(int contactId)
        {
            var currentContact = GetContactEntity(contactId);
            return (Mapper.Map<ContactViewModel>(currentContact));
        }

        private Contact GetContactEntity(int contactId)
        {
                 return _context.Contacts
                .Include(contact => contact.Emails)
                .Include(contact => contact.PhoneNumbers)
                .Include(contact => contact.ContactTags)
                .ThenInclude(contactTag => contactTag.Tag)
                .Where(contact => contact.ContactId == contactId)
                .FirstOrDefault();
        }

        private Phone GetPhoneEntity(int phoneId)
        {
            return _context.Phones.Where(t => t.PhoneId == phoneId).FirstOrDefault();
        }

        private Email GetEmailEntity(int emailId)
        {
            return _context.Emails.Where(t => t.EmailId == emailId).FirstOrDefault();
        }

        private Tag GetTagEntity(int tagId)
        {
            return _context.Tags.Where(t => t.TagId == tagId).FirstOrDefault();
        }

        private bool CheckIfAnyContactHasTag(ContactTag tag)
        {
            var anyContactHasTag = _context.Tags.Where(x => x.TagContacts.Any(contactTag => contactTag.TagId == tag.TagId && contactTag.ContactId != tag.ContactId)).Any();
            if (anyContactHasTag)
                return true;
            return false;
        }

        public bool UpdateContact(ContactViewModel updatedContact)
        {
            var contact = GetContactEntity(updatedContact.ContactId);

            if (contact != null)
            {
                contact.ContactName = updatedContact.ContactName;
                contact.Address = updatedContact.Address;
                contact.City = updatedContact.City;
                contact.ZipCode = updatedContact.ZipCode;
                contact.AdditionalInfo = updatedContact.AdditionalInfo;

                //remove empty tags, phones, emails if there are any
                RemoveEmptyTagsFromContact(updatedContact);
                RemoveEmptyPhonesFromContact(updatedContact);
                RemoveEmptyEmailsFromContact(updatedContact);

                UpdateContactPhones(updatedContact.PhoneNumbers, contact);
                UpdateContactEmails(updatedContact.Emails, contact);
                UpdateContactTags(updatedContact.Tags, contact);

                _context.Contacts.Update(contact);
                if (_context.SaveChanges() > 0)
                    return true;
            }
            return false;
        }

        private void UpdateContactEmails(IEnumerable<EmailViewModel> emails, Contact contact)
        {
            foreach (var item in contact.Emails)
            {
                if (!emails.Where(x => x.EmailId == item.EmailId).Any())
                {
                    _context.Emails.Remove(item);
                }
            }
            foreach (var item in emails)
            {
                var email = GetEmailEntity(item.EmailId);
                if (email != null)
                    email.EmailAddress = item.EmailAddress;
                else
                    AddEmailToContact(item, contact.ContactId);
            }
        }

        private void UpdateContactPhones(IEnumerable<PhoneViewModel> phones, Contact contact)
        {
            foreach (var item in contact.PhoneNumbers)
            {
                if (!phones.Where(x => x.PhoneId == item.PhoneId).Any())
                {
                    _context.Phones.Remove(item);
                }
            }
            foreach (var item in phones)
            {
                var phone = GetPhoneEntity(item.PhoneId);
                if (phone != null)
                    phone.PhoneNumber = item.PhoneNumber;
                else
                    AddPhoneToContact(item, contact.ContactId);
            }
        }

        private void UpdateContactTags(IEnumerable<TagViewModel> tags, Contact contact)
        {
            foreach (var item in contact.ContactTags.ToArray())
            {
                if (!tags.Where(x => x.TagName == item.Tag.TagName).Any())
                {
                    if (!CheckIfAnyContactHasTag(item))
                        _context.Tags.Remove(item.Tag);
                    contact.ContactTags.Remove(item);
                }
            }

            foreach (var item in tags)
            {
                if(!contact.ContactTags.Any(x => x.Tag.TagName == item.TagName))
                {
                    var existingTag = _context.Tags.Where(x => x.TagName == item.TagName).FirstOrDefault();
                    item.TagId = 0;
                    if (existingTag == null)
                        AddTagToContact(item, contact);
                    else
                    {
                        var tag = Mapper.Map<ContactTag>(item);
                        tag.Tag = existingTag;
                        contact.ContactTags.Add(tag);
                    }
                }
            }
        }

        public bool DeleteContact(int contactId)
        {
            var contactToBeDeleted = GetContactEntity(contactId);

            foreach (var tag in contactToBeDeleted.ContactTags)
            {
                if (!CheckIfAnyContactHasTag(tag))
                    _context.Tags.Remove(tag.Tag);
            }

            if (contactToBeDeleted != null)
            {
                _context.Contacts.Remove(contactToBeDeleted);
                if (_context.SaveChanges() > 0)
                    return true;
            }
            return false;
        }

        private void RemoveEmptyEmailsFromContact(ContactViewModel contact)
        {
            foreach (var email in contact.Emails.ToArray())
            {
                if (string.IsNullOrEmpty(email.EmailAddress))
                    contact.Emails.Remove(email);
            }
        }

        private void RemoveEmptyPhonesFromContact(ContactViewModel contact)
        {
            foreach (var phone in contact.PhoneNumbers.ToArray())
            {
                if (string.IsNullOrEmpty(phone.PhoneNumber))
                    contact.PhoneNumbers.Remove(phone);
            }
        }

        private void RemoveEmptyTagsFromContact(ContactViewModel contact)
        {
            foreach (var tag in contact.Tags.ToArray())
            {
                if (string.IsNullOrEmpty(tag.TagName))
                    contact.Tags.Remove(tag);
            }
        }
    }
}