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
    /// Логика взаимодействия для AddDetail.xaml
    /// </summary>
    public partial class AddDetail : Page
    {
        public event Action<Task> TaskAdded;

        public AddDetail()
        {
            InitializeComponent();
        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void buttonAddDeadlineDetail_Click(object sender, RoutedEventArgs e)
        {
        //    if (!string.IsNullOrWhiteSpace(TextBoxDescription.Text))
        //    {
        //        TaskAdded?.Invoke(new Task
        //        {
        //            Description = TextBoxDescription.Text
        //        });
        //        NavigationService.GoBack();
        //    }

        }
    }
}
