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
    /// Логика взаимодействия для DeadlineDetails.xaml
    /// </summary>
    public partial class DeadlineDetails : Page
    {
        //добавить readdata и savedata
        private List<Task> _tasks = new List<Task>();
        
        //принять дедлайн и для него вывести данные 
        public DeadlineDetails()
        {
            InitializeComponent();


        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void buttonAddDeadlineDetail_Click(object sender, RoutedEventArgs e)
        {
            var addDetailWindow = new AddDetail();
            addDetailWindow.TaskAdded += AddTask;
            NavigationService.Navigate(addDetailWindow);
            
        }

        private void AddTask(Task task)
        {
            AddCheckBox(task);
            UpdateProgressBar();

        }

        private void AddCheckBox(Task task)
        {
            var newCheckBox = new CheckBox();
            newCheckBox.Margin = new Thickness(0, 5, 0, 5);

            newCheckBox.Content = task.Description;
            newCheckBox.IsChecked = task.IsDone;

            newCheckBox.Checked += TaskFinishedChanged;
            newCheckBox.Unchecked += TaskFinishedChanged;

            PageContent.Children.Add(newCheckBox);
            _tasks.Add(task);
        }
        
        private void TaskFinishedChanged(object sender, RoutedEventArgs e)
        {
            var neededCheckBox = sender as CheckBox;
            if (neededCheckBox != null)
            {
                var neededIndex = PageContent.Children.IndexOf(neededCheckBox);
                _tasks[neededIndex].IsDone = !_tasks[neededIndex].IsDone;
                UpdateProgressBar();
            }
        }

        private void UpdateProgressBar()
        {
            var doneTasks = _tasks.FindAll(t => t.IsDone);
            var percentOfDoneTasks = (double)doneTasks.Count / _tasks.Count;
            ProgressBarTasks.Value = percentOfDoneTasks * 100;
        }
    }
}
