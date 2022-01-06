using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Login
{
    public static class UserDatabase
    {
        private static UserDBContext db;
        private static UserDBContext DB
        {
            get
            {
                if (db == null)
                {
                    var builder = new DbContextOptionsBuilder<UserDBContext>();
                    builder.UseInMemoryDatabase("MemoryDB");
                    var options = builder.Options;
                    db = new UserDBContext(options);
                    db.Add(new User("a", "a", "Anders"));
                    db.Add(new User("b", "b", "Bent"));
                    db.Add(new User("c", "c", "Chris"));
                    db.SaveChanges();
                }
                return db;
            }
        }
        public static bool TryCreateUser(string username, string password, string nickname, out User user)
        {
            user = null;
            try
            {
                User tmp = new User(username, password, nickname);
                DB.Users.Add(tmp);
                DB.SaveChanges();
                user = tmp;
            }
            catch (Exception e)
            {
                Console.WriteLine("!!! - " + e);
                return false;
            }
            return true;
        }

        public static bool TryGetUser(string username, string password, out User user)
        {
            user = null;
            try
            {
                user = DB.Users.Where((x) => x.Password == password && x.Username == username).First();
            }
            catch (Exception e)
            {
                Console.WriteLine("!!! - " + e);
                return false;
            }
            return true;
        }
    }

    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; private set; }
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {
        }
    }
}
