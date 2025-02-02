using Microsoft.Data.Sqlite;

namespace TraineeRotationPlaner.Repositories
{
    /// <summary>
    /// Stellt Methoden bereit, um Verbindungen zu öffnen, schließen, Transaktionen zu verwalten und DB abzufragen
    /// </summary>
    public class BaseRepository : IDisposable
    {
        // TODO [Should]: Diese Info soll zukünftig bitte aus einer Konfigurationsdatei geladen werden
        private readonly string _defaultConnectionString = "Data Source = TraineeRotationDB.db";

        public SqliteConnection? Connection;

        public BaseRepository(string? connectionString = null)
        {
            try
            {  
                connectionString ??= _defaultConnectionString;
                Connection = new SqliteConnection(connectionString);
            } 
            catch (Exception ex) 
            {
                throw; 
            }
        }

        public void Dispose()
        {
            if (Connection != null) 
            { 
                Connection.Dispose();
                Connection = null;
            }
        }
    }
}
