using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KsiazkaKucharska.Models;

namespace KsiazkaKucharska.ViewModel
{
    public class ViewModels
    {
        public IEnumerable<Danieglowne> Daniaglowne { get; set; }
        public IEnumerable<Zupa> Zupy { get; set; }
        public IEnumerable<Przepis> Przepisy { get; set; }
        public IEnumerable<Comment> Komentarze { get; set; }
    }
}