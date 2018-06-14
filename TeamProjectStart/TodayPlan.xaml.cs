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

namespace TeamProjectStart
{
    /// <summary>
    /// Логика взаимодействия для TodayPlan.xaml
    /// </summary>
    public partial class TodayPlan : Page
    {
        public TodayPlan()
        {
            InitializeComponent();
            UpdateTasks();
        }

        private async void UpdateTasks()
        {
            var ad = new ApiData();
            var tasks = await ad.GetTasksForToday();
            tasksBox.ItemsSource = tasks;
        }

        private void buttonViewDeadline_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeadlinePage());
        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new start());
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            AuthFile.Flush();
            System.Windows.Application.Current.Shutdown();
        }
    }
}
