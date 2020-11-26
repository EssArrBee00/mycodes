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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace question02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            
            string x = passwordB.Password;
            shownPass.Text = x;
            shownPass.Visibility = Visibility.Visible;
            passwordB.Visibility = Visibility.Hidden;
            

            
        }

        private void datalogin_Click(object sender, RoutedEventArgs e)
        {
            if((username.Text=="ADMIN"&& passwordB.Password=="ADMIN")||(username.Text=="admin" && passwordB.Password == "admin"))
            {
                MessageBox.Show("LOGGED IN");
            }
            else
            {
                MessageBox.Show("WRONG CREDENTIALS");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void leaving(object sender, RoutedEventArgs e)
        {

            shownPass.Visibility = Visibility.Hidden;
            passwordB.Visibility = Visibility.Visible;
        }
    }
    
}
