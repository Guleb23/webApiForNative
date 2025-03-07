using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webApiFoeNative.Models
{
    public class DoctorsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public int Stash { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        public SpecialisationModel? SpecialisationModel { get; set; }
    }
}
