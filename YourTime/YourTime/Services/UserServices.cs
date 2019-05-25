using System.Collections.Generic;
using System.Linq;
using YourTime.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

//This Class is for implementation of services for the User model
//The Class implements CRUD(Create, Read, Update, Delete) methods

namespace YourTime.Services
{
    public class UserServices
    {
        private readonly IMongoCollection<User> _users;

        public UserServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("SocialNetworkDb"));
            var database = client.GetDatabase("SocialNetworkDb");
            _users = database.GetCollection<User>("Users");
        }

        public List<User> Get()
        {
            return _users.Find(user => true).ToList();
        }

        public User Get(string id)
        {
            return _users.Find<User>(user => user.Id == id).FirstOrDefault();
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn)
        {
            _users.ReplaceOne(user => user.Id == id, userIn);
        }
        
        public void Remove(User userIn)
        {
            _users.DeleteOne(user => user.Id == userIn.Id);
        }

        public void Remove(string id)
        {
            _users.DeleteOne(user => user.Id == id);
        }
    }
}
