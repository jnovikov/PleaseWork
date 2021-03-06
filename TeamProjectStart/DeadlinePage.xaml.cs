﻿using System;
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
    /// Логика взаимодействия для DeadlinePage.xaml
    /// </summary>
    public partial class DeadlinePage : Page
    {
        public ApiData apiData = new ApiData();

        public DeadlinePage()
        {
            InitializeComponent();
            Loaded += OnLoad;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {

            UpdateDeadlines();
        }

        private async void UpdateDeadlines()
        {
            listBoxDeadlines.ItemsSource = null;
            listBoxDeadlines.ItemsSource = await apiData.GetDeadlines();
        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TodayPlan());
        }

        private void buttonAddDeadline_Click(object sender, RoutedEventArgs e)
        {
            var addDeadlinePage = new AddDeadline();
            NavigationService.Navigate(addDeadlinePage);
            UpdateDeadlines();
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedDeadline = listBoxDeadlines.SelectedItem as Deadline;
                if (selectedDeadline == null)
                    MessageBox.Show("Выберите дедлайн для удаления", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    if (MessageBox.Show("Вы действительно хотите удалить дедлайн?", "Question",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        await apiData.DeleteDeadline(selectedDeadline.Id);
                        UpdateDeadlines();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedDeadline = listBoxDeadlines.SelectedItem as Deadline;
                if (selectedDeadline == null)
                    MessageBox.Show("Выберите дедлайн, который Вы хотите открыть.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                var deadlineDetailsPage = new DeadlineDetails(selectedDeadline);
                NavigationService.Navigate(deadlineDetailsPage);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedDeadline = listBoxDeadlines.SelectedItem as Deadline;
            if (selectedDeadline == null)
                MessageBox.Show("Выберите дедлайн для изменения.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            NavigationService.Navigate(new DeadlineEdit(selectedDeadline));
        }
    }
}
