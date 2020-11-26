using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace question01
{
    /// <summary>
    /// Interaction logic for registration.xaml
    /// </summary>
    public partial class registration : Window
    {
        public registration()
        {
            InitializeComponent();
        }

        
            private void CancelButton_Click(object sender, RoutedEventArgs e)
            {
                MainWindow w = new MainWindow();
                    this.Hide();
                    w.Show();
            }

        private void AddData(object sender, RoutedEventArgs e)
        {
            MainWindow x = new MainWindow();
            if(fistN.Text!=""&& lastN.Text!="" && mail.Text!="" && pass.Text!="" && Cpass.Text != "")
            {
                if (pass.Text == Cpass.Text)
                {
                    MessageBox.Show("Form Submitted Successfully");
                }
                if (pass.Text != Cpass.Text)
                {
                    MessageBox.Show("your password didnot matced try again");
                }
            }
            else
            {
                MessageBox.Show("Please fill All the fieilds to continue");
            }
        }
    }
}
