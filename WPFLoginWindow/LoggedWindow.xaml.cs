using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFLoginWindow
{
    /// <summary>
    /// Interaction logic for LoggedWindow.xaml
    /// </summary>
    public partial class LoggedWindow : Window
    {
        public LoggedWindow(UserViewModel user)
        {
            InitializeComponent();
            this.DataContext = new LoggedViewModel(user);
        }
    }
}
