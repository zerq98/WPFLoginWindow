using System;
using System.Windows;
using System.Windows.Input;

namespace WPFLoginWindow
{
    class MainViewModel:BaseViewModel
    {
        #region Fields
        private string loginText;
        public string LoginText
        {
            get
            {
                return loginText;
            }
            set
            {
                loginText = value;
                OnPropertyChange("LoginText");
            }
        }
        private string passwordText;
        public string PasswordText
        {
            get
            {
                return passwordText;
            }
            set
            {
                passwordText = value;
                OnPropertyChange("PasswordText");
            }
        }
        #endregion
        #region Commands
        public ICommand Close { get; set; }
        public ICommand Minimize { get; set; }
        public ICommand Login { get; set; }
        #endregion
        #region Constructor
        public MainViewModel()
        {
            Close = new RelayCommand<Window>(_Close);
            Minimize = new RelayCommand<Window>(_Minimize);
            Login = new RelayCommand<Window>(_Login);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Closing window
        /// </summary>
        /// <param name="window"></param>
        private void _Close(Window window)
        {
            if (window != null) Environment.Exit(1);
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
        /// Perform login to application
        /// </summary>
        /// <param name="param">Needed variables</param>
        private void _Login(Window win)
        {
            object[] param = ListInstance.Instance.CheckForUser(LoginText, PasswordText);

            if ((bool)param[0])
            {
                LoggedWindow window = new LoggedWindow((UserViewModel)param[1]);
                window.Show();
                win.Hide();
            }
            else
            {
                MessageBox.Show(param[1].ToString(), "There was a problem!", MessageBoxButton.OK, MessageBoxImage.Error);

                LoginText = String.Empty;
                PasswordText = String.Empty;
            }
        }
        #endregion
    }
}
