using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KsiazkaKucharska.Models
{
    public class Danieglowne
    {
        public int DanieglowneID { get; set; }
        [Required(ErrorMessage = "Nie wprowadzono nazwy dania")]
        public string Nazwa { get; set; }
        [Required(ErrorMessage = "Nie wprowadzono składników")]
        public string Skladniki { get; set; }
        [Required(ErrorMessage = "Nie wprowadzono sposobu wykonania")]
        public string Wykonanie { get; set; }
        [Required(ErrorMessage = "Nie wprowadzono ilości porcji")]
        public int Iloscporcji { get; set; }
        public string Ktododal { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}