using System;

namespace WPFLoginWindow
{
    class UserViewModel:BaseViewModel
    {
        private UserModel user = new UserModel();

        #region Fields
        public int id
        {
            get
            {
                return user.id;
            }
            set
            {
                user.id = value;
                OnPropertyChange("id");
            }
        }
        public string login
        {
            get
            {
                return user.login;
            }
            set
            {
                if (value != null)
                    user.login = value;
                OnPropertyChange("login");
            }
        }
        public string password
        {
            get
            {
                return user.password;
            }
            set
            {
                if (value != null)
                    user.password = value;
                OnPropertyChange("password");
            }
        }
        public StatusTypes status
        {
            get
            {
                return user.status;
            }
            set
            {
                user.status = value;
                OnPropertyChange("status");
            }
        }
        public DateTime registered
        {
            get
            {
                return user.registered;
            }
            set
            {
                if (value != null)
                    user.registered = value;
                OnPropertyChange("value");
            }
        }
        public DateTime lastModified
        {
            get
            {
                return user.lastModified;
            }
            set
            {
                if (value != null)
                    user.lastModified = value;
                OnPropertyChange("lastModified");
            }
        }
        public DateTime lastLogged
        {
            get
            {
                return user.lastLogged;
            }
            set
            {
                user.lastLogged = value;
                OnPropertyChange("lastLogged");
            }
        }
        #endregion
    }
}
