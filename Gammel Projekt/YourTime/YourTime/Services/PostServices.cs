using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using YourTime.Models;

namespace YourTime.Services
{
    public class PostServices
    {
        private readonly IMongoCollection<Post> _posts;

        public PostServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("YourTimeDB"));
            var databaseUser = client.GetDatabase("YourTimeDB");
            _posts = databaseUser.GetCollection<Post>("Post");
        }

        public List<Post> GetPost()
        {
            return _posts.Find(Post => true).ToList();
        }

        public Post Create(Post post)
        {
            _posts.InsertOne(post);
            return post;
        }

        public Post Visibility(string id)
        {
            return _posts.Find<Post>(post => post.Privacy == id).FirstOrDefault();
        }
    }
}
