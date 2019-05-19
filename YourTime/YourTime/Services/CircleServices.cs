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
        private readonly IMongoCollection<User> _User;
       

        public CircleServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("YourTimeDB"));
            var database = client.GetDatabase("YourTimeDB");
            _User = database.GetCollection<User>("User");

        }

        public List<User> Get()
        {
            //return _User.Find(User => true).ToList();
            return _User.Find(User => true).ToList();
        }

        public User Get(string id)
        {
            return _User.Find<User>(User => User.Id == id).FirstOrDefault();
        }

        public User Create(User bruger)
        {
            _User.InsertOne(bruger);
            return bruger;
        }

        public void Update(string id, User UserIn)
        {
            _User.ReplaceOne(book => book.Id == id, UserIn);
        }

        public void Remove(User UserIn)
        {
            _User.DeleteOne(book => book.Id == UserIn.Id);
        }

        public void Remove(string id)
        {
            _User.DeleteOne(user => user.Id == id);
        }


    }
}
