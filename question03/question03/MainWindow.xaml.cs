using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace question03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> list1 = new ObservableCollection<string>();
        ObservableCollection<string> list2 = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            lBox1.ItemsSource = list1; //binding
            lBox2.ItemsSource = list2;
        }

        

        private void Button_Addvalue1(object sender, RoutedEventArgs e)
        {
            string x = input1.Text;
            if (x == "")
            {
                MessageBox.Show("no value entered");
            }
            else
            {
                list1.Add(x);
            }
        }

        private void Button_Addvalue2(object sender, RoutedEventArgs e)
        {
            string x = input2.Text;
            if (x == "")
            {
                MessageBox.Show("no value entered");
            }
            else
            {
                list2.Add(x);
            }
        }

        private void shiftLeft_Click(object sender, RoutedEventArgs e)
        {
            if (lBox1.SelectedItem == null)
            {
                MessageBox.Show("No item selected");
            }
            else if (lBox1.SelectedItem != null)
            {
                list2.Add(lBox1.SelectedItem as string);
                list1.Remove(lBox1.SelectedItem as string);
            }
        }

        private void shiftLeftAll_Click(object sender, RoutedEventArgs e)
        {
            while (lBox1.Items.Count != 0)
            {
                for(int i = 0; i < lBox1.Items.Count; i++)
                {

                    list2.Add(lBox1.Items[i] as string);
                    list1.Remove(lBox1.Items[i] as string);

                }
            }
           
            
        }

        private void shiftRight_Click(object sender, RoutedEventArgs e)
        {
            if (lBox2.SelectedItem == null)
            {
                MessageBox.Show("No item selected");
            }
            else if (lBox2.SelectedItem != null)
            {
                list1.Add(lBox2.SelectedItem as string);
                list2.Remove(lBox2.SelectedItem as string);
            }
        }

        private void shiftRightAll_Click(object sender, RoutedEventArgs e)
        {
            while (lBox2.Items.Count != 0)
            {
                for (int i = 0; i < lBox2.Items.Count; i++)
                {

                    list1.Add(lBox2.Items[i] as string);
                    list2.Remove(lBox2.Items[i] as string);

                }
            }


        }
    }
}
