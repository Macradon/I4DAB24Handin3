using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using YourTime.Models;

namespace YourTime.Services
{
    public class CircleServices
    {
    //This Class is for implementation of services for the circle model
    //The Class implements CRUD(Create, Read, Update, Delete) methods

   
            private readonly IMongoCollection<User> _circles;

            public CircleServices(IConfiguration config)
            {
                var client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
                var database = client.GetDatabase("SocialNetworkDb");
                _circles = database.GetCollection<User>("Circles");
            }

            public List<User> Get()
            {
                return _circles.Find(circle => true).ToList();
            }

            public User Get(string id)
            {
                return _circles.Find<User>(circle => circle.Id == id).FirstOrDefault();
            }

            public User Create(User circle)
            {
                _circles.InsertOne(circle);
                return circle;
            }

            public void Update(string id, User CircleIn)
            {
                _circles.ReplaceOne(circle => circle.Id == id, CircleIn);
            }

            public void Remove(User userIn)
            {
                _circles.DeleteOne(circle => circle.Id == userIn.Id);
            }

            public void Remove(string id)
            {
                _circles.DeleteOne(circle => circle.Id == id);
            }
        }
    }