using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ent = Peeplr.Main.Model;
using data = Peeplr.Data.Internal;
using am = AutoMapper;

namespace Peeplr.Main.Model.Queries
{
    public class ContactQueries : IContactQueries
    {
        public ent::Contact GetSingle(int id)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                return am::Mapper.Map<ent::Contact>(db.Contacts.SingleOrDefault(x => x.Id == id));
            }
        }
        public IEnumerable<Contact> GetAll()
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                return am::Mapper.Map<ent::Contact[]>(db.Contacts);
            }
        }
        public IEnumerable<Contact> Get_forQuery(string query)
        {
            var queryParts = query.ToLower().Split(' ');

            return GetAll().Where(contact =>
            {
                var tags = string.Join("", contact.Tags.Select(tag => tag.Name));
                var searchString = contact.FirstName.ToLower() + " " + contact.LastName.ToLower() + " " + tags.ToLower();

                return queryParts.All(part => searchString.Contains(part));
            });
        }
    }

    public class NumberQueries : INumberQueries
    {
        public IEnumerable<Number> GetNumbersForContact(int contactId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                return db.Numbers
                         .Where(number => number.ContactId == contactId)
                         .ToList()
                         .Select(number => am::Mapper.Map<ent::Number>(number));
            }
        }
    }

    public class TagQueries : ITagQueries
    {
        public IEnumerable<Tag> GetAll()
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                return am::Mapper.Map<ent::Tag[]>(db.Tags);
            }
        }
    }
}

namespace Peeplr.Main.Model.Commands
{
    using Peeplr.Main.Model.Queries;
    using System.Diagnostics.Contracts;

    using entValid = Peeplr.Main.Model.Validation;

    public class ContactCommands : IContactCommands
    {
        private readonly INumberCommands numberCommands;
        private readonly ITagCommands tagCommands;
        public ContactCommands(INumberCommands numberCommands, ITagCommands tagCommands)
        {
            this.numberCommands = numberCommands;
            this.tagCommands = tagCommands;
        }

        public void Create(ent::Contact contact)
        {
            Contract.Assert(entValid::Contact.IsValid(contact));

            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var dbContact = new data::Contact()
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    StreetAddress = contact.StreetAddress,
                    City = contact.City,
                    Company = contact.Company
                };

                db.Contacts.Add(dbContact);
                db.SaveChanges();

                foreach (var n in contact.Numbers)
                {
                    numberCommands.Create(n.Type, n.NumberString, dbContact.Id);
                }

                tagCommands.UpdateAndAddTags_forContact(dbContact.Id, contact.Tags);
            }
        }
        public void Update(int contactId, Contact contact)
        {
            Contract.Assert(entValid::Contact.IsValid(contact));

            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var dbContact = db.Contacts.SingleOrDefault(x => x.Id == contactId);

                dbContact.FirstName = contact.FirstName;
                dbContact.LastName = contact.LastName;
                dbContact.Email = contact.Email;
                dbContact.StreetAddress = contact.StreetAddress;
                dbContact.City = contact.City;
                dbContact.Company = contact.Company;

                db.SaveChanges();
            }

            numberCommands.UpdateNumbersForContact(contactId, contact.Numbers);

            tagCommands.UpdateAndAddTags_forContact(contactId, contact.Tags);
        }
        public void Delete(int contactId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var contact = db.Contacts.SingleOrDefault(x => x.Id == contactId);

                if (contact != null)
                {
                    db.Numbers.RemoveRange(contact.Numbers);
                    contact.Tags.Clear();
                    db.SaveChanges();

                    db.Contacts.Remove(contact);
                    db.SaveChanges();
                }
            }
        }
    }

    public class NumberCommands : INumberCommands
    {
        private readonly INumberQueries numberQueries;
        public NumberCommands(INumberQueries numberQueries)
        {
            this.numberQueries = numberQueries;
        }

        public void UpdateNumbersForContact(int contactId, IEnumerable<ent::Number> numbers)
        {
            var dbNumbers = numberQueries.GetNumbersForContact(contactId);

            var numbersToCreate = numbers.Where(n => !dbNumbers.Any(x => x.Id == n.Id));
            var numbersToUpdate = numbers.Where(n => dbNumbers.Any(x => x.Id == n.Id));
            var numbersToDelete = dbNumbers.Where(n => !numbers.Any(x => x.Id == n.Id));

            foreach (var n in numbersToCreate)
            {
                Create(n.Type, n.NumberString, contactId);
            }

            foreach (var n in numbersToUpdate)
            {
                Update(n);
            }

            foreach (var n in numbersToDelete)
            {
                Delete(n.Id);
            }
        }

        public void Create(string type, string numberString, int contactId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var number = new data::Number()
                {
                    Type = type,
                    NumberString = numberString,
                    ContactId = contactId
                };
                db.Numbers.Add(number);
                db.SaveChanges();
            }
        }
        public void Update(ent::Number number)
        {
            Contract.Assert(entValid::Number.IsValid(number));

            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var dbNumber = db.Numbers.SingleOrDefault(x => x.Id == number.Id);

                dbNumber.Type = number.Type;
                dbNumber.NumberString = number.NumberString;

                db.SaveChanges();
            }
        }
        public void Delete(int numberId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var dbNumber = db.Numbers.SingleOrDefault(x => x.Id == numberId);

                db.Numbers.Remove(dbNumber);
                db.SaveChanges();
            }
        }
    }

    public class TagCommands : ITagCommands
    {
        private readonly ITagQueries tagQueries;
        public TagCommands(ITagQueries tagQueries)
        {
            this.tagQueries = tagQueries;
        }

        public void UpdateAndAddTags_forContact(int contactId, IEnumerable<Tag> tags)
        {
            var dbTags = tagQueries.GetAll();

            var tagsToCreate = tags.Where(t => !dbTags.Any(x => x.Name == t.Name));
            var tagsToUpdate = tags.Except(tagsToCreate);

            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var contact = db.Contacts.SingleOrDefault(x => x.Id == contactId);

                contact.Tags.Clear();
                db.SaveChanges();

                foreach (var t in tagsToCreate)
                {
                    Create(t.Name, contactId);
                }

                foreach (var t in tagsToUpdate)
                {
                    AssignToContact(t.Name, contactId);
                }
            }
        }

        public void Create(string name, int contactId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var tag = new data::Tag() { Name = name };

                db.Tags.Add(tag);
                db.SaveChanges();

                var contact = db.Contacts.SingleOrDefault(x => x.Id == contactId);

                tag.Contacts.Add(contact);
                db.SaveChanges();
            }
        }
        public void AssignToContact(string name, int contactId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var tag = db.Tags.SingleOrDefault(x => x.Name == name);
                var contact = db.Contacts.SingleOrDefault(x => x.Id == contactId);

                contact.Tags.Add(tag);
                db.SaveChanges();
            }
        }
    }
}