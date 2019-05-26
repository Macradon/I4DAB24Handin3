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


        private readonly IMongoCollection<Wall> _wall;

        public WallServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
            var database = client.GetDatabase("SocialNetworkDb");
            _wall = database.GetCollection<Wall>("Walls");
        }

        public List<Wall> Get()
        {
            return _wall.Find(wall => true).ToList();
        }

        public Wall Get(string id)
        {
            return _wall.Find<Wall>(wall => wall.UserId == id).FirstOrDefault();
        }

        public Wall Create(Wall wall)
        {
            _wall.InsertOne(wall);
            return wall;
        }

        public void Update(string id, Wall wallIn)
        {
            _wall.ReplaceOne(wall => wall.UserId == id, wallIn);
        }

        public void Remove(Wall wallIn)
        {
            _wall.DeleteOne(wall => wall.UserId == wallIn.UserId);
        }

        public void Remove(string id)
        {
            _wall.DeleteOne(wall => wall.UserId == id);
        }
    }
}