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
            listBoxTasks.ItemsSource = await apiData.GetTasksForDeadline(_deadline.Id);
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


        private void AddCheckBox(DTO.Task task)
        {
            //var newCheckBox = new CheckBox();
            //newCheckBox.Margin = new Thickness(0, 5, 0, 5);

            ////newCheckBox.Content = task.Description;
            ////newCheckBox.IsChecked = task.IsDone;

            //newCheckBox.Checked += TaskFinishedChanged;
            //newCheckBox.Unchecked += TaskFinishedChanged;

            //PageContent.Children.Add(newCheckBox);
            //_tasks.Add(task);
        }
        
        private void TaskFinishedChanged(object sender, RoutedEventArgs e)
        {
        //    var neededCheckBox = sender as CheckBox;
        //    if (neededCheckBox != null)
        //    {
        //        var neededIndex = PageContent.Children.IndexOf(neededCheckBox);
        //       // _tasks[neededIndex].IsDone = !_tasks[neededIndex].IsDone;
        //        UpdateProgressBar();
        //    }
        }

        private void UpdateProgressBar()
        {
            //var doneTasks = _tasks.FindAll(t => t.IsDone == true);
            // var percentOfDoneTasks = (double)doneTasks.Count / _tasks.Count;
            // ProgressBarTasks.Value = percentOfDoneTasks * 100;
        }
    }
}
