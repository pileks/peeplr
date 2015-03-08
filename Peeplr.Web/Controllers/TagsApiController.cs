using Peeplr.Model.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ent = Peeplr.Model;

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