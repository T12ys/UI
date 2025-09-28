using System.Configuration;
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
using System.IO;
using Path = System.IO.Path;

namespace UI;
public partial class MainWindow : Window
{
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(3);
    public MainWindow()
    {
        InitializeComponent();
    }
    private async void StartProgram(object sender, RoutedEventArgs e)
    {
        
        if (!_semaphore.Wait(0)) {
            MessageBox.Show("Максимальное количество запущенных приложений достигнуто!");
            return; }

        try
        {
            string exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            string rootFolder = Path.GetFullPath(Path.Combine(exeFolder, @"..\..\..\.."));
            string targetExe = "I_hate_working_with_files.exe";

            string secondAppPath = Directory.GetFiles(rootFolder, targetExe, SearchOption.AllDirectories).FirstOrDefault();
            if (secondAppPath == null)
            {
                MessageBox.Show($"{targetExe} не найден в папке: {rootFolder}");
                return;
            }

            string targetLog = "Logs.txt";
            string logPath = Directory.GetFiles(rootFolder, targetLog, SearchOption.AllDirectories).FirstOrDefault();
            if (logPath == null)
            {
                MessageBox.Show($"{targetLog} не найден в папке: {rootFolder}");
                return;
            }

            string debug = DebugCheckBox.IsChecked == true ? "true" : "false";
            
            string arguments = $"2 6 {logPath} {debug}";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = secondAppPath,
                    Arguments = arguments,
                    WorkingDirectory = Path.GetDirectoryName(secondAppPath)
                },
                EnableRaisingEvents = true
            };

            process.Exited += (s, args) =>
            {
                _semaphore.Release();
            };

            process.Start();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при запуске второго проекта:\n{ex.Message}");
            _semaphore.Release();
        }
    }

    
    



}