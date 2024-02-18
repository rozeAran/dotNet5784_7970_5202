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

namespace PL.Engineer;

/// <summary>
/// Interaction logic for AddEngineer.xaml
/// </summary>
public partial class AddEngineer : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    BO.Engineer eng;
    public AddEngineer(int id=0)
    {
        InitializeComponent();
        if (id == 0)
            eng = new BO.Engineer();
        else
            eng = s_bl?.Engineer.Read(id)!;
    }
}
