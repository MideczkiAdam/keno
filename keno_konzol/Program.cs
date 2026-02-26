namespace keno_consol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<NapiKeno> huzasok = File.ReadAllLines("./Data/huzasok.csv").Skip(1).Select(sor => new NapiKeno(sor.Split(';'))).ToList();
            Console.WriteLine("6. feladat: Az állomány beolvasása sikeres!");

            Console.WriteLine($"7. feladat: Hibás sorok száma:{huzasok.Where(x => !x.Helyes()).Count()}");
            huzasok = huzasok.Where(x => x.Helyes()).ToList();

            NyeremenySzamitas(huzasok);

            Console.WriteLine("10. feladat:");
            Console.WriteLine("8-as játék 2020-ban, tét:4X [17,28,32,44,54,63,72,75]");

            List<int> tippek = new List<int>() { 17, 28, 32, 44, 54, 63, 72, 75 };
            List<NapiKeno> huzasok2020 = huzasok.Where(x => x.ev == 2020).ToList();

            int darabszam = 0;
            int nyert = 0;
            foreach (NapiKeno huzas in huzasok2020)
            {
                int nyeremeny = Szorzo(huzas, tippek) * 800;
                darabszam++;
                nyert += nyeremeny;
                if (nyeremeny > 0)
                {
                    Console.WriteLine($"{huzas.huzasDatum} - {nyeremeny}");
                }
            }
            int koltott = darabszam * 800;
            Console.WriteLine($"Összesen {koltott} Ft-ot költött Kenóra");
            Console.WriteLine($"Összesen {nyert} Ft-ot nyert");

        }

        static int Szorzo(NapiKeno keno, List<int> tippek)
        {
            Dictionary<String, int> nyeroParok = new Dictionary<string, int>(){
                {"10-10",1000000}, {"10-9",8000}, {"10-8",350}, {"10-7",30}, {"10-6",3}, {"10-5",1}, {"10-0",2},
                {"9-9",100000}, {"9-8",1200}, {"9-7",100}, {"9-6",12}, {"9-5",3}, {"9-0",1},
                {"8-8",20000}, {"8-7",350}, {"8-6",25}, {"8-5",5}, {"8-0",1},
                {"7-7",5000}, {"7-6",60}, {"7-5",6}, {"7-4",1}, {"7-0",1},
                {"6-6",500}, {"6-5",20}, {"6-4",3}, {"6-0",1},
                {"5-5",200}, {"5-4",10}, {"5-3",2},
                {"4-4",100}, {"4-3",2},
                {"3-3",15}, {"3-2",1},
                {"2-2",6},
                {"1-1",2}
            };
            int jatekTipus = tippek.Count;
            int talalatokSzama = keno.TalalatSzam(tippek);
            string kulcs = jatekTipus + "-" + talalatokSzama;

            if (nyeroParok.Keys.Contains(kulcs))
                return nyeroParok[kulcs];
            else
                return 0;
        }

        static void NyeremenySzamitas(List<NapiKeno> keno)
        {
            Console.WriteLine("9. feladat: Nyeremény számítása");

            List<int> tippek;
            while (true)
            {
                Console.Write("Kérem a tippjét! Vesszővel elválsztva sorolja fel a számokat:");
                string[] tippekStr = Console.ReadLine().Split(',');
                tippek = new List<int>();

                foreach (string tipp in tippekStr)
                {
                    tippek.Add(Convert.ToInt32(tipp));
                }

                if (tippek.Count < 1 || tippek.Count > 10)
                {
                    Console.WriteLine("A játéktípus 1..10 lehet!");
                    continue;
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                int[] joFogadasok = { 200, 400, 600, 800, 1000 };
                Console.Write("Kérem a fogadási összeget! :");
                int fogadas = Convert.ToInt32(Console.ReadLine());

                if (!joFogadasok.Contains(fogadas))
                {
                    Console.WriteLine("Hibás összeg!");
                    continue;
                }

                int nyeremeny = Szorzo(keno.OrderByDescending(x => x.huzasDatum).First(), tippek) * fogadas;

                if (nyeremeny > 0)
                {
                    Console.WriteLine($"Nyereménye:{nyeremeny}");
                    break;
                }
                else
                {
                    Console.WriteLine("Sajnos nem nyert!");
                    break;
                }
            }
        }
    }
}
