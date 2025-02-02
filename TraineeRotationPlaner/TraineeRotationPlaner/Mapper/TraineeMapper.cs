using TraineeRotationPlaner.Entities;
using TraineeRotationPlaner.Models;

namespace TraineeRotationPlaner.Mapper
{
    internal class TraineeMapper
    {
        public static TraineeEntity MapToEntity(Trainee source)
        {
            return new TraineeEntity()
            {
                Id = source.Id,
                LastName = source.LastName,
                FirstName = source.FirstName,
                Abbreviation = source.Abbreviation,
                EducationStart = source.EducationStart.ToString("yyyy-MM-dd"),
                EducationEnd = source.EducationEnd.ToString("yyyy-MM-dd"),
                ProfessionId = source.Profession.Id,
            };
        }

        public static Trainee MapToModel(TraineeEntity source)
        {
            return new Trainee()
            {
                Id = source.Id,
                LastName = source.LastName,
                FirstName = source.FirstName,
                Abbreviation = source.Abbreviation,
                EducationStart = DateOnly.Parse(source.EducationStart),
                EducationEnd = DateOnly.Parse(source.EducationEnd),
                Profession = new Profession()                        
                {
                    Id = source.ProfessionId  
                },              
            };
        }
    }
}
/* TODO Nachschlagen
 * ? (Null-Bedingter Operator)
 * ?? (Null-Coalescing Operator) - verwendet, um einen Standardwert zurückzugeben
 * ?. (Null-Conditional Operator) - null bedingten Indexzugriffsoperator
 */ 