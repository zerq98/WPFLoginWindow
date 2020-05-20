using System;

namespace WPFLoginWindow
{
    class UserModel
    {
        public int id;
        public string login;
        public string password;
        public StatusTypes status;
        public DateTime registered;
        public DateTime lastModified;
        public DateTime lastLogged;
    }
}
