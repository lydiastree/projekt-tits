using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KsiazkaKucharska.Models
{
    public class Przepis
    {
        public int PrzepisID { get; set; }
        [Required(ErrorMessage = "Nie wprowadzono nazwy przepisu")]
        public string Nazwa { get; set; }
        [Required(ErrorMessage = "Nie wprowadzono składników")]
        public string Skladniki { get; set; }
        [Required(ErrorMessage = "Nie wprowadzono sposobu wykonania")]
        public string Wykonanie { get; set; }
        [Required(ErrorMessage = "Nie wprowadzono kalorii na porcję")]
        public int Kalorie { get; set; }
        public string Ktododal { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}