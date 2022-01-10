using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Login
{
    [Index(nameof(Username), nameof(Password))]
    public class User
    {
        [Key]
        public Guid ID { get; private set; }
        [Required]
        public string Username { get; private set; }
        [Required]
        public string Password { get; private set; }
        [Required]
        public string Nickname { get; private set; }

        public DateTime CreationDate { get; private set; }
        public DateTime LastLogin { get; private set; }

        public int GamesPlayed { get; private set; }

        public User(string username, string password, string nickname)
        {
            Username = username;
            Password = password;
            Nickname = nickname;

            ID = Guid.NewGuid();
            CreationDate = DateTime.Now;
            LastLogin = default;
            GamesPlayed = 0;
        }

        public User(Guid iD, string username, string password, string nickname, DateTime creationDate, DateTime lastLogin, int gamesPlayed)
        {
            ID = iD;
            Username = username;
            Password = password;
            Nickname = nickname;
            CreationDate = creationDate;
            LastLogin = lastLogin;
            GamesPlayed = gamesPlayed;
        }

        public string[] GetAsStrings()
        {
            return new string[] { ID.ToString(), Username, Password, Nickname, CreationDate.ToString(), LastLogin.ToString(), GamesPlayed.ToString() };
        }
    }
}