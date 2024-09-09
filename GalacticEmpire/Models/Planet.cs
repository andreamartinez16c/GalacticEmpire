using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalacticEmpire.Models
{
    [Table("Planets")]
    public class Planet
    {
        [Key]
        [Column("IdPlanet")]
        public int IdPlanet { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
