namespace TraineeRotationPlaner.Models
{
    /// <summary>
    /// Repräsentiert die Daten eines Azubis.
    /// </summary>
    public class Trainee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Abbreviation { get; set; } 
        public DateOnly EducationStart { get; set; } 
        public DateOnly EducationEnd { get; set; }
        public int ProfessionId { get; set; } // Primär für die Speicherung
        public Profession? Profession { get; set; } // Optional für einfacheren Zugriff

        //TODO: Überlegung: Wie oft wird in der UI auf den Beruf zugegriffen? Falls häufig, wäre ein Profession-Objekt sinnvoll. Falls selten, reicht die ProfessionId
        public Trainee()
        {
            
        }

        public Trainee(int id, string lastName, string firstName, string abbreviation, DateOnly educationStart, DateOnly educationEnd, Profession profession)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Abbreviation = abbreviation;
            EducationStart = educationStart;
            EducationEnd = educationEnd;
            Profession = profession;
        }

        // TODO: oder so?
        public Trainee(int id, string lastName, string firstName, string abbreviation, DateOnly educationStart, DateOnly educationEnd, int professionId, Profession profession)
        {
            Id = id;
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            Abbreviation = abbreviation ?? throw new ArgumentNullException(nameof(abbreviation));
            EducationStart = educationStart;
            EducationEnd = educationEnd;

            ProfessionId = professionId;
            Profession = profession ?? throw new ArgumentNullException(nameof(profession));

            if (Profession.Id != professionId)
            {
                throw new ArgumentException("Die Profession-ID stimmt nicht mit dem übergebenen Objekt überein.");
            }
        }
    }
}
/*Rolle: Datenmodell für einen einzelnen Azubi
Aufgabe:
Diese Klasse repräsentiert die Daten eines einzelnen Azubis. 
Sie dient als "Container", um die Eigenschaften eines Azubis (z. B. Vorname, Nachname, ID) darzustellen.
Sie enthält keine Geschäftslogik, sondern nur: Vorname, Nachname, ID, Displayname

 * AzubiModel kapselt die Daten eines einzelnen Azubis.

Beispiel: Wenn in der View eine Liste von Azubis angezeigt wird,
besteht diese aus AzubiModel-Objekten. 
Diese enthalten die notwendigen Eigenschaften, die in der UI dargestellt werden.
*
*  folgt dem Single-Responsibility-Prinzip:
AzubiModel: Verwaltet nur die Daten eines einzelnen Azubis.
AzubiVerwaltungsViewModel: Verwaltet die Liste von Azubis und die Logik zur Datenbearbeitung.
 */