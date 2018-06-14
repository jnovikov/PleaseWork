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
    /// Логика взаимодействия для AddDetail.xaml
    /// </summary>
    public partial class AddDetail : Page
    {
        ApiData apiData = new ApiData();
        //int _deadlineId;
        Deadline _deadline;

        public AddDetail(Deadline deadline)
        {
            InitializeComponent();
            //_deadlineId = deadlineId;
            _deadline = deadline;
        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeadlineDetails(_deadline));
        }

        private async void buttonAddDeadlineDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = textBoxName.Text;
                var worktime = calendar.SelectedDate;

                if (name == null || worktime == null)
                {
                    MessageBox.Show("Заполите или выберите всю информацию о дедлайне", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (worktime.Value.Day < DateTime.Now.Day)
                {
                    MessageBox.Show("Дата выбрана некорректно", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (worktime.Value > _deadline.Finish.Date)
                {
                    MessageBox.Show("Дата таска не может быть позже, чем дата дедлайна", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                var result = await apiData.AddTask(_deadline.Id, name, worktime);

                if (result != null)
                {
                    MessageBox.Show(result.ErrorMessage);
                }
                else
                {
                    MessageBox.Show("Задача успешно добавлена", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                var deadlineDetailsPage = new DeadlineDetails(_deadline);
                NavigationService.Navigate(deadlineDetailsPage);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
