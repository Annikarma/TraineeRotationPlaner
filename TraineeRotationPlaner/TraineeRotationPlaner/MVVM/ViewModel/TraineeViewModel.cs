using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        public ObservableCollection<Profession> Professions { get; set; } = new ObservableCollection<Profession>();  // TODO: A
        public ICommand ExportCommand { get; } // TODO: Excel Export


        public event PropertyChangedEventHandler? PropertyChanged; 

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

        private int _educationYear;
        public int EducationYear
        {
            get
            {
                return _educationYear;
            }
            set
            { 
                _educationYear = value;
                OnPropertyChanged();
            }
        }

        private string _homebase;
        public string Homebase
        {
            get
            {
                return _homebase;
            }
            set
            {
                _homebase = value;
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
                EducationYear = _selectedTrainee.EducationYear;
                Homebase = _selectedTrainee.Homebase;
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
                Trainee trainee = new Trainee(Id, LastName, FirstName, Abbreviation, EducationStart, EducationEnd, EducationYear, Homebase, Profession ); // Objekt für Datentransport erstellen und füllen
                _traineeService.Save(trainee); // Objekt über den Service speichern
                Trainees.Add(trainee); // Hinzufügen des gespeicherten Trainee-Objektes in ListView
                var professions = MainWindow.ProfessionService.Get();  // Holt Berufe aus dem Service
                Professions = new ObservableCollection<Profession>(professions);  // Bindet die Berufe an die ObservableCollection
                
                // TODO: A Die Professions-Eigenschaft wird nun in der TraineeViewModel-Klasse mit einer Liste von Berufen gefüllt, die dann in der View gebunden wird.
            });

            ExportCommand = new RelayCommand(ExportToCsv); // TODO: Excel
        }

        private void ExportToCsv(object obj)
        {
            var csvContent = new StringBuilder();

            // Kopfzeile der CSV-Datei
            csvContent.AppendLine("Vorname,Nachname,Kürzel,Ausbildungsbeginn,Ausbildungsende,Lehrjahr,Beruf,Herkunft");

            // Durchlaufen aller Trainees und hinzufügen der Daten
            foreach (var trainee in Trainees)
            {
                // Für jedes Trainee-Objekt fügen wir eine Zeile in der CSV hinzu
                csvContent.AppendLine($"{trainee.FirstName},{trainee.LastName},{trainee.Abbreviation},{trainee.EducationStart.ToShortDateString()},{trainee.EducationEnd.ToShortDateString()},{trainee.EducationYear},{trainee.Profession.Name},{trainee.Homebase}");
            }

            // Datei speichern (z.B. unter "Trainees.csv")
            var filePath = "Trainees.csv";
            File.WriteAllText(filePath, csvContent.ToString());

            // Optional: Erfolgsnachricht anzeigen
            MessageBox.Show("Die CSV-Datei wurde erfolgreich exportiert!");
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs(name));
        }
    }
}

/*
 * 
 * Die Trainee-Klasse hat eine Profession-Eigenschaft, die eine Referenz auf die Profession enthält. In der ExportToCsv-Methode nutzen wir diese Verbindung, um den Beruf jedes Trainee-Objekts zu exportieren.
 * 
 * Professions enthält die Liste der Berufe, die in der ComboBox verwendet wird. 
 * Diese Liste in das Trainee-Objekt einbinden, wie auch für die ComboBox in der XAML .
 * 
 * CSV-Erstellung:

Die Methode ExportToCsv baut einen String zusammen, der alle Trainee-Daten enthält. Die Kopfzeile der CSV-Datei ist "Vorname,Nachname,Kürzel,Ausbildungsbeginn,Ausbildungsende,Lehrjahr,Beruf,Herkunft".
Für jedes Trainee-Objekt wird eine Zeile erzeugt, die die entsprechenden Werte enthält.
Verwendung ToShortDateString(), um die Daten im kurzen Datumsformat (z.B. 01.09.2023) darzustellen.
*
*Speichern der CSV:

Am Ende wird die CSV-Datei mit File.WriteAllText(filePath, csvContent.ToString()) gespeichert. 
Speicherort der Datei nach Bedarf anpassen.
Eine Bestätigungsmeldung (MessageBox.Show) informiert den Benutzer, dass die Datei erfolgreich exportiert wurde.
*
*
Button und Command:
Der Button in der XAML-Datei ist mit dem ExportCommand im ViewModel verbunden. Sobald der Button geklickt wird, wird die ExportToCsv-Methode aufgerufen.
*/
