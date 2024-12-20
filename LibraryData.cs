using OOP_Inlamning_3;
using System.Collections.Generic;

namespace OOP_Inlamning_3
{
    // Containerklass för serialisering av bibliotekets data
    public class LibraryData
    {
        public List<Bok> Böcker { get; set; }
        public List<Författare> Författare { get; set; }
    }
}
