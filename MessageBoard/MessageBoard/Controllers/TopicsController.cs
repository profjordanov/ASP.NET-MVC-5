using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessageBoard.Data;
using MessageBoard.Models;

namespace MessageBoard.Controllers
{
    public class TopicsController : ApiController
    {
        private IMessageBoardRepository _repo;
        public TopicsController(IMessageBoardRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Topic> Get(bool includeReplies = false)
        {
            IQueryable<Topic> results;

            if (includeReplies == true)
            {
                results = _repo.GetTopicsIncludingReplies();
            }
            else
            {
                results = _repo.GetTopics();
            }

            var topics = results.OrderByDescending(t => t.Created)
                                .Take(25)
                                .ToList();

            return topics;
        }

        public HttpResponseMessage Post([FromBody]Topic newTopic)
        {
            if (newTopic.Created == default(DateTime))
            {
                newTopic.Created = DateTime.UtcNow;
            }

            if (_repo.AddTopic(newTopic) &&
                _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                  newTopic);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
