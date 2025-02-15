using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TraineeRotationPlaner.Core;
using TraineeRotationPlaner.Models;
using TraineeRotationPlaner.MVVM.Model;

namespace TraineeRotationPlaner.MVVM.ViewModel
{
    internal class ProfessionViewModel : INotifyPropertyChanged
    {
        // private ProfessionService _professionService;
        // von hier aus direkt die static aus der Main aufrufen

        public int Id { get; set; }

        private string _name { get; set; } = string.Empty;
        public string Name // Sobald der Setter überschreiben wird, muss ein privates Field als Hilfsvariable verwendet werden!
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        /*
         * Eigenschaften (wie Name, Abbreviation, SelectedProfession) stellen die privaten Felder öffentlich zur Verfügung und 
         * ermöglichen es, dass das UI automatisch auf Änderungen reagiert (durch die Verwendung von INotifyPropertyChanged und der OnPropertyChanged-Methode).
         */

        private string _abbreviation { get; set; } = string.Empty;
        public string Abbreviation 
        {
            get { return _abbreviation; }
            set
            {
                _abbreviation = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Profession> Professions { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        private Profession _selectedProfession;

        // SelectedProfession-Eigenschaft definieren, die das aktuell ausgewählte Element aus der ListView hält.
        public Profession SelectedProfession
        {
            get { return _selectedProfession; }
            set // Dieser Setter ist nur überschrieben worden, um die Werte des selektierten Profession Eintrags in die Textboxen zu übertragen
            {
                _selectedProfession = value;
                Id = _selectedProfession.Id;
                Name = _selectedProfession.ProfessionName; // Wenn der name des selektierten Profession-Objekt in PreName gesetzt wird, dann wird das UI aktualisiert
                Abbreviation = _selectedProfession.Abbreviation;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveProfessionCommand { get; set; }

        public ProfessionViewModel()
        {
            //_professionService = new ProfessionService();
            // service aus mehreren klassen aufrufen.... wird erspart. weil von überalls --> ressourcen schonend !!!
            // roslin (compiler) --> wenn variabel die verweist auf andere, dann nur referenz auf arbeitsspeicher...

            var professions = MainWindow.ProfessionService.Get();
            Professions = new ObservableCollection<Profession>(professions);

            SaveProfessionCommand = new RelayCommand(o =>
            {
                Profession profession = new Profession(Id, Name, Abbreviation); // Objekt für Datentransport erstellen und füllen
                MainWindow.ProfessionService.Save(profession); // Objekt über Service speichern lassen
                Professions.Add(profession); // Hinzufügen des erfolreich gespeicherten Vacation-Objektes zur Collection/LiestView
            });           
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

/*
 * 
 * Im Wesentlichen stellt sie die Daten für die Benutzeroberfläche zur Verfügung und sorgt dafür, 
 * dass die Benutzeroberfläche automatisch aktualisiert wird, wenn sich diese Daten ändern.
 */