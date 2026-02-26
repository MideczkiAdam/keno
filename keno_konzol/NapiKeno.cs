using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keno_consol
{
    internal class NapiKeno
    {
        public int ev;
        public int het;
        public int nap;
        public string huzasDatum;
        public List<int> huzottSzamok;

        public NapiKeno(string[] sor)
        {
            this.ev = int.Parse(sor[0]);
            this.het = int.Parse(sor[1]);
            this.nap = int.Parse(sor[2]);
            this.huzasDatum = sor[3];
            this.huzottSzamok = new List<int>();

            for (int i = 4; i < sor.Length; i++)
            {
                this.huzottSzamok.Add(int.Parse(sor[i]));
            }
        }

        public int TalalatSzam(List<int> tippek)
        {
            int talalatokSzama = 0;
            foreach (int tipp in tippek)
            {
                if (this.huzottSzamok.Contains(tipp))
                    talalatokSzama++;
            }
            return talalatokSzama;
        }

        public bool Helyes()
        {
            if (huzottSzamok.Count() != 20 || huzottSzamok.Distinct().Count() != 20)
                return false;
            return true;
        }
    }


}
