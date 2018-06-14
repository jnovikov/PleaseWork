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
    /// Логика взаимодействия для DeadlineDetails.xaml
    /// </summary>
    public partial class DeadlineDetails : Page
    {

        public ApiData apiData = new ApiData();

        public Deadline _deadline;

        private List<DTO.Task> tasks;

        public DeadlineDetails(Deadline deadline) 
        {
            InitializeComponent();
            _deadline = deadline;
            deadlineBlock.Text = _deadline.Name;
            UpdateTasks();

        }

        private async void UpdateTasks()
        {
            listBoxTasks.ItemsSource = null;
            tasks = await apiData.GetTasksForDeadline(_deadline.Id);
            listBoxTasks.ItemsSource = tasks;
            UpdateProgressBar();

        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeadlinePage());
        }

        private void buttonAddDeadlineDetail_Click(object sender, RoutedEventArgs e)
        {
            var addDetailWindow = new AddDetail(_deadline);
            NavigationService.Navigate(addDetailWindow);
            UpdateTasks();
        }


        private void UpdateProgressBar()
        {
            if (tasks == null)
            {
                ProgressBarTasks.Value = 0;
                return;
            }
            var doneTasks = tasks.FindAll(t => t.IsDone == true);
            var percentOfDoneTasks = tasks.Count != 0 ? (double)doneTasks.Count / tasks.Count : 0;
            ProgressBarTasks.Value = percentOfDoneTasks * 100;
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeadlineEdit());
        }

        private async void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            var cb = (sender as CheckBox);
            var id = (int)cb.Tag;
            var value = cb.IsChecked;
            //var api = new ApiData();
            if (value == true)
            {
                await apiData.SetDone(id);
            } else
            {
                await apiData.SetUndone(id);
            }
            UpdateTasks();
        }

        private async void buttonDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = listBoxTasks.SelectedItem as DTO.Task;
            if (selectedTask == null)
                MessageBox.Show("Выберите задачу для удаления.");
            else
            {
                if (MessageBox.Show("Вы действительно хотите удалить задачу?", "Question",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    await apiData.DeleteTask(selectedTask.Id);
                    UpdateTasks();
                }
            }
        }
    }
}
