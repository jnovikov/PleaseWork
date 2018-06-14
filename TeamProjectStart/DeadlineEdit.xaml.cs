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
    /// Логика взаимодействия для DeadlineEdit.xaml
    /// </summary>
    public partial class DeadlineEdit : Page
    {
        public Deadline _deadline;
        public ApiData apiData = new ApiData();

        public DeadlineEdit(Deadline deadline)
        {
            InitializeComponent();
            _deadline = deadline;

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

            textBoxName.Text = deadline.Name;
            calendar.SelectedDate = deadline.Finish.Date;

            comboBoxHour.SelectedIndex = hours.IndexOf(deadline.Finish.Hour);
            comboBoxMinute.SelectedIndex = hours.IndexOf(deadline.Finish.Minute);
        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeadlinePage());
        }

        private async void buttonAddDeadline_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                var name = textBoxName.Text;
                var finish = calendar.SelectedDate;

                if (name == null || finish == null)
                {
                    MessageBox.Show("Заполите или выберите всю информацию о дедлайне", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var hour = double.Parse(comboBoxHour.SelectedItem.ToString());
                var minutes = double.Parse(comboBoxMinute.SelectedItem.ToString());
                finish = finish.Value.AddHours(hour);
                finish = finish.Value.AddMinutes(minutes);

                if (finish.Value < DateTime.Now)
                {
                    MessageBox.Show("Дата выбрана некорректно", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                var result = await apiData.EditDeadline(_deadline.Id, name, finish);

                if (result != null)
                {
                    MessageBox.Show(result.ErrorMessage);
                }
                else
                {
                    MessageBox.Show("Дедлайн изменен успешно", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                var deadlinePage = new DeadlinePage();
                NavigationService.Navigate(deadlinePage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Заполите или выберите всю информацию о дедлайне", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
