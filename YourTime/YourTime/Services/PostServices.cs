﻿using System.Collections.Generic;
using System.Linq;
using YourTime.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace YourTime.Services
{
    public class PostServices
    {
        private readonly IMongoCollection<Post> _posts;

        public PostServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
            var database = client.GetDatabase("SocialNetworkDb");
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
