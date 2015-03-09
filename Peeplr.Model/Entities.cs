using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Peeplr.Model
{
    public class Contact
    {
        public Contact()
        {
            Emails = new Collection<Email>();
            Numbers = new Collection<Number>();
            Tags = new Collection<Tag>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public virtual ICollection<Number> Numbers { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }

    public class Number
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string NumberString { get; set; }
        public int ContactId { get; set; }
    }

    public class Email
    {
        public int Id { get; set; }
        public string EmailString { get; set; }
        public int ContactId { get; set; }
    }

    public class Tag
    {
        public Tag()
        {
            Contacts = new Collection<Contact>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
