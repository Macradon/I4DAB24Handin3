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
            return _blacklist.Find(blacklist => true).ToList();
        }

        public User Get(string id)
        {
            return _blacklist.Find<User>(blacklist => blacklist.Id == id).FirstOrDefault();
        }

        public User Create(User blacklist)
        {
            _blacklist.InsertOne(blacklist);
            return blacklist;
        }

        public void Update(string id, User blkIn)
        {
            _blacklist.ReplaceOne(blacklist => blacklist.Id == id, blkIn);
        }

        public void Remove(User blkIn)
        {
            _blacklist.DeleteOne(blacklist => blacklist.Id == blkIn.Id);
        }

        public void Remove(string id)
        {
            _blacklist.DeleteOne(blacklist => blacklist.Id == id);
        }
    }
}