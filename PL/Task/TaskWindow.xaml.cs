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
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public TaskWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_List(object sender, RoutedEventArgs e)
        {
            new TaskForListWindow().ShowDialog();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            //if not finished bulidlig schedule then add task
        }
    }
}
