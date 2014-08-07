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

        public List<ent::Contact> GetAll()
        {
            return contactQueries.GetAll().ToList();
        }
    }
}