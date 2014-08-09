using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Peeplr.Web.Controllers
{
    using ent = Peeplr.Main.Model;
    using Peeplr.Main.Model.Queries;
    using Peeplr.Main.Model.Commands;

    public class ContactsApiController : ApiController
    {
        private readonly IContactQueries contactQueries;
        private readonly IContactCommands contactCommands;
        public ContactsApiController(IContactQueries contactQueries, IContactCommands contactCommands)
        {
            this.contactQueries = contactQueries;
            this.contactCommands = contactCommands;
        }
        
        [HttpGet]
        public List<ent::Contact> GetAll()
        {
            return contactQueries.GetAll().ToList();
        }

        [HttpGet]
        public ent::Contact GetSingle(int id)
        {
            return contactQueries.GetSingle(id);
        }

        [HttpGet]
        public List<ent::Contact> Get_forQuery(string q)
        {
            return contactQueries.Get_forQuery(q).ToList();
        }

        [HttpGet]
        public void Delete(int id)
        {
            contactCommands.Delete(id);
        }

        [HttpPost]
        public void Create(ent::Contact contact)
        {
            contactCommands.Create(contact);
        }

        [HttpPost]
        public void Update(int id, ent::Contact contact)
        {
            contactCommands.Update(id, contact);
        }
    }
}