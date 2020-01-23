using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using restapi.Models;

namespace restapi.Controllers
{
    public class RootController : Controller
    {
        // GET api/values
        [Route("~/")]
        [HttpGet]
        [Produces(ContentTypes.Root)]
        [ProducesResponseType(typeof(IDictionary<ApplicationRelationship, object>), 200)]
        public IDictionary<ApplicationRelationship, object> Get()
        {
            return new Dictionary<ApplicationRelationship, object>()
            {
                {
                    ApplicationRelationship.Timesheets, new List<DocumentLink>()
                    {
                        new DocumentLink()
                        {
                            Method = Method.Get,
                            Type = ContentTypes.Timesheets,
                            Relationship = DocumentRelationship.Timesheets,
                            Reference = "/timesheets"
                        }
                    }
                },
                {
                    ApplicationRelationship.Version, "0.1"
                }
            };
        }

        // Create new timecard object
        [Route("~/")]
        [HttpPost]
        [Produces(ContentTypes.Timesheet)]
        [ProducesResponseType(typeof(Timecard), 200)]
        public Timecard Create([FromBody] DocumentPerson p){
            logger.LogInformation($"Creation of timesheet for {p.ToString()}");
            var tc = new Timecard(p.Id);
            var entered = new Entered() {Person = p.Id};
            tc.Transitions.Add(new Transition(entered));
            repository.Add(tc);
            return tc;
        }
    }
}
