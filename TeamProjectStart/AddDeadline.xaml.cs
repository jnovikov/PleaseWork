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
using TeamProjectStart.DTO;

namespace TeamProjectStart
{
    /// <summary>
    /// Логика взаимодействия для AddDeadline.xaml
    /// </summary>
    public partial class AddDeadline : Page
    {

        public AddDeadline()
        {
            InitializeComponent();
            List<int> hours = new List<int>();
            for (int i = 0; i <= 23; i++)
            {
                hours.Add(i);
            }
            List<int> minutes = new List<int>();
            for (int i = 0; i <= 59; i++)
            {
                minutes.Add(i);
            }
            comboBoxHour.ItemsSource = hours;
            comboBoxMinute.ItemsSource = minutes;

        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void buttonAddDeadline_Click(object sender, RoutedEventArgs e)
        {
            var apiData = new ApiData();
            var name = textBoxName.Text;
            var finish = calendar.SelectedDate;

            var hour = double.Parse(comboBoxHour.SelectedItem.ToString());
            var minutes = double.Parse(comboBoxMinute.SelectedItem.ToString());
            finish = finish.Value.AddHours(hour);
            finish = finish.Value.AddMinutes(minutes);


            var result = await apiData.AddDeadline(name, finish);

            if (result != null)
            {
                MessageBox.Show(result.ErrorMessage);
            }
            else
            {
                MessageBox.Show("OK");
            }
            var deadlinePage = new DeadlinePage();
            NavigationService.Navigate(deadlinePage);
        }
    }
}
