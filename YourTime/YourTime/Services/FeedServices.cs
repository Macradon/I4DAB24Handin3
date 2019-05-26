using System.Collections.Generic;
using System.Linq;
using YourTime.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace YourTime.Services
{
    public class FeedServices
    {
        private readonly IMongoCollection<Feed> _feeds;

        public FeedServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
            var database = client.GetDatabase("SocialNetworkDb");
            _feeds = database.GetCollection<Feed>("Feeds");
        }

        public List<Feed> Get()
        {
            return _feeds.Find(feed => true).ToList();
        }

        public Feed Get(string id)
        {
            return _feeds.Find<Feed>(feed => feed.Id == id).FirstOrDefault();
        }

        public Feed Create(Feed feed)
        {
            _feeds.InsertOne(feed);
            return feed;
        }

        public void Update(string id, Feed feedIn)
        {
            _feeds.ReplaceOne(feed => feed.Id == id, feedIn);
        }

        public void Remove(Feed feedIn)
        {
            _feeds.DeleteOne(feed => feed.Id == feedIn.Id);
        }

        public void Remove(string id)
        {
            _feeds.DeleteOne(feed => feed.Id == id);
        }
    }
}
