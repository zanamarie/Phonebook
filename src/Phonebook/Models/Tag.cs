using System.Collections.Generic;

namespace Phonebook.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public ICollection<ContactTag> TagContacts { get; set; }
    }
}
