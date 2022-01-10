﻿using System;

namespace Login
{
    public class User
    {
        public Guid ID { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Nickname { get; private set; }

        public DateTime CreationDate { get; private set; }
        public DateTime LastLogin { get; private set; }

        public int GamesPlayed { get; private set; }

        /*
        public User(string userName, string password)
        {
            Username = userName;
            Password = password;
        }*/

        public User(string userName, string password, string nickName)
        {
            Username = userName;
            Password = password;
            Nickname = nickName;

            ID = Guid.NewGuid();
            CreationDate = DateTime.Now;
            LastLogin = default;
            GamesPlayed = 0;
        }

        public User(Guid iD, string userName, string password, string nickName, DateTime creationDate, DateTime lastLogin, int gamesPlayed)
        {
            ID = iD;
            Username = userName;
            Password = password;
            Nickname = nickName;
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