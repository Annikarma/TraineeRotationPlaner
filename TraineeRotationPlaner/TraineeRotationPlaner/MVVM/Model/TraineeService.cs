using Microsoft.Extensions.Logging;
using TraineeRotationPlaner.Models;
using TraineeRotationPlaner.Repositories;

namespace TraineeRotationPlaner.MVVM.Model
{
    /// <summary>
    /// Enthält die eigentlich Geschäftslogik zur Verwaltung aller Ferieneinträge.
    /// </summary>
    public class TraineeService
    {
        private TraineeRepository _traineeRepository;

        public TraineeService()
        {
            _traineeRepository = new TraineeRepository();
        }

        public void Save(Trainee trainee)
        {
            if (trainee == null)
            {
                throw new ArgumentNullException(nameof(trainee));
            }

            if (!Validate(trainee)) // Reminder, wo eine Validierung des Trainee-Objektes stattfinden kann
            {
                throw new ArgumentException(nameof(trainee));
            }

            try
            {
                if (trainee.Id == 0)
                {
                    _traineeRepository.Create(trainee); // Ist die Trainee.Id 0, dann bedeutet dies es handelt sich um einen neuen Eintrag
                }
                else
                {
                    _traineeRepository.Update(trainee); // Ist die Trainee.Id ungleich 0, dann bedeutet dies es handelt sich um einen existierenden Eintrag
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Saving trainee '{name}' failed", trainee.PreName);
                throw;
            }
        }

        public List<Trainee> Get()
        {
            try
            {
                var trainees = _traineeRepository.Read();
                return trainees;
            }
            catch (Exception ex)
            {
               // _logger?.LogError(ex, "Getting trainees failed");
                throw;
            }
        }

        // TODO [Info]: Ggf. brauchst du diese Methode gar nicht
        public Trainee Get(int id)
        {
            try
            {
                var trainee = _traineeRepository.Read(id);
                return trainee;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Remove(int id)
        {
            try
            {
                _traineeRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool Validate(Trainee trainee)
        {
            // TODO: Not implemented yet
            return true;
        }

    }
}
