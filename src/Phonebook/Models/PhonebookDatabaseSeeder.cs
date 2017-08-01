using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Phonebook.Models
{
    public class PhonebookDatabaseSeeder
    {
        private PhonebookContext _context;
        public PhonebookDatabaseSeeder(PhonebookContext context)
        {
            _context = context;
        }
        public void EnsurePhonebookSeedData()
        {
            _context.Database.Migrate();

            if (_context.Contacts.Count() == 0)
            {
                var availableTags = new List<Tag>()
                    {
                        new Tag() {TagName = "bussiness"},
                        new Tag() {TagName = "sunday"},
                        new Tag() {TagName = "fun"},
                        new Tag() {TagName = "movies"},
                        new Tag() {TagName = "running"},
                    };

                Random randomTagIndex = new Random();

                for (var temp = 0; temp < 5; temp++)
                {
                    var person = new Person();
                    int firstTag = randomTagIndex.Next(5);
                    int secondTag;

                    do
                        secondTag = randomTagIndex.Next(5);
                    while (secondTag == firstTag);

                    var randomTags = new List<Tag>()
                    {
                        availableTags[firstTag],
                        availableTags[secondTag],
                    };

                    var emails = new List<Email>()
                    {
                        new Email() { EmailAddress = person.Email },
                    };

                    var phoneNumbers = new List<Phone>()
                    {
                        new Phone() { PhoneNumber = person.Phone },
                    };

                    var dummyContact = new Contact();
                    dummyContact.ContactName = person.FirstName + " " + person.LastName;
                    dummyContact.ContactAvatar = person.Avatar;
                    dummyContact.Address = person.Address.Street;
                    dummyContact.City = person.Address.City;
                    dummyContact.ZipCode = person.Address.ZipCode;
                    dummyContact.Emails = emails;
                    dummyContact.PhoneNumbers = phoneNumbers;
                    dummyContact.ContactTags = new List<ContactTag>();

                    foreach (var tag in randomTags)
                    {
                        var contactsTag = new ContactTag();
                        contactsTag.Tag = tag;
                        contactsTag.Contact = dummyContact;
                        dummyContact.ContactTags.Add(contactsTag);
                    }

                    _context.Contacts.Add(dummyContact);
                 //   var state = _context.Entry(dummyContact).State;

                }
                _context.SaveChanges();
            }
        }
    }
}
