using System.Collections.Generic;

namespace Phonebook.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactAvatar { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string AdditionalInfo { get; set; }
        public ICollection<Email> Emails { get; set; }
        public ICollection<Phone> PhoneNumbers { get; set; }
        public ICollection<ContactTag> ContactTags { get; set; }
    }
}
