using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Documents;
using TraineeRotationPlaner.Entities;
using TraineeRotationPlaner.Mapper;
using TraineeRotationPlaner.Models;
using TraineeRotationPlaner.MVVM.Model;

namespace TraineeRotationPlaner.Repositories
{
    // Diese Klasse kümmert sich um die Verwaltung der Trainee-Daten in der SQLite-Datenbank.
    // Sie enthält Methoden zum Erstellen, Lesen, Aktualisieren und Löschen von Trainees.
    public class TraineeRepository : BaseRepository
    {
        // Der Konstruktor wird verwendet, um die Tabelle zu erstellen, wenn die Klasse instanziiert wird.
        // TODO: FRAGE: wann wird sie instanziiert
        public TraineeRepository()
        {
            CreateTable(); // Methode sorgt dafür, dass die Tabelle für die Trainees existiert.
        }

        // TODO: Hier die Fremdschlüssel nullable machen, damit auch leere Berufe möglich sind.
        private void CreateTable()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS Trainees (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,  
                    LastName TEXT NOT NULL, 
                    FirstName TEXT NOT NULL, 
                    Abbreviation TEXT NOT NULL,  
                    EducationStart DATE NOT NULL, 
                    EducationEnd DATE NOT NULL,  
                    EducationYear INTEGER NOT NULL,  
                    ProfessionId INTEGER, 
                    FOREIGN KEY (ProfessionId) REFERENCES Professions(Id)  
                );
                """;
            try
            {
                Connection?.Execute(sql);
            }
            catch (SqliteException ex)
            {
                throw new SqliteException("Fehler beim Erstellen der Trainee-Tabelle.", ex.SqliteErrorCode, ex.ErrorCode);
            }
            catch (Exception ex)
            {
                throw new Exception("Unbekannter Fehler beim Erstellen der Trainee-Tabelle", ex);
            }
            //finally
            //{
            //    Connection?.Dispose();
            //}
        }

        public void Create(Trainee trainee)
        {
            if (trainee == null)
            {
                throw new ArgumentException("Trainee darf nicht null sein.");
                // Prüfe, ob das Trainee-Objekt gültig ist
                if (string.IsNullOrEmpty(trainee.LastName) || trainee.Profession == null)
                {
                    throw new InvalidOperationException("Trainee must have a valid name and profession.");
                }
            }

            string sql = """ 
            INSERT INTO Trainees(LastName, FirstName, Abbreviation, EducationStart, EducationEnd, EducationYear,  ProfessionId) 
            VALUES(@LastName, @FirstName, @Abbreviation, @EducationStart, @EducationEnd, @EducationYear,  @ProfessionId);
            """;

            // Werte aus Trainee zu TraineeEntity konvertieren, um Datum in Text umzuwandeln
            var traineeEntity = TraineeMapper.MapToEntity(trainee);

            //var 

            object param = new
            {
                LastName = traineeEntity.LastName,
                FirstName = traineeEntity.FirstName,
                Abbreviation = traineeEntity.Abbreviation,
                EducationStart = traineeEntity.EducationStart,
                EducationEnd = traineeEntity.EducationEnd,
                EducationYear = traineeEntity.EducationYear,
                ProfessionId = traineeEntity.ProfessionId
            };

            try
            {
                Connection?.Execute(sql, param);
            }
            catch (SqliteException ex)
            {
                // Fehler beim Ausführen der SQL-Anweisung in der SQLite-Datenbank.
                throw new SqliteException("Fehler beim Einfügen des Trainees.", ex.ErrorCode);
            }
            catch (Exception ex)
            {
                // Allgemeiner Fehler bei der Verarbeitung.
                throw new InvalidOperationException("Ein unbekannter Fehler trat beim Erstellen des Trainees auf.", ex);
            }

            //finally
            //{
            //    Connection?.Close(); //ist nicht nötig,  using verwendet wird
            //}
        }

        public List<Trainee> Read()
        {

            string sql = """
                   SELECT 
            t.Id, 
            t.LastName, 
            t.FirstName, 
            t.Abbreviation,
            t.ProfessionId, 
            p.Name AS ProfessionName, 
            t.EducationStart, 
            t.EducationEnd, 
            t.EducationYear
        FROM 
            Trainees t
        INNER JOIN 
            Professions p ON t.ProfessionId = p.Id;
    """;

            try
            {
                /*
                 *  Verbindung zur DB herstellen (Versuch - try)
                 *  Abfrage ausführen
                 *  diese Abfrage gespeichert in schoolEntities
                 *  wenn Abfrage null ergibt, dann erstelle neue leere Liste
                 */
                var traineeEntities = Connection?.Query<TraineeEntity>(sql).ToList() ?? new();

                var trainees = new List<Trainee>();
                foreach (var traineeEntity in traineeEntities)
                {
                    var trainee = TraineeMapper.MapToModel(traineeEntity);
                    trainees.Add(trainee);
                }

                return trainees;
            }
            catch (SqliteException ex)
            {
                MessageBox.Show("", ex.Message);
                // Fehler bei der Datenbankabfrage.
                throw new SqliteException("Fehler beim Abrufen der Trainees.", ex.ErrorCode);
            }
            catch (Exception ex)
            {
                // Allgemeiner Fehler bei der Verarbeitung.
                throw new InvalidOperationException("Ein unbekannter Fehler trat beim Abrufen der Trainees auf.", ex);
            }
            //finally
            //{
            //    Connection?.Close();
            //}
        }

        // TODO [Info]: Ggf. wird diese Methode gar nicht mehr benötigt
        public Trainee Read(int id)
        {
            string sql = """
                t.Id, 
                    t.LastName, 
                    t.FirstName, 
                    t.Abbreviation, 
                    t.EducationStart, 
                    t.EducationEnd, 
                    t.EducationYear, 
                    p.Name AS Profession 
                FROM 
                    Trainees t
                INNER JOIN 
                    Professions p ON t.ProfessionId = p.Id;
                """;

            var param = new
            {
                Id = id
            };

            try
            {
                var trainee = Connection?.QuerySingle<Trainee>(sql, param);
                return trainee ?? throw new SqliteException("Error on reading specific trainee (no connection?)", 14);
            }
            catch (Exception ex)
            {
                throw;
            }
            //finally
            //{
            //    Connection?.Close();
            //}



        }

        public void Update(Trainee trainee)
        {
            if (trainee == null)
            {
                throw new ArgumentException("Trainee darf nicht null sein.");
            }

            string sql = """ 
                UPDATE Trainees
                SET LastName = @LastName,
                FirstName = @FirstName,
                Abbreviation = @Abbreviation,
                EducationStart = @EducationStart,
                EducationEnd = @EducationEnd,
                EducationYear = @EducationYear,
                ProfessionId = @ProfessionId
                WHERE Id = @Id;
                """;

            var traineeEntity = TraineeMapper.MapToEntity(trainee);

            var param = new
            {
                LastName = traineeEntity.LastName,
                FirstName = traineeEntity.FirstName,
                Abbreviation = traineeEntity.Abbreviation,
                EducationStart = traineeEntity.EducationStart,
                EducationEnd = traineeEntity.EducationEnd,
                EducationYear = traineeEntity.EducationYear,
                ProfessionId = traineeEntity.ProfessionId,
            };

            try
            {
                Connection?.Execute(sql, param);
            }
            catch (SqliteException ex)
            {
                // Fehler beim Aktualisieren eines Trainees.
                throw new SqliteException("Fehler beim Aktualisieren des Trainees.", ex.ErrorCode);
            }
            catch (Exception ex)
            {
                // Allgemeiner Fehler bei der Verarbeitung.
                throw new InvalidOperationException("Ein unbekannter Fehler trat beim Aktualisieren des Trainees auf.", ex);
            }
            finally
            {
                Connection?.Close();
            }
        }

        public void Delete(int id)
        {
            string sql = """
            DELETE FROM Trainee WHERE Id = @Id
            """;

            var param = new { Id = id };

            try
            {
                Connection?.Execute(sql, param);
            }
            catch (SqliteException ex)
            {
                // Fehler beim Löschen eines Trainees.
                throw new SqliteException("Fehler beim Löschen des Trainees.", ex.ErrorCode);
            }
            catch (Exception ex)
            {
                // Allgemeiner Fehler bei der Verarbeitung.
                throw new InvalidOperationException("Ein unbekannter Fehler trat beim Löschen des Trainees auf.", ex);
            }
            finally
            {
                Connection?.Close();
            }
        }
    }
}
/*
 * SqliteException: Diese Ausnahme ist spezifisch für SQLite-Datenbankfehler. 
 * wird geworfen, wenn ein Problem mit der Datenbankverbindung oder der SQL-Ausführung auftritt.
 * 
 * InvalidOperationException: 
 * Wenn ein Fehler auftritt, der nicht direkt mit der SQLite-Datenbank zu tun hat, wird eine InvalidOperationException geworfen. 
 * Diese Ausnahme ist sinnvoll, wenn ein Zustand der Anwendung ungültig ist oder ein nicht spezifizierter Fehler auftritt.
 * z.B., wenn Verbindung zur Datenbank bereits geschlossen wurde oder ein ungültiger Zustand in der Anwendung vorliegt.
 *
 * ArgumentException: 
 * Wird geworfen, wenn ungültige Argumente übergeben werden, wie z.B. ein null-Wert für den Trainee, der in die Datenbank eingefügt werden soll.
 *
 *
 *
 * HINWEISE
 * ProfessionId INTEGER,: Der Fremdschlüssel ProfessionId ist jetzt nicht mehr NOT NULL, was bedeutet, dass er auch NULL sein kann.
Dies erlaubt es, Trainees ohne einen zugewiesenen Beruf zu speichern, also ProfessionId kann leer (null) sein.

Wenn ein NULL für ProfessionId gespeichert wird, muss der referenzierte ProfessionId in der Profession-Tabelle nicht existieren. 
Das bedeutet, dass es möglich ist, einen Trainee ohne Beruf zu haben, aber der Eintrag in der Trainees-Tabelle verweist dann nicht auf einen Profession-Eintrag.
*/