using System.Collections.Generic;

namespace Peeplr.Main.Model
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

namespace Peeplr.Main.Model.Mappers
{
    using am = AutoMapper;
    using data = Peeplr.Data.Internal;
    using ent = Peeplr.Main.Model;

    public static class AutoMapperConfiguration
    {
        public static void ConfigureDataToDomainModelMappings()
        {
            am::Mapper.CreateMap<data::Tag, ent::Tag>();
            am::Mapper.CreateMap<data::Number, ent::Number>();
            am::Mapper.CreateMap<data::Contact, ent::Contact>();
            am::Mapper.CreateMap<data::Email, ent::Email>();
        }
    }
}

namespace Peeplr.Main.Model.Validation
{
    using ent = Peeplr.Main.Model;
    using buru = Peeplr.Main.BusinessRules;

    using System.Linq;
    public static class Contact
    {
        public static bool IsValid(ent::Contact contact)
        {
            return
                !string.IsNullOrWhiteSpace(contact.FirstName) &&
                !string.IsNullOrWhiteSpace(contact.LastName) &&
                contact.Numbers.All(number => Number.IsValid(number)) &&
                contact.Tags.All(tag => Tag.IsValid(tag));
        }
    }
    public static class Number
    {
        public static bool IsValid(ent::Number number)
        {
            return
                !string.IsNullOrWhiteSpace(number.NumberString) &&
                !string.IsNullOrWhiteSpace(number.Type) &&
                buru.Number.AllowedNumberTypes.Contains(number.Type);
        }
    }
    public static class Tag
    {
        public static bool IsValid(ent::Tag tag)
        {
            return
                !string.IsNullOrWhiteSpace(tag.Name);
        }
    }
}