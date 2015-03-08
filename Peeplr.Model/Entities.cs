using System.Collections.Generic;

namespace Peeplr.Model
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Email> Emails { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public IEnumerable<Number> Numbers { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }

    public class Number
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string NumberString { get; set; }
    }

    public class Email
    {
        public int Id { get; set; }
        public string EmailString { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
