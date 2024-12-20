using OOP_Inlamning_3;
using System;
using System.Collections.Generic;

namespace OOP_Inlamning_3
{
    public class Bok
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public Författare Författare { get; set; }
        public string Genre { get; set; }
        public int Publiceringsår { get; set; }
        public string Isbn { get; set; }
        public List<int> Recensioner { get; set; } = new List<int>();

        // Beräkna och returnera genomsnittligt betyg
        public double GenomsnittligtBetyg()
        {
            if (Recensioner.Count == 0)
                return 0;

            return Math.Round((double)Recensioner.Sum() / Recensioner.Count, 1);
        }
    }
}
