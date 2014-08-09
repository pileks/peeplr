using Peeplr.Main.Model.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ent = Peeplr.Main.Model;

namespace Peeplr.Web.Controllers
{
    public class TagsApiController : ApiController
    {
        private readonly ITagQueries tagQueries;
        public TagsApiController(ITagQueries tagQueries)
        {
            this.tagQueries = tagQueries;
        }

        [HttpGet]
        public IEnumerable<ent::Tag> GetAll()
        {
            return tagQueries.GetAll().ToList();
        }
    }
}