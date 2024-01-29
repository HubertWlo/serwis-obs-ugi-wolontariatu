using System.Collections.Generic;

namespace frontend.Models
{
    public class ModelListy<T>
    {
        // Listy obiektów "T"
        public IEnumerable<T> List1 { get; set; }
        public IEnumerable<T> List2 { get; set; }
    }
}
