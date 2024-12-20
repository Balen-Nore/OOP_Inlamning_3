using OOP_Inlamning_3;

namespace OOP_Inlamning_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Bibliotek bibliotek = new Bibliotek();
            bibliotek.LaddaData();  // Ladda data från JSON vid start
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Välj ett alternativ:");
                    Console.WriteLine("1. Lägg till ny bok");
                    Console.WriteLine("2. Lägg till ny författare");
                    Console.WriteLine("3. Uppdatera bokdetaljer");
                    Console.WriteLine("4. Uppdatera författardetaljer");
                    Console.WriteLine("5. Ta bort bok");
                    Console.WriteLine("6. Ta bort författare");
                    Console.WriteLine("7. Lista alla böcker och författare");
                    Console.WriteLine("8. Sök och filtrera böcker");
                    Console.WriteLine("9. Avsluta och spara data");
                    var val = Console.ReadLine();
                    switch (val)
                    {
                        case "1":
                            bibliotek.LäggTillBok();
                            break;
                        case "2":
                            bibliotek.LäggTillFörfattare();
                            break;
                        case "3":
                            bibliotek.UppdateraBok();
                            break;
                        case "4":
                            bibliotek.UppdateraFörfattare();
                            break;
                        case "5":
                            bibliotek.TaBortBok();
                            break;
                        case "6":
                            bibliotek.TaBortFörfattare();
                            break;
                        case "7":
                            bibliotek.ListaAlla();
                            break;
                        case "8":
                            bibliotek.SökOchFiltreraBöcker();
                            break;
                        case "9":
                            bibliotek.SparaData();  // Spara data tillbaka till JSON
                            return;
                        default:
                            Console.WriteLine("Ogiltigt val, försök igen.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ett fel inträffade: {ex.Message}");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                }
            }
        }
    }
}