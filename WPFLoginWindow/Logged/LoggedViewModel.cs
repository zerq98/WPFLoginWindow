using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFLoginWindow
{
    [Serializable()]
    class LoggedViewModel:BaseViewModel
    {
        #region Commands
        public ICommand Close { get; set; }
        public ICommand Minimize { get; set; }
        public ICommand WindowClosing { get; set; }
        public ICommand WindowLoaded { get; set; }
        public ICommand AddUser { get; set; }
        public ICommand RemoveUser { get; set; }
        public ICommand EditUser { get; set; }
        public ICommand LostFocus { get; set; }
        #endregion
        #region Fields and properties

        public ObservableCollection<UserViewModel> users
        {
            get
            {
                return ListInstance.Instance.users;
            }
        }

        private UserViewModel User;
        public UserViewModel user
        {
            get
            {
                return User;
            }
            set
            {
                if (value != null)
                    User = value;
                OnPropertyChange("user");
            }
        }

        #endregion
        #region Constructor
        public LoggedViewModel(UserViewModel user)
        {
            this.user = user;
            Close = new RelayCommand<Window>(_Close);
            Minimize = new RelayCommand<Window>(_Minimize);
            WindowClosing = new NormalCommand(Closing);
            WindowLoaded = new RelayCommand<WrapPanel>(windowLoaded);
            LostFocus = new RelayCommand<ListBox>(lostFocus);
            AddUser = new NormalCommand(addUser);
            RemoveUser = new RelayCommand<int>(removeUser);
            EditUser = new RelayCommand<int>(editUser);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Closing window
        /// </summary>
        /// <param name="window"></param>
        private void _Close(Window window)
        {
            MainWindow main = new MainWindow();
            main.Show();
            window?.Close();
        }
        /// <summary>
        /// Changing window state to minimize
        /// </summary>
        /// <param name="window"></param>
        private void _Minimize(Window window)
        {
            if (window != null) window.WindowState = WindowState.Minimized;
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
        private void windowLoaded(WrapPanel panel)
        {
            if (user.status == StatusTypes.admin)
            {
                foreach(Button btn in panel.Children)
                {
                    btn.IsEnabled = true;
                }
            }
            else if (user.status == StatusTypes.moderator)
            {
                foreach (Button btn in panel.Children)
                {
                    if (btn.Tag.ToString() == "moderator")
                    {
                        btn.IsEnabled = true;
                    }
                }
            }
        }
        /// <summary>
        /// Set selected index in list to -1
        /// </summary>
        /// <param name="box"></param>
        private void lostFocus(ListBox box)
        {
            box.SelectedIndex = -1;
        }
        /// <summary>
        /// Remove user from collection
        /// </summary>
        private void removeUser(int Index)
        {
            if (Index > -1)
            {
                if (Index != user.id) ListInstance.Instance.RemoveUser(Index);
                else MessageBox.Show("Cannot remove already logged user!");
            }
        }
        /// <summary>
        /// Open window for edit user
        /// </summary>
        private void editUser(int Index)
        {
            if (Index > -1)
            {
                
            }
        }
        /// <summary>
        /// Open window for add user
        /// </summary>
        private void addUser()
        {

        }
        #endregion
    }
}
