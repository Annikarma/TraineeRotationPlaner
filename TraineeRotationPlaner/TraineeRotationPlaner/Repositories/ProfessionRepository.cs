using Dapper;
using Microsoft.Data.Sqlite;
using System.Windows;
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
                settingCoreValues();

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

        public void settingCoreValues()
        {
            string countValuesInDB = "SELECT COUNT(*) FROM Professions";
            int count = Connection.ExecuteScalar<int>(countValuesInDB); // Scalar: gibt einen einzelnen Wert zurück

            if (count == 0)
            {
                string insertSql = """
                    INSERT INTO Professions (Name, Abbreviation) VALUES 
                    (@Name1, @Abbreviation1),
                    (@Name2, @Abbreviation2),
                    (@Name3, @Abbreviation3),
                    (@Name4, @Abbreviation4);
                    """;

                var coreParam = new
                {
                    Name1 = "Anwendungsentwicklung",
                    Abbreviation1 = "FiAe",
                    Name2 = "Systemintegration",
                    Abbreviation2 = "FiSy",
                    Name3 = "KaufmannIT",
                    Abbreviation3 = "KfIT",
                    Name4 = "Kaufmann Büro",
                    Abbreviation4 = "KfB",
                };

                try
                {
                    Connection?.Execute(insertSql, coreParam);
                }
                catch
                {
                    MessageBox.Show("Ups");
                    throw;
                }
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
                Name = profession.ProfessionName,
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
                Name = profession.ProfessionName,
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
