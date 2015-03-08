using System.Collections.Generic;

using ent = Peeplr.Model;
using data = Peeplr.Data.Internal;

namespace Peeplr.Model.Queries
{
    public interface IContactQueries 
    {
        Contact TryGetSingle(int id);
        IEnumerable<Contact> GetAll();
        IEnumerable<Contact> Get_forQuery(string query);
    }

    public interface INumberQueries
    {
        IEnumerable<Number> GetNumbersForContact(int contactId);
    }

    public interface ITagQueries
    {
        IEnumerable<Tag> GetAll();
    }

    public interface IEmailQueries
    {
        IEnumerable<Email> Get_forContact(int contactId);
    }
}

namespace Peeplr.Model.Commands
{
    public interface IContactCommands
    {
        void Create(Contact contact);
        void Update(int contactId, Contact contact);
        void Delete(int contactId);
    }

    public interface INumberCommands
    {
        void UpdateNumbersForContact(int contactId, IEnumerable<Number> numbers);

        void Create(string type, string numberString, int contactId);
        void Update(Number number);
        void Delete(int numberId);
    }

    public interface ITagCommands 
    {
        void UpdateAndAddTagsForContact(int contactId, IEnumerable<Tag> tags);

        void Create(string name, int contactId);
        void AssignToContact(string name, int contactId);
    }

    public interface IEmailCommands
    {
        void UpdateEmailsForContact(int contactId, IEnumerable<Email> emails);

        void Create(string emailString, int contactId);
        void Update(Email email);
        void Delete(int id);
    }
}