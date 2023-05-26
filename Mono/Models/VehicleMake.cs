using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mono.Models
{
    public class VehicleMake
    {
        [Key]
        public int id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Abrv")]
        public string abrv { get; set; }
    }
}
