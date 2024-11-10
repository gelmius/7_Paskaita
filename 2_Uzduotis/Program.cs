

class Uzsakymas
{
    public int UzsakymoNumeris { get; set; }
    public string KlientoVardas { get; set; }
    public int PrekiuKiekis { get; set; }
    public double BendraSuma { get; set; }
    public DateTime UzsakymoData { get; set; }

    public Uzsakymas(int uzsakymoNumeris, string klientoVardas, int prekiuKiekis, double bendraSuma, DateTime uzsakymoData)
    {
        UzsakymoNumeris = uzsakymoNumeris;
        KlientoVardas = klientoVardas;
        PrekiuKiekis = prekiuKiekis;
        BendraSuma = bendraSuma;
        UzsakymoData = uzsakymoData;
    }
}

class Program
{
    static void Main()
    {
        List<Uzsakymas> uzsakymai = NuskaitytiUzsakymusIsFailo("uzsakymai.txt");

        int prioritetiniuUzsakymuKiekis = 0;
        DateTime siandien = DateTime.Today;

        foreach (var uzsakymas in uzsakymai)
        {
            if ((siandien - uzsakymas.UzsakymoData).TotalDays <= 7 && uzsakymas.BendraSuma >= 100)
            {
                Console.WriteLine($"Užsakymas {uzsakymas.UzsakymoNumeris} iš kliento {uzsakymas.KlientoVardas} yra prioritetinis.");
                prioritetiniuUzsakymuKiekis++;
            }
            else if (uzsakymas.PrekiuKiekis >= 10 && uzsakymas.BendraSuma < 50)
            {
                Console.WriteLine($"Užsakymas {uzsakymas.UzsakymoNumeris} iš kliento {uzsakymas.KlientoVardas} turi didelį kiekį, bet žemą vertę.");
            }
            else
            {
                Console.WriteLine($"Užsakymas {uzsakymas.UzsakymoNumeris} iš kliento {uzsakymas.KlientoVardas} neatitinka specialių kriterijų ir nėra prioritetinis.");
            }
        }

        Console.WriteLine($"\nPrioritetinių užsakymų kiekis: {prioritetiniuUzsakymuKiekis}");
    }

    static List<Uzsakymas> NuskaitytiUzsakymusIsFailo(string failoPavadinimas)
    {
        List<Uzsakymas> uzsakymai = new List<Uzsakymas>();

        try
        {
            string[] eilutes = File.ReadAllLines(failoPavadinimas);
            foreach (var eilute in eilutes)
            {
                string[] dalys = eilute.Split(',');

                if (dalys.Length == 5)
                {
                    int uzsakymoNumeris = int.Parse(dalys[0]);
                    string klientoVardas = dalys[1];
                    int prekiuKiekis = int.Parse(dalys[2]);
                    double bendraSuma = double.Parse(dalys[3]);
                    DateTime uzsakymoData = DateTime.Parse(dalys[4]);

                    Uzsakymas uzsakymas = new Uzsakymas(uzsakymoNumeris, klientoVardas, prekiuKiekis, bendraSuma, uzsakymoData);
                    uzsakymai.Add(uzsakymas);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Klaida skaitant failą: {ex.Message}");
        }

        return uzsakymai;
    }
}
