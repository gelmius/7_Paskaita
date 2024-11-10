
class Projektas
{
    public int ProjektoID { get; set; }
    public string ProjektoPavadinimas { get; set; }
    public string VadovoVardas { get; set; }
    public double Biudzetas { get; set; }
    public DateTime PradziosData { get; set; }
    public DateTime PabaigosData { get; set; }

    public Projektas(int projektoID, string projektoPavadinimas, string vadovoVardas, double biudzetas, DateTime pradziosData, DateTime pabaigosData)
    {
        ProjektoID = projektoID;
        ProjektoPavadinimas = projektoPavadinimas;
        VadovoVardas = vadovoVardas;
        Biudzetas = biudzetas;
        PradziosData = pradziosData;
        PabaigosData = pabaigosData;
    }
}

class Program
{
    static List<Projektas> projektai = new List<Projektas>();

    static void Main()
    {

        projektai = NuskaitytiProjektusIsFailo("projektai.txt");

        bool running = true;
        while (running)
        {
            Console.WriteLine("\nMeniu pasirinkimai:");
            Console.WriteLine("1. Peržiūrėti visus projektus");
            Console.WriteLine("2. Peržiūrėti projektą pagal ID");
            Console.WriteLine("3. Pridėti naują projektą");
            Console.WriteLine("4. Ištrinti projektą pagal ID");
            Console.WriteLine("5. Išsaugoti visus pakeitimus į failą");
            Console.WriteLine("6. Išeiti iš programos");

            Console.Write("\nPasirinkite veiksmą: ");
            int pasirinkimas = int.Parse(Console.ReadLine());

            switch (pasirinkimas)
            {
                case 1:
                    PerziuretiVisusProjektus();
                    break;
                case 2:
                    PerziuretiProjektaPagalID();
                    break;
                case 3:
                    PridetiNaujaProjekta();
                    break;
                case 4:
                    IstrintiProjektaPagalID();
                    break;
                case 5:
                    IssaugotiProjektusIFaila("projektai.txt");
                    break;
                case 6:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Neteisingas pasirinkimas. Bandykite dar kartą.");
                    break;
            }
        }
    }

    static List<Projektas> NuskaitytiProjektusIsFailo(string failoPavadinimas)
    {
        List<Projektas> projektai = new List<Projektas>();

        try
        {
            string[] eilutes = File.ReadAllLines(failoPavadinimas);
            foreach (var eilute in eilutes)
            {
                string[] dalys = eilute.Split(',');

                if (dalys.Length == 6)
                {
                    int projektoID = int.Parse(dalys[0]);
                    string projektoPavadinimas = dalys[1];
                    string vadovoVardas = dalys[2];
                    double biudzetas = double.Parse(dalys[3]);
                    DateTime pradziosData = DateTime.Parse(dalys[4]);
                    DateTime pabaigosData = DateTime.Parse(dalys[5]);

                    Projektas projektas = new Projektas(projektoID, projektoPavadinimas, vadovoVardas, biudzetas, pradziosData, pabaigosData);
                    projektai.Add(projektas);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Klaida skaitant failą: {ex.Message}");
        }

        return projektai;
    }

    static void PerziuretiVisusProjektus()
    {
        foreach (var projektas in projektai)
        {
            SpausdintiProjektoInformacija(projektas);
            PatikrintiProjektoKriterijus(projektas);
        }
    }

    static void PerziuretiProjektaPagalID()
    {
        Console.Write("Įveskite projekto ID: ");
        int id = int.Parse(Console.ReadLine());
        Projektas projektas = projektai.Find(p => p.ProjektoID == id);

        if (projektas != null)
        {
            SpausdintiProjektoInformacija(projektas);
            PatikrintiProjektoKriterijus(projektas);
        }
        else
        {
            Console.WriteLine("Projektas su tokiu ID nerastas.");
        }
    }

    static void PridetiNaujaProjekta()
    {
        Console.Write("Įveskite projekto ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Įveskite projekto pavadinimą: ");
        string pavadinimas = Console.ReadLine();
        Console.Write("Įveskite vadovo vardą: ");
        string vadovas = Console.ReadLine();
        Console.Write("Įveskite biudžetą: ");
        double biudzetas = double.Parse(Console.ReadLine());
        Console.Write("Įveskite pradžios datą (yyyy-MM-dd): ");
        DateTime pradziosData = DateTime.Parse(Console.ReadLine());
        Console.Write("Įveskite pabaigos datą (yyyy-MM-dd): ");
        DateTime pabaigosData = DateTime.Parse(Console.ReadLine());

        projektai.Add(new Projektas(id, pavadinimas, vadovas, biudzetas, pradziosData, pabaigosData));
        Console.WriteLine("Projektas pridėtas sėkmingai.");
    }

    static void IstrintiProjektaPagalID()
    {
        Console.Write("Įveskite projekto ID: ");
        int id = int.Parse(Console.ReadLine());
        Projektas projektas = projektai.Find(p => p.ProjektoID == id);

        if (projektas != null)
        {
            projektai.Remove(projektas);
            Console.WriteLine("Projektas sėkmingai ištrintas.");
        }
        else
        {
            Console.WriteLine("Projektas su tokiu ID nerastas.");
        }
    }

    static void IssaugotiProjektusIFaila(string failoPavadinimas)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(failoPavadinimas))
            {
                foreach (var projektas in projektai)
                {
                    writer.WriteLine($"{projektas.ProjektoID},{projektas.ProjektoPavadinimas},{projektas.VadovoVardas},{projektas.Biudzetas},{projektas.PradziosData:yyyy-MM-dd},{projektas.PabaigosData:yyyy-MM-dd}");
                }
            }
            Console.WriteLine("Pakeitimai išsaugoti sėkmingai.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Klaida išsaugant failą: {ex.Message}");
        }
    }

    static void SpausdintiProjektoInformacija(Projektas projektas)
    {
        Console.WriteLine($"\nProjekto ID: {projektas.ProjektoID}");
        Console.WriteLine($"Projekto pavadinimas: {projektas.ProjektoPavadinimas}");
        Console.WriteLine($"Vadovo vardas: {projektas.VadovoVardas}");
        Console.WriteLine($"Biudžetas: {projektas.Biudzetas} EUR");
        Console.WriteLine($"Pradžios data: {projektas.PradziosData:yyyy-MM-dd}");
        Console.WriteLine($"Pabaigos data: {projektas.PabaigosData:yyyy-MM-dd}");
    }

    static void PatikrintiProjektoKriterijus(Projektas projektas)
    {
        DateTime siandien = DateTime.Today;

        if (projektas.PabaigosData > siandien && projektas.Biudzetas > 30000)
        {
            Console.WriteLine($"Projektas {projektas.ProjektoPavadinimas} vadovaujamas {projektas.VadovoVardas} yra tęsiamas su aukštu biudžetu.");
        }
        else if (projektas.Biudzetas < 10000 && (siandien - projektas.PradziosData).TotalDays > 365)
        {
            Console.WriteLine($"Projektas {projektas.ProjektoPavadinimas} yra su mažu biudžetu ir ilgai trunka.");
        }
        else if (projektas.PabaigosData < siandien)
        {
            Console.WriteLine($"Projektas {projektas.ProjektoPavadinimas} jau užbaigtas.");
        }
    }
}
