using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using YourTime.Models;

namespace YourTime.Services
{
    public class BlacklistServices
    {
        //This Class is for implementation of services for the circle model
        //The Class implements CRUD(Create, Read, Update, Delete) methods


        private readonly IMongoCollection<User> _blacklist;

        public BlacklistServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
            var database = client.GetDatabase("SocialNetworkDb");
            _blacklist = database.GetCollection<User>("Blacklist");
        }

        public List<User> Get()
        {
            return _blacklist.Find(circle => true).ToList();
        }

        public User Get(string id)
        {
            return _blacklist.Find<User>(circle => circle.Id == id).FirstOrDefault();
        }

        public User Create(User circle)
        {
            _blacklist.InsertOne(circle);
            return circle;
        }

        public void Update(string id, User CircleIn)
        {
            _blacklist.ReplaceOne(circle => circle.Id == id, CircleIn);
        }

        public void Remove(User userIn)
        {
            _blacklist.DeleteOne(circle => circle.Id == userIn.Id);
        }

        public void Remove(string id)
        {
            _blacklist.DeleteOne(circle => circle.Id == id);
        }
    }
}