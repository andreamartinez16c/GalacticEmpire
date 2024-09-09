using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalacticEmpire.Models
{
    [Table("Species")]
    public class Specie
    {
        [Key]
        [Column("IdSpecie")]
        public int IdSpecie { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
