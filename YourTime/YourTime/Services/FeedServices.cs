using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourTime.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace YourTime.Services
{
    public class FeedServices
    {
        private readonly IMongoCollection<Post> _posts;
        private readonly IMongoCollection<Circle> _circles;

        public FeedServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("YourTimeDB"));
            var database = client.GetDatabase("YourTimeDB");
            _posts = database.GetCollection<Post>("Posts");
        }

        public List<Post> Get()
        {
            return _posts.Find(post => true).ToList();
        }

        public Post Get(string id)
        {
            return _posts.Find<Post>(post => post.Id == id).FirstOrDefault();
        }

        public Post Create(Post post)
        {
            _posts.InsertOne(post);
            return post;
        }

        public void Update(string id, Post postIn)
        {
            _posts.ReplaceOne(post => post.Id == id, postIn);
        }

        public void Remove(Post postIn)
        {
            _posts.DeleteOne(post => post.Id == postIn.Id);
        }

        public void Remove(string id)
        {
            _posts.DeleteOne(post => post.Id == id);
        }
    }
}
