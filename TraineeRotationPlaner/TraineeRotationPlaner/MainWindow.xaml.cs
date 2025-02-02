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
using TraineeRotationPlaner.MVVM.Model;

namespace TraineeRotationPlaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// hier initialisieren ich die beiden Services als Static und mache diese somit im gesamten Code direkt verfügbar
        /// Hierdurch muss das repository nur einmalig initialisiert werden und spart somit weitere ressourcen
        /// Lösung --> in eine eigene dafür ausgelegte klasse exportieren ???

        /// probleme mit multithreading könnten auftreten. 
        /// - Microsoft.Extensions.Hosting
        /// - https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked?view=net-9.0&redirectedfrom=MSDN
        /// - https://learn.microsoft.com/en-us/dotnet/standard/collections/thread-safe/
        public static ProfessionService ProfessionService;
        public static TraineeService TraineeService;

        public MainWindow()
        {
            MainWindow.ProfessionService = new ProfessionService();
            MainWindow.TraineeService = new TraineeService();

            InitializeComponent();
        }
    }
}
