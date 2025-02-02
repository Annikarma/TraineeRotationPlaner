namespace TraineeRotationPlaner.Models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;

        public Profession()
        {

        }

        public Profession(int id, string name, string abbreviation)
        {
            Id = id;
            Name = name;
            Abbreviation = abbreviation;
        }
    }
}
