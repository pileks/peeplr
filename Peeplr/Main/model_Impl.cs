﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ent = Peeplr.Main.Model;
using data = Peeplr.Data.Internal;
using am = AutoMapper;

namespace Peeplr.Main.Model.Queries
{
    public class ContactQueries : IContactQueries
    {
        public ent::Contact TryGetSingle(int id)
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

    public class EmailQueries : IEmailQueries
    {
        public IEnumerable<Email> Get_forContact(int contactId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var contact = db.Contacts.Single(x => x.Id == contactId);

                return am::Mapper.Map<ent::Email[]>(contact.Emails);
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
        private readonly IEmailCommands emailCommands;
        public ContactCommands(INumberCommands numberCommands, ITagCommands tagCommands, IEmailCommands emailCommands)
        {
            this.numberCommands = numberCommands;
            this.tagCommands = tagCommands;
            this.emailCommands = emailCommands;
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

                foreach (var e in contact.Emails)
                {
                    emailCommands.Create(e.EmailString, dbContact.Id);
                }

                tagCommands.UpdateAndAddTagsForContact(dbContact.Id, contact.Tags);
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
                dbContact.StreetAddress = contact.StreetAddress;
                dbContact.City = contact.City;
                dbContact.Company = contact.Company;

                db.SaveChanges();
            }

            numberCommands.UpdateNumbersForContact(contactId, contact.Numbers);

            emailCommands.UpdateEmailsForContact(contactId, contact.Emails);

            tagCommands.UpdateAndAddTagsForContact(contactId, contact.Tags);
        }
        public void Delete(int contactId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var contact = db.Contacts.SingleOrDefault(x => x.Id == contactId);

                if (contact != null)
                {
                    ClearContactNumbers(contact.Id);
                    ClearContactEmails(contact.Id);
                    ClearContactTags(contact.Id);

                    db.Contacts.Remove(contact);
                    db.SaveChanges();
                }
            }
        }

        private void ClearContactTags(int contactId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var contact = db.Contacts.Single(x => x.Id == contactId);

                contact.Tags.Clear();
                db.SaveChanges();
            }
        }
        private void ClearContactNumbers(int contactId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var contact = db.Contacts.Single(x => x.Id == contactId);

                db.Numbers.RemoveRange(contact.Numbers);
                db.SaveChanges();
            }
        }
        private void ClearContactEmails(int contactId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var contact = db.Contacts.Single(x => x.Id == contactId);

                db.Emails.RemoveRange(contact.Emails);
                db.SaveChanges();
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

            foreach (var number in numbersToCreate)
            {
                Create(number.Type, number.NumberString, contactId);
            }

            foreach (var number in numbersToUpdate)
            {
                Update(number);
            }

            foreach (var number in numbersToDelete)
            {
                Delete(number.Id);
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

        public void UpdateAndAddTagsForContact(int contactId, IEnumerable<Tag> tags)
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

    public class EmailCommands : IEmailCommands
    {
        private readonly IEmailQueries emailQueries;
        public EmailCommands(IEmailQueries emailQueries)
        {
            this.emailQueries = emailQueries;
        }

        public void UpdateEmailsForContact(int contactId, IEnumerable<ent::Email> emails)
        {
            var dbEmails = emailQueries.Get_forContact(contactId);

            var emailsToCreate = emails.Where(e => !dbEmails.Any(x => x.Id == e.Id));
            var emailsToUpdate = emails.Where(e => dbEmails.Any(x => x.Id == e.Id));
            var emailsToDelete = dbEmails.Where(e => !emails.Any(x => x.Id == e.Id));

            foreach (var email in emailsToCreate)
            {
                Create(email.EmailString, contactId);
            }

            foreach (var email in emailsToUpdate)
            {
                Update(email);
            }

            foreach (var email in emailsToDelete)
            {
                Delete(email.Id);
            }
        }

        public void Create(string emailString, int contactId)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var dbEmail = new data::Email() { EmailString = emailString, ContactId = contactId };

                db.Emails.Add(dbEmail);
                db.SaveChanges();
            }
        }

        public void Update(ent::Email email)
        {
            Contract.Assert(entValid::Email.IsValid(email));

            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var dbEmail = db.Emails.Single(x => x.Id == email.Id);

                dbEmail.EmailString = email.EmailString;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var dbEmail = db.Emails.Single(x => x.Id == id);

                db.Emails.Remove(dbEmail);
                db.SaveChanges();
            }
        }
    }
}