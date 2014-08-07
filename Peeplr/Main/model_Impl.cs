using System.Collections;
using System.Collections.Generic;

using ent = Peeplr.Main.Model;
using data = Peeplr.Data.Internal;
using map = AutoMapper;

namespace Peeplr.Main.Model.Queries
{
    public class ContactQueries : IContactQueries
    {
        public IEnumerable<Contact> GetAll()
        {
            using (var db = new data::PeeplrDatabaseModelContainer())
            {
                var data = db.Contacts;

                return map::Mapper.Map<ent::Contact[]>(data);
            }
        }
        public IEnumerable<Contact> Get_forQuery(string query)
        {
            throw new System.NotImplementedException();
        }
    }
}

namespace Peeplr.Main.Model.Commands
{
    public class ContactCommands : IContactCommands
    {
        public void Create()
        {
            throw new System.NotImplementedException();
        }
        public void Update(Contact contact)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int contactId)
        {
            throw new System.NotImplementedException();
        }
    }
}