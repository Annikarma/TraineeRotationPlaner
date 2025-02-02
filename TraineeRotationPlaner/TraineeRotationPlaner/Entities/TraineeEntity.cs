namespace TraineeRotationPlaner.Entities
{
    public class TraineeEntity
    {
        public int Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string EducationStart {  get; set; } = string.Empty;
        public string EducationEnd {  get; set; } = string.Empty;
        public int EducationYear {  get; set; }
        public int ProfessionId { get; set; }
        public string ProfessionName { get; set; } = string.Empty;

        public TraineeEntity()
        {
            
        }

        public TraineeEntity( int id, string lastname, string firstname, string abbreviation, string educationStart, string educationEnd, int educationYear, int departmentId, int schoolId, int professionId)
        {
            Id = id;
            LastName = lastname;
            FirstName = firstname;
            Abbreviation = abbreviation;
            EducationStart = educationStart;
            EducationEnd = educationEnd;
            ProfessionId = professionId;
        }
    }
 }
