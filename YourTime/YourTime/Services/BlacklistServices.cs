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
        //This Class is for implementation of services for the blacklist model
        //The Class implements CRUD(Create, Read, Update, Delete) methods


        private readonly IMongoCollection<Blacklist> _blacklist;

        public BlacklistServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
            var database = client.GetDatabase("SocialNetworkDb");
            _blacklist = database.GetCollection<Blacklist>("Blacklist");
        }

        public List<Blacklist> Get()
        {
            return _blacklist.Find(blacklist => true).ToList();
        }

        public Blacklist Get(string id)
        {
            return _blacklist.Find<Blacklist>(blacklist => blacklist.Id == id).FirstOrDefault();
        }

        public Blacklist Create(Blacklist blacklist)
        {
            _blacklist.InsertOne(blacklist);
            return blacklist;
        }

        public void Update(string id, Blacklist blkIn)
        {
            _blacklist.ReplaceOne(blacklist => blacklist.UserId == id, blkIn);
        }

        public void Remove(Blacklist blkIn)
        {
            _blacklist.DeleteOne(blacklist => blacklist.UserId == blkIn.UserId);
        }

        public void Remove(string id)
        {
            _blacklist.DeleteOne(blacklist => blacklist.UserId == id);
        }
    }
}