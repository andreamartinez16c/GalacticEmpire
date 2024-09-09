using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalacticEmpire.Models
{
    [Table("Habitants")]
    public class Habitant
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("IdSpecie")]
        public int IdSpecie { get; set; }
        [Column("IdPlanet")]
        public int IsPlanetOfOrigin { get; set; }
        
        [Column("IsRebel")]
        public bool IsRebel { get; set; }
    }
}
