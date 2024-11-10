using System;
using System.Collections.Generic;
using System.IO;

class Studentas
{
    public string Vardas { get; set; }
    public string Pavarde { get; set; }
    public int Amzius { get; set; }
    public double Vidurkis { get; set; }

    public Studentas(string vardas, string pavarde, int amzius, double vidurkis)
    {
        Vardas = vardas;
        Pavarde = pavarde;
        Amzius = amzius;
        Vidurkis = vidurkis;
    }
}

class Program
{
    static void Main()
    {
        List<Studentas> studentai = NuskaitytiStudentusIsFailo("studentai.csv");

        foreach (var studentas in studentai)
        {
            if (studentas.Amzius > 20 && studentas.Vidurkis > 7.0)
            {
                Console.WriteLine($"Studentas {studentas.Vardas} {studentas.Pavarde} atitinka kriterijus.");
            }
            else
            {
                Console.WriteLine($"Studentas {studentas.Vardas} {studentas.Pavarde} neatitinka kriterijų.");
            }
        }
    }

    static List<Studentas> NuskaitytiStudentusIsFailo(string failoPavadinimas)
    {
        List<Studentas> studentai = new List<Studentas>();

        try
        {
            string[] eilutes = File.ReadAllLines(failoPavadinimas);
            foreach (var eilute in eilutes)
            {
                string[] dalys = eilute.Split(',');

                if (dalys.Length == 4)
                {
                    string vardas = dalys[0];
                    string pavarde = dalys[1];
                    int amzius = int.Parse(dalys[2]);
                    double vidurkis = double.Parse(dalys[3]);

                    Studentas studentas = new Studentas(vardas, pavarde, amzius, vidurkis);
                    studentai.Add(studentas);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Klaida skaitant failą: {ex.Message}");
        }

        return studentai;
    }
}
