using question01.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for dummy_data.xaml
    /// </summary>
    public partial class dummy_data : Window
    {

        public ObservableCollection<personData> list1 = new ObservableCollection<personData>();
        public dummy_data()
        {
            InitializeComponent();

            list1.Add(new personData { firstName = "syeda", lastName = "ramsha", email = "syeda@123", password = "0000" });

            list1.Add(new personData { firstName = "ali", lastName = "ahmad", email = "ahmad@123", password = "0000" });

            list1.Add(new personData { firstName = "Ayesha", lastName = "saleem", email = "asaleem@123", password = "0000" });

            dummyFN.ItemsSource = list1;
            dummyLN.ItemsSource = list1;
            dummyMail.ItemsSource = list1;


            
        }

        
        
    }
}
