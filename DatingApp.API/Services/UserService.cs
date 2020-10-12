using DatingApp.API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DatingApp.API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserForMongo> _users;
        public UserService(IUserDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<UserForMongo>(settings.CollectionName);
        }
        public List<UserForMongo> Get() =>
            _users.Find(user => true).ToList();

        public UserForMongo Get(string id) =>
            _users.Find<UserForMongo>(user => user.Id == id).FirstOrDefault();

        public UserForMongo Create(UserForMongo user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, UserForMongo userId) =>
            _users.ReplaceOne(user => user.Id == id, userId);

        public void Remove(UserForMongo userId) =>
            _users.DeleteOne(user => user.Id == userId.Id);

        public void Remove(string id) => 
            _users.DeleteOne(user => user.Id == id);
        
    }
}