﻿using DotObjTools.DOM;
using DotObjTools.Parser;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace GUISection;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private (Vector4, Vector4)[]? modelLines;
    private (Point, Point)[]? actualLines;
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = ".obj";
        if (openFileDialog.ShowDialog() is true) 
        { 
            DotObjParser parser = new DotObjParser();
            DotObjDOM dom = parser.Parse(openFileDialog.FileName);
        }
    }
}