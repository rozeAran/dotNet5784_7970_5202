﻿using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskForListWindow.xaml
    /// </summary>
    public partial class TaskForListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Status Status { get; set; } = BO.Status.Status;
        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.Level;

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register(nameof(TaskList), typeof(IEnumerable<BO.Task>), typeof(TaskForListWindow));
        public IEnumerable<BO.Task?>? TaskList
        {
            get => (IEnumerable<BO.Task>)GetValue(TaskListProperty);
            set => SetValue(TaskListProperty, value);
        }
       
        public TaskForListWindow()
        {
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll()!;
        }

        private void ComboBoxLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (Experience != BO.EngineerExperience.Level) ?
                s_bl?.Task.ReadAll(item => item.Complexity == Experience) : s_bl?.Task.ReadAll();
        }

        private void ComboBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (Status != BO.Status.Status) ?
                 s_bl?.Task.ReadAll(item => item.TaskStatus == Status) : s_bl?.Task.ReadAll();
        }

        private void ListView_OpenTaskWindow(object sender, RoutedEventArgs e)
        {
            BO.Task? tsk = (sender as ListView)?.SelectedItem as BO.Task;
            new TaskWindow(tsk.Id).ShowDialog();

        }

        private void Button_Click_Add_Task(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
        }
    }
}
