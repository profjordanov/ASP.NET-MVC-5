using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBoard.Data;

namespace MessageBoard.Tests.Fakes
{
    public class FakeMessageBoardRepository : IMessageBoardRepository
    {
        public IQueryable<Reply> GetRepliesByTopic(int topicId)
        {
            return new Reply[]
            {
        new Reply()
        {
          Id = 1,
          TopicId = topicId,
          Body = "Fake Body",
          Created = DateTime.UtcNow
        },
        new Reply()
        {
          Id = 2,
          TopicId = topicId,
          Body = "Another Fake Body",
          Created = DateTime.UtcNow
        },
        new Reply()
        {
          Id = 3,
          TopicId = topicId,
          Body = "Yet Another Fake Body",
          Created = DateTime.UtcNow
        },
            }.AsQueryable();
        }

        public IQueryable<Topic> GetTopics()
        {
            return new Topic[]
            {
        new Topic()
        {
          Id = 1,
          Title = "This is a title",
          Body = "This is a body",
          Created = DateTime.UtcNow
        },
        new Topic()
        {
          Id = 2,
          Title = "This is another title",
          Body = "This is a body",
          Created = DateTime.UtcNow
        },
        new Topic()
        {
          Id = 3,
          Title = "This is yet another title",
          Body = "This is a body",
          Created = DateTime.UtcNow
        },
            }.AsQueryable();
        }

        public IQueryable<Topic> GetTopicsIncludingReplies()
        {
            return new Topic[]
            {
        new Topic()
        {
          Id = 1,
          Title = "This is a title",
          Body = "This is a body",
          Created = DateTime.UtcNow,
          Replies = new Reply[]
          {
            new Reply()
            {
              Id = 1,
              TopicId = 1,
              Body = "Fake Body",
              Created = DateTime.UtcNow
            },
            new Reply()
            {
              Id = 2,
              TopicId = 1,
              Body = "Another Fake Body",
              Created = DateTime.UtcNow
            },
            new Reply()
            {
              Id = 3,
              TopicId = 1,
              Body = "Yet Another Fake Body",
              Created = DateTime.UtcNow
            },
          }
        },
        new Topic()
        {
          Id = 2,
          Title = "This is another title",
          Body = "This is a body",
          Created = DateTime.UtcNow,
          Replies = new List<Reply>()
        },
        new Topic()
        {
          Id = 3,
          Title = "This is yet another title",
          Body = "This is a body",
          Created = DateTime.UtcNow,
          Replies = new List<Reply>()
        },
            }.AsQueryable();
        }

        public bool Save()
        {
            return true;
        }

        public bool AddTopic(Topic topic)
        {
            topic.Id = new Random().Next(5, 1000);
            topic.Created = DateTime.UtcNow;
            return true;
        }

        public bool AddReply(Reply reply)
        {
            reply.Id = new Random().Next(5, 1000);
            reply.Created = DateTime.UtcNow;
            return true;
        }
    }
}
