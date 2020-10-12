using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Models;


namespace DatingApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext contex) 
        {
            if (!contex.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordhash, passwordSalt;
                    CreatePasswordHash("password", out passwordhash, out passwordSalt);
                    user.PasswordHash = passwordhash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    contex.Users.Add(user);
                }
                contex.SaveChanges();
            }
        }
        private static void CreatePasswordHash(string password, out byte[] passwordhash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }
    }
}