using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;


namespace UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void StartProgram(object sender, RoutedEventArgs e)
    {
        string secondAppPath = @"C:\Users\User\RiderProjects\Entity Framework\SecondProgram\SecondProgram\bin\Debug\net9.0\SecondProgram.exe";
        string debug = DebugCheckBox.IsChecked == true ? "true" : "false";
        Process.Start(secondAppPath, new string[] { "1", "6", @"C:\Users\User\RiderProjects\Entity Framework\UI\UI\Logs.txt" , debug });
        Application.Current.Shutdown();
    }
}