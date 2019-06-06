using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuDing.JwtBearer.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }

    public interface IUserDataClient
    {
        List<User> GetAll();

        User GetByKey(int id);

        User GetByName(string name);
    }

    public class UserDataClient : IUserDataClient
    {
        private readonly List<User> _data = new List<User>()
        {
            new User{Id=1,Name="godric1",Password="123456",Email="godric1@outlook.com",PhoneNumber="11111111111"},
            new User{Id=2,Name="godric2",Password="123456",Email="godric2@outlook.com",PhoneNumber="11111111112"},
            new User{Id=3,Name="godric3",Password="123456",Email="godric3@outlook.com",PhoneNumber="11111111113"},
        };

        public List<User> GetAll()
        {
            return _data;
        }

        public User GetByKey(int id)
        {
            return _data.FirstOrDefault(t => t.Id == id);
        }

        public User GetByName(string name)
        {
            return _data.FirstOrDefault(t => t.Name == name);
        }
    }
}
