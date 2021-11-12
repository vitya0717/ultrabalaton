using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultrabalaton
{
    class Versenyzo
    {
        //6. feladat:
        /*Készítsen IdőÓrában azonosítóval valós típusú értékkel visszatérő függvényt vagy
        Jellemzőt, ami a versenyző időeredményét órában határozza meg! Egy óra 60 percből,
        illetve 3600 másodpercből áll.*/
        public double IdőÓra()
        {
            string[] ido_split = versenyido.Split(':');
            double óra = double.Parse(ido_split[0]);
            double perc = double.Parse(ido_split[1]);
            double másodperc = double.Parse(ido_split[2]);

            return  óra + perc / 60 + másodperc / 3600;
        }

        //Versenyzo;Rajtszam;Kategoria;Versenyido;TavSzazalek
        //Acsadi Lajos;1;Ferfi;30:28:42;100
        public string versenyzo_neve;
        public int rajtszam;
        public bool kategoria;
        public string versenyido;
        public double tavszazalek;

        public Versenyzo(string versenyzo_neve, int rajtszam, bool kategoria, string versenyido, double tavszazalek)
        {
            this.versenyzo_neve = versenyzo_neve;
            this.rajtszam = rajtszam;
            this.kategoria = kategoria;
            this.versenyido = versenyido;
            this.tavszazalek = tavszazalek;
        }

        public override string ToString()
        {
            return versenyzo_neve+";"+rajtszam+";"+kategoria+";"+versenyido+";"+tavszazalek;
        }


    }
    class Program
    {
        private static bool neme(string value)
        {
            if (value.ToLower() == "ferfi".ToLower())
            {
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            //2. feladat
            string[] sorok = File.ReadAllLines("ub2017egyeni.txt");
            List<Versenyzo> Versenyzok = new List<Versenyzo>();

            for (int i = 1; i < sorok.Length; i++)
            {
                string[] darabok = sorok[i].Split(';');
                Versenyzok.Add(new Versenyzo(darabok[0], int.Parse(darabok[1]), neme(darabok[2]), darabok[3], int.Parse(darabok[4])));
            }
            /*foreach (var item in Versenyzok)
            {
                Console.WriteLine(item);
            }*/


            //3. feladat:
            /*Határozza meg és írja ki a képernyőre a minta szerint, hogy hány egyéni sportoló indult el
            a versenyen! */
            Console.WriteLine("3. feladat: Egyéni indulók: {0} Fő", Versenyzok.Count);


            //4.feladat:
            /*Számolja meg és írja ki a képernyőre a minta szerint, hogy hány női sportoló teljesítette
            a teljes távot!*/
            int celba_erk_no = 0;
            foreach (var item in Versenyzok)
            {
                if(!item.kategoria && item.tavszazalek == 100)
                {
                    celba_erk_no++;
                }
            }
            Console.WriteLine("4. feladat: Célba érkező női sportolók: {0} Fő", celba_erk_no);


            //5.feladat:
            /*Kérje be a felhasználótól egy sportoló nevét, majd határozza meg és írja ki a minta szerint,
            hogy a sportoló indult-e a versenyen! A keresést ne folytassa, ha az eredményt meg tudja
            határozni! Ha a sportoló indult a versenyen, akkor azt is írja ki a képernyőre, hogy a teljes
            távot teljesítette-e! Feltételezheti, hogy nem indultak azonos nevű sportolók ezen
            a versenyen. */
            Console.Write("5. feladat: Kérem a sportoló nevét: ");
            string neve = Console.ReadLine();

            bool egyeni = false;
            bool teljes_tav = false;

            foreach (var item in Versenyzok)
            {
                if(item.versenyzo_neve == neve)
                {
                    egyeni = true;
                    if(item.tavszazalek == 100)
                    {
                        teljes_tav = true;
                        break;
                    }
                }
            }
            Console.WriteLine("\tIndult egyéniben a sportoló? {0}", egyeni);
            if(egyeni) {
                Console.WriteLine("\tTeljesítette a teljes távot? {0}", teljes_tav);
            }



            //7. feladat:
            /*Határozza meg és írja ki a minta szerint a teljes távot teljesítő férfi sportolók átlagos idejét
            órában! Feltételezheti, hogy legalább egy ilyen sportoló volt. 
            */
            int teljes_db = 0;
            double atlag = 0;

            foreach (var item in Versenyzok)
            {
                if(item.kategoria && item.tavszazalek == 100)
                {
                    teljes_db++;
                    atlag += item.IdőÓra();
                }
            }
            atlag /= teljes_db;
            Console.WriteLine("7. feladat: Átlagos idő: {0} óra", atlag);

            //8. feladat:
            /*
             Keresse meg a női és a férfi kategóriák győzteseit és írja ki nevüket, rajtszámukat és
             időeredményeiket a minta szerint! Feltételezheti, hogy egyik kategóriában sem alakult ki
             holtverseny és mindkét kategóriában volt célba érkező futó. 
            */
            Versenyzo ferfi = Versenyzok[0];
            Versenyzo noi = Versenyzok[0];

            foreach (var item in Versenyzok)
            {
                if(item.kategoria && item.IdőÓra() < ferfi.IdőÓra() && item.tavszazalek == 100)
                {
                    ferfi = item;
                }
                if(!item.kategoria && item.IdőÓra() < noi.IdőÓra() && item.tavszazalek == 100)
                {
                    noi = item;
                }
            }
           Console.WriteLine("8. feladat: Verseny győztesei:\n\tNők: {0} ({1}.) - {2}\n\tFérfiak: {3} ({4}.) - {5}",noi.versenyzo_neve, noi.rajtszam, noi.versenyido, ferfi.versenyzo_neve, ferfi.rajtszam,ferfi.versenyido);
           

            Console.ReadKey();

         }


    }
}
