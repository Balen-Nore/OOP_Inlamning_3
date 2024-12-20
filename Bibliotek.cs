using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OOP_Inlamning_3;

namespace OOP_Inlamning_3
{
    public class Bibliotek
    {
        private List<Bok> böcker;
        private List<Författare> författare;
        private const string DataFil = "LibraryData.json";

        public Bibliotek()
        {
            böcker = new List<Bok>();
            författare = new List<Författare>();
        }

        // Ladda data från JSON-fil
        public void LaddaData()
        {
            if (File.Exists(DataFil))
            {
                try
                {
                    var json = File.ReadAllText(DataFil);
                    var data = JsonConvert.DeserializeObject<LibraryData>(json);

                    böcker = data.Böcker;
                    författare = data.Författare;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fel vid inläsning av data: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Ingen datafil hittades. Skapar en ny.");
            }

            if (!böcker.Any())
            {
                LäggTillStandardBokOchFörfattare();
            }
        }

        private void LäggTillStandardBokOchFörfattare()
        {
            Författare balen = new Författare { Id = 1, Namn = "Balen", Land = "Sverige" };
            författare.Add(balen);

            Bok bok = new Bok
            {
                Id = 1,
                Titel = "30 miljoner",
                Författare = balen,
                Genre = "Drama",
                Publiceringsår = 2020,
                Isbn = "1234567890123",
                Recensioner = new List<int> { 5, 4, 5 }
            };

            böcker.Add(bok);
            Console.WriteLine("En standardbok '30 miljoner' av 'Balen' har lagts till i biblioteket.");
        }

        // Spara data till JSON-fil
        public void SparaData()
        {
            try
            {
                var data = new LibraryData { Böcker = böcker, Författare = författare };
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(DataFil, json);

                Console.WriteLine("Data har sparats.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparande av data: {ex.Message}");
            }
        }

        // Lägg till bok
        public void LäggTillBok()
        {
            Console.WriteLine("Ange boktitel:");
            var titel = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(titel))
            {
                Console.WriteLine("Titel kan inte vara tom.");
                return;
            }

            Console.WriteLine("Ange ISBN:");
            var isbn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("ISBN kan inte vara tom.");
                return;
            }

            Console.WriteLine("Ange publiceringsår:");
            if (!int.TryParse(Console.ReadLine(), out int publiceringsår))
            {
                Console.WriteLine("Ogiltigt år.");
                return;
            }

            Console.WriteLine("Välj författare (ange namn):");
            var författareNamn = Console.ReadLine();
            var författareObj = författare.FirstOrDefault(f => f.Namn.Equals(författareNamn, StringComparison.OrdinalIgnoreCase));

            if (författareObj == null)
            {
                Console.WriteLine("Författare hittades inte. Lägg till författare först.");
                return;
            }

            Console.WriteLine("Ange genre:");
            var genre = Console.ReadLine();

            var bok = new Bok
            {
                Id = böcker.Count > 0 ? böcker.Max(b => b.Id) + 1 : 1,
                Titel = titel,
                Isbn = isbn,
                Publiceringsår = publiceringsår,
                Författare = författareObj,
                Genre = genre
            };

            böcker.Add(bok);
            Console.WriteLine("Bok tillagd.");
        }

        // Lägg till författare
        public void LäggTillFörfattare()
        {
            Console.WriteLine("Ange författarens namn:");
            var namn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(namn))
            {
                Console.WriteLine("Namn kan inte vara tomt.");
                return;
            }

            Console.WriteLine("Ange författarens land:");
            var land = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(land))
            {
                Console.WriteLine("Land kan inte vara tomt.");
                return;
            }

            var författareObj = new Författare
            {
                Id = författare.Count > 0 ? författare.Max(f => f.Id) + 1 : 1,
                Namn = namn,
                Land = land
            };

            författare.Add(författareObj);
            Console.WriteLine("Författare tillagd.");
        }

        // Uppdatera bok
        public void UppdateraBok()
        {
            Console.WriteLine("Ange bok-ID att uppdatera:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID.");
                return;
            }

            var bok = böcker.FirstOrDefault(b => b.Id == id);

            if (bok == null)
            {
                Console.WriteLine("Bok med angivet ID hittades inte.");
                return;
            }

            Console.WriteLine("Ange ny titel (lämna tomt för att behålla nuvarande):");
            var nyTitel = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyTitel)) bok.Titel = nyTitel;

            Console.WriteLine("Ange ny genre (lämna tomt för att behålla nuvarande):");
            var nyGenre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyGenre)) bok.Genre = nyGenre;

            Console.WriteLine("Ange ny ISBN (lämna tomt för att behålla nuvarande):");
            var nyIsbn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyIsbn)) bok.Isbn = nyIsbn;

            Console.WriteLine("Bok uppdaterad.");
        }

        // Uppdatera författare
        public void UppdateraFörfattare()
        {
            Console.WriteLine("Ange författar-ID att uppdatera:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID.");
                return;
            }

            var författareObj = författare.FirstOrDefault(f => f.Id == id);

            if (författareObj == null)
            {
                Console.WriteLine("Författare med angivet ID hittades inte.");
                return;
            }

            Console.WriteLine("Ange nytt namn (lämna tomt för att behålla nuvarande):");
            var nyttNamn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyttNamn)) författareObj.Namn = nyttNamn;

            Console.WriteLine("Ange nytt land (lämna tomt för att behålla nuvarande):");
            var nyttLand = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyttLand)) författareObj.Land = nyttLand;

            Console.WriteLine("Författare uppdaterad.");
        }

        // Ta bort författare
        public void TaBortFörfattare()
        {
            Console.WriteLine("Ange författar-ID att ta bort:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID.");
                return;
            }

            var författareObj = författare.FirstOrDefault(f => f.Id == id);

            if (författareObj == null)
            {
                Console.WriteLine("Författare med angivet ID hittades inte.");
                return;
            }

            if (böcker.Any(b => b.Författare.Id == id))
            {
                Console.WriteLine("Författaren har kopplade böcker och kan inte tas bort.");
                return;
            }

            författare.Remove(författareObj);
            Console.WriteLine("Författare borttagen.");
        }

        // Sök och filtrera böcker
        public void SökOchFiltreraBöcker()
        {
            Console.WriteLine("Ange sökord för titel eller författare:");
            var sökord = Console.ReadLine();

            var filtreradeBöcker = böcker.Where(b =>
                b.Titel.Contains(sökord, StringComparison.OrdinalIgnoreCase) ||
                b.Författare.Namn.Contains(sökord, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!filtreradeBöcker.Any())
            {
                Console.WriteLine("Inga böcker hittades.");
                return;
            }

            Console.WriteLine("Filtrerade böcker:");
            foreach (var bok in filtreradeBöcker)
            {
                Console.WriteLine($"{bok.Titel} ({bok.Publiceringsår}) - Författare: {bok.Författare.Namn}");
            }
        }

        // Ta bort bok
        public void TaBortBok()
        {
            Console.WriteLine("Ange bok-ID att ta bort:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID.");
                return;
            }

            var bok = böcker.FirstOrDefault(b => b.Id == id);

            if (bok == null)
            {
                Console.WriteLine("Bok med angivet ID hittades inte.");
                return;
            }

            böcker.Remove(bok);
            Console.WriteLine("Bok borttagen.");
        }

        // Lista alla böcker och författare
        public void ListaAlla()
        {
            Console.WriteLine("\nBöcker:");
            foreach (var bok in böcker)
            {
                Console.WriteLine($"{bok.Titel} ({bok.Publiceringsår}) - Författare: {bok.Författare.Namn}");
            }

            Console.WriteLine("\nFörfattare:");
            foreach (var författareObj in författare)
            {
                Console.WriteLine($"{författareObj.Namn} ({författareObj.Land})");
            }
        }
    }
}
