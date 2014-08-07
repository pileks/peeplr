using System.Collections;
using System.Collections.Generic;

using ent = Peeplr.Main.Model;
using data = Peeplr.Data.Internal;


namespace Peeplr.Main.Model.Queries
{
    public interface IContactQueries 
    {
        IEnumerable<ent::Contact> GetAll();
    }
}

namespace Peeplr.Main.Model.Commands
{
    public interface IContactCommands
    {
        void Create();
        void Update(ent::Contact contact);
        void Delete(int contactId);
    }
}