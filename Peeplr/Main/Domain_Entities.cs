using System.Collections.Generic;

namespace Peeplr.Main.Model
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
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
        }
    }
}