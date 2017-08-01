using Phonebook.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public interface IPhonebookRepository
    {
        IEnumerable<ContactViewModel> GetContactsBySearchText(string searchText);

        ContactViewModel GetContactById(int contactId);

        bool DeleteContact(int contactId);

        bool AddContact(ContactViewModel newContact);

        bool UpdateContact(ContactViewModel editedContact);

    }
}