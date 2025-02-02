using Microsoft.Extensions.Logging;
using TraineeRotationPlaner.Repositories;
using TraineeRotationPlaner.Models;
using TraineeRotationPlaner.MVVM.Model;

namespace TraineeRotationPlaner.MVVM.Model
{
    internal class ProfessionService
    {
        private ILogger? _logger; // Das Interface fürs Logging ist zwar schon da, aber es steckt noch keine Logger-Instanz drin
        private ProfessionRepository _professionRepository;

        public ProfessionService()
        {
            _professionRepository = new ProfessionRepository();
        }

        public void Save(Profession profession)
        {
            if (profession == null)
            {
                throw new ArgumentNullException(nameof(profession));
            }

            if (!Validate(profession)) // Reminder, wo eine Validierung des Vacation-Objektes stattfinden kann
            {
                throw new ArgumentException(nameof(profession));
            }

            try
            {
                if (profession.Id == 0)
                {
                    _professionRepository.Create(profession); // Ist die ProfessionService.Id 0, dann bedeutet dies es handelt sich um einen neuen Eintrag
                }
                else
                {
                    _professionRepository.Update(profession); // Ist die ProfessionService.Id ungleich 0, dann bedeutet dies es handelt sich um einen existierenden Eintrag
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Saving profession '{name}' failed", profession.PreName);
                throw;
            }
        }

        public List<Profession> Get()
        {
            try
            {
                var professions = _professionRepository.Read();
                return professions;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Getting professions failed");
                throw;
            }
        }

        public Profession Get(int id)
        {
            try
            {
                var profession = _professionRepository.Read(id);
                return profession;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Getting profession with id {id} failed", id);
                throw;
            }
        }

        public void Remove(int id)
        {
            try
            {
                _professionRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Deleting profession with id {id} failed", id);
                throw;
            }
        }

        private bool Validate(Profession profession)
        {
            // TODO: Not implemented yet
            return true;
        }

    }
}
