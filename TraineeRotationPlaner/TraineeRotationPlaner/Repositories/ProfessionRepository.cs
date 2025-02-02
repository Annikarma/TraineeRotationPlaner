using Dapper;
using Microsoft.Data.Sqlite;
using TraineeRotationPlaner.Models;

namespace TraineeRotationPlaner.Repositories
{
    public class ProfessionRepository : BaseRepository
    {
        public ProfessionRepository(string? connectionString = null) : base(connectionString)
        {
            try
            {
                CreateTable();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Fehler beim Erstellen der ProfessionRepository-Instanz. Überprüfe die Verbindung und Tabellenstruktur", ex);
            }
        }

        private void CreateTable()
        {
            string sql = """
                    CREATE TABLE IF NOT EXISTS Professions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Abbreviation TEXT NOT NULL
                    );
                """;

            try
            {
                Connection?.Execute(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating table Professions: {ex.Message}");
                throw;
            }
            finally
            {
                Connection?.Close();
            }

        }



        /// <summary>
        /// Add a new Profession in DB.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="abbreviation"></param>


        public void Create(Profession profession)
        {
            string sql = """
                INSERT INTO Professions (Name, Abbreviation)
                VALUES (@Name, @Abbreviation);
                """;

            object param = new
            {
                Name = profession.Name,
                Abbreviation = profession.Abbreviation,
            };

            try
            {
                Connection?.Execute(sql, param);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Connection?.Close();
            }
        }

        public List<Profession> Read()
        {
            string sql = """
                SELECT Id, Name, Abbreviation FROM Professions
                """;

            try
            {
                var result = Connection?.Query<Profession>(sql).ToList();
                return result ?? new List<Profession>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler: ", ex);
            }
            finally
            {
                Connection?.Close();
            }
        }

        public Profession Read(int id)
        {
            string sql = """
                SELECT Id, Name, Abbreviation FROM Professions WHERE Id = @Id;
                """;

            var param = new
            {
                Id = id
            };


            try
            {
                var result = Connection?.QuerySingle<Profession>(sql, param);
                return result ?? throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler: ", ex);
            }
            finally
            {
                Connection?.Close();
            }
        }

        public void Update(Profession profession)
        {
            string sql = """ 
                UPDATE Professions
                SET Name = @Name, 
                Abbreviation = @Abbreviation,
                WHERE Id = @Id;
            """;

            var param = new
            {
                Name = profession.Name,
                Abbreviation = profession.Abbreviation,
            };

            try
            {
                Connection?.Execute(sql, param);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Connection?.Close();
            }
        }

        public void Delete(int id)
        {
            string sql = """
                DELETE FROM Profession WHERE Id = @Id
                """;

            var param = new { Id = id };

            try
            {
                Connection?.Execute(sql, param);
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler: ", ex);
            }
            finally
            {
                Connection?.Close();
            }
        }       
    }
}
