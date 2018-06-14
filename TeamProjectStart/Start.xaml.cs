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
using TeamProjectStart.Helpers;

namespace TeamProjectStart
{
    /// <summary>
    /// Логика взаимодействия для start.xaml
    /// </summary>
    public partial class start : Page
    {
        public start()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Rega());
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ac = new AuthController();
                var email = textBoxEmail.Text;
                var password = textBoxPassword.Password;
                ApiError error;
                var result = ac.Login(email, password, out error);
                if (error != null)
                {
                    MessageBox.Show(error.ErrorMessage);
                }
                else
                {
                    TokenClient.Token = result;
                    NavigationService.Navigate(new TodayPlan());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
