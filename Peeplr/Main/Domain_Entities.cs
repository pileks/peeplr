namespace Peeplr.Model.Mappers
{
    using am = AutoMapper;
    using data = Peeplr.Data.Internal;
    using ent = Model;

    public static class AutoMapperConfiguration
    {
        public static void ConfigureDataToDomainModelMappings()
        {
            am::Mapper.CreateMap<data::Tag, Tag>();
            am::Mapper.CreateMap<data::Number, Number>();
            am::Mapper.CreateMap<data::Contact, Contact>();
            am::Mapper.CreateMap<data::Email, Email>();
        }
    }
}

namespace Peeplr.Model.Validation
{
    using ent = Model;
    using buru = Peeplr.Main.BusinessRules;

    using System.Linq;
    public static class Contact
    {
        public static bool IsValid(Model.Contact contact)
        {
            return
                !string.IsNullOrWhiteSpace(contact.FirstName) &&
                !string.IsNullOrWhiteSpace(contact.LastName) &&
                contact.Numbers.All(number => Number.IsValid(number)) &&
                contact.Tags.All(tag => Tag.IsValid(tag)) &&
                contact.Emails.All(email => Email.IsValid(email));
        }
    }
    public static class Number
    {
        public static bool IsValid(Model.Number number)
        {
            return
                !string.IsNullOrWhiteSpace(number.NumberString) &&
                buru.Number.AllowedNumberTypes.Contains(number.Type);
        }
    }
    public static class Email
    {
        public static bool IsValid(Model.Email email)
        {
            return !string.IsNullOrWhiteSpace(email.EmailString);
        }
    }
    public static class Tag
    {
        public static bool IsValid(Model.Tag tag)
        {
            return
                !string.IsNullOrWhiteSpace(tag.Name);
        }
    }
}