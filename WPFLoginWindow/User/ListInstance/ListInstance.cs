using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace WPFLoginWindow
{
    class ListInstance
    {
        #region Instance
        public static ListInstance instance = new ListInstance();
        #endregion

        #region Variables
        public ObservableCollection<UserViewModel> users = new ObservableCollection<UserViewModel>();
        private string FilePath = "8999TM.bin";
        #endregion
        #region Constructor
        public ListInstance()
        {
            LoadDatabase();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load database from file
        /// </summary>
        private void LoadDatabase()
        {
            if (File.Exists(FilePath))
            {
                using (var stream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
                {
                    var bin = new BinaryFormatter();
                    users = (ObservableCollection<UserViewModel>)bin.Deserialize(stream);
                }
            }
            else
            {
                NewUser("Mateusz", "Trybula", StatusTypes.admin);
            }
        }
        /// <summary>
        /// Save database to file
        /// </summary>
        public void SaveDatabase()
        {
            using (var stream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
            {
                var bin = new BinaryFormatter();
                bin.Serialize(stream, users);
            }
        }
        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="status">Available(user,moderator,admin)</param>
        /// <returns></returns>
        public void NewUser(string login,string password,StatusTypes status)
        {
            UserViewModel user = new UserViewModel();

            user.id = CheckAvailableId();
            user.login = login;

            user.password = CodePass(user.id,password);

            user.status = status;
            user.registered = DateTime.Now;
            user.lastModified = DateTime.Now;
            user.lastLogged = Convert.ToDateTime(null);

            users.Add(user);

        }
        /// <summary>
        /// Check for available id
        /// </summary>
        /// <returns>First available id</returns>
        private int CheckAvailableId()
        {
            int id = 0;
            List<int> ids = users.Select(x => x.id).ToList();

            for (int i = 0; ; i++)
            {
                if (!ids.Contains(i))
                {
                    id = i;
                    break;
                }
            }

            return id;
        }
        /// <summary>
        /// Remove user from list
        /// </summary>
        /// <param name="id">Id of user to delete</param>
        public void RemoveUser(int id)
        {
            users.RemoveAt(id);
        }
        /// <summary>
        /// Edit user data
        /// </summary>
        /// <param name="param">Array of variables</param>
        public void EditUser(object[] param)
        {
            UserViewModel user = users[(int)param[0]];
            user.password = param[1].ToString() != String.Empty ? CodePass((int)param[0], param[1].ToString()) : user.password;
            user.status = (StatusTypes)param[2];
            user.lastModified = DateTime.Now;
            users[(int)param[0]] = user;
        }
        /// <summary>
        /// Code password
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="password">Uncrypted password</param>
        /// <returns>Crypted password</returns>
        public string CodePass(int id,string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes(id.ToString() + "8999Trybula");
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, salt);
            return Encoding.ASCII.GetString(rfc.GetBytes(salt.Length));
        }
        #endregion
    }
}
