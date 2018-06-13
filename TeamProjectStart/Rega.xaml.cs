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
    /// Логика взаимодействия для Rega.xaml
    /// </summary>
    public partial class Rega : Page
    {
        public Rega()
        {
            InitializeComponent();
        }

        private async void RegisterClick(object sender, RoutedEventArgs e)
        {
            var name = textBoxName.Text;
            var email = textBoxEmail.Text;
            var password = passwordBoxPassword.Password;
            var controller = new AuthController();

            var result =  await controller.Register(email, password, name);
            if (result != null)
            {
                MessageBox.Show(result.ErrorMessage);
            } else
            {
                MessageBox.Show("OK");
            }


        }
    }
}
