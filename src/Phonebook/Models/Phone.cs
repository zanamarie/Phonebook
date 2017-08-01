namespace Phonebook.Models
{
    public class Phone
    {
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}