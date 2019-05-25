using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using YourTime.Models;
using MongoDB.Driver;
using YourTime.Models;

namespace YourTime.Services
{
    public class UserServices
    {
        private readonly IMongoCollection<Circle> _circle;
        private readonly IMongoCollection<Post> _posts;
        private readonly IMongoCollection<User> _User;


        public UserServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("YourTimeDB"));
            var databaseCircle = client.GetDatabase("YourTimeDB");
            _circle = databaseCircle.GetCollection<Circle>("Circle");

            var databaseUser = client.GetDatabase("YourTimeDB");
            _User = databaseUser.GetCollection<User>("User");

        }

        public List<Circle> GetCirle()
        {
            //return _User.Find(User => true).ToList();
            return _circle.Find(Circle => true).ToList();
        }

        public List<Post> GetPost()
        {
            return _posts.Find(Post => true).ToList();
        }

        public List<User> GetBlackList()
        {
            return _User.Find(Blacklist => true).ToList();
        }

        public List<User> GetFollowList()
        {
            return _User.Find(Follow => true).ToList();
        }

        public Circle Get(string id)
        {
            return _circle.Find<Circle>(User => User.Id == id).FirstOrDefault();
        }

        public Circle Create(Circle cirkel)
        {
            _circle.InsertOne(cirkel);
            return cirkel;
        }

        public void Update(string id, Circle CirkelIn)
        {
            _circle.ReplaceOne(cirkel => cirkel.Id == id, CirkelIn);
        }

        public void Remove(Circle CircleIn)
        {
            _circle.DeleteOne(cirkel => cirkel.Id == CircleIn.Id);
        }

        public void RemoveCircle(string id)
        {
            _circle.DeleteOne(Circle => Circle.Id == id);
        }

        public User Create(User BlkList)
        {
            _User.InsertOne(BlkList);
            return BlkList;
        }

        public void Remove(User BlkListIn)
        {
            _circle.DeleteOne(BlkList => BlkList.Id == BlkListIn.Id);
        }

        public void RemoveBlkList(string id)
        {
            _circle.DeleteOne(BlkList => BlkList.Id == id);
        }

    }
}
