using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceCheese
{
    class Menu
    {
         public static void WyborMenu(int poziom=1)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            WyswietlMenu();
            try
            {
                int wybor = Convert.ToInt32(Console.ReadLine());
                switch (wybor)
                {
                    case 1://gra
                        ZacznijGre(poziom);
                        break;
                    case 2://opcje
                        poziom = WyswietlOpcje();
                        WyborMenu(poziom);
                        break;
                    case 3://Dane
                        WyswietlDane();
                        WyborMenu(poziom);
                        break;
                    case 4://Wyjdz
                            Console.Clear();
                            break;
                    default:
                        //Console.Clear();
                        Console.SetCursorPosition(0, 9);
                        Console.WriteLine("Nie ma takiej opcji");
                        WyborMenu(poziom);
                        break;
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 9);
                Console.WriteLine("Nieodpowiedni znak");
                Console.SetCursorPosition(0, 0);
                WyborMenu(poziom);
            }
        }
        public static void ZacznijGre(int nrPoziomuTrudnosci)
        {
            Console.Clear();
            Gra gra = new Gra();
            gra = ZmianaOpcji(nrPoziomuTrudnosci, gra);
            
            gra.Wyswietl();
            gra.Update();
            EkranKoncaGry(gra.punkty,gra.xRozmiar,nrPoziomuTrudnosci);
        }
        public static void WyswietlDane()
        {
            Console.Clear();
            Console.WriteLine("|    Cheese Invaders     |");
            Console.WriteLine("|========================|");
            Console.WriteLine("|    Kamil Zieliński     |");
            Console.WriteLine("|========================|");
            Console.WriteLine();
            Console.WriteLine("Wcisnij dowolny przycisk aby wrócić");
            Console.ReadKey();
        }
        public static void WyswietlMenu()
        {
            Console.WriteLine("Cheese Invaders");
            Console.WriteLine("---------------");
            Console.WriteLine("1.Graj");
            Console.WriteLine("2.Opcje");
            Console.WriteLine("3.Informacje");
            Console.WriteLine("4.Wyjdz");
            Console.WriteLine("===============");
        }
        public static int WyswietlOpcje()
        {
            Console.Clear();
            Console.WriteLine("Cheese Invaders");
            Console.WriteLine("---------------");
            Console.WriteLine("Wpisz liczbe 1-4, aby zmienić:");
            Console.WriteLine("Poziomy trudności:");
            Console.WriteLine("---------------");
            Console.WriteLine("1.Latwy");
            Console.WriteLine("2.Sredni");
            Console.WriteLine("3.Trudny");
            Console.WriteLine("4.Bardzo Trudny");
            Console.WriteLine("===============");
            Console.WriteLine("Wpisz dowolny inny przycisk aby wrócić");
            int wyborOpcji = Convert.ToInt32(Console.ReadLine());
            return wyborOpcji;
        }
        public static void EkranKoncaGry(long punkty,int x, int poziom)
        {
            string napis= "Twoj wynik: " + punkty;
            Console.SetCursorPosition((x - napis.Length) /2, 10);
            Console.WriteLine(napis);
            napis = "Zagraj ponownie? y/n";
            Console.SetCursorPosition((x- napis.Length ) / 2, 11);
            Console.WriteLine(napis);
            Console.SetCursorPosition(x / 2, 12);
            char nowaGra = Console.ReadKey().KeyChar;
            if (nowaGra == 'y' || nowaGra == 'Y') ZacznijGre(poziom);
            else if (nowaGra == 'n' || nowaGra == 'N') WyborMenu(poziom);
            else EkranKoncaGry(punkty,x,poziom);
        }
        public static Gra ZmianaOpcji(int wybor,Gra gra)
        {
            switch (wybor)
            {
                case 1:
                    //Latwy
                    gra = new Gra();
                    break;
                case 2:
                    //Sredni
                    gra = new Gra(5,35,200);
                    break;
                case 3:
                    //Trudny;
                    gra = new Gra(3, 40, 100);
                    break;
                case 4:
                    //Bardzo Trudny;
                    gra = new Gra(1, 40, 50);
                    break;
            }
            return gra;
        }
    }
}
