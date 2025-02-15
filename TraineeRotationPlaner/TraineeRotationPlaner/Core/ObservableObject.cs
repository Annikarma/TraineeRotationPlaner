using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TraineeRotationPlaner.Core
{
    public class ObservableObject : INotifyPropertyChanged
    {
        /*
         Die Klasse ObservableObject stellt die Funktionalität bereit, um Änderungen an Eigenschaften zu melden.
        Die Methode OnPropertyChanged ruft das PropertyChanged-Event aus, das Abonnenten (z. B. die UI) informiert.
        [CallerMemberName] sorgt dafür, dass der PreName der geänderten Eigenschaft automatisch an OnPropertyChanged übergeben wird.
        Ergebnis: Änderungen an Eigenschaften werden erkannt, und die Benutzeroberfläche bleibt aktuell, ohne zusätzlichen manuellen Aufwand.
         */

        // PropertyChanged ist ein Event vom Typ PropertyChangedEventHandler
        /* Event ist in C# eine Art Benachrichtigung, die ausgelöst wird, 
         * wenn etwas Bestimmtes passiert – in diesem Fall eine Änderung an einer Eigenschaft. */
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
/*
 * Ein Event ist eine spezielle Art von Delegaten, der in C# verwendet wird, um andere Teile des Programms zu benachrichtigen. 
 * Hier handelt es sich um das PropertyChanged-Event, 
 * das die Benutzeroberfläche (oder andere Teile der Anwendung) darüber informiert, dass eine Eigenschaft geändert wurde.
 * 
 * In einer typischen MVVM-Architektur (Model-View-ViewModel) sorgt das PropertyChanged-Event dafür, 
 * dass die View (Benutzeroberfläche) automatisch aktualisiert wird, wenn sich ein Wert im ViewModel ändert. 
 * Dies ist notwendig, um eine Datenbindung in Anwendungen wie WPF oder Xamarin zu ermöglichen, 
 * bei denen sich die Ansicht automatisch aktualisiert, wenn sich die zugrunde liegenden Daten ändern.
 * 
 * Der Teil, der das Event tatsächlich auslöst - Invoke
 * 1) Invoke ist die Methode, mit der ein Event ausgelöst wird. Sie übergibt die Argumente an alle Abonnenten des Events.
 * 2) this repräsentiert das Objekt, das das Event auslöst – also die Instanz der Klasse, in der PropertyChanged definiert ist. 
 * Normalerweise handelt es sich dabei um ein ViewModel oder ein Model, das seine Eigenschaften geändert hat.
 * this wird an die Abonnenten weitergegeben, damit diese wissen, welches Objekt das Event ausgelöst hat.
 * 3) PropertyChangedEventArgs ist eine Klasse, die die Ereignisargumente für das PropertyChanged-Event enthält. 
 * Sie benötigt den Namen der Eigenschaft, die geändert wurde.name ist der PreName der geänderten Eigenschaft. 
 * In der Praxis wird dieser oft durch das Attribut [CallerMemberName] automatisch gesetzt, 
 * aber in der manuellen Implementierung ist name der PreName der geänderten Eigenschaft.
 * 
 * Bsp. Wenn Eigenschaft PreName geändert wird, wird der PreName der geänderten Eigenschaft als Argument (name = "PreName") an PropertyChangedEventArgs übergeben.
 * 
 * 1 - Wenn Abonnenten für das PropertyChanged-Event existieren (es ist also nicht null), wird das Event ausgelöst.
 * 2 - Die Methode Invoke benachrichtigt alle Abonnenten, dass sich eine Eigenschaft geändert hat.
 * 3 - this stellt das Objekt dar, das die Änderung vorgenommen hat.
 * 4 - new PropertyChangedEventArgs(name) gibt den Namen der geänderten Eigenschaft weiter, sodass Abonnenten wissen, welche Eigenschaft geändert wurde.
 * 
 */