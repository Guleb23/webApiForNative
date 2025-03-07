namespace webApiFoeNative.Models
{
    public class SpecialisationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<DoctorsModel> DoctorsModels { get; set; }
    }
}
