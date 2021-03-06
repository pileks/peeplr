﻿using System.Collections;
using System.Collections.Generic;

using ent = Peeplr.Main.Model;
using data = Peeplr.Data.Internal;

namespace Peeplr.Main.Model.Queries
{
    public interface IContactQueries 
    {
        ent::Contact TryGetSingle(int id);
        IEnumerable<ent::Contact> GetAll();
        IEnumerable<ent::Contact> Get_forQuery(string query);
    }

    public interface INumberQueries
    {
        IEnumerable<Number> GetNumbersForContact(int contactId);
    }

    public interface ITagQueries
    {
        IEnumerable<ent::Tag> GetAll();
    }

    public interface IEmailQueries
    {
        IEnumerable<ent::Email> Get_forContact(int contactId);
    }
}

namespace Peeplr.Main.Model.Commands
{
    public interface IContactCommands
    {
        void Create(ent::Contact contact);
        void Update(int contactId, ent::Contact contact);
        void Delete(int contactId);
    }

    public interface INumberCommands
    {
        void UpdateNumbersForContact(int contactId, IEnumerable<ent::Number> numbers);

        void Create(string type, string numberString, int contactId);
        void Update(ent::Number number);
        void Delete(int numberId);
    }

    public interface ITagCommands 
    {
        void UpdateAndAddTagsForContact(int contactId, IEnumerable<ent::Tag> tags);

        void Create(string name, int contactId);
        void AssignToContact(string name, int contactId);
    }

    public interface IEmailCommands
    {
        void UpdateEmailsForContact(int contactId, IEnumerable<ent::Email> emails);

        void Create(string emailString, int contactId);
        void Update(ent::Email email);
        void Delete(int id);
    }
}