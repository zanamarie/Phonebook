using System.Collections.Generic;

namespace Phonebook.ViewModels
{
    public class ContactViewModel
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactAvatar = "http://denison.edu/files/default_images/default_1.jpeg";
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string AdditionalInfo { get; set; }
        public ICollection<EmailViewModel> Emails { get; set; }
        public ICollection<PhoneViewModel> PhoneNumbers { get; set; }
        public ICollection<TagViewModel> Tags { get; set; }
    }
}
