using DotObjTools.DOM;
using DotObjTools.Parser;
using GUISection.Extensions;
using GUISection.Viewports;
using MatrixTools;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUISection;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private CustomCamera _camera = new CustomCamera
    {
        ZFar = 100f,
        ZNear = 0.1f,
        WorldPosition = new Vector3(100, 100, 0),
        Target = new Vector3(0, 0, 0),
        Up = new Vector3(0, 0, 1)
    };

    private (int, int)[]? _modelLines;
    private Vector4[]? _initialGeometricVertices;
    private Vector4[]? _geometricVertices;
    private Brush _brush = new SolidColorBrush(Color.FromRgb(255,0,0));
    
    public MainWindow()
    {
        InitializeComponent();
        Dispatcher.UnhandledException += (sender, args) => 
        {
            mCanvas.Children.Clear();
            Label label = new Label
            {
                Content = args.Exception.Message,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Red)
            };
            mCanvas.Children.Add(label);
        };
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() is true) 
        { 
            DotObjParser parser = new DotObjParser();
            DotObjDOM dom = parser.Parse(openFileDialog.FileName);
            _initialGeometricVertices = dom.GeometricVertices;
            _geometricVertices = dom.GeometricVertices;
            _modelLines = dom.getLines();
            CalculatePoints();
            mCanvas.Children.Clear();
            PaintModel();
        }
    }

    private void CalculatePoints()
    {
        Matrix4x4 viewport = Matrices.Viewport.FromDimesions(0f, 0f, (float)mCanvas.ActualWidth, (float)mCanvas.ActualHeight);

        Matrix4x4 projection = Matrices.Projection.FromFOV(1f, 1.5f, _camera.ZNear, _camera.ZFar);

        Matrix4x4 translation = Matrices.Translation.FromVector(new Vector3(0, 2, 0));

        Matrix4x4 scale = Matrices.Scale.FromVector(new Vector3(0.1f, 0.01f, 0.01f));

        Matrix4x4 result = viewport * projection * scale * translation;

        _geometricVertices = _geometricVertices?.AsParallel().AsOrdered().WithDegreeOfParallelism(4)
            .Select(geometricVertex => Vector4.Transform(geometricVertex, result))
            .Select(newVertex => newVertex / newVertex.W)
            .ToArray();
    }
    
    private void PaintModel() 
    {       
        foreach (Line line in _modelLines.Select(modelLine => new Line
        {
            X1 = _geometricVertices[modelLine.Item1].X,
            Y1 = _geometricVertices[modelLine.Item1].Y,
            X2 = _geometricVertices[modelLine.Item2].X,
            Y2 = _geometricVertices[modelLine.Item2].Y,
            Stroke = _brush,
            StrokeThickness = 1
        }))
        {
            mCanvas.Children.Add(line);
        }
        MainMenu.Background = new SolidColorBrush(Color.FromRgb(255, 0, 255));
    }
}
