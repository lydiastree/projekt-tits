using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KsiazkaKucharska.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int? DanieglowneID { get; set; }
        public int? ZupaID { get; set; }
        public int? PrzepisID { get; set; }
        [Required(ErrorMessage ="Nie wprowadzono nazwy użytkownika")]
        public string Nazwauzyt { get; set; }
        [Required(ErrorMessage = "Komentarz jest pusty")]
        public string Komentarz { get; set; }
        public DateTime? Datadodania { get; set; }

        public virtual Danieglowne Dania {get; set;}
        public virtual Przepis Przepisy { get; set; }
        public virtual Zupa Zupy { get; set; }
    }
}