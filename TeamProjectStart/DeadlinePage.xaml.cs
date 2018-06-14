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
    /// Логика взаимодействия для DeadlinePage.xaml
    /// </summary>
    public partial class DeadlinePage : Page
    {
        public DeadlinePage()
        {
            InitializeComponent();
        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void buttonAddDeadline_Click(object sender, RoutedEventArgs e)
        {
            var addDeadlinePage = new AddDeadline();
            addDeadlinePage.DeadlineAdded += AddDeadlinePage_DeadlineAdded;
            NavigationService.Navigate(addDeadlinePage);
        }

        private void AddDeadlinePage_DeadlineAdded(object obj) //принять дедлайн
        {
            var deadlineBlock = new StackPanel();

            var deadlineLabel = new Label();
            deadlineLabel.Content = "DEADLINE";
            deadlineLabel.Foreground = Brushes.Black;
            deadlineLabel.Margin = new Thickness(5);
            deadlineLabel.MouseDoubleClick += Label_MouseDoubleClick;

            var deadlineProgress = new ProgressBar();
            deadlineProgress.Height = 15;
            deadlineProgress.Width = 150;
            deadlineProgress.Margin = new Thickness(5);

            deadlineBlock.Children.Add(deadlineLabel);
            deadlineBlock.Children.Add(deadlineProgress);

            DeadlinesBlock.Children.Add(deadlineBlock);
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedLabel = sender as Label;
            if (selectedLabel != null)
            {
                var selectedStackPanel = selectedLabel.Parent as StackPanel;
                if (selectedStackPanel != null)
                {
                    var deadlineIndex = DeadlinesBlock.Children.IndexOf(selectedStackPanel); //TO DO: найти дедлайн и передать страницу
                    NavigationService.Navigate(new DeadlineDetails());
                }
            }
        }
    }
}
