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
            NavigationService.GoBack();
        }

        private async void buttonAddDeadlineDetail_Click(object sender, RoutedEventArgs e)
        {
            var name = textBoxName.Text;
            var worktime = calendar.SelectedDate;

            var result = await apiData.AddTask(_deadline.Id, name, worktime);

            if (result != null)
            {
                MessageBox.Show(result.ErrorMessage);
            }
            else
            {
                MessageBox.Show("OK");
            }
            var deadlineDetailsPage = new DeadlineDetails(_deadline);
            NavigationService.Navigate(deadlineDetailsPage);
        }
    }
}
