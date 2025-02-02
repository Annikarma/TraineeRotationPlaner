using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TraineeRotationPlaner.Core;
using TraineeRotationPlaner.Models;
using TraineeRotationPlaner.MVVM.Model;
using static System.Net.Mime.MediaTypeNames;

namespace TraineeRotationPlaner.MVVM.ViewModel
{
    /*
     * TraineeViewModel: Diese Klasse ist der ViewModel-Teil des MVVM-Designmusters. Sie enthält die Logik für die Anzeige von Azubis und stellt sicher, dass Änderungen in der Benutzeroberfläche synchronisiert werden, indem sie die INotifyPropertyChanged-Schnittstelle implementiert. Sie enthält auch Befehle, die die Benutzeroberfläche steuern, wie z. B. das Speichern von Trainees
     * 
     * TraineeViewModel: Verwaltet die Datenbindung und Benutzeroberfläche für Trainee in der Anwendung.
     * 
     * TraineeViewModel interagiert mit TraineeService, um die Ferien zu verwalten und speichert sie in einer ObservableCollection<Trainee>.
     * 
     */
    /* ToDo:
     * property an die combobox beruf binden welche alle existierende berufe enthält. professionobjekte (id, name,...)
     * wenn auf speichern geklickt, muss selektierte beruf in trainee objekt getan. services, mapper, repo die id enthält.
     * 
     */
    public class TraineeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged; // TODO ???

        private TraineeService _traineeService; // Deklaration. noch nicht gefüllt

        public int Id { get; set; }

        private string _lastName = string.Empty;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged();

            }
        }

        private string _abbreviation;
        public string Abbreviation
        {
            get
            {
                return _abbreviation;
            }
            set
            {
                _abbreviation = value;
                OnPropertyChanged();
            }
        }
        

        private DateOnly _educationStart;

        public DateOnly EducationStart
        {
            get 
            { 
                return _educationStart;
            }
            set
            {
                _educationStart = value;
                OnPropertyChanged();
            }
        }

        private DateOnly _educationEnd;
        public DateOnly EducationEnd
        {
            get
            {
                return _educationEnd;
            }
            set
            {
                _educationEnd = value;
                OnPropertyChanged();
            }
        }

        private Profession _profession;
        public Profession Profession
        {
            get
            {
                return _profession;
            }
            set
            {
                _profession = value;
                OnPropertyChanged();
            }
        }


        // Liste aller gesammelter Trainees - Liste inital erstellen
        public ObservableCollection<Trainee> Trainees { get; set; } = new();

        private Trainee _selectedTrainee; 

        private Trainee SelectedTrainee
        {
            get
            {
                return _selectedTrainee;
            }
            set
            {
                _selectedTrainee = value;
                Id = _selectedTrainee.Id;
                LastName = _selectedTrainee.LastName;
                FirstName = _selectedTrainee.FirstName;
                Abbreviation = _selectedTrainee.Abbreviation;
                EducationStart = _selectedTrainee.EducationStart;
                EducationEnd = _selectedTrainee.EducationEnd;
                Profession = _selectedTrainee.Profession;              
            }
        }

        public RelayCommand SaveTraineeCommand { get; set; }

        /// <summary>
        /// Konstruktor füllt die Variable _traineeService mit einem TraineeService
        /// </summary>
        public TraineeViewModel()
        {
            _traineeService = new TraineeService(); // TraineeService wird erstellt/Initialisiert. 
            var trainees = _traineeService.Get(); // sobald der Service da ist, werden mit get alle Trainees über das Repo aus DB gefüllt
            Trainees = new ObservableCollection<Trainee>(trainees); // Trainees binding im xaml


            SaveTraineeCommand = new RelayCommand(o =>
            {
                Trainee trainee = new Trainee(Id, LastName, FirstName, Abbreviation, EducationStart, EducationEnd, Profession ); // Objekt für Datentransport erstellen und füllen
                _traineeService.Save(trainee); // Objekt über den Service speichern
                Trainees.Add(trainee); // Hinzufügen des gespeicherten Trainee-Objektes in ListView
            });
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs(name));
        }
    }
}


