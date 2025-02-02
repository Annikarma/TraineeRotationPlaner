using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using TraineeRotationPlaner.MVVM.Model;

namespace TraineeRotationPlaner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
     
        // biede services instanziieren
            // dann service von überall abrufbar
        public static ProfessionService PService = new ProfessionService();
        public static TraineeService TService = new TraineeService();
    }

}
