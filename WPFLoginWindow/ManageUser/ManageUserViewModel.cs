using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFLoginWindow
{
    class ManageUserViewModel:BaseViewModel
    {
        #region Commands
        public ICommand Close { get; set; }
        public ICommand WindowClosing { get; set; }
        public ICommand WindowLoaded { get; set; }
        public ICommand Confirm { get; set; }
        #endregion
        #region Fields and Properties
        private UserViewModel User;
        public UserViewModel user
        {
            get
            {
                return User;
            }
            set
            {
                User = value;
                OnPropertyChange("user");
            }
        }
        private StatusTypes status;
        #endregion
        #region Constructor
        public ManageUserViewModel(UserViewModel user,StatusTypes status)
        {
            this.user = user;
            this.status = status;
            Close = new RelayCommand<Window>(_Close);
            WindowClosing = new NormalCommand(Closing);
            WindowLoaded = new RelayCommand<object[]>(windowLoaded);
            Confirm = new RelayCommand<object[]>(confirm);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Closing window
        /// </summary>
        /// <param name="window"></param>
        private void _Close(Window window)
        {
            window?.Close();
        }
        /// <summary>
        /// Save database to file when window is closing
        /// </summary>
        private void Closing()
        {
            ListInstance.Instance.SaveDatabase();
        }
        /// <summary>
        /// Set button to enable when window is loaded
        /// </summary>
        /// <param name="panel">Panel to do operation</param>
        private void windowLoaded(object[] box)
        {
            if (user != null)
            {
                ((TextBox)box[0]).IsEnabled = status == StatusTypes.admin ? true : false;
                ((TextBox)box[0]).Text = user.login;
                ((ComboBox)box[1]).SelectedIndex = (int)user.status;
                ((ComboBox)box[1]).IsEnabled = status == StatusTypes.admin ? true : false;
            }
        }
        /// <summary>
        /// Add or edit user
        /// </summary>
        /// <param name="box">Parameters to add or edit user</param>
        private void confirm(object[] box)
        {
            if (user == null)
            {
                bool canAdd = true;
                foreach(var item in box)
                {
                    if(item is PasswordBox) canAdd = ((PasswordBox)item).Password.Length > 0 ? true : false;
                    else if(item is TextBox) canAdd = ((TextBox)item).Text.Length > 0 ? true : false;
                }
                canAdd = ((PasswordBox)box[1]).Password == ((PasswordBox)box[2]).Password ? true : false;
                if (canAdd)
                {
                    
                    ListInstance.Instance.NewUser(((TextBox)box[0]).Text, ((PasswordBox)box[1]).Password, (StatusTypes)((ComboBox)box[3]).SelectedIndex);
                }
                else
                {
                    MessageBox.Show("Wrong data");
                }
            }
            else
            {
                if(this.status==StatusTypes.moderator) ListInstance.Instance.EditUser(new object[]{user.id, ((TextBox)box[0]).Text,
                    ((PasswordBox)box[1]).Password, ((ComboBox)box[3]).SelectedIndex });
                else
                {
                    bool canAdd = true;
                    foreach (var item in box)
                    {
                        if (item is TextBox) canAdd = ((TextBox)item).Text.Length > 0 ? true : false;
                    }
                    canAdd = ((PasswordBox)box[1]).Password == ((PasswordBox)box[2]).Password ? true : false;
                    if (canAdd)
                    {

                        ListInstance.Instance.EditUser(new object[]{user.id, ((TextBox)box[0]).Text,
                        ((PasswordBox)box[1]).Password, ((ComboBox)box[3]).SelectedIndex });
                    }
                    else
                    {
                        MessageBox.Show("Wrong data");
                    }
                }
            }
            ((Window)box[4]).Close();
        }
        #endregion
    }
}
