using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using YourTime.Models;

namespace YourTime.Services
{
    public class WallServices
    {
        //This Class is for implementation of services for the wall model
        //The Class implements CRUD(Create, Read, Update, Delete) methods


        private readonly IMongoCollection<User> _wall;

        public WallServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
            var database = client.GetDatabase("SocialNetworkDb");
            _wall = database.GetCollection<User>("Walls");
        }

        public List<User> Get()
        {
            return _wall.Find(wall => true).ToList();
        }

        public User Get(string id)
        {
            return _wall.Find<User>(wall => wall.Id == id).FirstOrDefault();
        }

        public User Create(User wall)
        {
            _wall.InsertOne(wall);
            return wall;
        }

        public void Update(string id, User wallIn)
        {
            _wall.ReplaceOne(wall => wall.Id == id, wallIn);
        }

        public void Remove(User wallIn)
        {
            _wall.DeleteOne(wall => wall.Id == wallIn.Id);
        }

        public void Remove(string id)
        {
            _wall.DeleteOne(wall => wall.Id == id);
        }
    }
}