using TraineeRotationPlaner.Core;

namespace TraineeRotationPlaner.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        // Command-Eigenschaften, die in der View verwendet werden
        public RelayCommand TraineeViewCommand { get; set; }
        public RelayCommand ProfessionViewCommand { get; set; }


        // Instanzen deklarieren
        public TraineeViewModel TraineeVM { get; set; }
        public ProfessionViewModel ProfessionVM { get; set; }


        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            TraineeVM = new TraineeViewModel();
            ProfessionVM = new ProfessionViewModel();

            CurrentView = TraineeVM;

            TraineeViewCommand = new RelayCommand(o => 
            {
                CurrentView = TraineeVM;
            });


            ProfessionViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProfessionVM;
            });


            //    // alternative Schreibweise zum Lambda-Ausdruck

            //    ProfessionViewCommand = new RelayCommand(MeineExecuteProfessionalViewCommand);
            //}

            //public void MeineExecuteProfessionalViewCommand(object Parameter)
            //{
            //    // Console.WriteLine("ProfessionViewCommand wurde ausgeführt.");
            //    CurrentView = ProfessionVM;
            //}
        }
    }
}
